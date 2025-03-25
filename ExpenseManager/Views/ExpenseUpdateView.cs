using ExpenseManager.Models;
using ExpenseManager.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseManager.Views
{
    public partial class ExpenseUpdateView : Form, IExpenseView
    {
        private ExpensePresenter? presenter;
        private readonly DataGridView? dgvExpenses;

        public ExpenseUpdateView(ExpensePresenter? _)
        {
            try
            {
                InitializeComponent();

                Size = new Size(800, 500);
                Text = "Sửa/Xóa Chi Tiêu";

                dgvExpenses = new DataGridView
                {
                    Location = new Point(20, 20),
                    Size = new Size(740, 300),
                    AutoGenerateColumns = true,
                    Visible = true,
                    Enabled = true,
                    BackgroundColor = Color.White,
                };

                var btnEdit = new Button { Text = "Sửa", Location = new Point(20, 340), Width = 100 };
                var btnDelete = new Button { Text = "Xóa", Location = new Point(130, 340), Width = 100 };

                btnEdit.Click += BtnEdit_Click;
                btnDelete.Click += BtnDelete_Click;

                Controls.AddRange([dgvExpenses, btnEdit, btnDelete]);

                Debug.WriteLine("ExpenseUpdateView: Constructor hoàn tất");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi trong constructor: {ex.Message}");
                MessageBox.Show($"Lỗi khởi tạo ExpenseUpdateView: {ex.Message}");
            }
        }

        public string Description => string.Empty;
        public decimal Amount => 0;
        public DateTime Date => DateTime.Now;
        public string Category => string.Empty;

        public void ShowMessage(string message) => MessageBox.Show(message);

        public void SetPresenter(ExpensePresenter? newPresenter)
        {
            Debug.WriteLine("ExpenseUpdateView: Đang gán presenter mới");
            this.presenter = newPresenter;
            if (this.presenter == null)
            {
                Debug.WriteLine("ExpenseUpdateView: Presenter vẫn là null sau khi gán!");
            }
            else
            {
                Debug.WriteLine("ExpenseUpdateView: Presenter đã được gán, gọi UpdateView");
                try
                {
                    this.presenter.UpdateView();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Lỗi khi gọi UpdateView trong SetPresenter: {ex.Message}");
                    MessageBox.Show($"Lỗi khi gọi UpdateView: {ex.Message}");
                }
            }
        }

        public void UpdateExpenseList(List<Expense> expenses)
        {
            try
            {
                Debug.WriteLine("UpdateExpenseList: Bắt đầu thực thi (ExpenseUpdateView)");
                Debug.WriteLine($"Số lượng chi tiêu: {expenses?.Count ?? 0}");
                if (expenses != null && expenses.Count > 0)
                {
                    foreach (var expense in expenses)
                    {
                        Debug.WriteLine($"Chi tiêu: ID={expense.Id}, Desc={expense.Description}, Amount={expense.Amount}, Date={expense.Date}, Cat={expense.Category}");
                    }
                }
                else
                {
                    Debug.WriteLine("Danh sách chi tiêu rỗng hoặc null!");
                }

                if (dgvExpenses == null)
                {
                    Debug.WriteLine("UpdateExpenseList: dgvExpenses là null!");
                    return;
                }

                if (dgvExpenses.InvokeRequired)
                {
                    Debug.WriteLine("UpdateExpenseList: Yêu cầu Invoke");
                    dgvExpenses.Invoke(new Action(() =>
                    {
                        dgvExpenses.DataSource = null;
                        dgvExpenses.DataSource = expenses;
                        dgvExpenses.Refresh();
                    }));
                }
                else
                {
                    Debug.WriteLine("UpdateExpenseList: Gán trực tiếp");
                    dgvExpenses.DataSource = null;
                    dgvExpenses.DataSource = expenses;
                    dgvExpenses.Refresh();
                }
                Debug.WriteLine("UpdateExpenseList: Gán DataSource hoàn tất (ExpenseUpdateView)");
                Debug.WriteLine("ExpenseUpdateView: Đặt Visible = true");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi trong UpdateExpenseList: {ex.Message}");
                MessageBox.Show($"Lỗi khi cập nhật danh sách chi tiêu: {ex.Message}");
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (dgvExpenses == null)
            {
                Debug.WriteLine("UpdateExpenseList: dgvExpenses là null!");
                return;
            }

            if(presenter == null)
            {
                Debug.WriteLine("BtnEdit_Click: Presenter là null!");
                return;
            }

            if (dgvExpenses.SelectedRows.Count == 0)
            {
                ShowMessage("Chọn một chi tiêu để sửa!");
                return;
            }

            var selectedExpense = (Expense)dgvExpenses.SelectedRows[0].DataBoundItem;
            var editForm = new ExpenseCreateView(presenter, selectedExpense);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                presenter.EditExpense(new Expense
                {
                    Id = selectedExpense.Id,
                    Description = editForm.Description,
                    Amount = editForm.Amount,
                    Date = editForm.Date,
                    Category = editForm.Category
                });
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvExpenses == null)
            {
                Debug.WriteLine("UpdateExpenseList: dgvExpenses là null!");
                return;
            }

            if(presenter == null)
            {
                Debug.WriteLine("BtnDelete_Click: Presenter là null!");
                return;
            }

            if (dgvExpenses.SelectedRows.Count == 0)
            {
                ShowMessage("Chọn một chi tiêu để xóa!");
                return;
            }

            if (MessageBox.Show("Xác nhận xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var selectedExpense = (Expense)dgvExpenses.SelectedRows[0].DataBoundItem;
                presenter.DeleteExpense(selectedExpense.Id);
            }
        }

        public void UpdateTotal(decimal total)
        {
            //throw new NotImplementedException();
        }

        public void UpdateChart(Dictionary<string, decimal> expensesByCategory)
        {
            //throw new NotImplementedException();
        }
    }
}
