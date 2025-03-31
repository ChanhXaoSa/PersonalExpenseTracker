using ExpenseManager.Common;
using ExpenseManager.Entities;
using ExpenseManager.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class ExpenseDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<RecurringTransaction> RecurringTransactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Goal> Goals { get; set; }

        private readonly string connectionString;

        public ExpenseDbContext()
        {
            connectionString = "Server=(local);Database=ExpenseManagerDB;uid=sa;pwd=Trieu123!;TrustServerCertificate=true";
        }

        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options)
            : base(options)
        {
            connectionString = null!;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(t => typeof(BaseAuditableEntity).IsAssignableFrom(t.ClrType) && t.ClrType != typeof(BaseAuditableEntity)))
            {
                modelBuilder.Entity(entityType.ClrType, b =>
                {
                    b.HasKey("Id");
                    b.Property("Id").HasColumnType("uniqueidentifier").ValueGeneratedOnAdd().IsRequired();
                    b.Property("Created").HasColumnType("datetime").IsRequired();
                    b.Property("CreatedBy").HasMaxLength(128).IsRequired(false);
                    b.Property("LastModified").HasColumnType("datetime").IsRequired(false);
                    b.Property("LastModifiedBy").HasMaxLength(128).IsRequired(false);
                });
            }

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.Property(e => e.Description).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Category).IsRequired().HasMaxLength(50);
                entity.HasOne<ApplicationUser>()
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                LockoutEnabled = false
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "123456");
            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            );
        }
    }
}
