using AuthenticationWCF_Client.ServiceReferenceClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using System.Web;

namespace AuthenticationWCF_Client.Models
{
    public class HelloClient
    {
        public string HelloWorld()
        {
            try
            {
                DecryptorServiceClient service = new DecryptorServiceClient();
                service.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
                service.ClientCredentials.UserName.UserName = "acc1";
                service.ClientCredentials.UserName.Password = "123";
                return service.HelloWorld();
            }
            catch
            {
                return null;
            }
        }
    }
}