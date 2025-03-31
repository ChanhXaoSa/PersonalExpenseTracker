using ExpenseManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class IncomeModel
    {
        private readonly ExpenseDbContext dbContext;

        public IncomeModel()
        {
            dbContext = new ExpenseDbContext();
            dbContext.Database.EnsureCreated();
        }

        public List<Income> GetAllIncomes()
        {
            return [.. dbContext.Incomes.Where(i => i.IsDeleted == false)];
        }

        public List<Income> GetAllIncomesByUser(string userId)
        {
            return [.. dbContext.Incomes.Where(i => i.UserId == userId && i.IsDeleted == false)];
        }

        public void AddIncome(Income income)
        {
            dbContext.Incomes.Add(income);
            dbContext.SaveChanges();
        }

        public void UpdateIncome(Income income)
        {
            var existingIncome = dbContext.Incomes.FirstOrDefault(i => i.Id == income.Id);
            if (existingIncome != null)
            {
                existingIncome.Amount = income.Amount;
                existingIncome.Description = income.Description;
                existingIncome.Date = income.Date;
                existingIncome.Sourse = income.Sourse;
                dbContext.SaveChanges();
            }
        }

        public void DeleteIncome(Guid id)
        {
            var income = dbContext.Incomes.FirstOrDefault(i => i.Id == id);
            if(income != null)
            {
                dbContext.Incomes.Remove(income);
                dbContext.SaveChanges();
            }
        }
    }
}
