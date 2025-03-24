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
        private readonly ExpensePresenter presenter;
        private readonly DataGridView dgvExpenses;

        public ExpenseUpdateView(ExpensePresenter presenter)
        {
            InitializeComponent();
            this.presenter = presenter;

            Size = new Size(800, 500);
            Text = "Sửa/Xóa Chi Tiêu";

            Controls.Clear();

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
            Debug.WriteLine($"dgvExpenses có trong Controls: {Controls.Contains(dgvExpenses)}");
            Debug.WriteLine($"Số lượng control: {Controls.Count}");
            presenter?.UpdateView();
        }

        public string Description => string.Empty;
        public decimal Amount => 0;
        public DateTime Date => DateTime.Now;
        public string Category => string.Empty;

        public void ShowMessage(string message) => MessageBox.Show(message);
        public void UpdateExpenseList(List<Expense> expenses)
        {
            Debug.WriteLine($"Số lượng chi tiêu: {expenses?.Count ?? 0}");
            dgvExpenses.DataSource = null;
            dgvExpenses.DataSource = new List<object> { new { TestColumn = "Kiểm tra" } };
            //dgvExpenses.DataSource = expenses;
            dgvExpenses.BringToFront();
            dgvExpenses.Invalidate();
            //MessageBox.Show("Dữ liệu đã gán");
            Refresh();
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                ShowMessage("Chọn một chi tiêu để sửa!");
                return;
            }

            var selectedExpense = (Expense)dgvExpenses.SelectedRows[0].DataBoundItem;
            var editForm = new ExpenseCreateView(presenter) { Text = "Sửa Chi Tiêu" };
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
