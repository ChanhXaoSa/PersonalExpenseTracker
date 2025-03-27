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
            }
            public void UpdateTotal(decimal total) => form.lblTotal.Text = $"Tổng chi tiêu: {total:C}";
            public void UpdateChart(Dictionary<string, decimal> expensesByCategory) => form.UpdateChart(expensesByCategory);
        }

        private void UpdateChart(Dictionary<string, decimal> expensesByCategory)
        {
            _expensesByCategory = expensesByCategory;
            RenderChart();
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

        public bool IsLoggingOut => isLoggingOut;
    }
}
