using Study2019.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Study2019.WebUI.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            var model = Data.BLL.Db.GetPosts().Select(AutoMapper.Mapper.Map<PostModel>).ToList();
            return View(model);
        }

        public ActionResult CreateEdit()
        {
            var model = new PostModel { ImageIds = new List<int>() };
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult CreateEdit(PostModel model)
        {
            var post = AutoMapper.Mapper.Map<Data.DTO.PostDTO>(model);
            post.UserId = ((CustomAuth.CustomPrincipal)User).UserId;
            var newPost = Data.BLL.Db.GetPost(Data.BLL.Db.CreateUpdatePost(post));
            return Json(newPost);
        }


    }
}