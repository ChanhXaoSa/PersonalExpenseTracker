using ExpenseManager.Models;
using ExpenseManager.Presenters;
using ScottPlot.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpenseManager.Views
{
    public partial class ExpenseCreateView : Form
    {
        //private readonly ExpensePresenter presenter;

        public ExpenseCreateView(ExpensePresenter presenter)
        {
            InitializeComponent();
            //this.presenter = presenter;

            Size = new Size(400, 300);
            Text = "Thêm chi tiêu";

            var lblDesc = new Label { Text = "Mô tả:", Location = new Point(20, 20) };
            var txtDesc = new TextBox { Location = new Point(100, 20), Width = 250, Name = "txtDescription" };
            var lblAmount = new Label { Text = "Số tiền:", Location = new Point(20, 60) };
            var txtAmount = new TextBox { Location = new Point(100, 60), Width = 250, Name = "txtAmount" };
            var lblDate = new Label { Text = "Ngày:", Location = new Point(20, 100) };
            var dtpDate = new DateTimePicker { Location = new Point(100, 100), Width = 250, Name = "dtpDate" };
            var lblCat = new Label { Text = "Danh mục:", Location = new Point(20, 140) };
            var cmbCat = new ComboBox { Location = new Point(100, 140), Width = 250, Name = "cmbCategory" };
            cmbCat.Items.AddRange(["Ăn uống", "Mua sắm", "Hóa đơn", "Khác"]);
            cmbCat.SelectedIndex = 0;

            var btnAdd = new Button { Text = "Thêm", Location = new Point(150, 200), Width = 100 };
            btnAdd.Click += (s, e) => { presenter.AddExpense(new Expense { Description = Description, Amount = Amount, Date = Date, Category = Category }); Close(); };

            Controls.AddRange([lblDesc, txtDesc, lblAmount, txtAmount, lblDate, dtpDate, lblCat, cmbCat, btnAdd]);
        }

        public string Description => Controls["txtDescription"]?.Text ?? string.Empty;
        public decimal Amount => decimal.TryParse(Controls["txtAmount"]?.Text, out var amount) ? amount : 0;
        public DateTime Date
        {
            get
            {
                var dateTimePicker = Controls["dtpDate"] as DateTimePicker;
                return dateTimePicker?.Value ?? DateTime.Now;
            }
        }
        public string Category => ((ComboBox?)Controls["cmbCategory"])?.SelectedItem?.ToString() ?? "Khác";

        //public void ShowMessage(string message) => MessageBox.Show(message);
        //public void UpdateExpenseList(List<Expense> expenses) { }
        //public void UpdateTotal(decimal total) { }
        //public void UpdateChart(Dictionary<string, decimal> expensesByCategory) { }
    }
}
