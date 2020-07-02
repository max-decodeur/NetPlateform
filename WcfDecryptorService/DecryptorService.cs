using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Diagnostics;
using WcfDecryptorService.Models;

namespace WcfDecryptorService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class DecryptorService : IDecryptorService
    {
        private string tokenApp = "TokenComingFromNoWhere";

        public string m_service(string msg)
        {
            Debug.WriteLine("[SERVICE] Message reçu = " + msg);
            if (string.IsNullOrEmpty(msg)) return (new Message("errorMessage", "0.1")).serialize();

            Message message = Message.deserialize(msg);
            if (message.tokenApp != this.tokenApp) return (new Message("errorApplication", "0.1")).serialize();
            if (message.operationName == "login")
            {
                message.statusOp = true;
                // TODO: token generator
                message.tokenUser = "RandomlyGeneratedUserToken";

                // TODO: log the connection
            }
            else if (message.operationName == "decrypt")
            {
                message.statusOp = true;

                Decryption decryption = new Decryption(message.data);
                decryption.start();
            }
            else
            {
                message.statusOp = false;
            }
            return message.serialize();
        }

        public string java(string msg)
        {
            // WCF
            try
            {
                ValidatorProxy.ValidatorEndpointClient service = new ValidatorProxy.ValidatorEndpointClient();
                string message = "{ \"info\":\"\",\"data\":[\"{ \"filename\":\"nativelog.txt\",\"path\":\"C:\\Users\\KLEIN Aurélien\\Desktop\\nativelog.txt\",\"content\":\"Je suis le contenu du fichier bla bla bla...\"}\"],\"operationName\":\"decrypt\",\"appVersion\":\"0.1\",\"operationVersion\":\"0.1\",\"statusOp\":null,\"tokenApp\":\"token\",\"tokenUser\":\"\"}";
                service.validatorOperation(message);
                //string returnValue = service.java("{ name:John, age:31, city:New York}");
                Console.WriteLine("logged");
                //Console.WriteLine(returnValue);
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("failed");
                Console.ReadLine();
            }
            return "good";
        }

        public IAsyncResult BeginDecryptAsyncMethod(string msg, AsyncCallback callback, object asyncState)
        {
            Console.WriteLine("HelloAsyncMethod called");
            return new CompletedAsyncResult<string>(msg);
        }

        public string EndDecryptAsyncMethod(IAsyncResult r)
        {
            CompletedAsyncResult<string> result = r as CompletedAsyncResult<string>;
            Console.WriteLine("EndServiceAsyncMethod called");
            return result.Data;
        }

        // Simple async result implementation.
        class CompletedAsyncResult<T> : IAsyncResult
        {
            T data;

            public CompletedAsyncResult(T data)
            { this.data = data; }

            public T Data
            { get { return data; } }

            #region IAsyncResult Members
            public object AsyncState
            { get { return (object)data; } }

            public WaitHandle AsyncWaitHandle
            { get { throw new Exception("The method or operation is not implemented."); } }

            public bool CompletedSynchronously
            { get { return true; } }

            public bool IsCompleted
            { get { return true; } }
            #endregion
        }
    }
}
