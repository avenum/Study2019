using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study2019.Data.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public string UserNickname { get; set; }
        public  List<int> PostImages { get; set; }

    }
}
