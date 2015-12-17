using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamat.DC.Interface;
using Jamat.EntityFramework;

namespace Jamat.DC
{
    public class AccountRepository : IAccountRepository
    {
        private DbEntityContext _ctx;

        public AccountRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }


        public User GetUser(string username, string userPass)
        {
            try
            {
                //return new User()
                //{
                //    UserName = username,
                //    UserPassword = EncryptionHelper.Encrypt(userPass)
                //};

                //userPass = PasswordHash.CreateHash(userPass);

                var user = _ctx.Users
                    .Single(c => c.UserName.ToLower().Equals(username.ToLower()));

                //return user;

                if (EncryptionHelper.Decrypt(user.UserPassword).Equals(userPass))
                {
                    user.UserPassword = string.Empty;
                    return user;
                }
                else
                {
                    user.UserId = 0;
                    user.UserName = string.Empty;
                    user.UserPassword = string.Empty;
                    user.TajneedId = 0;
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
