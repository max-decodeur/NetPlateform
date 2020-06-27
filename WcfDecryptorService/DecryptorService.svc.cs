using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace WcfDecryptorService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class DecryptorService : IDecryptorService
    {
        
        public string HelloWorld()
        {
            return "Hello World";
        }

        public string Register()
        {
            string token="";
            return token;
        }

        public void Decrypt(string file)
        {

            AuthenticationWCF.Models.File objectFile = new AuthenticationWCF.Models.File("crypted", "/","btgdr fydcfgsegh ghdcjh");
            Decrypt(objectFile.data);

        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

    }
}
