using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfDecryptorService.Models.Persistence
{
    public class DbRepository : IDbRepository
    {
        public List<user> GetUsers()
        {
            SqlServerDbContext context = new SqlServerDbContext();
            return context.users.ToList();
        }

        public void updateToken(string username, string usertoken)
        {
            SqlServerDbContext context = new SqlServerDbContext();
            try
            {
                user u = context.users.First(i => i.login == username);
                u.usertoken = usertoken;
                context.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        public void addLog(string username)
        {
            SqlServerDbContext context = new SqlServerDbContext();
            try
            {
                user u = context.users.FirstOrDefault(i => i.login == username);

                log log = new log
                {
                    date = DateTime.Now,
                    iduser = u.id
                };
                context.logs.Add(log);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

        }
    }
}