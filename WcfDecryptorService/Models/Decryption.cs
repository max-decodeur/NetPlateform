using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace WcfDecryptorService.Models
{
    public class Decryption
    {
        private int beginning = 65; // 65 => A
        private int limit = 90; // 90 => Z
        private int[] key;
        private List<FormattedFile> files = new List<FormattedFile>();

        public Decryption(object[] data)
        {
            foreach(string strFile in data)
            {
                FormattedFile file = FormattedFile.deserialize(strFile);
                this.files.Add(file);
            }
        }

        public PDF start()
        {
            return this.decryptFile(this.files[0]);
        }

        private PDF decryptFile(FormattedFile file)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            key = new int[4] { beginning - 1, beginning, beginning, beginning };
            int nbMaxKeys = (int)Math.Pow((limit - beginning) + 1, key.Length);

            var runningTasks = new CountdownEvent(1);
            int keysUsed = 0;

            // Use ParallelOptions instance to limit to processor
            ParallelOptions po = new ParallelOptions();
            po.MaxDegreeOfParallelism = Environment.ProcessorCount;

            PDF fileToReturn = null;
            object returnLock = new object();
            indexListKeys = 0;

            List<string> keys = new List<string>();
            Parallel.For(0, nbMaxKeys, po, (index, loop) =>
            {
                int[] keyArray;
                lock (this.key)
                {
                    keyArray = this.keyAvailable();
                    //if (keysUsed % 676 == 1) Debug.WriteLine("[SERVICE] current keys used = " + keysUsed);
                }

                string newKey = "";
                foreach (int i in keyArray)
                {
                    newKey += Convert.ToChar(i);
                }

                FormattedFile fileToVerify = this.decryptTask(file, newKey);
                Interlocked.Increment(ref keysUsed);

                PDF resultVerification = this.verificationRequest(fileToVerify, newKey);

                // TODO: Treat the verification response
                if (resultVerification != null)
                {
                    loop.Stop();
                    //sendEmail(newKey, resultVerification.filename);
                    lock (returnLock)
                    {
                        fileToReturn = resultVerification;
                    }
                }
            });


            Debug.WriteLine("[SERVICE] Decryption - decryptFile: keysUsed = " + keysUsed);
            Debug.WriteLine("[SERVICE] Decryption - decryptFile: time = " + sw.ElapsedMilliseconds / 1000 + "." + sw.ElapsedMilliseconds % 1000 + " sec");
            return fileToReturn;
        }

        private FormattedFile decryptTask(FormattedFile file, string newKey)
        {
            string decryptedContent = this.decryptContent(file.content, newKey);

            // Create a copy so we send variations of the original file.
            FormattedFile clonedFile = (FormattedFile)file.Clone();
            clonedFile.content = decryptedContent;
            return clonedFile;
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
                Debug.WriteLine("[ERROR][SERVICE] decryptContent: " + e.Message);
                return null;
                //return (new Message("error", "0", new string[] { "FileCorrupted" })).serialize();
            }
        }

        private bool nextKey()
        {
            int index = 0;
            if (this.key[index] < this.limit)
            {
                this.key[index]++;
                return true;
            }
            else
            {
                return Array.Exists(this.key, v => v < this.limit) ? this.incNext(index) : false;
            }
        }

        private int indexListKeys = 0;

        private int[] keyAvailable()
        {
            int index = 0;
            if (this.key[index] < this.limit)
            {
                this.key[index]++;
                return this.key;
            }
            if (Array.Exists(this.key, v => v < this.limit))
            {
                if (this.incNext(index)) return this.key;
            }
            return null;
        }

        private bool incNext(int index)
        {
            if (this.key[index + 1] < limit)
            {
                this.key[index] = beginning;
                this.key[index + 1]++;
                return true;
            }
            else if (this.key[index + 1] == limit)
            {
                if(incNext(index + 1))
                {
                    this.key[index] = beginning;
                    return true;
                }
            }
            return false;
        }

        private PDF verificationRequest(FormattedFile file, string key)
        {
            // TODO: Connect to JEE
            Message messageToSend = new Message("verificationFile", "0.1", new string[] { file.serialize() });



            // Mock the response
            PDF pdf = new PDF();
            pdf.path = file.path;
            pdf.filename = file.filename;
            pdf.content = file.content;
            pdf.validity = false;
            pdf.pourcentage = 24.5;
            pdf.tested = 200;
            pdf.recognized = 49;
            Message mockedResponse = new Message("verificationFile", "0.1", new string[] { pdf.serialize() });


            PDF receivedFile = PDF.deserialize((string)mockedResponse.data[0]);
            receivedFile.key = key;
            if (receivedFile.validity)
            {
                return receivedFile;
            }

            return null;
        }

        public void sendEmail(string key, string file)
        {
            Debug.WriteLine("[SERVICE] Decryption - sendEmail: sending email with key = " + key + ", file = " + file);
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("groupeprojetdevnonmobile@gmail.com");
            mail.To.Add(new MailAddress("alexis.hoyez@viacesi.fr"));
            mail.To.Add(new MailAddress("maximilien.apprill@viacesi.fr"));
            mail.Subject = "Réultat de décryptage obtenu";
            mail.Body = "La clé est " + key + " pour le fichier : " + file;

            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            //mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("groupeprojetdevnonmobile@gmail.com", "ProjetDev123");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}