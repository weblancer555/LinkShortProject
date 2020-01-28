using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortProject.Models
{
    public interface IShortLinkRepository
    {
        ShortLink GetShortLinkByShortUrl(string shortUrl);
        List<ShortLink> GetShortLinksByUserId(string userId);
        ShortLink AddShortLink(ShortLink shortLink);
        void UpdateShortLink(ShortLink shortLink);
        void DeleteShortLink(ShortLink shortLink);
    }
}
