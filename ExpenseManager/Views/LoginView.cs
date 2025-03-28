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
            txtUsername.KeyDown += async (s, e) => await HandleEnterKey(e);
            txtPassword.KeyDown += async (s, e) => await HandleEnterKey(e);
            var msLogo = new MemoryStream(Resources.logo);
            pbLogo.Image = Image.FromStream(msLogo);
            btnLogin.Click += async (s, e) => await PerformLogin();
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
            loginSuccess = await model.ValidateUserAsync(username, txtPassword.Text);

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
                MessageBox.Show($"Tên đăng nhập hoặc {("mật khẩu")} không đúng!",
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}