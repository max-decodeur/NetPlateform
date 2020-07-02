using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfDecryptorService.Models
{
    public class AccountModel
    {
        private List<Account> listAccounts = new List<Account>();

        public AccountModel()
        {
            listAccounts.Add(new Account { Username = "acc1", Password = "123" });
            listAccounts.Add(new Account { Username = "acc2", Password = "123" });
            listAccounts.Add(new Account { Username = "acc3", Password = "123" });
            listAccounts.Add(new Account { Username = "aa", Password = "zz" });
        }

        public bool login(string username, string password)
        {
            return listAccounts.Count(acc => acc.Username.Equals(username) && acc.Password.Equals(password)) > 0;
        }

        public bool login(string token)
        {
            return listAccounts.Count(acc => acc.UserToken.Equals(token)) > 0;
        }
    }
}