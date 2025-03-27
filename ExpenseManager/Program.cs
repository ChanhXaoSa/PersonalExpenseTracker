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
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("vi-VN");

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

            var context = new MainApplicationContext(userManager);
            Application.Run(context);
        }
    }

    public class MainApplicationContext : ApplicationContext
    {
        private readonly UserManager<IdentityUser> userManager;

        public MainApplicationContext(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            ShowLoginView();
        }

        private void ShowLoginView()
        {
            var loginView = new LoginView(userManager);
            loginView.FormClosed += (s, e) =>
            {
                if (loginView.DialogResult == DialogResult.OK)
                {
                    ShowDashboardView(loginView.Username, loginView.UserId!);
                }
                else
                {
                    ExitThread();
                }
            };
            loginView.Show();
        }

        private void ShowDashboardView(string username, string userId)
        {
            var dashboardView = new DashboardView(username, userId, userManager);
            dashboardView.FormClosed += (s, e) =>
            {
                if (dashboardView.IsLoggingOut)
                {
                    ShowLoginView();
                }
                else
                {
                    ExitThread();
                }
            };
            dashboardView.Show();
        }
    }
}