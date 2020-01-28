using LinkShortProject.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortProject.Services
{
    public class ShortLinkService
    {
        IShortLinkRepository shortLinkRepository;
        private static Random random = new Random();
        public ShortLinkService(IShortLinkRepository shortLinkRepository)
        {
            this.shortLinkRepository = shortLinkRepository;
        }

        public ShortLink GenerateShortLink(string fullUrl, IdentityUser user)
        {
            string shortUrl = FreeUrlGenerate();

            var shortLink = new ShortLink()
            {
                ShortUrl = shortUrl,
                FullUrl = fullUrl,
                IdentityUser = user,
                CountTransitions = 0
            };
            return shortLinkRepository.AddShortLink(shortLink);
        }

        public string GetShortLinkWithUse(string shortUrl)
        {
            var shortLink = shortLinkRepository.GetShortLinkByShortUrl(shortUrl);
            if(shortLink!=null)
            {
                shortLink.CountTransitions++;
                shortLinkRepository.UpdateShortLink(shortLink);
                return shortLink.FullUrl;
            }
            return "";
        }

        public void DeleteShortLink(string shortUrl, IdentityUser user)
        {
            var link = shortLinkRepository.GetShortLinkByShortUrl(shortUrl);
            if (link.IdentityUserId == user.Id)
                shortLinkRepository.DeleteShortLink(link);
        }

        string FreeUrlGenerate()
        {
            string newShorUrl = GenerateRandomString(6);
            var link = shortLinkRepository.GetShortLinkByShortUrl(newShorUrl);
            if(link != null)
            {
                return FreeUrlGenerate();
            }

            return newShorUrl;
        }

        string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
