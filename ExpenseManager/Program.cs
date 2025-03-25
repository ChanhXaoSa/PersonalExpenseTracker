using ExpenseManager.Views;

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
            using var loginView = new LoginView();
            if (loginView.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new DashboardView(loginView.Username));
            }
            else
            {
                MessageBox.Show("Bạn cần đăng nhập để sử dụng ứng dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
    }
}