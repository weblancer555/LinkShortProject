using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortProject.Controllers
{
    public class LinkController : Controller
    {
        ShortLinkService shortLinkService;

        public LinkController(ShortLinkService shortLinkService)
        {
            this.shortLinkService = shortLinkService;
        }
        public ActionResult Index(string shortUrl)
        {
            var shortLink = shortLinkService.GetShortLinkWithUse(shortUrl);
            if(!string.IsNullOrEmpty(shortLink))
            {
                if(!(shortLink.Contains("http://") || shortLink.Contains("https://")))
                {
                    shortLink = "http://" + shortLink;
                }               
                return Redirect(shortLink);
            }
            else
            {
                return Redirect("404");
            }           
        }
    }
}