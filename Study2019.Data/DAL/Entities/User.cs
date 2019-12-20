using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study2019.Data.DAL.Entities
{
    public class User
    {
        public User()
        {
            Avatars = new HashSet<Avatar>();
            Images = new HashSet<Image>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string LoginName { get; set; }
        public string PasswordHash { get; set; }
        [StringLength(50)]
        public string Salt { get; set; }
        [StringLength(100)]
        public string Nickname { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime BirtDate { get; set; }
        public string Description { get; set; }
        public bool IsShared { get; set; }

        public virtual ICollection<Avatar> Avatars { get; set; }
        public virtual ICollection<Image> Images { get; set; }

    }
}
