using System;
using System.Collections.Generic;
using System.Linq;
using FUMiniHotelSystem.Data.Models;
using FUMiniHotelSystem.Data.Repositories;

namespace FUMiniHotelSystem.Business.Services
{
    public class BookingService
    {
        private readonly BookingRepository _repo = new();

        public IEnumerable<Booking> GetAll() => _repo.GetAll();
        public Booking? GetById(int id) => _repo.GetById(id);
        public void Create(Booking booking) => _repo.Add(booking);
        
        public IEnumerable<Booking> GetByCustomer(int customerId) => _repo.GetByCustomer(customerId);
        public IEnumerable<Booking> GetByPeriod(DateTime startDate, DateTime endDate) => _repo.GetByPeriod(startDate, endDate);
        
        public decimal CalculateTotalPrice(int roomId, DateTime startDate, DateTime endDate)
        {
            var roomRepo = new RoomRepository();
            var room = roomRepo.GetById(roomId);
            if (room == null) return 0;
            
            var days = (endDate - startDate).Days;
            return room.RoomPricePerDate * days;
        }
    }
}