using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Study2019.WebUI.Models
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class CustomSerializeModel
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }

    }

}