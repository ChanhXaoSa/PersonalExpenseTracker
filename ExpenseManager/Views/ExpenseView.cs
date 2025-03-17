using ExpenseManager.Models;
using ExpenseManager.Presenters;
using ScottPlot;
using System.Drawing;
using System.Text.RegularExpressions;

namespace ExpenseManager
{
    public partial class ExpenseView : Form, IExpenseView
    {
        private readonly ExpensePresenter presenter;
        private readonly PictureBox chartPictureBox;
        private readonly Plot chartPlot;
        private readonly ListBox chatListBox;
        private readonly TextBox txtChatInput;
        private List<Expense> expenses;

        public ExpenseView()
        {
            InitializeComponent();

            expenses = [];

            //picture box
            chartPictureBox = new PictureBox
            {
                Location = new Point(15, 250),
                Size = new Size(700, 300),
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(chartPictureBox);

            chartPlot = new Plot();

            //chat list box
            chatListBox = new ListBox
            {
                Location = new Point(15, 570),
                Size = new Size(700, 200),
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(chatListBox);

            //chat text box
            txtChatInput = new TextBox
            {
                Location = new Point(15, 780),
                Size = new Size(700, 50),
                BorderStyle = BorderStyle.FixedSingle
            };

            txtChatInput.KeyDown += TxtChatInput_KeyDown;
            Controls.Add(txtChatInput);

            chatListBox.Items.Add("Chatbot: Nhập chi tiêu (25 nghìn, 2 triệu đồng), xóa (xóa 15000), hoặc tổng (tổng)");

            presenter = new ExpensePresenter(this, new ExpenseModel());
            LoadCategories();
            btnAdd.Click += (s, e) =>
            {
                presenter.AddExpense(new Expense
                {
                    Description = Description,
                    Amount = Amount,
                    Date = Date,
                    Category = Category
                });
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
            this.expenses = expenses;
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

        private void TxtChatInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessChatInput(txtChatInput.Text.Trim());
                txtChatInput.Clear();
                e.SuppressKeyPress = true;
            }
        }

        private void ProcessChatInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                chatListBox.Items.Add("Chatbot: Vui lòng nhập thông tin!");
                return;
            }

            chatListBox.Items.Add($"Bạn: {input}");
            input = input.ToLower().Trim();

            if (input == "tổng")
            {
                decimal total = expenses?.Sum(e => e.Amount) ?? 0;
                chatListBox.Items.Add($"Chatbot: Tổng chi tiêu hiện tại: {total:C}");
                return;
            }
            if (input.StartsWith("xóa "))
            {
                var match = DeleteExpenseRegex().Match(input);
                if (match.Success)
                {
                    decimal amount_delete = ParseAmount(match.Groups[1].Value, match.Groups[2].Value);
                    var expenseToDelete = expenses?.LastOrDefault(e => e.Amount == amount_delete);
                    if (expenseToDelete != null)
                    {
                        presenter.DeleteExpense(expenseToDelete.Id); // Giả sử có DeleteExpense
                        chatListBox.Items.Add($"Chatbot: Đã xóa chi tiêu {amount_delete:C}");
                    }
                    else
                    {
                        chatListBox.Items.Add("Chatbot: Không tìm thấy chi tiêu để xóa!");
                    }
                }
                else
                {
                    chatListBox.Items.Add("Chatbot: Định dạng xóa không hợp lệ! Ví dụ: 'xóa 15000'");
                }
                return;
            }

            var (description, amount, category) = ParseExpenseInput(input);

            if (amount <= 0)
            {
                chatListBox.Items.Add("Chatbot: Định dạng không hợp lệ! Ví dụ: 'mua cá 25 nghìn', '15000đ'");
                return;
            }

            var expense = new Expense
            {
                Description = description,
                Amount = amount,
                Date = DateTime.Now,
                Category = category
            };
            presenter.AddExpense(expense);

            chatListBox.Items.Add($"Chatbot: Đã thêm '{description}' - {amount:C} vào '{category}'");
        }

        private static (string description, decimal amount, string category) ParseExpenseInput(string input)
        {
            var match = ExpenseInputRegex().Match(input);
            if (!match.Success)
                return ("", 0, "Khác");

            string description = match.Groups[1].Value.Trim();
            string numberStr = match.Groups[2].Value;
            string unit = match.Groups[3].Value;
            if (!decimal.TryParse(numberStr, out _))
            {
                return ("", 0, "Khác");
            }

            decimal amount = ParseAmount(numberStr, unit);
            string category = CategorizeExpense(description);
            if (string.IsNullOrEmpty(description))
                description = category;

            return (description, amount, category);
        }

        private static decimal ParseAmount(string numberStr, string unit)
        {
            if (!decimal.TryParse(numberStr, out decimal amount))
                return 0;

            return unit.ToLower() switch
            {
                "k" or "nghìn" => amount * 1000,
                "m" or "triệu" => amount * 1000000,
                "" or "vnd" or "vnđ" or "đ" or "đồng" => amount,
                _ => 0,
            };
        }

        private static string CategorizeExpense(string description)
        {
            if (description.Contains("ăn") || description.Contains("uống"))
                return "Ăn uống";
            if (description.Contains("mua") || description.Contains("sắm"))
                return "Mua sắm";
            if (description.Contains("hóa") || description.Contains("đơn"))
                return "Hóa đơn";
            return "Khác";
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

        [GeneratedRegex(@"xóa\s+(\d*\.?\d+)([mk]|vnd|vnđ|đ|đồng|nghìn|triệu)?$", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex DeleteExpenseRegex();
        [GeneratedRegex(@"^(.*?)?\s*(\d*\.?\d+)([mk]|vnd|vnđ|đ|đồng|nghìn|triệu)?$", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex ExpenseInputRegex();
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


