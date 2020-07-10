using Newtonsoft.Json;

namespace WcfDecryptorService.Models
{
    public class PDF : FormattedFile
    {
        public bool validity;
        public int nbWords;
        public int tested;
        public int recognized;
        public string key;

        public PDF() { }

        public static PDF deserialize(string strFile)
        {
            return JsonConvert.DeserializeObject<PDF>(strFile);
        }

        public static PDF From(FormattedFile file)
        {
            return PDF.deserialize(file.serialize());
        }
    }
}