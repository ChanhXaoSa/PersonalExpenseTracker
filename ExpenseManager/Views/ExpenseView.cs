using ExpenseManager.Models;
using ExpenseManager.Presenters;
using Guna.Charts.WinForms;

namespace ExpenseManager
{
    public partial class ExpenseView : Form, IExpenseView
    {
        private readonly ExpensePresenter presenter;
        private readonly GunaChart chartExpenses;
        public ExpenseView()
        {
            InitializeComponent();
            chartExpenses = new GunaChart
            {
                Location = new Point(50, 300),
                Size= new Size(500, 300)
            };
            this.Controls.Add(chartExpenses);
            presenter = new ExpensePresenter(this, new ExpenseModel());
            LoadCategories();
            btnAdd.Click += (s, e) =>
            {
                presenter.AddExpense();
            };
            presenter.UpdateView();
        }

        private void LoadCategories()
        {
            cmbCategory.Items.AddRange(["Ăn uống", "Mua sắm", "Hóa đơn", "Khác"]);
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
            chartExpenses.Datasets.Clear();

            var dataset = new GunaPieDataset
            {
                Label = "Chi tiêu theo loại",
                BorderWidth = 1,
            };

            foreach (var category in expensesByCategory)
            {
                dataset.DataPoints.Add(new LPoint { Label = category.Key, Y = (double)category.Value });
            }

            chartExpenses.Datasets.Add(dataset);
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
                var expense = new Models.Expense
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


