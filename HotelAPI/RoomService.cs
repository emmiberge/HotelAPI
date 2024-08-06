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
        return rooms.FirstOrDefault(room => room.ID == id);
    }

    
}
