using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortProject.Models;
using LinkShortProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortProject.Controllers
{
    [Authorize]
    public class CabinetController : Controller
    {
        IShortLinkRepository repository;
        ShortLinkService shortLinkService;
        UserManager<IdentityUser> userMng;

        public CabinetController(IShortLinkRepository shortLinkRepository, ShortLinkService shortLinkService, UserManager<IdentityUser> userMng)
        {
            repository = shortLinkRepository;
            this.shortLinkService = shortLinkService;
            this.userMng = userMng;
        }

        public ActionResult Index()
        {
            
            var links = repository.GetShortLinksByUserId(userMng.GetUserId(User));
            //links.ForEach((x) =>
            //{
            //    x.ShortUrl = HttpContext.Request.Scheme+ "//:" + HttpContext.Request.HttpContext.Request.Host + "/" + x.ShortUrl;
            //});
            return View(links);
        }

        [HttpPost]
        public async Task<ActionResult> AddLink(string url)
        {
            shortLinkService.GenerateShortLink(url, await userMng.GetUserAsync(User));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteLink(string shortUrl)
        {
            shortLinkService.DeleteShortLink(shortUrl, await userMng.GetUserAsync(User));
            return RedirectToAction("Index");
        }
    }
}