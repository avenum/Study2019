using Newtonsoft.Json;
using Study2019.WebUI.CustomAuth;
using Study2019.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Study2019.WebUI.Utils;

namespace Study2019.WebUI.Controllers
{
    [Authorize]

    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOff()
        {
            HttpCookie cookie = new HttpCookie("Cookie2", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", null);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl, Models.LoginModel model)
        {
            if (ModelState.IsValid)
                if (Membership.ValidateUser(model.Login, model.Password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(model.Login, false);
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new Models.CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            Nickname = user.Nickname,
                            Email = user.Email,
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, model.Login, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie2", enTicket);
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

            ModelState.AddModelError("", "Something Wrong : Username or Password invalid ^_^ ");
            return View(model);


        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult RegUser()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegUser(Models.RegUserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var salt = Data.BLL.Hash.CreateSalt(16);
                    var passhash = Data.BLL.Hash.GenerateSaltedHash(model.Password, salt);

                    var user = AutoMapper.Mapper.Map<Data.DTO.UserDTO>(model);
                    user.Salt = Convert.ToBase64String(salt);
                    user.PasswordHash = Convert.ToBase64String(passhash);

                    Data.BLL.Db.CreateUpdateUser(user);
                }
                else return View(model);

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

        [Authorize]
        public ActionResult ViewProfile()
        {
            var model = AutoMapper.Mapper.Map<Models.UserModel>(Data.BLL.Db.GetUser(User.Identity.Name));

            return View(model);
        }
        public ActionResult EditProfile()
        {
            var model = AutoMapper.Mapper.Map<Models.UserModel>(Data.BLL.Db.GetUser(User.Identity.Name));

            return View(model);
        }
        [HttpPost]
        public ActionResult EditProfile(Models.UserModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "FormValidError";
                return View(model);
            }

            var user = AutoMapper.Mapper.Map<Data.DTO.UserDTO>(model);

            Data.BLL.Db.CreateUpdateUser(user);

            return View(model);
        }


        


    }
}