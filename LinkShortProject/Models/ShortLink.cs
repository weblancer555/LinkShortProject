using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortProject.Models
{
    public class ShortLink
    {
        public int Id { get; set; }
        public string ShortUrl { get; set; }
        public string FullUrl { get; set; }

        [MaxLength(256)]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public int CountTransitions { get; set; }
    }
}
