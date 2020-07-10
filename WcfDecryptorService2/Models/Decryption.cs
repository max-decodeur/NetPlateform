using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace WcfDecryptorService.Models
{
    public class Decryption
    {
        private static readonly int beginning = 65; // 65 => A
        private static readonly int limit = 90; // 90 => Z
        private int[] key;
        private List<List<KeyValuePair<string, int>>> ranks;
        private readonly List<FormattedFile> files = new List<FormattedFile>();
        private static readonly string tokenAppJEE = "2db639a3cb7117abc4b04870560ef1e93e2815495e097d87ddd43a6e09a678bc";

        public Decryption(List<object> data)
        {
            foreach(JObject file in data)
            {
                this.files.Add(file.ToObject<FormattedFile>());
            }
        }

        public PDF start()
        {
            return this.decryptFile(this.files[0]);
        }

        private int nbPossibilitiesPerChar = 3;

        private void getAllKeys(FormattedFile file)
        {
            // TODO: limit file size
            string content = file.content.Length < 800 ? file.content : file.content.Substring(0, 800);

            this.key = new int[] { -1, 0, 0, 0 };
            this.ranks = new List<List<KeyValuePair<string, int>>>();

            for (int cursor = 0; cursor < key.Length; cursor++)
            {
                var letters = new List<KeyValuePair<string, int>>();

                for (int letter = 0; letter < 26; letter++)
                {
                    int count = 0;
                    for(int i = cursor; i < content.Length; i+=key.Length)
                    {
                        char letterDecrypted = Convert.ToChar(this.decryptContent(content[i].ToString(), Convert.ToChar(beginning + letter).ToString()));
                        uint letterInt = Convert.ToUInt32(letterDecrypted);
                        if (letterInt > 64 && letterInt < 91 || letterInt > 96 && letterInt < 123 || letterDecrypted == ' ')
                        {
                            count++;
                        }
                    }

                    letters.Add(new KeyValuePair<string, int>(Convert.ToChar(beginning + letter).ToString(), count));
                }

                // Keep 3 best letters
                letters.Sort(sortLetters);
                letters.RemoveRange(this.nbPossibilitiesPerChar, letters.Count - this.nbPossibilitiesPerChar);
                this.ranks.Add(letters);
            }
        }

        #region getNextKey
        private string matchingKeyRanks()
        {
            string result = "";
            for (int i = 0; i < this.ranks.Count; i++)
            {
                result += this.ranks[i][this.key[i]].Key;
            }
            return result;
        }

        private string getNextKey(int nbPossibilitiesPerChar)
        {
            int index = 0;
            // { 0, 0, 0, 0 } => { 3, 0, 0, 0 }
            if (this.key[index] < nbPossibilitiesPerChar)
            {
                this.key[index]++;
                return this.matchingKeyRanks();
            }
            // { 3, 0, 0, 0 } => { 3, 3, 3, 3 }
            if (Array.Exists(this.key, v => v < nbPossibilitiesPerChar))
            {
                if (this.incNext(index, nbPossibilitiesPerChar)) return this.matchingKeyRanks();
            }
            // { 3, 3, 3, 3 }
            return null;
        }

        private bool incNext(int index, int nbPossibilitiesPerChar)
        {
            // { 0, 3, 0, 0 } => { 0, 0, 1, 0 }
            if (this.key[index + 1] < nbPossibilitiesPerChar)
            {
                this.key[index] = 0;
                this.key[index + 1]++;
                return true;
            }
            // { 0, 3, 3, 0 } => { 0, 0, 3, 0 } + loop
            else if (this.key[index + 1] == nbPossibilitiesPerChar)
            {
                if (incNext(index + 1, nbPossibilitiesPerChar))
                {
                    this.key[index] = 0;
                    return true;
                }
            }
            return false;
        }
        #endregion

        private int sortLetters(KeyValuePair<string, int> list1, KeyValuePair<string, int> list2)
        {
            return list2.Value.CompareTo(list1.Value);
        }


        private PDF decryptFile(FormattedFile file)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            this.getAllKeys(file);

            string content = file.content.Length < 9000 ? file.content : file.content.Substring(0, 9000);
            int nbMaxKeys = (int)Math.Pow(this.nbPossibilitiesPerChar, key.Length);
            //string newContent = "";
            int nbKeysTested = 0;

            PDF fileToReturn = null;
            object fileToReturnLock = new object();

            /* Use ParallelOptions instance to limit to processor */
            ParallelOptions po = new ParallelOptions();
            po.MaxDegreeOfParallelism = Environment.ProcessorCount;

            /* Brutforce */
            Parallel.For(0, nbMaxKeys, po, (i, loop) =>
            {
                string newKey;
                lock (this.key) newKey = this.getNextKey(this.nbPossibilitiesPerChar - 1);

                /* [PoC] Add content for test */
                //if (!String.IsNullOrEmpty(newKey))
                //{
                //    lock (fileToReturnLock) newContent += newKey + " => " + this.decryptContent(content, newKey) + "\n";
                //}


                /* Decrypt the file and return a copy of the original file with the decrypted content for the specific key */
                FormattedFile fileToVerify = (FormattedFile)file.Clone();
                fileToVerify.content = this.decryptContent(file.content, newKey);
                Interlocked.Increment(ref nbKeysTested);

                /* Send request for verification */
                PDF resultVerification = this.verificationRequest(fileToVerify, newKey);

                /* TODO: Treat the verification response */
                if (resultVerification != null && resultVerification.validity)
                {
                    loop.Stop();
                    //sendEmail(newKey, resultVerification.filename);
                    lock (fileToReturnLock) fileToReturn = resultVerification;
                }
            });
            /* [PoC] Prepare file for test */
            //fileToReturn = PDF.From(file);
            //fileToReturn.content = newContent;
            //fileToReturn.validity = true;

            Debug.WriteLine("[SERVICE] Decryption - decryptFile: nbKeysTested = " + nbKeysTested);
            Debug.WriteLine("[SERVICE] Decryption - decryptFile: time = " + sw.ElapsedMilliseconds / 1000 + "." + sw.ElapsedMilliseconds % 1000 + " sec");
            return fileToReturn;
        }

        private string decryptContent(string input, string key)
        {
            try
            {
                string result = "";
                for (int index = 0; index < input.Length; index++)
                {
                    uint byteChar = Convert.ToUInt32(input[index]);
                    uint byteCompare = Convert.ToUInt32(key[index % key.Length]);
                    result += Convert.ToChar(byteChar ^ byteCompare);
                }
                return result;
            } catch (Exception e)
            {
                Debug.WriteLine("[ERROR][SERVICE] decryptContent: " + e.Message + ", " + key);
                return null;
            }
        }

        private PDF verificationRequest(FormattedFile file, string key)
        {
            //if (key != "AABA") return null;
            /* Connect to JEE */
            Message messageToSend = new Message("verificationFile", "0.1", new List<object>() { file.serialize() });
            messageToSend.tokenApp = tokenAppJEE;
            messageToSend.info = key;

            AuthenticationWCF.Validator.ValidatorEndpointClient service = new AuthenticationWCF.Validator.ValidatorEndpointClient();
            var response = service.validatorOperation(messageToSend.serialize());
            Debug.WriteLine("[JEE] response = " + response);

            Message messageReceived = Message.deserialize(response);
            PDF pdf = PDF.deserialize(messageReceived.data[0].ToString());
            pdf.key = messageReceived.info;

            // Mock the response
            //PDF pdf = new PDF();
            //pdf.path = file.path;
            //pdf.filename = file.filename;
            //pdf.content = file.content;
            //pdf.validity = true;
            //pdf.nbWords = 24.5;
            //pdf.tested = 200;
            //pdf.recognized = 49;
            //pdf.key = key;

            if (pdf.validity)
            {
                return pdf;
            }

            return null;
        }

        public void sendEmail(string key, string file)
        {
            Debug.WriteLine("[SERVICE] Decryption - sendEmail: sending email with key = " + key + ", file = " + file);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("groupeprojetdevnonmobile@gmail.com");
            //mail.To.Add(new MailAddress("alexis.hoyez@viacesi.fr"));
            //mail.To.Add(new MailAddress("maximilien.apprill@viacesi.fr"));
            //mail.To.Add(new MailAddress("aurelien.klein@viacesi.fr"));
            //mail.To.Add(new MailAddress("tperquis@cesi.fr"));
            mail.Subject = "Réultat de décryptage obtenu";
            mail.Body = "La clé est " + key + " pour le fichier : " + file;

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.Credentials = new NetworkCredential("groupeprojetdevnonmobile@gmail.com", "ProjetDev123");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}