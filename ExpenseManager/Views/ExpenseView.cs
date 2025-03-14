using ExpenseManager.Models;
using ExpenseManager.Presenters;

namespace ExpenseManager
{
    public partial class ExpenseView : Form, IExpenseView
    {
        private readonly ExpensePresenter presenter;
        public ExpenseView()
        {
            InitializeComponent();
            presenter = new ExpensePresenter(this, new ExpenseModel());
            LoadCategories();
            btnAdd.Click += (s, e) =>
            {
                presenter.AddExpense();
            };
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
    }
}
