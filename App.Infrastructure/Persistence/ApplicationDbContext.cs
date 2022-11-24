using App.Application.Services;
using App.Domain.Entities;
using App.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
                                        IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
                                        IdentityRoleClaim<string>, IdentityUserToken<string>>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public  DbSet<ApplicationUser> Users { get; set; }
        public  DbSet<ApplicationRole> Roles { get; set; }
        public  DbSet<ApplicationUserRole> UserRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<User> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<ApplicationUser>()
                    .HasKey(x => x.Id);

            builder.Entity<ApplicationRole>()
                    .HasKey(x => x.Id);

            builder.Entity<ApplicationRole>()
                    .Property(x => x.RoleType)
                    .IsRequired();

            builder.Entity<ApplicationUserRole>()
                    .ToTable("AspNetUserRoles");

            builder.Entity<User>()
                    .ToTable("Customers");

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                // setting primary key
                userRole.HasKey(aur => new
                {
                    aur.UserId,
                    aur.RoleId
                });

                userRole.HasOne(aur => aur.User)
                        .WithMany(au => au.UserRoles)
                        .HasForeignKey(aur => aur.UserId);

                userRole.HasOne(aur => aur.Role)
                        .WithMany(ar => ar.UserRoles)
                        .HasForeignKey(aur => aur.RoleId);
            });

            builder.Entity<ApplicationRole>()
                    .HasMany( ar => ar.Users)
                    .WithMany(au => au.Roles)
                    .UsingEntity<ApplicationUserRole>(aur =>
                    {
                        aur.HasOne(aur => aur.Role)
                            .WithMany(ar => ar.UserRoles);

                        aur.HasOne(aur => aur.User)
                            .WithMany(au => au.UserRoles);
                    });    
            
            // mapping entites relationship



        }
    }
}
