using ExpenseManager.Models;
using ExpenseManager.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseManager.Views
{
    public partial class ExpenseUpdateView : Form
    {
        private readonly ExpensePresenter presenter;
        private readonly DataGridView dgvExpenses;

        public ExpenseUpdateView(ExpensePresenter presenter)
        {
            InitializeComponent();
            this.presenter = presenter;

            Size = new Size(800, 500);
            Text = "Sửa/Xóa Chi Tiêu";

            dgvExpenses = new DataGridView { Location = new Point(20, 20), Size = new Size(740, 300) };
            var btnEdit = new Button { Text = "Sửa", Location = new Point(20, 340), Width = 100 };
            var btnDelete = new Button { Text = "Xóa", Location = new Point(130, 340), Width = 100 };

            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;

            Controls.AddRange([dgvExpenses, btnEdit, btnDelete]);
            presenter.UpdateView();
        }

        public static string Description => string.Empty;
        public static decimal Amount => 0;
        public static DateTime Date => DateTime.Now;
        public static string Category => string.Empty;

        public static void ShowMessage(string message) => MessageBox.Show(message);
        public void UpdateExpenseList(List<Expense> expenses)
        {
            dgvExpenses.DataSource = null;
            dgvExpenses.DataSource = expenses;
        }
        //public void UpdateTotal(decimal total) { }
        //public void UpdateChart(Dictionary<string, decimal> expensesByCategory) { }

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
    }
}
