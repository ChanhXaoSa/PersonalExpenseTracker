using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class ExpenseDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        private readonly string connectionString;

        public ExpenseDbContext()
        {
            connectionString = "Server=(local);Database=ExpenseManagerDB;Integrated Security=true;uid=sa;pwd=Trieu123!;TrustServerCertificate=true";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine);
            }
        }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Category).IsRequired().HasMaxLength(50);
            });


        }
    }
}
