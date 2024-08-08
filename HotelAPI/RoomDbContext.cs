using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using HotelAPI.Model;

public class RoomDbContext : DbContext
{
    public virtual DbSet<Room> RoomsTestTable { get; set; }
    public virtual DbSet<Booking> BookingsTestTable { get; set; }

    public RoomDbContext() { }
    public RoomDbContext(DbContextOptions<RoomDbContext> options) { }

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


