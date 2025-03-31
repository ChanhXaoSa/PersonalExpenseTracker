using ExpenseManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class BudgetModel
    {
        private readonly ExpenseDbContext dbContext;

        public BudgetModel()
        {
            dbContext = new ExpenseDbContext();
            dbContext.Database.EnsureCreated();
        }

        public List<Budget> GetAllBudgets()
        {
            return [.. dbContext.Budgets.Where(b => b.IsDeleted == false)];
        }

        public List<Budget> GetBudgetsByUser(string userId)
        {
            return [.. dbContext.Budgets.Where((b) => b.UserId == userId && b.IsDeleted == false)];
        }

        public List<Budget> GetBudgetsByCategory(Guid categoryId)
        {
            return [.. dbContext.Budgets.Where(b => b.CategoryId == categoryId && b.IsDeleted == false)];
        }

        public List<Budget> GetBudgetsByUserAndCategory(string userId, Guid categoryId)
        {
            return [.. dbContext.Budgets.Where(b => 
            b.UserId == userId
            && b.CategoryId == categoryId
            && b.IsDeleted == false)];
        }

        public void AddBudget(Budget budget, string userId)
        {
            budget.UserId = userId;
            dbContext.Budgets.Add(budget);
            dbContext.SaveChanges();
        }

        public void UpdateBudget(Budget budget, string userId)
        {
            var existingBudget = dbContext.Budgets
                .FirstOrDefault(b => b.Id == budget.Id && b.UserId == userId);
            if(existingBudget != null)
            {
                existingBudget.Amount = budget.Amount;
                existingBudget.StartDate = budget.StartDate;
                existingBudget.EndDate = budget.EndDate;
                dbContext.SaveChanges();
            }
        }

        public void DeleteBudget(Guid id)
        {
            var budget = dbContext.Budgets.FirstOrDefault(b => b.Id == id);
            if(budget != null)
            {
                dbContext.Budgets.Remove(budget);
                dbContext.SaveChanges();
            }
        }
    }
}
