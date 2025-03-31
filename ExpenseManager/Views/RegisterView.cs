using ExpenseManager.Identity;
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
    public partial class RegisterView : Form
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IdentityService identityService;

        public RegisterView(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.identityService = new IdentityService(userManager);
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            txtUsername.KeyDown += async (s, e) => await HandleEnterKey(e);
            txtEmail.KeyDown += async (s, e) => await HandleEnterKey(e);
            txtPassword.KeyDown += async (s, e) => await HandleEnterKey(e);
            txtConfirmPassword.KeyDown += async (s, e) => await HandleEnterKey(e);

            btnRegister.Click += async (s, e) => await PerformRegister();
            llbLogin.LinkClicked += (s, e) => SwitchToLogin();
        }

        private async Task HandleEnterKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await PerformRegister();
            }
        }

        private async Task PerformRegister()
        {
            if (!ValidateInput())
            {
                return;
            }

            var user = new ApplicationUser
            {
                UserName = txtUsername.Text,
                Email = txtEmail.Text,
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            var result = await userManager.CreateAsync(user, txtPassword.Text);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");

                MessageBox.Show("Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SwitchToLogin();
            }
            else
            {
                string errors = string.Join("\n", result.Errors.Select(e => e.Description));
                MessageBox.Show($"Đăng ký thất bại:\n{errors}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!txtEmail.Text.Contains('@') || !txtEmail.Text.Contains('.'))
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void SwitchToLogin()
        {
            //var loginView = new LoginView(userManager);
            //loginView.Show();
            this.Close();
        }

        private void RegisterView_Load(object sender, EventArgs e)
        {
        }
    }
}
