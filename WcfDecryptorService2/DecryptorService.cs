using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using WcfDecryptorService.Models;
using WcfDecryptorService.Models.Persistence;

namespace WcfDecryptorService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class DecryptorService : IDecryptorService
    {
        private string tokenApp = "93b7262b62089fae82bff40bf5fe0180650f4e33d0a114bccf72ab6301465473";

        public string m_service(string msg)
        {
            //(new CustomValidator()).Validate();
            Debug.WriteLine("[SERVICE] Message reçu = " + msg);
            if (string.IsNullOrEmpty(msg)) return (new Message("errorMessage", "0.1")).serialize();

            Message message = Message.deserialize(msg);
            if (message.tokenApp != this.tokenApp) return (new Message("errorApplication", "0.1")).serialize();
            if (message.operationName == "login")
            {
                if ((new UsersModel()).login(message.data[0].ToString(), message.data[1].ToString()))
                {
                    message.tokenUser = this.generateToken(message.data.Cast<string>().ToArray());

                    IDbRepository repo = new DbRepository();
                    repo.updateToken(message.data[0].ToString(), message.tokenUser);

                    message.statusOp = true;
                }
                else
                {
                    message.statusOp = false;
                }
            }
            else if (message.operationName == "decrypt")
            {
                if ((new UsersModel()).login(message.tokenUser))
                {
                    Decryption decryption = new Decryption(message.data);
                    PDF pdf = decryption.start();
                    if (pdf != null) message.data = new List<object>() { pdf.serialize() };
                    message.statusOp = true;
                }
            }
            else
            {
                message.statusOp = false;
            }
            return message.serialize();
        }

        private string generateToken(string[] args)
        {
            SHA256 chat = SHA256.Create();
            byte[] hash = chat.ComputeHash(Encoding.UTF8.GetBytes(String.Concat(args)));

            string token = "";
            foreach (byte x in hash)
            {
                token += String.Format("{0:x2}", x);
            }

            return token;
        }
    }
}
