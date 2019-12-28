namespace Study2019.Data.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class UserDTO
    {
        public UserDTO() {}
    
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Nickname { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime BirtDate { get; set; }
        public string Description { get; set; }
        public bool SharedProfile { get; set; }

    }
}
