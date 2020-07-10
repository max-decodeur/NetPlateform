using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using WcfDecryptorService.Models;

namespace WcfDecryptorService
{
    public class CustomValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            UsersModel um = new UsersModel();
            if (um.login(userName, password)) return;
            throw new SecurityTokenException("Account Invalid");
        }
    }
}