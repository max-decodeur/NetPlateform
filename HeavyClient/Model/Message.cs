using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace HeavyClient.Model
{
    public class Message
    {
        public bool statusOp { get; set; } = false;
        public string info = "";
        public object[] data = null;
        public string operationName = "";
        public string tokenApp { get; set; }
        public string tokenUser { get; set; }
        public string appVersion = "";
        public string operationVersion = "";

        private NameValueCollection appConfig = ConfigurationManager.AppSettings;

        //
        // Résumé :
        //     Crée un message serializable.
        public Message()
        {
            this.init();
        }

        //
        // Résumé :
        //     Crée un message serializable en définissant une opération spécifique.
        public Message(string operationName, string operationVersion)
        {
            this.operationName = operationName;
            this.operationVersion = operationVersion;

            this.init();
        }

        //
        // Résumé :
        //     Crée un message serializable en définissant une opération spécifique et les données à transférer.
        public Message(string operationName, string operationVersion, object[] data)
        {
            this.operationName = operationName;
            this.operationVersion = operationVersion;
            this.data = data;

            this.init();
        }

        private void init()
        {
            this.tokenApp = this.appConfig["ApplicationToken"];
            this.appVersion = this.appConfig["ApplicationVersion"];
            this.tokenUser = this.appConfig["UserToken"];
        }

        public void setTokenUser(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                this.appConfig["UserToken"] = token;
                this.tokenUser = token;
            }
        }

        public string serialize()
        {
            // Serialize to XML
            //XmlSerializer serializer = new XmlSerializer(this.GetType());
            //StringWriter sw = new StringWriter();
            //serializer.Serialize(sw, this);
            //return sw.ToString();


            // Serialize to JSON
            return JsonConvert.SerializeObject(this);
        }

        public static Message deserialize(string strMessage)
        {
            return JsonConvert.DeserializeObject<Message>(strMessage);
        }
    }
}
