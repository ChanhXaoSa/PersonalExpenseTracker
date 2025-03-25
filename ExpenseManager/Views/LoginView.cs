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
        public LoginView()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            Size = new Size(300, 300);

            Text = "Đăng nhập";
            StartPosition = FormStartPosition.CenterScreen;

            var lblUsername = new Label { Text = "Tên đăng nhập:", Location = new Point(20, 20) };
            var txtUsername = new TextBox { Location = new Point(120, 20), Width = 150, Name = "txtUsername" };
            var lblPassword = new Label { Text = "Mật khẩu:", Location = new Point(20, 60) };
            var txtPassword = new TextBox { Location = new Point(120, 60), Width = 150, Name = "txtPassword", UseSystemPasswordChar = true };
            var btnLogin = new Button { Text = "Đăng nhập", Location = new Point(100, 100), Width = 100 };
            var btnExit = new Button { Text = "Thoát", Location = new Point(100, 140), Width = 100 };
            btnExit.Click += (s, e) => Application.Exit();
            Controls.Add(btnExit);
            btnLogin.Click += (s, e) =>
            {
                if (ValidateLogin(txtUsername.Text, txtPassword.Text))
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

        private static bool ValidateLogin(string username, string password)
        {
            var validUsers = new (string Username, string Password)[]
            {
                ("admin", "123456"),
                ("user", "password")
            };

            return validUsers.Any(u => u.Username == username && u.Password == password);
        }

        public string Username => Controls["txtUsername"]?.Text ?? string.Empty;
    }
}
