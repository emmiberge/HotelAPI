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
        public int RoomId { get; set;}
        public int CustomerId { get; set;}
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public Room Room { get; set; } = null!;

        public Booking()
        {
        }

        public Booking(int id, int roomId, int customerId, DateTime checkInDate, DateTime checkOutDate)
        {
            Id = id;
            RoomId = roomId;
            CustomerId = customerId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }

    


    }
}
