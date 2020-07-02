﻿using HeavyClient.View;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using HeavyClient.WCF;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Pdf.Canvas.Draw;

namespace HeavyClient.Model
{
    public class Client
    {
        private static Client instance = new Client();
        private Client() { }
        public static Client Instance
        {
            get { return instance; }
        }
        
        public MainWindow mainWindow;

        private string username;
        private string password;

        public object loadFiles(string[] files)
        {
            Console.WriteLine("Model - loadFiles: loading");

            List<FormattedFile> results = new List<FormattedFile>();
            int filesDecrypted = 0;
            int filesFailed = 0;
            int filesCorrupted = 0;

            FilesView view = null;
            mainWindow.Dispatcher.BeginInvoke(new Action(() =>
            {
                view = (FilesView)mainWindow.DataContext;
                view.totalFiles = files.Length;
            }));

            ParallelOptions po = new ParallelOptions();
            po.MaxDegreeOfParallelism = Environment.ProcessorCount;

            Parallel.ForEach(files, po, (file, loop) =>
            {
                List<string> contentList = new List<string>() { this.importFile(file) };
                Message message = new Message("decrypt", "0.1", contentList.ToArray());
                string resultStrMessage = this.platformRequest(message);

                // Get the result of the request and return it
                Console.WriteLine("Model - loadFiles: result = " + resultStrMessage);
                if (string.IsNullOrEmpty(resultStrMessage))
                {
                    filesCorrupted++;
                }
                else
                {
                    Message resultMessage = Message.deserialize(resultStrMessage);

                    PDF pdfReceived = PDF.deserialize((string)resultMessage.data[0]);
                    results.Add(pdfReceived);
                    Console.WriteLine("Model - loadFiles: results length = " + results.Count);

                    if (resultMessage.statusOp)
                    {
                        filesDecrypted++;
                    }
                    else
                    {
                        filesFailed++;
                    }

                    if (pdfReceived.validity)
                    {
                        Console.WriteLine("Model - loadFiles: FICHIER DECRYPTE");
                        this.generateFiles(pdfReceived);
                    }
                }

                // Update view values
                mainWindow.Dispatcher.BeginInvoke(new Action(() =>
                {
                    view.filesDecrypted = filesDecrypted;
                    view.filesFailed = filesFailed;
                    view.filesCorrupted = filesCorrupted;
                }));
            });

            Console.WriteLine("Model - loadFiles: filesProcessed = " + filesDecrypted + ", filesFailed = " + filesFailed + ", filesCorrupted = " + filesCorrupted);
            return filesDecrypted + filesFailed + filesCorrupted == files.Length;
        }

        private string importFile(string path)
        {
            FormattedFile file = new FormattedFile(path);
            return file.serialize();
        }

        private string platformRequest(Message message)
        {
            // Perform a request to the .NET platform
            Console.WriteLine("Model - platformRequest: sending message = " + message.serialize());
            
            // WCF
            try
            {
                DecryptorServiceClient service = new DecryptorServiceClient();
                //service.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
                //service.ClientCredentials.UserName.UserName = this.username;
                //service.ClientCredentials.UserName.Password = this.password;
                return service.m_service(message.serialize());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
                //return (new Message("error", "0", new string[] { "platformRequest" })).serialize();
            }
        }

        public bool login(string username, string password)
        {
            this.username = username;
            this.password = password;
            string[] loginInformations = new string[2] { this.username, this.password };
            
            Message message = new Message("login", "0.1", loginInformations);
            string resultStrMessage = this.platformRequest(message);

            // TODO: Get the result of the request and return it
            if (string.IsNullOrEmpty(resultStrMessage)) return false;

            Console.WriteLine("Model - login: result = " + resultStrMessage);
            Message resultMessage = Message.deserialize(resultStrMessage);

            // Set the UserToken setting with the token received
            NameValueCollection appConfig = ConfigurationManager.AppSettings;
            appConfig["UserToken"] = resultMessage.tokenUser;

            return resultMessage.statusOp;
        }

        //public void processReceivedMessage(string strMessage)
        //{
        //    if (string.IsNullOrEmpty(strMessage)) return;

        //    // Deserialize message
        //    Message message = Message.deserialize(strMessage);
        //    Console.WriteLine("Model - processReceivedMessage: received message.data length = " + message.data.Length);

        //    // TODO: Also check the statusOp
        //    if(message.operationName == "decrypt")
        //    {
        //        // Deserialize message.data
        //        PDF file = PDF.deserialize(message.data[0].ToString());
        //        // Create the new file
        //        this.generateDecryptedFile(file);
        //    }
        //}

        private void generateFiles(PDF file)
        {
            // Create txt file with decrypted content
            string newPath = Path.GetDirectoryName(file.path);
            string ext = Path.GetExtension(file.filename);
            string name = Path.GetFileNameWithoutExtension(file.filename) + "-decrypted " + this.formattedCurrentTime();

            FileStream newFile = File.Create("D:\\Projet\\A4-3 Dominante Dev\\results\\" + name + ext);
            byte[] content = new UTF8Encoding(true).GetBytes(file.content);
            newFile.Write(content, 0, content.Length);

            // Create PDF report
            PdfWriter writer = new PdfWriter("D:\\Projet\\A4-3 Dominante Dev\\results\\" + name + ".pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("Decryption report")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);
            document.Add(header);
            document.Add(new LineSeparator(new SolidLine()));
            document.Add(new Paragraph("Clef utilisée : " + file.key));
            document.Add(new Paragraph("Nombre de mots testés : " + file.tested));
            document.Add(new Paragraph("Nombre de mots reconnu : " + file.recognized));
            document.Add(new Paragraph("Taux de confiance : " + file.pourcentage + "%"));

            document.Close();

            Console.WriteLine("Model - generateDecryptedFile: file created = " + name);
        }

        private string formattedCurrentTime()
        {
            return DateTime.Now.GetDateTimeFormats()[6] + " " + DateTime.Now.GetDateTimeFormats()[71].Replace(":", "h");
        }
    }
}
