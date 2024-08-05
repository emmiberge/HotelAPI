using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

public class RoomContext : DbContext
{
    public virtual DbSet<Room> RoomsTestTable { get; set; }

    public RoomContext() { }
    public RoomContext(DbContextOptions<RoomContext> options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("SqlConnectionString environment variable is not set.");
        }

        optionsBuilder.UseSqlServer(connectionString);
    }


}

public class Room
{
	public int ID { get; set; }
	public int AmountOfBeds { get; set; }
    public decimal PricePerNight { get; set; }

    public Room(int iD, int amountOfBeds, decimal pricePerNight)
    {
        ID = iD;
        AmountOfBeds = amountOfBeds;
        PricePerNight = pricePerNight;
    }
}
