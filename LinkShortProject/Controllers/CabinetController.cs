using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortProject.Controllers
{
    [Authorize]
    public class CabinetController : Controller
    {
        // GET: Cabinet
        public ActionResult Index()
        {
            return View();
        }
    }
}