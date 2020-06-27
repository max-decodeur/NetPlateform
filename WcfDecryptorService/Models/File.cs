using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationWCF.Models
{
    public class File
    {
        public string name { get; set; }

        public string path { get; set; }

        public string data { get; set; }

        public File(string n, string p, string d)
        {
            name = n;
            path = p;
            data = d;
        }
    }
}