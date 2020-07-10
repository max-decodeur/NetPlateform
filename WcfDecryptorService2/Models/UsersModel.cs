using WcfDecryptorService.Models.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace WcfDecryptorService.Models
{
    public class UsersModel
    {
        private List<user> listAccounts = new List<user>();
        IDbRepository repo;

        public UsersModel()
        {
            this.repo = new DbRepository();
            listAccounts = repo.GetUsers();
        }

        public bool login(string username, string password)
        {
            password = ComputeSha256Hash(password);
            bool result = listAccounts.Count(acc => acc.login.Equals(username) && acc.password.Equals(password)) > 0;
            repo.addLog(username);
            return result;
        }

        public bool login(string token)
        {
            bool result = listAccounts.Count(acc => acc.usertoken.Equals(token)) > 0;
            return result;
        }

        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}