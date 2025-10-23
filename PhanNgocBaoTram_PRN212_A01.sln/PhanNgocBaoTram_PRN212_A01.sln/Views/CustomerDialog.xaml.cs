using System;
using System.Windows;
using FUMiniHotelSystem.Data.Models;

namespace PhanNgocBaoTramWPF.Views
{
    public partial class CustomerDialog : Window
    {
        public Customer CustomerData { get; private set; }

        public CustomerDialog(Customer existing = null)
        {
            InitializeComponent();

            if (existing != null)
            {
                // Gán dữ liệu để sửa
                CustomerData = existing;
                txtFullName.Text = existing.CustomerFullName;
                txtEmail.Text = existing.EmailAddress;
                txtPhone.Text = existing.Telephone;
                dpBirthday.SelectedDate = existing.CustomerBirthday;
            }
            else
            {
                // Tạo mới
                CustomerData = new Customer();
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter full name and email.", "Warning",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CustomerData.CustomerFullName = txtFullName.Text.Trim();
            CustomerData.EmailAddress = txtEmail.Text.Trim();
            CustomerData.Telephone = txtPhone.Text?.Trim();
            CustomerData.CustomerBirthday = dpBirthday.SelectedDate ?? DateTime.Now;
            CustomerData.CustomerStatus = 1;

            // Nếu Password rỗng (thêm mới)
            if (string.IsNullOrEmpty(CustomerData.Password))
                CustomerData.Password = "123";

            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
