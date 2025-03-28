using ExpenseManager.Models;
using ExpenseManager.Presenters;
using Microsoft.AspNetCore.Identity;
using ScottPlot;
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
    public partial class DashboardView : Form
    {
        private readonly ExpensePresenter presenter;
        private readonly ExpenseModel model;
        private readonly UserManager<IdentityUser> userManager;
        private bool isLoggingOut = false;
        private Dictionary<string, decimal> _expensesByCategory = [];
        private Dictionary<DateTime, decimal> _expensesByMonth = [];
        public DashboardView(string username, string userId, UserManager<IdentityUser> userManager)
        {
            InitializeComponent();

            this.userManager = userManager;

            model = new ExpenseModel(userManager);

            lblUsername.Text = $"Xin chào, {username}";

            var menuStrip = new MenuStrip();
            var expenseMenu = new ToolStripMenuItem("Quản lý chi tiêu");
            var chatMenu = new ToolStripMenuItem("Chat");
            var userMenu = new ToolStripMenuItem("Tài Khoản");
            menuStrip.Items.Add(expenseMenu);
            menuStrip.Items.Add(chatMenu);
            menuStrip.Items.Add(userMenu);
            Controls.Add(menuStrip);

            presenter = new ExpensePresenter(new DashboardPresenterView(this), model, userId);
            presenter.OnDataChanged += () =>
            {
                Debug.WriteLine("DashboardView: OnDataChanged được gọi");
                presenter.UpdateView();
            };

            expenseMenu.DropDownItems.Add("Thêm chi tiêu", null, (s, e) =>
            {
                var createView = new ExpenseCreateView(presenter);
                if (createView.ShowDialog(Owner) == DialogResult.OK)
                {
                    presenter.AddExpense(new Expense
                    {
                        Description = createView.Description,
                        Amount = createView.Amount,
                        Date = createView.Date,
                        Category = createView.Category,
                        UserId = userId
                    });
                }
            });
            expenseMenu.DropDownItems.Add("Sửa/Xoá chi tiêu", null, (s, e) =>
            {
                Debug.WriteLine("DashboardView: Bắt đầu tạo ExpenseUpdateView");
                var updateView = new ExpenseUpdateView(null);
                presenter.AddView(updateView);
                updateView.SetPresenter(presenter);
                Debug.WriteLine("DashboardView: Đã gán presenter");
                try
                {
                    Debug.WriteLine("DashboardView: Gọi ShowDialog");
                    updateView.ShowDialog();
                    Debug.WriteLine("DashboardView: ShowDialog hoàn tất");
                    presenter.RemoveView(updateView);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Lỗi khi hiển thị ExpenseUpdateView: {ex.Message}");
                    MessageBox.Show($"Lỗi khi hiển thị ExpenseUpdateView: {ex.Message}");
                }
            });

            userMenu.DropDownItems.Add("Đăng xuất", null, (s, e) =>
            {
                if (File.Exists("login_token.json"))
                {
                    File.Delete("login_token.json");
                }
                isLoggingOut = true;
                Close();
            });
            chatMenu.Click += (s, e) => new ChatView(presenter, userId).ShowDialog();

            lvDashboardExpenses.Columns.Add("ID", 50);
            lvDashboardExpenses.Columns.Add("Mô tả", 200);
            lvDashboardExpenses.Columns.Add("Số tiền", 100);
            lvDashboardExpenses.Columns.Add("Thời gian", 150);
            lvDashboardExpenses.Columns.Add("Danh mục", 100);
            lvDashboardExpenses.Columns.Add("Người dùng", 100);

            presenter.UpdateView();
        }
        private class DashboardPresenterView(DashboardView form) : IExpenseView
        {
            public string Description => string.Empty;
            public decimal Amount => 0;
            public DateTime Date => DateTime.Now;
            public string Category => string.Empty;

            public void ShowMessage(string message) => MessageBox.Show(message);
            public void UpdateExpenseList(List<Expense> expenses)
            {
                Debug.WriteLine("chạy ở đây nè");
                form._expensesByMonth = GetMonthlyExpenses(expenses);
                form.RenderComboChart();
                form.lb5Lastest.Items.Clear();
                form.lb5Lastest.Items.Add("5 CHI TIÊU GẦN NHẤT");
                var list5LastestExpenses = Get5LatestExpenses(expenses);
                foreach (var expense in list5LastestExpenses)
                {
                    form.lb5Lastest.Items.Add($"[{expense.Category}] {expense.Description} - {expense.Amount:C} - {expense.Date:dd/MM/yyyy}");
                }
                form.lvDashboardExpenses?.Items.Clear();
                foreach (var expense in expenses)
                {
                    var item = new ListViewItem([
                        expense.Id.ToString(),
                        expense.Description,
                        expense.Amount.ToString("C"),
                        expense.Date.ToString("hh:mm, dd/MM/yyyy"),
                        expense.Category,
                        expense.UserId
                        ]);
                    form.lvDashboardExpenses?.Items.Add(item);
                }
            }
            public void UpdateTotal(decimal total) => form.lblTotal.Text = $"Tổng chi tiêu: {total:C}";
            public void UpdateChart(Dictionary<string, decimal> expensesByCategory) => form.UpdateChart(expensesByCategory);
        }

        private void UpdateChart(Dictionary<string, decimal> expensesByCategory)
        {
            _expensesByCategory = expensesByCategory;
            RenderChart();

            //var expenses = presenter.GetExpenses();
            //_expensesByMonth = GetMonthlyExpenses(expenses);
            RenderComboChart();
        }

        private void RenderComboChart()
        {
            if (_expensesByMonth.Count == 0) return;

            var chartPlot = new Plot();

            // Chuẩn bị dữ liệu
            var dates = _expensesByMonth.Keys.OrderBy(d => d).ToArray();
            var values = dates.Select(d => (double)_expensesByMonth[d]).ToArray();
            var positions = Enumerable.Range(0, dates.Length).Select(x => (double)x).ToArray();

            // Thêm biểu đồ cột
            var bars = chartPlot.Add.Bars(positions, values);
            bars.LegendText = "Chi tiêu hàng tháng";

            // Thêm biểu đồ đường
            var line = chartPlot.Add.Scatter(positions, values);
            line.LegendText = "Xu hướng";
            line.Color = ScottPlot.Color.FromColor(System.Drawing.Color.Red);
            line.MarkerSize = 5;

            // Cấu hình trục X
            chartPlot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
                positions,
                [.. dates.Select(d => d.ToString("MM/yyyy"))]);

            // Cấu hình biểu đồ
            chartPlot.ShowLegend(Alignment.UpperRight);
            chartPlot.Axes.Left.Label.Text = "Số tiền (VND)";
            chartPlot.Axes.Bottom.Label.Text = "Tháng";
            chartPlot.Axes.AutoScale();

            // Render biểu đồ
            comboChartPictureBox.Image?.Dispose();
            using var scottImage = chartPlot.GetImage(comboChartPictureBox.Width, comboChartPictureBox.Height);
            using var stream = new MemoryStream(scottImage.GetImageBytes());
            comboChartPictureBox.Image = System.Drawing.Image.FromStream(stream);
        }

        private void RenderChart()
        {
            var chartPlot = new Plot();
            double[] values = [.. _expensesByCategory.Values.Select(c => (double)c)];
            string[] labels = [.. _expensesByCategory.Keys];
            var pie = chartPlot.Add.Pie(values);
            pie.DonutFraction = 0.5;

            double total = pie.Slices.Select(x => x.Value).Sum();
            for (int i = 0; i < pie.Slices.Count; i++)
            {
                pie.Slices[i].Label = labels[i];
                pie.Slices[i].LegendText = $"{labels[i]}: {pie.Slices[i].Value:C} ({pie.Slices[i].Value / total:p1})";
            }

            chartPlot.ShowLegend(Alignment.UpperRight);
            chartPlot.Axes.Frameless();
            chartPlot.Grid.IsVisible = false;

            chartPictureBox.Image?.Dispose();
            using var scottImage = chartPlot.GetImage(chartPictureBox.Width, chartPictureBox.Height);
            using var stream = new MemoryStream(scottImage.GetImageBytes());
            chartPictureBox.Image = System.Drawing.Image.FromStream(stream);
        }

        private static Dictionary<DateTime, decimal> GetMonthlyExpenses(List<Expense> expenses)
        {
            return expenses
                .GroupBy(e => new DateTime(e.Date.Year, e.Date.Month, 1))
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(e => e.Amount));
        }

        private static List<Expense> Get5LatestExpenses(List<Expense> expenses)
        {
            return [.. expenses.OrderByDescending(e => e.Date).Take(5)];
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!isLoggingOut && e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show("Bạn có chắc muốn thoát ứng dụng?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            base.OnFormClosing(e);
        }

        private void ChartPictureBox_Resize(object sender, EventArgs e)
        {
            if (_expensesByCategory != null && _expensesByCategory.Count > 0)
            {
                RenderChart();
            }
        }

        private void ComboChartPictureBox_Resize(object sender, EventArgs e)
        {
            if (_expensesByMonth != null && _expensesByMonth.Count > 0)
            {
                RenderComboChart();
            }
        }

        public bool IsLoggingOut => isLoggingOut;
    }
}
