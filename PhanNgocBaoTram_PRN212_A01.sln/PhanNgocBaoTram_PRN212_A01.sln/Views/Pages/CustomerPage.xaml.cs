using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FUMiniHotelSystem.Business.Services;
using FUMiniHotelSystem.Data.Models;

namespace PhanNgocBaoTramWPF.Views.Pages
{
    public partial class CustomerPage : Page
    {
        private readonly CustomerService _customerService = new();

        public CustomerPage()
        {
            InitializeComponent();
            LoadData();
        }

        // 📋 Load danh sách khách hàng
        private void LoadData()
        {
            dgCustomers.ItemsSource = _customerService.GetAll().ToList();
        }

        // ➕ Thêm khách hàng
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                var c = new Customer
                {
                    CustomerFullName = txtName.Text?.Trim() ?? string.Empty,
                    EmailAddress = txtEmail.Text?.Trim() ?? string.Empty,
                    Telephone = txtPhone.Text?.Trim() ?? string.Empty,
                    CustomerBirthday = dpBirth.SelectedDate ?? DateTime.Now,
                    Password = "123",   // default password
                    CustomerStatus = 1
                };

                _customerService.Create(c);
                LoadData();

                txtName.Clear();
                txtEmail.Clear();
                txtPhone.Clear();
                dpBirth.SelectedDate = null;

                MessageBox.Show("Customer added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding customer: {ex.Message}");
            }
        }

        // ❌ Xóa khách hàng
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomers.SelectedItem is Customer selected)
            {
                if (MessageBox.Show($"Delete {selected.CustomerFullName}?",
                    "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _customerService.Delete(selected.CustomerID);
                        LoadData();
                        MessageBox.Show("Customer deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a customer first.");
            }
        }

        // 🔍 Tìm kiếm khách hàng
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Enter a keyword to search.");
                return;
            }

            var result = _customerService.GetAll()
                .Where(c =>
                    c.CustomerFullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    c.EmailAddress.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    c.Telephone.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            dgCustomers.ItemsSource = result;
        }

        // 🔁 Làm mới danh sách
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            LoadData();
        }
    }
}
