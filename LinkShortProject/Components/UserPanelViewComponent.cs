using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortProject.Components
{
    public class UserPanelViewComponent : ViewComponent
    {
        UserManager<IdentityUser> userMng;

        public UserPanelViewComponent(UserManager<IdentityUser> userMng)
        {
            this.userMng = userMng;
        }

        public IViewComponentResult Invoke()
        {
            string username = userMng.GetUserName(Request.HttpContext.User);
            return View("Default", username);
        }
    }
}
