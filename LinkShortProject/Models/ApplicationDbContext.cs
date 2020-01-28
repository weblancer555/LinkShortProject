using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortProject.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<ShortLink> ShortLinks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortLink>()
                .HasIndex(b => b.ShortUrl)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
