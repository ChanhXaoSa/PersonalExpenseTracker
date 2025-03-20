using ExpenseManager.Models;
using ExpenseManager.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseManager.Views
{
    public partial class ChatView : Form
    {
        private readonly ExpensePresenter presenter;
        private readonly ListBox chatListBox;
        private readonly TextBox txtChatInput;
        private readonly List<Expense> expenses;

        public ChatView(ExpensePresenter presenter)
        {
            InitializeComponent();
            this.presenter = presenter;

            Size = new Size(750, 400);
            Text = "Chatbot Quản Lý Chi Tiêu";

            chatListBox = new ListBox { Location = new Point(15, 20), Size = new Size(700, 300), BorderStyle = BorderStyle.FixedSingle };
            txtChatInput = new TextBox { Location = new Point(15, 330), Size = new Size(700, 30), BorderStyle = BorderStyle.FixedSingle };
            txtChatInput.KeyDown += TxtChatInput_KeyDown;

            Controls.AddRange([chatListBox, txtChatInput]);
            chatListBox.Items.Add("Chatbot: Nhập chi tiêu (25 nghìn, 2 triệu đồng), xóa (xóa 15000), hoặc tổng (tổng)");
            expenses = presenter.model.GetAllExpenses();
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

        [GeneratedRegex(@"xóa\s+(\d*\.?\d+)([mk]|vnd|vnđ|đ|đồng|nghìn|triệu)?$", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex DeleteExpenseRegex();

        [GeneratedRegex(@"^(.*?)?\s*(\d*\.?\d+)([mk]|vnd|vnđ|đ|đồng|nghìn|triệu)?$", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex ExpenseInputRegex();
    }
}
