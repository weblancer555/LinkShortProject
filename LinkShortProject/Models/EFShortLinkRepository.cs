using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortProject.Models
{
    public class EFShortLinkRepository : IShortLinkRepository
    {
        ApplicationDbContext applicationDbContext;

        public EFShortLinkRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public ShortLink AddShortLink(ShortLink shortLink)
        {
            var link = applicationDbContext.ShortLinks.Add(shortLink);
            applicationDbContext.SaveChanges();

            return link.Entity;
        }

        public void DeleteShortLink(ShortLink shortLink)
        {
            applicationDbContext.ShortLinks.Remove(shortLink);
            applicationDbContext.SaveChanges();
        }

        public ShortLink GetShortLinkByShortUrl(string shortUrl)
        {
            return applicationDbContext.ShortLinks.FirstOrDefault(x => x.ShortUrl == shortUrl);
        }

        public List<ShortLink> GetShortLinksByUserId(string userId)
        {
            return applicationDbContext.ShortLinks.Where(x => x.IdentityUserId == userId).ToList();
        }

        public void UpdateShortLink(ShortLink shortLink)
        {
            applicationDbContext.Entry(shortLink).State = EntityState.Modified;
            applicationDbContext.SaveChanges();
        }
    }
}
