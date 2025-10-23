using System;
using System.Collections.Generic;
using System.Linq;
using FUMiniHotelSystem.Data.Database;
using FUMiniHotelSystem.Data.Models;

namespace FUMiniHotelSystem.Business.Services
{
    public class ReportService
    {
        private readonly FUMiniHotelContext _context = FUMiniHotelContext.Instance;

        // Trả về tổng tiền từng khách hàng trong khoảng ngày
        public IEnumerable<object> GetRevenueByCustomer(DateTime start, DateTime end)
        {
            var data = _context.Bookings
                .Where(b => b.StartDate >= start && b.EndDate <= end)
                .Join(_context.Customers,
                    b => b.CustomerID,
                    c => c.CustomerID,
                    (b, c) => new { c.CustomerFullName, b.TotalPrice })
                .GroupBy(x => x.CustomerFullName)
                .Select(g => new
                {
                    CustomerName = g.Key,
                    Total = g.Sum(x => x.TotalPrice)
                })
                .ToList();

            return data;
        }
    }
}
