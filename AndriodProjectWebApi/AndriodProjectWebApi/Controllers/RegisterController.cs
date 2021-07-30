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
    public class RegisterController : ControllerBase
    {
        DbTestContext dbContext = new DbTestContext();
        //// GET: api/<RegisterController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<RegisterController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<RegisterController>
        [HttpPost]
        public string Post([FromBody] TblUser value)
        {
            if (!dbContext.TblUsers.Any(User => User.Username.Equals(value.Username)))
            {
                 TblUser user = new TblUser();
                user.Username = value.Username; //assign value from post to user
                user.Salt = Convert.ToBase64String(Common.GetRandomSalt(16));
                user.Password = Convert.ToBase64String(Common.SaltHashPassword(
                    Encoding.ASCII.GetBytes(value.Password), Convert.FromBase64String(user.Salt)));
                //Add to Database
                try
                {
                    dbContext.Add(user);
                    dbContext.SaveChanges();
                    return JsonConvert.SerializeObject("Register successfully");
                }
                catch (Exception ex)
                {

                    return JsonConvert.SerializeObject(ex.Message);
                }
               
               
            }
            else
            {
                return JsonConvert.SerializeObject("User is existing in Database");
            }
            //first we need check user have existing in database
        }

        //// PUT api/<RegisterController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<RegisterController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
