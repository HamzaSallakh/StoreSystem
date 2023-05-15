using Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AppDbContext :IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base (options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Buyers>();
            modelBuilder.Entity<Orders>();
            modelBuilder.Entity<Payments>();
            modelBuilder.Entity<ApplicationUser>();

        }
    }
}
