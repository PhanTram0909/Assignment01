using System.Collections.Generic;
using System.Linq;
using FUMiniHotelSystem.Data.Models;
using FUMiniHotelSystem.Data.Repositories;

namespace FUMiniHotelSystem.Business.Services
{
    public class RoomService
    {
        private readonly RoomRepository _repo = new();

        public IEnumerable<Room> GetAll() => _repo.GetAll();
        public Room? GetById(int id) => _repo.GetById(id);
        public void Create(Room room) => _repo.Add(room);
        public void Update(Room room) => _repo.Update(room);
        public void Delete(int id) => _repo.Delete(id);
        
        public IEnumerable<Room> SearchByNumber(string roomNumber)
        {
            if (string.IsNullOrWhiteSpace(roomNumber)) return GetAll();
            return GetAll().Where(r => r.RoomNumber.Contains(roomNumber, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}