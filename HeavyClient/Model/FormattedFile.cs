using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HeavyClient.Model
{
    public class FormattedFile
    {
        public string filename;
        public string path;
        public string content;

        public FormattedFile() { }

        public FormattedFile(string path)
        {
            this.filename = Path.GetFileName(path);
            this.content = File.ReadAllText(path, Encoding.UTF8);
            this.path = path;
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

        public static FormattedFile deserialize(string strFile)
        {
            return JsonConvert.DeserializeObject<FormattedFile>(strFile);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
