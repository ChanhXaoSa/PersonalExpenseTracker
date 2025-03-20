using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Presenters
{
    public interface IExpenseView
    {
        string Description { get; }
        decimal Amount { get; }
        DateTime Date { get; }
        string Category { get; }

        void ShowMessage(string message);
        void UpdateExpenseList(List<Expense> expenses);
        void UpdateTotal(decimal total);
        void UpdateChart(Dictionary<string, decimal> expensesByCategory);
    }
}
