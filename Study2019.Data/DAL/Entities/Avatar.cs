using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study2019.Data.DAL.Entities
{
    internal class Avatar
    {
        public Avatar()
        {
            Images = new HashSet<Image>();
        }
        [Key]
        public int UserId { get; set; }
        [Key]
        public int ImageId { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
