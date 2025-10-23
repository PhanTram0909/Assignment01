using System;
using System.Collections.Generic;
using System.Linq;
using FUMiniHotelSystem.Data.Database;
using FUMiniHotelSystem.Data.Models;

namespace FUMiniHotelSystem.Data.Repositories
{
    public class BookingRepository
    {
        private readonly List<Booking> _bookings = FUMiniHotelContext.Instance.Bookings;

        public IEnumerable<Booking> GetAll() => _bookings.ToList();

        public Booking? GetById(int id) => _bookings.FirstOrDefault(b => b.BookingID == id);

        public void Add(Booking b)
        {
            b.BookingID = _bookings.Count > 0 ? _bookings.Max(x => x.BookingID) + 1 : 1;
            _bookings.Add(b);
        }

        public IEnumerable<Booking> GetByCustomer(int customerId) =>
            _bookings.Where(b => b.CustomerID == customerId).ToList();

        public IEnumerable<Booking> GetByPeriod(DateTime start, DateTime end) =>
            _bookings.Where(b => b.StartDate >= start && b.EndDate <= end).ToList();
    }
}


