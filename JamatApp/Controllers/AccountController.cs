using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Jamat.DC.Interface;
using System.Threading.Tasks;
using Jamat.EntityFramework;

namespace JamatApp.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private IAccountRepository _repo;

        public AccountController(IAccountRepository repository)
        {
            _repo = repository;
        }

        [Route("GetUserByUserName/{username}/{userpass}")]
        public User GetUserByUserName(string username, string userpass)
        {
            try
            {
                return _repo.GetUser(username, userpass);
            }
            catch (Exception)
            {
                return null;
            }
            
        }


    }
}
