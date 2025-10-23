using System.Windows;
using PhanNgocBaoTramWPF.Views.Pages;

namespace PhanNgocBaoTramWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // load page mặc định khi app chạy
            MainFrame.Navigate(new CustomerPage());
        }

        private void BtnCustomers_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CustomerPage());
        }

        private void BtnRooms_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RoomPage());
        }

        private void BtnBookings_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BookingPage());
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReportPage());
        }
    }
}
