using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using web_mvc.Models;

namespace web_mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<web_mvc.Models.Marque> Marque { get; set; }
        public DbSet<web_mvc.Models.Categorie> Categorie { get; set; }
        public DbSet<web_mvc.Models.Figurine> Figurine { get; set; }
        public DbSet<web_mvc.Models.Reservation> Reservation { get; set; }
    }
}
