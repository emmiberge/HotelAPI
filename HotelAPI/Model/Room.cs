using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Model
{
    public class Room
    {
        public int Id { get; set; }
        public int AmountOfBeds { get; set; }
        public decimal PricePerNight { get; set; }

        public Room()
        {
        }

        public Room(int id, int amountOfBeds, decimal pricePerNight)
        {
            Id = id;
            AmountOfBeds = amountOfBeds;
            PricePerNight = pricePerNight;
        }
    }
}
