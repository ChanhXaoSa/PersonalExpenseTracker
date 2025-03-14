using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class ExpenseModel
    {
        private readonly List<Expense> expenses = [];
        private int nextId = 1;

        public void AddExpense(Expense expense)
        {
            expense.Id = nextId++;
            expenses.Add(expense);
        }

        public List<Expense> GetExpenses()
        {
            return expenses;
        }

        public decimal GetTotalExpenses()
        {
            decimal total = 0;
            foreach (var expense in expenses)
            {
                total += expense.Amount;
            }
            return total;
        }
    }
}
