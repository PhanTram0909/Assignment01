using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FUMiniHotelSystem.Business.Services;
using FUMiniHotelSystem.Data.Models;

namespace PhanNgocBaoTramWPF.Views.Pages
{
    public partial class BookingPage : Page
    {
        private readonly BookingService _bookingService = new();
        private readonly CustomerService _customerService = new();
        private readonly RoomService _roomService = new();

        public BookingPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Nạp dữ liệu combobox và danh sách booking
            cbCustomer.ItemsSource = _customerService.GetAll().ToList();
            cbRoom.ItemsSource = _roomService.GetAll().ToList();

            // Load navigation properties cho booking
            var bookings = _bookingService.GetAll().ToList();
            var customers = _customerService.GetAll().ToList();
            var rooms = _roomService.GetAll().ToList();

            // Gán navigation properties
            foreach (var booking in bookings)
            {
                booking.Customer = customers.FirstOrDefault(c => c.CustomerID == booking.CustomerID);
                booking.Room = rooms.FirstOrDefault(r => r.RoomID == booking.RoomID);
            }

            // Hiển thị danh sách booking
            var bookingData = bookings.Select(b => new
            {
                b.BookingID,
                CustomerName = b.Customer?.CustomerFullName ?? "N/A",
                RoomNumber = b.Room?.RoomNumber ?? "N/A",
                b.StartDate,
                b.EndDate,
                b.TotalPrice
            }).ToList();

            dgBookings.ItemsSource = bookingData;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (cbCustomer.SelectedItem is not Customer customer)
            {
                MessageBox.Show("Please select a customer.");
                return;
            }

            if (cbRoom.SelectedItem is not Room room)
            {
                MessageBox.Show("Please select a room.");
                return;
            }

            if (dpFrom.SelectedDate == null || dpTo.SelectedDate == null)
            {
                MessageBox.Show("Please select booking dates.");
                return;
            }

            DateTime start = dpFrom.SelectedDate.Value;
            DateTime end = dpTo.SelectedDate.Value;

            if (end <= start)
            {
                MessageBox.Show("End date must be after start date.");
                return;
            }

            // Tính tổng tiền dựa trên giá phòng thực tế
            decimal total = _bookingService.CalculateTotalPrice(room.RoomID, start, end);

            var booking = new Booking
            {
                CustomerID = customer.CustomerID,
                RoomID = room.RoomID,
                StartDate = start,
                EndDate = end,
                TotalPrice = total
            };

            _bookingService.Create(booking);
            MessageBox.Show("Booking created successfully!");

            LoadData();
        }
    }
}
