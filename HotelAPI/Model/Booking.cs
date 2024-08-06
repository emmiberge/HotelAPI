using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public Room Room { get; set;}
        public int CustomerId { get; set;}
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public Booking()
        {
        }

        public Booking(int id, Room room, int customerId, DateTime checkInDate, DateTime checkOutDate)
        {
            Id = id;
            Room = room;
            CustomerId = customerId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }

        public decimal calcCost()
        {
            return (decimal) (CheckOutDate - CheckInDate).TotalDays * Room.PricePerNight;
        }


    }
}
