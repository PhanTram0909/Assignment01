using System.Windows;
using FUMiniHotelSystem.Business.Services;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PhanNgocBaoTramWPF.Views
{
    public partial class LoginWindow : Window
    {
        private readonly AuthService _authService;

        public LoginWindow()
        {
            InitializeComponent();

            // Load cấu hình từ appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            _authService = new AuthService(config);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text;
            var pass = pwd.Password;

            var role = _authService.Authenticate(email, pass);

            if (role == "Admin" || role == "Customer")
            {
                txtStatus.Text = $"Login success as {role}!";
                // Open MainWindow and close LoginWindow
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                txtStatus.Text = "Invalid email or password!";
            }
        }
    }
}
