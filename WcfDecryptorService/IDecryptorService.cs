using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfDecryptorService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IDecryptorService
    {
        [OperationContract]
        string m_service(string msg);

        [OperationContractAttribute(AsyncPattern = true)]
        IAsyncResult BeginDecryptAsyncMethod(string msg, AsyncCallback callback, object asyncState);

        // Note: There is no OperationContractAttribute for the end method.
        string EndDecryptAsyncMethod(IAsyncResult result);

        [OperationContract]
        string java(string msg);
    }
}
