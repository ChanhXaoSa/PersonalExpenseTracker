using ExpenseManager.Models;
using ExpenseManager.Presenters;
using ScottPlot;
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
    public partial class DashboardView : Form
    {
        private readonly ExpensePresenter presenter;
        private readonly PictureBox chartPictureBox;
        private readonly System.Windows.Forms.Label lblTotal;
        private readonly ExpenseModel model;
        public DashboardView()
        {
            InitializeComponent();

            model = new ExpenseModel();

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

            var menuStrip = new MenuStrip();
            var expenseMenu = new ToolStripMenuItem("Quản lý chi tiêu");
            var chatMenu = new ToolStripMenuItem("Chat");
            menuStrip.Items.Add(expenseMenu);
            menuStrip.Items.Add(chatMenu);
            Controls.Add(menuStrip);

            presenter = new ExpensePresenter(new DashboardPresenterView(this), model);
            expenseMenu.DropDownItems.Add("Thêm chi tiêu", null, (s, e) => new ExpenseCreateView(presenter).ShowDialog());
            expenseMenu.DropDownItems.Add("Sửa/Xoá chi tiêu", null, (s, e) =>
            {
                var updatePresenter = new ExpensePresenter(new ExpenseUpdateView(presenter), model);
                var updateView = new ExpenseUpdateView(updatePresenter);
                updateView.ShowDialog();
                updateView.Activate();
            });
            chatMenu.Click += (s, e) => new ChatView(presenter).ShowDialog();

            presenter.UpdateView();
        }
        private class DashboardPresenterView(DashboardView form) : IExpenseView
        {
            public string Description => string.Empty;
            public decimal Amount => 0;
            public DateTime Date => DateTime.Now;
            public string Category => string.Empty;

            public void ShowMessage(string message) => MessageBox.Show(message);
            public void UpdateExpenseList(List<Expense> expenses) { }
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
