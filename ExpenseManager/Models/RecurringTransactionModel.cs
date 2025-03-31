using ExpenseManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class RecurringTransactionModel
    {
        private readonly ExpenseDbContext dbContext;

        public RecurringTransactionModel()
        {
            dbContext = new ExpenseDbContext();
            dbContext.Database.EnsureCreated();
        }

        public List<RecurringTransaction> GetRecurringTransactions()
        {
            return [.. dbContext.RecurringTransactions.Where(r => r.IsDeleted == false)];
        }

        public List<RecurringTransaction> GetRecurringTransactionsByUser(string userId)
        {
            return [.. dbContext.RecurringTransactions.Where(r => r.UserId == userId && r.IsDeleted == false)];
        }

        public void AddRecurringTransaction(RecurringTransaction recurringTransaction)
        {
            dbContext.RecurringTransactions.Add(recurringTransaction);
            dbContext.SaveChanges();
        }

        public void UpdateRecurringTransaction(RecurringTransaction recurringTransaction)
        {
            var existingRecurringTransaction = dbContext.RecurringTransactions.FirstOrDefault(r => r.Id == recurringTransaction.Id);
            if(existingRecurringTransaction != null)
            {
                existingRecurringTransaction.Type = recurringTransaction.Type;
                existingRecurringTransaction.Amount = recurringTransaction.Amount;
                existingRecurringTransaction.Description = recurringTransaction.Description;
                existingRecurringTransaction.Frequency = recurringTransaction.Frequency;
                existingRecurringTransaction.NextOrcurrence = recurringTransaction.NextOrcurrence;
            }
        }

        public void DeleteRecurringTransaction(Guid id)
        {
            var recurringTransaction = dbContext.RecurringTransactions.FirstOrDefault(r => r.Id == id);
            if(recurringTransaction != null)
            {
                dbContext.RecurringTransactions.Remove(recurringTransaction);
                dbContext.SaveChanges();
            }
        }
    }
}
