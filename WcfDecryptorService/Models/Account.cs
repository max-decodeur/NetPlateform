using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfDecryptorService.Models
{
    public class Account
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string UserToken { get; set; }

    }
}