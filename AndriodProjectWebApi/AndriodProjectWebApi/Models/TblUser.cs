using System;
using System.Collections.Generic;

#nullable disable

namespace AndriodProjectWebApi.Models
{
    public partial class TblUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
