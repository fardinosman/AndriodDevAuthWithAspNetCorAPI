using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndriodProjectWebApi.Models;
using AndriodProjectWebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AndriodProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        DbTestContext dbContext = new DbTestContext();
        // GET: api/<LoginController>

        // POST api/<LoginController>
        [HttpPost]
        public string Post([FromBody] TblUser value)
        {
            //Check Exists
            if (dbContext.TblUsers.Any(User => User.Username.Equals(value.Username)))
            {
                TblUser user = dbContext.TblUsers.Where(u => u.Username.Equals(value.Username)).First();
                //Calculate hash password from data of client and compare with hash in server with salt
                var client_post_hash_password = Convert.ToBase64String(Common.SaltHashPassword(Encoding.ASCII.GetBytes(
                    value.Password), Convert.FromBase64String(user.Salt)));

                if (client_post_hash_password.Equals(user.Password))
                    return JsonConvert.SerializeObject(user);
                else
                    return JsonConvert.SerializeObject("Wrong Password");
            }
            else
            {
                return JsonConvert.SerializeObject("User is existing in Database");
            }



        }
    }
}
    

