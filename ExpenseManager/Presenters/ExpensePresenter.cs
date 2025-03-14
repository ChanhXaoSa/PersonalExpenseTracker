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
        private readonly ExpenseModel model = model;

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

        private void UpdateView()
        {
            view.UpdateExpenseList(model.GetExpenses());
            view.UpdateTotal(model.GetTotalExpenses());
        }
    }
}
