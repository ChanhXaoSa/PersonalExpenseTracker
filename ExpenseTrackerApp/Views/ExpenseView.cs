using ExpenseManager.Models;
using ExpenseManager.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseTrackerApp.Views
{
    public partial class ExpenseView : Form, IExpenseView
    {
        private readonly ExpensePresenter presenter;

        public ExpenseView()
        {
            InitializeComponent();
            this.Controls.Add(chartExpenses);
            presenter = new ExpensePresenter(this, new ExpenseModel());
            LoadCategories();
            btnAdd.Click += (s, e) => presenter.AddExpense();
            presenter.UpdateView();
        }
        private void LoadCategories()
        {
            cmbCategory.Items.AddRange(new[] { "Ăn uống", "Mua sắm", "Hóa đơn", "Khác" });
            cmbCategory.SelectedIndex = 0;
        }

        public string Description => txtDescription.Text;
        public decimal Amount => decimal.TryParse(txtAmount.Text, out var amount) ? amount : 0;
        public DateTime Date => dtpDate.Value;
        public string Category => cmbCategory.SelectedItem?.ToString() ?? string.Empty;

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void UpdateExpenseList(List<Expense> expenses)
        {
            dgvExpenses.DataSource = null;
            dgvExpenses.DataSource = expenses;
        }

        public void UpdateTotal(decimal total)
        {
            lblTotal.Text = $"Tổng chi tiêu: {total:C}";
        }

        public void UpdateChart(Dictionary<string, decimal> expensesByCategory)
        {
            chartExpenses.Series.Clear();
            var series = chartExpenses.Series.Add("Expenses");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "{0:C}";
            foreach (var category in expensesByCategory)
            {
                series.Points.AddXY(category.Key, category.Value);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                ShowMessage("Vui lòng chọn một chi tiêu để sửa!");
                return;
            }
            else
            {
                var selectedRow = dgvExpenses.SelectedRows[0];
                var expense = new Expense
                {
                    Id = (int)selectedRow.Cells["Id"].Value,
                    Description = Description,
                    Amount = Amount,
                    Date = Date,
                    Category = Category
                };
                presenter.EditExpense(expense);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count == 0)
            {
                ShowMessage("Vui lòng chọn một chi tiêu để xóa!");
                return;
            }
            else if (MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiêu này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                presenter.DeleteExpense((int)dgvExpenses.SelectedRows[0].Cells[0].Value);
            }
        }
    }

    public interface IExpenseView
    {
        string Description { get; }
        decimal Amount { get; }
        DateTime Date { get; }
        string Category { get; }
        void UpdateExpenseList(List<Expense> expenses);
        void UpdateTotal(decimal total);
        void ShowMessage(string message);
        void UpdateChart(Dictionary<string, decimal> expensesByCategory);
    }
}
