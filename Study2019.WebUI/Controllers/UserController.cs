using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Study2019.WebUI.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.LoginModel model)
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegUser(Models.RegUserModel model)
        {

            var salt = Data.BLL.Hash.CreateSalt(16);
            var passhash = Data.BLL.Hash.GenerateSaltedHash(model.Password, salt);

            var user = new Data.DTO.UserDTO
            {
                Birtdate = model.Birtdate,
                SharedProfile = model.SharedProfile,
                Description = model.Description,
                LoginName = model.LoginName,
                Nickname = model.Nickname,
                RegDate = DateTime.Now,
                Salt = Convert.ToBase64String(salt),
                PasswordHash = Convert.ToBase64String(passhash)
            };

            try
            {
                Data.BLL.Db.CreateUpdateUser(user);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                model.Password = null;
                model.RetryPassword = null;
                return View(model);
            }

            return RedirectToAction("Login");
        }


    }
}