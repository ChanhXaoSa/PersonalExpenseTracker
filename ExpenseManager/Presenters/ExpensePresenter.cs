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
        private readonly List<IExpenseView> views = [];
        public readonly ExpenseModel model;
        private readonly string userId;
        public event Action? OnDataChanged;

        public ExpensePresenter(IExpenseView initialView, ExpenseModel model, string userId)
        {
            views.Add(initialView);
            this.model = model;
            Debug.WriteLine($"ExpensePresenter: Được khởi tạo với view loại {initialView.GetType().Name}");
            this.userId = userId;
        }

        public void AddView(IExpenseView view)
        {
            if (!views.Contains(view))
            {
                views.Add(view);
                Debug.WriteLine($"ExpensePresenter: Thêm view loại {view.GetType().Name}");
            }
            UpdateView();
        }

        public void RemoveView(IExpenseView view)
        {
            views.Remove(view);
            Debug.WriteLine($"ExpensePresenter: Xóa view loại {view.GetType().Name}");
        }

        public void AddExpense(IExpenseView sourceView)
        {
            if (string.IsNullOrEmpty(sourceView.Description) || sourceView.Amount <= 0)
            {
                views.ForEach(v => v.ShowMessage("Vui lòng nhập đầy đủ và hợp lệ!"));
                return;
            }

            var expense = new Expense
            {
                Description = sourceView.Description,
                Amount = sourceView.Amount,
                Date = sourceView.Date,
                Category = sourceView.Category,
                UserId = userId,
            };

            model.AddExpense(expense, userId);
            UpdateView();
            OnDataChanged?.Invoke();
        }

        public void AddExpense(Expense expense)
        {
            if (string.IsNullOrEmpty(expense.Description) || expense.Amount <= 0)
            {
                views.ForEach(v => v.ShowMessage("Vui lòng nhập đầy đủ và hợp lệ!"));
                return;
            }

            model.AddExpense(expense, expense.UserId);
            UpdateView();
            OnDataChanged?.Invoke();
        }

        public void DeleteExpense(Guid id)
        {
            model.DeleteExpense(id, userId);
            UpdateView();
            OnDataChanged?.Invoke();
        }

        public void EditExpense(Expense expense)
        {
            if(string.IsNullOrEmpty(expense.Description) || expense.Amount <= 0)
            {
                views.ForEach(v => v.ShowMessage("Vui lòng nhập đầy đủ và hợp lệ!"));
                return;
            }
            model.UpdateExpense(expense, userId);
            UpdateView();
            OnDataChanged?.Invoke();
        }

        public void UpdateView()
        {
            try
            {
                var expenses = model.GetAllExpenses(userId);
                Debug.WriteLine($"Presenter: Truyền {expenses?.Count ?? 0} chi tiêu vào view");
                foreach (var view in views)
            {
                Debug.WriteLine($"Presenter: Cập nhật view loại {view.GetType().Name}");
                view.UpdateExpenseList(expenses!);
                view.UpdateTotal(model.GetTotalExpense(userId));
                var expensesByCategory = model.GetExpensesByCategory(userId);
                if (expensesByCategory.Count > 0)
                {
                    view.UpdateChart(expensesByCategory);
                }
                else
                {
                    view.UpdateChart(new Dictionary<string, decimal> { { "Không có dữ liệu", 1m } });
                }
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
