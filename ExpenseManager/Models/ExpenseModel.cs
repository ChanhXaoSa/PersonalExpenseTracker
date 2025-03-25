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

        public void AddExpense(Expense expense)
        {
            dbContext.Expenses.Add(expense);
            dbContext.SaveChanges();
        }

        public List<Expense> GetAllExpenses()
        {
            return [.. dbContext.Expenses];
        }

        public decimal GetTotalExpense()
        {
            return dbContext.Expenses.Sum(e => e.Amount);
        }

        public void DeleteExpense(int id)
        {
            var expense = dbContext.Expenses.Find(id);
            if(expense != null)
            {
                dbContext.Expenses.Remove(expense);
                dbContext.SaveChanges();
            }
        }

        public void UpdateExpense(Expense expense)
        {
            var existingExpense = dbContext.Expenses.Find(expense.Id);
            if(existingExpense != null)
            {
                existingExpense.Description = expense.Description;
                existingExpense.Amount = expense.Amount;
                existingExpense.Date = expense.Date;
                existingExpense.Category = expense.Category;
                dbContext.SaveChanges();
            }
        }

        public Dictionary<string, decimal> GetExpensesByCategory()
        {
            return dbContext.Expenses.GroupBy(e => e.Category)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
        }
    }
}
