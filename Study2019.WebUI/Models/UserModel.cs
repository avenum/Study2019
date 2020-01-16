using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Study2019.WebUI.Models
{
    public class UserModel
    {
        [Display(Name = "Имя пользователя (email)")]
        [Required(ErrorMessage = "Поле не заполнено")]
        [RegularExpression(@"\b[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b", ErrorMessage = "введенное не соответствует email")]
        public string LoginName { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}")]
        public DateTime Birtdate { get; set; }
        public string Description { get; set; }
        public bool SharedProfile { get; set; }
        public int Id { get; set; }
        public int?  ImageAvatarId { get; set; }

    }
}