using AutoMapper;
using Newtonsoft.Json;
using Study2019.WebUI.CustomAuth;
using Study2019.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Study2019.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        [Obsolete]
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutoMapper.Mapper.Initialize(cfg => {
                cfg.AddProfile<Data.BLL.DataProfile>();
                cfg.AddProfile<Utils.MapperProfile>();
            });
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["Cookie2"];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);

                CustomPrincipal principal = new CustomPrincipal(authTicket.Name)
                {
                    UserId = serializeModel.UserId,
                    Nickname = serializeModel.Nickname,
                    Email = serializeModel.Email
                };
                HttpContext.Current.User = principal;
            }

        }
    }
}
