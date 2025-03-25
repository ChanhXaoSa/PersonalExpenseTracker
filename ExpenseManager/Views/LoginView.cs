using ExpenseManager.Models;
using Microsoft.AspNetCore.Identity;
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
    public partial class LoginView : Form
    {
        private readonly ExpenseModel model;
        private readonly UserManager<IdentityUser> userManager;

        public LoginView(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.model = new ExpenseModel(userManager);
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            Size = new Size(300, 200);
            Text = "Đăng nhập";
            StartPosition = FormStartPosition.CenterScreen;

            var lblUsername = new Label { Text = "Tên đăng nhập:", Location = new Point(20, 20) };
            var txtUsername = new TextBox { Location = new Point(120, 20), Width = 150, Name = "txtUsername" };
            var lblPassword = new Label { Text = "Mật khẩu:", Location = new Point(20, 60) };
            var txtPassword = new TextBox { Location = new Point(120, 60), Width = 150, Name = "txtPassword", UseSystemPasswordChar = true };
            var btnLogin = new Button { Text = "Đăng nhập", Location = new Point(100, 100), Width = 100 };

            btnLogin.Click += async (s, e) =>
            {
                if (await model.ValidateUserAsync(txtUsername.Text, txtPassword.Text))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            Controls.AddRange([lblUsername, txtUsername, lblPassword, txtPassword, btnLogin]);
        }

        public string Username => Controls["txtUsername"]?.Text ?? string.Empty;
        public string? UserId => userManager.FindByNameAsync(Username).Result?.Id;
    }
}
