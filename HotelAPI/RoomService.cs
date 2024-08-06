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

    public Response GetRoomById(int id)
    {
        List<Room> rooms = GetAllRooms();
        var room = rooms.FirstOrDefault(room => room.ID == id);

        // No room found with id
        if (room == null)
        {
            return new Response
            {
                Message = $"Room with ID {id} not found.",
                Status = 404
            };
            
        }

        return new Response
        {
            Message = $"Room with ID {id} not found.",
            Status = 200
        };

        

    }

    
}
