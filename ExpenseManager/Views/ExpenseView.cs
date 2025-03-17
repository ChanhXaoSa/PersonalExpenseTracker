using ExpenseManager.Models;
using ExpenseManager.Presenters;
using ScottPlot;
using System.Drawing;

namespace ExpenseManager
{
    public partial class ExpenseView : Form, IExpenseView
    {
        private readonly ExpensePresenter presenter;
        private readonly PictureBox chartPictureBox;
        private readonly Plot chartPlot;
        public ExpenseView()
        {
            InitializeComponent();

            chartPictureBox = new PictureBox
            {
                Location = new Point(15, 250),
                Size = new Size(700, 300),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(chartPictureBox);

            chartPlot = new Plot();


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
            chartPlot.Clear();

            double[] values = expensesByCategory.Values.Select(c => (double)c).ToArray();
            string[] labels = [.. expensesByCategory.Keys];
            ScottPlot.Color[] colors = expensesByCategory.Values.Select(_ => ScottPlot.Color.RandomHue()).ToArray();

            var pie = chartPlot.Add.Pie(values);
            //pie.ExplodeFraction = 0.1;
            //pie.SliceLabelDistance = 1.4;
            pie.DonutFraction = .5;

            double total = pie.Slices.Select(x => x.Value).Sum();
            for (int i = 0; i < pie.Slices.Count; i++)
            {
                pie.Slices[i].Label = $"{labels[i]}";
                pie.Slices[i].LegendText = $"{pie.Slices[i].Label}: {pie.Slices[i].Value:C} " +
                $"({pie.Slices[i].Value / total:p1})";
            }

            chartPlot.ShowLegend(Alignment.UpperRight);

            chartPlot.Axes.Frameless();
            chartPlot.Grid.IsVisible = false;

            chartPictureBox.Image?.Dispose();
            using var scottImage = chartPlot.GetImage(chartPictureBox.Width, chartPictureBox.Height);
            using var stream = new MemoryStream(scottImage.GetImageBytes());
            chartPictureBox.Image = System.Drawing.Image.FromStream(stream);
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


