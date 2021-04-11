using KromelSite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KromelSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Slider> Slider { get; set; }
        public DbSet<UrunGruplari> UrunGruplari { get; set; }
        public DbSet<Makina> Makina { get; set; }
    }
}
