using System.Collections.Generic;
using System.Linq;
using FUMiniHotelSystem.Data.Models;
using FUMiniHotelSystem.Data.Repositories;

namespace FUMiniHotelSystem.Business.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _repo = new();

        public IEnumerable<Customer> GetAll() => _repo.GetAll();
        public Customer? GetById(int id) => _repo.GetById(id);
        public void Create(Customer c) => _repo.Add(c);
        public void Update(Customer c) => _repo.Update(c);
        public void Delete(int id) => _repo.Delete(id);
        public IEnumerable<Customer> SearchByName(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return GetAll();
            return GetAll().Where(c => c.CustomerFullName.Contains(keyword, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}

