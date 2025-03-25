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
        private readonly PictureBox chartPictureBox;
        private readonly System.Windows.Forms.Label lblTotal;
        private readonly System.Windows.Forms.Label lblUsername;
        private readonly ExpenseModel model;
        public DashboardView(string username, string userId, UserManager<IdentityUser> userManager)
        {
            InitializeComponent();

            model = new ExpenseModel(userManager);

            chartPictureBox = new PictureBox
            {
                Size = new Size(700, 300),
                Location = new Point(15, 50),
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(chartPictureBox);

            lblTotal = new System.Windows.Forms.Label
            {
                Text = "Tổng chi: 0",
                Location = new Point(15, 20),
                Size = new Size(200, 20),
            };
            Controls.Add(lblTotal);

            lblUsername = new System.Windows.Forms.Label
            {
                Text = $"Xin chào, {username}",
                Location = new Point(220, 20),
                Size = new Size(200, 20),
            };
            Controls.Add(lblUsername);

            var menuStrip = new MenuStrip();
            var expenseMenu = new ToolStripMenuItem("Quản lý chi tiêu");
            var chatMenu = new ToolStripMenuItem("Chat");
            menuStrip.Items.Add(expenseMenu);
            menuStrip.Items.Add(chatMenu);
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
                if(createView.ShowDialog(Owner) == DialogResult.OK)
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
            var chartPlot = new Plot();
            double[] values = expensesByCategory.Values.Select(c => (double)c).ToArray();
            string[] labels = [.. expensesByCategory.Keys];
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
    }
}
