using ExpenseManager.Entities;
using ExpenseManager.Identity;
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

        public ExpenseModel()
        {
            dbContext = new ExpenseDbContext();
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
            return [.. dbContext.Expenses.Where(e => e.UserId == userId && !e.IsDeleted).OrderByDescending(e => e.Date)];
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
                .FirstOrDefault(e => e.Id == id && e.UserId == userId);
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
    }
}
