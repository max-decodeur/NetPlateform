using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WcfDecryptorService.Models
{
    public class Decryption
    {
        private int beginning = 65; // 97 => A
        private int limit = 90; // 122 => Z
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

        public void start()
        {
            foreach(FormattedFile file in this.files)
            {
                this.decryptFile(file);
            }
        }

        private void decryptFile(FormattedFile file)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            key = new int[4] { beginning - 1, beginning, limit, limit };

            // TODO: Implement a loop on keys
            while (this.nextKey())
            {
                string newKey = "";
                foreach (int i in this.key)
                {
                    newKey += Convert.ToChar(i);
                }
                // TODO: Implement Parallel Tasks

                //Console.WriteLine("[SERVICE] Model - decryptFile: key = " + newKey);
                //ThreadPool.SetMaxThreads(80000, 0);

                //ThreadPool.QueueUserWorkItem(new WaitCallback(state => this.decryptTask(file, newKey)));

                //Task task = new Task(() => decryptTask(file, newKey));
                //task.Start();
                //string decryptedContent = this.decryptContent(file.content, newKey);

                //FormattedFile clonedFile = (FormattedFile)file.Clone();
                //clonedFile.content = decryptedContent;


                FormattedFile fileToVerify = this.decryptTask(file, newKey);

                // TODO: Maybe move this line to the end of decryptTask function
                string resultVerification = this.verificationRequest(fileToVerify);

                // TODO: Treat the verification response
            }


            Debug.WriteLine("[SERVICE] Decryption - decryptFile: time = " + sw.ElapsedMilliseconds / 1000 + "." + sw.ElapsedMilliseconds % 1000 + " sec");
        }

        private StreamWriter newFile = File.CreateText("D:\\Projet\\A4-3 Dominante Dev\\test\\test-async.txt");

        private FormattedFile decryptTask(FormattedFile file, string newKey)
        {
            // DEBUG: Le contenu n'est pas décripté pour permettre les tests
            //string decryptedContent = this.decryptContent(file.content, newKey);
            string decryptedContent = file.content;

            // Create a copy so we send variations of the original file.
            FormattedFile clonedFile = (FormattedFile)file.Clone();
            clonedFile.content = decryptedContent;

            //Console.WriteLine("[SERVICE] Model - decryptTask: key = " + newKey);
            this.newFile.WriteLine(decryptedContent);
            return clonedFile;
        }

        private string decryptContent(string input, string key)
        {
            try
            {
                string test = "";
                for (int i = 0; i < input.Length; i++)
                {
                    var tt = input[i];
                    test += Convert.ToChar(Convert.ToByte(input[i]) ^ Convert.ToByte(key[i % key.Length]));
                }
                return test;
            } catch
            {
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
                this.key[index] = beginning;
                incNext(index + 1);
                return true;
            }
            return false;
        }

        private string verificationRequest(FormattedFile file)
        {
            // TODO: Connect to JEE
            return null;
        }
    }
}