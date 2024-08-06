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
}
