using System;
using System.Collections.Generic;
using System.Linq;
using FUMiniHotelSystem.Data.Database;
using FUMiniHotelSystem.Data.Models;

namespace FUMiniHotelSystem.Data.Repositories
{
    public class CustomerRepository
    {
        private readonly List<Customer> _customers = FUMiniHotelContext.Instance.Customers;

        public IEnumerable<Customer> GetAll() => _customers.Where(c => c.CustomerStatus == 1).ToList();

        public Customer? GetById(int id) => _customers.FirstOrDefault(c => c.CustomerID == id);

        public void Add(Customer c)
        {
            c.CustomerID = _customers.Count > 0 ? _customers.Max(x => x.CustomerID) + 1 : 1;
            _customers.Add(c);
        }

        public void Update(Customer c)
        {
            var existing = GetById(c.CustomerID);
            if (existing is null) throw new Exception("Customer not found");
            existing.CustomerFullName = c.CustomerFullName;
            existing.EmailAddress = c.EmailAddress;
            existing.Telephone = c.Telephone;
            existing.CustomerBirthday = c.CustomerBirthday;
            existing.Password = c.Password;
        }

        public void Delete(int id)
        {
            var existing = GetById(id);
            if (existing is not null) existing.CustomerStatus = 2; // soft delete
        }
    }
}
