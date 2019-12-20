using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study2019.Data.DAL.Entities
{
    public class Post
    {
        public Post()
        {
            PostImages = new HashSet<PostImage>();
        }
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PostImage> PostImages { get; set; }

    }
}
