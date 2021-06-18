﻿using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebSite.Models;

namespace WebSite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            if (userIdentity != null)
            {
                // custom full name of user

                if (this.fullName != null)
                {
                    userIdentity.AddClaim(new Claim("fullName", this.fullName.ToString()));
                }

            }

            // Add custom user claims here
            return userIdentity;
        }

        public string fullName { get; set; }
        public bool isBlocked { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("WebSite", throwIfV1Schema: false)
        {

        }


        public DbSet<Job> Jobs { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<UserRole> userRoles { get; set; }
       

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}