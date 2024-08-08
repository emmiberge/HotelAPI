using HotelAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

public class RoomService
{
    private readonly RoomDbContext dbContext;

    public RoomService(RoomDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<Room> GetAllRooms()
    {
        return dbContext.RoomsTestTable.ToList();
        
    }

    // Might use Optional type instead of return null in the future
    public Room GetRoomById(int id)
    {
        List<Room> rooms = GetAllRooms();
        return rooms.FirstOrDefault(room => room.Id == id);
    }

    public Response AddRoom(dynamic data)
    {
        Room room = new Room
        {
            Id = data.Id,
            AmountOfBeds = data.AmountOfBeds,
            PricePerNight = data.PricePerNight
        };

        dbContext.RoomsTestTable.Add(room);

        try
        {
            dbContext.SaveChanges();
            var response = new Response
            {
                Message = "Room added successfully",
                Status = 200
            };
            return response;
        }

        catch (Exception error)
        {
            var response = new Response
            {
                Message = $"Something went wrong while trying to add room with id {room.Id}: {error.Message}",
                Status = 500
            };
            return response;
        }


    }

    public Response AddBooking(dynamic data)
    {


        Booking booking = new Booking
        {
            Id = data.Id,
            RoomId = data.RoomId,
            CustomerId = data.CustomerId,
            CheckInDate = data.CheckInDate,
            CheckOutDate = data.CheckOutDate,
        };

        dbContext.BookingsTestTable.Add(booking);

        try
        {
            dbContext.SaveChanges();
            var response = new Response
            {
                Message = "Booking added successfully",
                Status = 200
            };
            return response;
        }

        catch (Exception error)
        {
            var response = new Response
            {
                Message = $"Something went wrong while trying to add booking with id {booking.Id}: {error.Message}",
                Status = 500
            };
            return response;
        }
        


    }

    public List<Booking> GetAllBookings  () {
        return dbContext.BookingsTestTable.ToList();
    }

    public Booking GetBooking (int id)
    {
        List<Booking> bookings = GetAllBookings();
        return bookings.FirstOrDefault(room => room.Id == id);
    }


}
