using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study2019.Data.DAL.Entities
{
   internal class Image
    {
        public int Id { get; set; }
        public string MimeType { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }
        public Guid BlobId { get; set; }
        public virtual User User { get; set; }
    }
}
