using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeavyClient.Model
{
    public class PDF : FormattedFile
    {
        public bool validity;
        public int nbWords;
        public int tested;
        public int recognized;
        public string key;

        public static PDF deserialize(string strFile)
        {
            return JsonConvert.DeserializeObject<PDF>(strFile);
        }
    }
}