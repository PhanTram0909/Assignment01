using Microsoft.Extensions.Configuration;
using System.Linq;
using FUMiniHotelSystem.Data.Repositories;

namespace FUMiniHotelSystem.Business.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        private readonly CustomerRepository _customerRepo = new();

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        // Trả về "Admin" hoặc "Customer" hoặc null
        public string? Authenticate(string email, string password)
        {
            var adminEmail = _config["DefaultAdmin:Email"];
            var adminPass = _config["DefaultAdmin:Password"];
            if (!string.IsNullOrEmpty(adminEmail) && email == adminEmail && password == adminPass) return "Admin";

            var c = _customerRepo.GetAll().FirstOrDefault(x => x.EmailAddress == email && x.Password == password);
            return c != null ? "Customer" : null;
        }
    }
}

