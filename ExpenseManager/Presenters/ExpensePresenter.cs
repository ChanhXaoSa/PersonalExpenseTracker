using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Presenters
{
    public class ExpensePresenter
    {
        private readonly IExpenseView view;
        public readonly ExpenseModel model;

        public ExpensePresenter(IExpenseView view, ExpenseModel model)
        {
            this.view = view;
            this.model = model;
            Debug.WriteLine($"ExpensePresenter: Được khởi tạo với view loại {view.GetType().Name}");
        }

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
            try
            {
                var expenses = model.GetAllExpenses();
                Debug.WriteLine($"Presenter: Truyền {expenses?.Count ?? 0} chi tiêu vào view");
                Debug.WriteLine($"Presenter: Gọi UpdateExpenseList trên {view.GetType().Name}");
                view.UpdateExpenseList(expenses!);
                view.UpdateTotal(model.GetTotalExpense());
                var expensesByCategory = model.GetExpensesByCategory();
                if (expensesByCategory.Count > 0)
                {
                    view.UpdateChart(expensesByCategory);
                }
                else
                {
                    view.UpdateChart(new Dictionary<string, decimal> { { "Không có dữ liệu", 1m } });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi trong UpdateView: {ex.Message}");
                MessageBox.Show($"Lỗi trong presenter: {ex.Message}");
            }
        }
    }
}
