using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Study2019.WebUI.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public List<int> ImageIds { get; set; }

        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public string UserNickname { get; set; }

    }
}