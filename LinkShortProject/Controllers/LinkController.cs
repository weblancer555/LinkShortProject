using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortProject.Controllers
{
    public class LinkController : Controller
    {
        public ActionResult Index(string shortLink)
        {
            return new JsonResult(shortLink);
        }
    }
}