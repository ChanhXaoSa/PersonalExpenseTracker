using ExpenseManager.Models;
using ExpenseManager.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using ExpenseManager.Entities;

namespace ExpenseManager
{
    internal static class Program
    {
        [STAThread]
        static async Task Main()
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
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ExpenseDbContext>();

            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<RoleManager<IdentityRole>>();

            var serviceProvider = services.BuildServiceProvider();

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await InitializeIdentityData(userManager, roleManager);

            var context = new MainApplicationContext(userManager);
            Application.Run(context);
        }

        private static async Task InitializeIdentityData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = ["Admin", "User"];
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole(roleName);
                    var result = await roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Không thể tạo role {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }

            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };
                var createResult = await userManager.CreateAsync(adminUser, "123456");
                if (!createResult.Succeeded)
                {
                    throw new Exception($"Không thể tạo admin user: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                }
            }

            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                var addToRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                if (!addToRoleResult.Succeeded)
                {
                    throw new Exception($"Không thể gán role Admin cho admin user: {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
                }
            }
        }
    }

    public class MainApplicationContext : ApplicationContext
    {
        private readonly UserManager<ApplicationUser> userManager;

        public MainApplicationContext(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            ShowLoginView();
        }

        private async void ShowLoginView()
        {
            var loginView = new LoginView(userManager);

            bool isLoggedIn = await loginView.CheckSavedLoginAsync();
            if (isLoggedIn) 
            {
                ShowDashboardView(loginView.Username, loginView.UserId!);
                return;
            }
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