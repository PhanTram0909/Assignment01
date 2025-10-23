using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FUMiniHotelSystem.Business.Services;
using FUMiniHotelSystem.Data.Models;

namespace PhanNgocBaoTramWPF.Views.Pages
{
    public partial class ReportPage : Page
    {
        private readonly BookingService _bookingService = new();

        public ReportPage()
        {
            InitializeComponent();
            dpStart.SelectedDate = DateTime.Today.AddMonths(-1);
            dpEnd.SelectedDate = DateTime.Today;
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (!dpStart.SelectedDate.HasValue || !dpEnd.SelectedDate.HasValue)
            {
                MessageBox.Show("Vui lòng chọn khoảng thời gian");
                return;
            }

            var start = dpStart.SelectedDate.Value;
            var end = dpEnd.SelectedDate.Value;

            if (end < start)
            {
                MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu");
                return;
            }

            try
            {
                // Lấy dữ liệu booking trong khoảng thời gian
                var bookings = _bookingService.GetByPeriod(start, end).ToList();
                var customers = new CustomerService().GetAll().ToList();
                var rooms = new RoomService().GetAll().ToList();

                // Gán navigation properties
                foreach (var booking in bookings)
                {
                    booking.Customer = customers.FirstOrDefault(c => c.CustomerID == booking.CustomerID);
                    booking.Room = rooms.FirstOrDefault(r => r.RoomID == booking.RoomID);
                }

                var bookingData = bookings.Select(b => new
                {
                    b.BookingID,
                    CustomerName = b.Customer?.CustomerFullName ?? "N/A",
                    RoomNumber = b.Room?.RoomNumber ?? "N/A",
                    b.StartDate,
                    b.EndDate,
                    Days = (b.EndDate - b.StartDate).Days,
                    b.TotalPrice
                })
                .OrderByDescending(x => x.TotalPrice) // Sắp xếp theo tổng tiền giảm dần
                .ToList();

                dgReport.ItemsSource = bookingData;

                // Hiển thị thống kê tổng quan
                var totalBookings = bookingData.Count;
                var totalRevenue = bookingData.Sum(b => b.TotalPrice);
                var averageRevenue = totalBookings > 0 ? totalRevenue / totalBookings : 0;

                txtTotalBookings.Text = $"Tổng số booking: {totalBookings}";
                txtTotalRevenue.Text = $"Tổng doanh thu: {totalRevenue:C0}";
                txtAverageRevenue.Text = $"Doanh thu trung bình: {averageRevenue:C0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo báo cáo: {ex.Message}");
            }
        }
    }
}
