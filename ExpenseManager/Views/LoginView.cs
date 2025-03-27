using ExpenseManager.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace ExpenseManager.Views
{
    public partial class LoginView : Form
    {
        private readonly ExpenseModel model;
        private readonly UserManager<IdentityUser> userManager;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtPin;
        private RadioButton rbPassword;
        private RadioButton rbPin;
        private CheckBox chkRememberMe;
        private const string TokenFilePath = "login_token.json";

        public LoginView(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.model = new ExpenseModel(userManager);
            InitializeComponent();
            SetupUI();
            CheckSavedLogin();
        }

        private void SetupUI()
        {
            Size = new Size(300, 330);
            Text = "Đăng nhập";
            StartPosition = FormStartPosition.CenterScreen;

            var lblUsername = new Label { Text = "Tên đăng nhập:", Location = new Point(20, 20) };
            txtUsername = new TextBox { Location = new Point(120, 20), Width = 150, Name = "txtUsername" };

            rbPassword = new RadioButton { Text = "Dùng mật khẩu", Location = new Point(20, 60), Checked = true };
            var lblPassword = new Label { Text = "Mật khẩu:", Location = new Point(20, 90) };
            txtPassword = new TextBox
            {
                Location = new Point(120, 90),
                Width = 150,
                Name = "txtPassword",
                UseSystemPasswordChar = true
            };

            rbPin = new RadioButton { Text = "Dùng PIN", Location = new Point(20, 130) };
            var lblPin = new Label { Text = "Mã PIN:", Location = new Point(20, 160) };
            txtPin = new TextBox
            {
                Location = new Point(120, 160),
                Width = 150,
                Name = "txtPin",
                MaxLength = 6
            };

            chkRememberMe = new CheckBox { Text = "Lưu đăng nhập", Location = new Point(120, 190), Width = 150 };

            var btnLogin = new Button { Text = "Đăng nhập", Location = new Point(100, 230), Width = 100 };

            txtUsername.KeyDown += async (s, e) => await HandleEnterKey(e);
            txtPassword.KeyDown += async (s, e) => await HandleEnterKey(e);
            txtPin.KeyDown += async (s, e) => await HandleEnterKey(e);

            rbPassword.CheckedChanged += (s, e) => UpdateInputVisibility();
            rbPin.CheckedChanged += (s, e) => UpdateInputVisibility();

            btnLogin.Click += async (s, e) => await PerformLogin();

            Controls.AddRange([lblUsername, txtUsername, rbPassword, lblPassword, txtPassword,
                rbPin, lblPin, txtPin, chkRememberMe, btnLogin]);

            UpdateInputVisibility();
        }

        private void UpdateInputVisibility()
        {
            txtPassword.Enabled = rbPassword.Checked;
            txtPin.Enabled = rbPin.Checked;
        }

        private async Task HandleEnterKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await PerformLogin();
            }
        }

        private async Task PerformLogin()
        {
            string username = txtUsername.Text;
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool loginSuccess = false;
            if (rbPassword.Checked)
            {
                loginSuccess = await model.ValidateUserAsync(username, txtPassword.Text);
            }
            else if (rbPin.Checked)
            {
                loginSuccess = await model.ValidatePinAsync(username, txtPin.Text);
            }

            if (loginSuccess)
            {
                if (chkRememberMe.Checked)
                {
                    await SaveLoginToken(username);
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show($"Tên đăng nhập hoặc {(rbPassword.Checked ? "mật khẩu" : "mã PIN")} không đúng!",
                    "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task SaveLoginToken(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                var tokenData = new LoginToken { UserId = user.Id, Username = username, Expiry = DateTime.Now.AddDays(30) };
                string json = JsonSerializer.Serialize(tokenData);
                byte[] encryptedData = ProtectedData.Protect(Encoding.UTF8.GetBytes(json), null, DataProtectionScope.CurrentUser);
                File.WriteAllBytes(TokenFilePath, encryptedData);
            }
        }

        private async void CheckSavedLogin()
        {
            if (File.Exists(TokenFilePath))
            {
                byte[] encryptedData = File.ReadAllBytes(TokenFilePath);
                byte[] decryptedData = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
                string json = Encoding.UTF8.GetString(decryptedData);
                var tokenData = JsonSerializer.Deserialize<LoginToken>(json);
                if (tokenData != null && tokenData.Expiry > DateTime.Now)
                {
                    var user = await userManager.FindByIdAsync(tokenData.UserId);
                    if (user != null && user.UserName == tokenData.Username)
                    {
                        txtUsername.Text = tokenData.Username;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            }
        }

        public string Username => txtUsername.Text ?? string.Empty;
        public string? UserId => userManager.FindByNameAsync(Username).Result?.Id;

        private class LoginToken
        {
            public string UserId { get; set; }
            public string Username { get; set; }
            public DateTime Expiry { get; set; }
        }
    }
}