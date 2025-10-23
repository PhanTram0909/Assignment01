using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FUMiniHotelSystem.Business.Services;
using FUMiniHotelSystem.Data.Models;
using PhanNgocBaoTramWPF.Helpers;

namespace PhanNgocBaoTramWPF.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        private readonly CustomerService _customerService = new();
        private ObservableCollection<Customer> _customers = new();
        private Customer? _selectedCustomer;
        private string? _searchText;

        public CustomerViewModel()
        {
            LoadCustomers();
        }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetProperty(ref _selectedCustomer, value);
        }

        public string? SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                SearchCustomers(null);
            }
        }

        public ICommand AddCommand => new RelayCommand(AddCustomer);
        public ICommand EditCommand => new RelayCommand(EditCustomer, _ => SelectedCustomer is not null);
        public ICommand DeleteCommand => new RelayCommand(DeleteCustomer, _ => SelectedCustomer is not null);
        public ICommand SearchCommand => new RelayCommand(SearchCustomers);
        public ICommand RefreshCommand => new RelayCommand(_ => LoadCustomers());

        private void LoadCustomers()
        {
            var customers = _customerService.GetAll().ToList();
            Customers = new ObservableCollection<Customer>(customers);
        }

        private void AddCustomer(object? parameter)
        {
            // Logic để mở dialog thêm customer
            // Sẽ được implement trong View
        }

        private void EditCustomer(object? parameter)
        {
            // Logic để mở dialog sửa customer
            // Sẽ được implement trong View
        }

        private void DeleteCustomer(object? parameter)
        {
            if (SelectedCustomer is not null)
            {
                try
                {
                    _customerService.Delete(SelectedCustomer.CustomerID);
                    LoadCustomers();
                }
                catch (Exception)
                {
                    // Handle error
                }
            }
        }

        private void SearchCustomers(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadCustomers();
                return;
            }

            var filteredCustomers = _customerService.GetAll()
                .Where(c => c.CustomerFullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                           c.EmailAddress.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                           c.Telephone.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Customers = new ObservableCollection<Customer>(filteredCustomers);
        }
    }
}
