using ExpenseManager.Models;
using ExpenseManager.Presenters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ExpenseManager.Views
{
    public partial class ChatView : Form
    {
        private readonly ExpensePresenter presenter;
        private readonly FlowLayoutPanel chatPanel;
        private readonly TextBox txtChatInput;
        private readonly Button btnSend;
        private readonly List<Expense> expenses;
        private readonly string userIdMain;

        public ChatView(ExpensePresenter presenter, string userId)
        {
            InitializeComponent();
            this.presenter = presenter;
            userIdMain = userId;
            Size = new Size(750, 500);
            Text = "Chatbot Quản Lý Chi Tiêu";
            BackColor = Color.White;

            chatPanel = new FlowLayoutPanel
            {
                Location = new Point(10, 10),
                Size = new Size(720, 400),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BorderStyle = BorderStyle.FixedSingle
            };

            txtChatInput = new TextBox
            {
                Location = new Point(10, 420),
                Size = new Size(620, 30),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10)
            };
            txtChatInput.KeyDown += TxtChatInput_KeyDown;

            btnSend = new Button
            {
                Location = new Point(640, 420),
                Size = new Size(90, 30),
                Text = "Gửi",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSend.Click += (s, e) => ProcessChatInput(txtChatInput.Text.Trim(), userIdMain);

            Controls.AddRange([chatPanel, txtChatInput, btnSend]);
            AddMessage("Chatbot: Nhập chi tiêu (25 nghìn, 2 triệu đồng), xóa (xóa 15000), hoặc tổng (tổng)", false);
            expenses = presenter.model.GetAllExpenses(userId);
        }

        private void TxtChatInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessChatInput(txtChatInput.Text.Trim(), userIdMain);
                e.SuppressKeyPress = true;
            }
        }

        private void ProcessChatInput(string input, string userId)
        {
            if (string.IsNullOrEmpty(input)) return;

            AddMessage($"Bạn: {input}", true);
            input = input.ToLower().Trim();

            if (input == "tổng")
            {
                decimal total = expenses?.Sum(e => e.Amount) ?? 0;
                AddMessage($"Chatbot: Tổng chi tiêu hiện tại: {total:C}", false);
            }
            else if (input.StartsWith("xóa "))
            {
                var match = DeleteExpenseRegex().Match(input);
                if (match.Success)
                {
                    decimal amount_delete = ParseAmount(match.Groups[1].Value, match.Groups[2].Value);
                    var expenseToDelete = expenses?.LastOrDefault(e => e.Amount == amount_delete);
                    if (expenseToDelete != null)
                    {
                        presenter.DeleteExpense(expenseToDelete.Id);
                        AddMessage($"Chatbot: Đã xóa chi tiêu {amount_delete:C}", false);
                    }
                    else
                    {
                        AddMessage("Chatbot: Không tìm thấy chi tiêu để xóa!", false);
                    }
                }
                else
                {
                    AddMessage("Chatbot: Định dạng xóa không hợp lệ! Ví dụ: 'xóa 15000'", false);
                }
            }
            else
            {
                var (description, amount, category) = ParseExpenseInput(input);
                if (amount <= 0)
                {
                    AddMessage("Chatbot: Định dạng không hợp lệ! Ví dụ: 'mua cá 25 nghìn', '15000đ'", false);
                    return;
                }

                var expense = new Expense
                {
                    Description = description,
                    Amount = amount,
                    Date = DateTime.Now,
                    Category = category,
                    UserId = userId
                };
                presenter.AddExpense(expense);
                AddMessage($"Chatbot: Đã thêm '{description}' - {amount:C} vào '{category}'", false);
            }

            txtChatInput.Clear();
            chatPanel.ScrollControlIntoView(chatPanel.Controls[chatPanel.Controls.Count - 1]);
        }

        private void AddMessage(string message, bool isUser)
        {
            var messagePanel = new Panel
            {
                Size = new Size(700, 50),
                Margin = new Padding(5)
            };

            var icon = new PictureBox
            {
                Size = new Size(30, 30),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            try
            {
                if (isUser)
                {
                    using (var ms = new System.IO.MemoryStream(Resources.user))
                    {
                        icon.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    using (var ms = new System.IO.MemoryStream(Resources.bot))
                    {
                        icon.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception)
            {
                icon.BackColor = isUser ? Color.Blue : Color.Green;
                icon.BorderStyle = BorderStyle.FixedSingle;
            }

            var bubble = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(600, 0),
                Text = message,
                Padding = new Padding(10),
                BackColor = isUser ? Color.FromArgb(0, 122, 204) : Color.FromArgb(240, 240, 240),
                ForeColor = isUser ? Color.White : Color.Black,
                Font = new Font("Segoe UI", 10),
                Location = new Point(50, 10),
                TextAlign = ContentAlignment.MiddleLeft
            };

            messagePanel.Controls.AddRange([icon, bubble]);

            if (isUser)
            {
                bubble.Left = messagePanel.Width - bubble.Width - 40;
                icon.Left = bubble.Left + bubble.Width + 10;
            }
            else
            {
                bubble.Left = 50;
                icon.Left = 10;
            }

            messagePanel.Height = Math.Max(bubble.Height + 20, 50);
            bubble.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, bubble.Width, bubble.Height, 15, 15));

            chatPanel.Controls.Add(messagePanel);
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int left, int top, int right, int bottom, int width, int height);

        private static (string description, decimal amount, string category) ParseExpenseInput(string input)
        {
            var match = ExpenseInputRegex().Match(input);
            if (!match.Success)
                return ("", 0, "Khác");

            string description = match.Groups[1].Value.Trim();
            string numberStr = match.Groups[2].Value;
            string unit = match.Groups[3].Value;
            if (!decimal.TryParse(numberStr, out _))
                return ("", 0, "Khác");

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