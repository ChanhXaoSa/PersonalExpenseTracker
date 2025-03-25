using ExpenseManager.Models;
using ExpenseManager.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace ExpenseManager
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi-VN");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var services = new ServiceCollection();
            services.AddDbContext<ExpenseDbContext>(options =>
            {
                options.UseSqlServer("Server=(local);Database=ExpenseManagerDB;uid=sa;pwd=Trieu123!;TrustServerCertificate=true");
            });
            services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ExpenseDbContext>();
            services.AddScoped<UserManager<IdentityUser>>();

            var serviceProvider = services.BuildServiceProvider();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            using var loginView = new LoginView(userManager);
            if (loginView.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new DashboardView(loginView.Username, loginView.UserId!, userManager));
            }
            else
            {
                MessageBox.Show("Bạn cần đăng nhập để sử dụng ứng dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
    }
}