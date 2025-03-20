using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Presenters
{
    public class ExpensePresenter(IExpenseView view, ExpenseModel model)
    {
        private readonly IExpenseView view = view;
        public ExpenseModel model = model;

        public void AddExpense()
        {
            if(string.IsNullOrEmpty(view.Description) || view.Amount <= 0)
            {
                view.ShowMessage("Vui lòng nhập đầy đủ và hợp lệ!");
                return;
            }

            var expense = new Expense
            {
                Description = view.Description,
                Amount = view.Amount,
                Date = view.Date,
                Category = view.Category
            };

            model.AddExpense(expense);
            UpdateView();
        }

        public void AddExpense(Expense expense)
        {
            if (string.IsNullOrEmpty(expense.Description) || expense.Amount <= 0)
            {
                view.ShowMessage("Vui lòng nhập đầy đủ và hợp lệ!");
                return;
            }

            model.AddExpense(expense);
            UpdateView();
        }

        public void DeleteExpense(int id)
        {
            model.DeleteExpense(id);
            UpdateView();
        }

        public void EditExpense(Expense expense)
        {
            if(string.IsNullOrEmpty(expense.Description) || expense.Amount <= 0)
            {
                view.ShowMessage("Vui lòng nhập đầy đủ và hợp lệ!");
                return;
            }
            model.UpdateExpense(expense);
            UpdateView();
        }

        public void UpdateView()
        {
            view.UpdateExpenseList(model.GetAllExpenses());
            view.UpdateTotal(model.GetTotalExpense());
            var expensesByCategory = model.GetExpensesByCategory();
            if(expensesByCategory.Count > 0)
            {
                view.UpdateChart(expensesByCategory);
            } else
            {
                view.UpdateChart(new Dictionary<string, decimal> { { "Không có dữ liệu", 1m } });
            }
        }
    }
}
