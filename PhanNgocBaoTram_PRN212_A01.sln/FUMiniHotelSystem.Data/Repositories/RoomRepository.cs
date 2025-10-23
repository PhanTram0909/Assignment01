using System;
using System.Collections.Generic;
using System.Linq;
using FUMiniHotelSystem.Data.Database;
using FUMiniHotelSystem.Data.Models;

namespace FUMiniHotelSystem.Data.Repositories
{
    public class RoomRepository
    {
        private readonly List<Room> _rooms = FUMiniHotelContext.Instance.Rooms;

        public IEnumerable<Room> GetAll() => _rooms.Where(r => r.RoomStatus == 1).ToList();

        public Room? GetById(int id) => _rooms.FirstOrDefault(r => r.RoomID == id);

        public void Add(Room r)
        {
            r.RoomID = _rooms.Count > 0 ? _rooms.Max(x => x.RoomID) + 1 : 1;
            _rooms.Add(r);
        }

        public void Update(Room r)
        {
            var existing = GetById(r.RoomID);
            if (existing is null) throw new Exception("Room not found");
            existing.RoomNumber = r.RoomNumber;
            existing.RoomDescription = r.RoomDescription;
            existing.RoomMaxCapacity = r.RoomMaxCapacity;
            existing.RoomPricePerDate = r.RoomPricePerDate;
            existing.RoomTypeID = r.RoomTypeID;
        }

        public void Delete(int id)
        {
            var existing = GetById(id);
            if (existing is not null) existing.RoomStatus = 2;
        }
    }
}

