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
            model.ForEach(x =>
            {
                x.CanEdit = User.Identity.IsAuthenticated && x.UserId == ((CustomAuth.CustomPrincipal)User).UserId;
            });
            return View(model);
        }
        [Authorize]
        public ActionResult CreateEdit(int? id = null)
        {
            PostModel model = null;
            if (id.HasValue)
                model = AutoMapper.Mapper.Map<PostModel>(Data.BLL.Db.GetPost(id.Value));
            else
                model = new PostModel { ImageIds = new List<int>() };
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult CreateEdit(PostModel model)
        {
            var post = AutoMapper.Mapper.Map<Data.DTO.PostDTO>(model);
            post.UserId = ((CustomAuth.CustomPrincipal)User).UserId;
            var newPost = Data.BLL.Db.GetPost(Data.BLL.Db.CreateUpdatePost(post));
            var postModel = AutoMapper.Mapper.Map<PostModel>(newPost);
            postModel.CanEdit = true;
            return Json(postModel);
        }

        [HttpGet]
        public JsonResult GetMorePosts(int id)
        {
            var model = Data.BLL.Db.GetPosts(id).Select(AutoMapper.Mapper.Map<PostModel>).ToList();
            model.ForEach(x =>
            {
                x.CanEdit =User.Identity.IsAuthenticated && x.UserId == ((CustomAuth.CustomPrincipal)User).UserId;
            });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }
}