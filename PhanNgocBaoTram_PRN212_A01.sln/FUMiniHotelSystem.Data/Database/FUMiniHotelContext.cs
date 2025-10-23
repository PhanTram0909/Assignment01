// FUMiniHotelSystem.Data/Database/FUMiniHotelContext.cs
using System;
using System.Collections.Generic;
using FUMiniHotelSystem.Data.Models;

namespace FUMiniHotelSystem.Data.Database
{
    public class FUMiniHotelContext
    {
        private static FUMiniHotelContext _instance;
        public static FUMiniHotelContext Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FUMiniHotelContext();
                return _instance;
            }
        }

        public List<Customer> Customers { get; set; } = new();
        public List<Room> Rooms { get; set; } = new();
        public List<RoomType> RoomTypes { get; set; } = new();
        public List<Booking> Bookings { get; set; } = new();

        private FUMiniHotelContext()
        {
            // Sample data
            Customers = new List<Customer>
            {
                new() { CustomerID = 1, CustomerFullName="Nguyen A", EmailAddress="a@gmail.com", Telephone="0901", Password="123", CustomerBirthday=new DateTime(2000,1,1), CustomerStatus=1 }
            };

            RoomTypes = new List<RoomType>
            {
                new() { RoomTypeID = 1, RoomTypeName = "Single", TypeDescription = "Single bed room", TypeNote = "For 1 person" },
                new() { RoomTypeID = 2, RoomTypeName = "Double", TypeDescription = "Double bed room", TypeNote = "For 2 persons" },
                new() { RoomTypeID = 3, RoomTypeName = "Suite", TypeDescription = "Luxury suite", TypeNote = "For VIP guests" }
            };

            Rooms = new List<Room>
            {
                new() { RoomID=1, RoomNumber="101", RoomDescription="Single room with city view", RoomMaxCapacity=1, RoomStatus=1, RoomPricePerDate=200000, RoomTypeID=1 },
                new() { RoomID=2, RoomNumber="102", RoomDescription="Double room with balcony", RoomMaxCapacity=2, RoomStatus=1, RoomPricePerDate=350000, RoomTypeID=2 },
                new() { RoomID=3, RoomNumber="201", RoomDescription="Luxury suite", RoomMaxCapacity=4, RoomStatus=1, RoomPricePerDate=500000, RoomTypeID=3 }
            };

            Bookings = new List<Booking>();
        }
    }
}

