using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class ExpenseModel
    {
        private readonly ExpenseDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public ExpenseModel(UserManager<IdentityUser> userManager)
        {
            dbContext = new ExpenseDbContext();
            this.userManager = userManager;
            dbContext.Database.EnsureCreated();
        }

        public void AddExpense(Expense expense, string userId)
        {
            expense.UserId = userId;
            dbContext.Expenses.Add(expense);
            dbContext.SaveChanges();
        }

        public List<Expense> GetAllExpenses(string userId)
        {
            return [.. dbContext.Expenses.Where(e => e.UserId == userId && !e.IsDeleted)];
        }

        public decimal GetTotalExpense(string userId)
        {
            return dbContext.Expenses
                .Where(e => e.UserId == userId && !e.IsDeleted)
                .Sum(e => e.Amount);
        }

        public void DeleteExpense(Guid id, string userId)
        {
            var expense = dbContext.Expenses
                .FirstOrDefault(e => e.Id == id && e.UserId == userId); ;
            if(expense != null)
            {
                dbContext.Expenses.Remove(expense);
                dbContext.SaveChanges();
            }
        }

        public void UpdateExpense(Expense expense, string userId)
        {
            var existingExpense = dbContext.Expenses
                .FirstOrDefault(e => e.Id == expense.Id && e.UserId == userId);
            if(existingExpense != null)
            {
                existingExpense.Description = expense.Description;
                existingExpense.Amount = expense.Amount;
                existingExpense.Date = expense.Date;
                existingExpense.Category = expense.Category;
                dbContext.SaveChanges();
            }
        }

        public Dictionary<string, decimal> GetExpensesByCategory(string userId)
        {
            return dbContext.Expenses
                .Where(e => e.UserId == userId && !e.IsDeleted)
                .GroupBy(e => e.Category)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                return await userManager.CheckPasswordAsync(user, password);
            }
            return false;
        }

        public async Task<bool> ValidatePinAsync(string username, string pin)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user is ApplicationUser appUser && appUser != null)
            {
                return appUser.Pin == pin;
            }
            return false;
        }
    }
}
