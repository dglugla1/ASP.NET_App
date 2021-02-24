using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication13.Models;

namespace WebApplication13.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
                movies = Set<Movie>();
                //users = Set<User>();

            
        }
        public DbSet<Movie> movies { get; set; }
        //public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Movie>()
                .HasOne<User>(m => m.User)
                .WithMany(a => a.listOfMovies)
                .HasForeignKey(m => m.userId);
        }

        public ApplicationDbContext() { }
    }
}
