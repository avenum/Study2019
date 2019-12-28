using System;
using System.ComponentModel.DataAnnotations;

namespace Study2019.WebUI.Models
{
    public class RegUserModel : UserModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string RetryPassword { get; set; }

    }
}