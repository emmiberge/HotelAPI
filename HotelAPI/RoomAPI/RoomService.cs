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

 
    public Response MakeReservation(dynamic data)
    {
        int id = data.Id;
        int roomId = data.RoomId;
        int customerId = data.CustomerId;
        DateTime checkInDate = data.CheckInDate;
        DateTime checkOutDate = data.CheckOutDate;

        Console.WriteLine($"ID: {id}");
        Console.WriteLine($"RoomID: {roomId}");
        Console.WriteLine($"Check in: {checkInDate}");
        Console.WriteLine($"Check out: {checkOutDate}");

        Booking booking = new Booking
        {
            Id = id,
            RoomId = roomId,
            CustomerId = customerId,
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate,
        };

        // Check if Id already exists
        if(GetAllBookings().Select(b => b.Id).Contains(booking.Id))
        {
            return new Response
            {
                Message = "There is already a reservation with the same id",
                Status = 404
            };
        }

        // Check if room is available between the selected dates
        if(ValidateReservation(roomId, checkInDate, checkOutDate))
        {
            dbContext.BookingsTestTable.Add(booking);
            dbContext.SaveChanges();
            return new Response
            {
                Message = "Reservation confirmed",
                Status = 200
            };
        }

        return new Response
        {
            Message = "Room is unavailable between the selected dates.",
            Status = 404
        };


    }

    public bool ValidateReservation(int roomId, DateTime startDate, DateTime endDate)
    {
        List<int> availableRooms = GetAvailableRooms(startDate, endDate);
        return availableRooms.Contains(roomId);
    }

    public List<Booking> GetAllBookings  () {
        return dbContext.BookingsTestTable.ToList();
    }

    public Booking GetBooking (int id)
    {
        List<Booking> bookings = GetAllBookings();
        return bookings.FirstOrDefault(room => room.Id == id);
    }

    // Note: This only returns the list of available rooms by their id
    internal List<int> GetAvailableRooms(DateTime startDate, DateTime endDate)
    { 
        List<int> unAvailableRooms = dbContext.BookingsTestTable.Where(b => b.CheckInDate < endDate && b.CheckOutDate > startDate).Select(b => b.RoomId).ToList();
        List<int> allRooms = dbContext.RoomsTestTable.Select(r => r.Id).ToList();
        return allRooms.Where(r => !unAvailableRooms.Contains(r)).ToList();
    }
}
