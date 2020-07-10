using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfDecryptorService.Models.Persistence
{
    interface IDbRepository
    {
        List<user> GetUsers();

        void updateToken(string username, string usertoken);

        void addLog(string username);
    }
}
