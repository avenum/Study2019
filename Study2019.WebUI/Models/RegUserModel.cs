using System;
using System.ComponentModel.DataAnnotations;

namespace Study2019.WebUI.Models
{
    public class RegUserModel
    {
        [Display(Name = "Имя пользователя (email)")]
        [Required(ErrorMessage = "Поле не заполнено")]
        [RegularExpression(@"\b[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b", ErrorMessage ="введенное не соответствует email")]
        public string LoginName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string RetryPassword { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public DateTime Birtdate { get; set; }
        public string Description { get; set; }
        public bool SharedProfile { get; set; }
    }
}