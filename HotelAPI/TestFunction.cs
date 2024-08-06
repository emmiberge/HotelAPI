using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;



namespace HotelAPI
{
    public static class TestFunction
    {
        [FunctionName("GetAllRooms")]
        public static async Task<IActionResult> GetAllRooms(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var db = new RoomContext();
            var rooms = await db.RoomsTestTable.ToListAsync();
            return new OkObjectResult(rooms);
            

        }

        [FunctionName("GetRoomByID")]
        public static async Task<IActionResult> GetRoomByID(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            int id = int.Parse(req.Query["id"]);

            log.LogInformation("Id :" + id);

            var db = new RoomContext();
            var room = await db.RoomsTestTable.FindAsync(id);

            // Could not find room with id
            if(room == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(room);


        }

        [FunctionName("DeleteRoomByID")]
        public static async Task<IActionResult> DeleteRoomByID(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "delete")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            int id = int.Parse(req.Query["id"]);

            log.LogInformation("Id :" + id);

            var context = new RoomContext();
            var room = await context.RoomsTestTable.FindAsync(id);

            // No room found
            if (room == null)
            {
                return new NotFoundResult();
            }

            // Delete room
            context.RoomsTestTable.Remove(room);
            context.SaveChanges();

            return new OkObjectResult("Deleted room with id " + room.ID);

        }
    }


    // Return a default value for now
    public static class OutputType
    {
        [SqlOutput("dbo.RoomsTestTable", connectionStringSetting: "SqlConnectionString")]
        public static Room ExampleRoom { get; set; }
        public static HttpResponseData HttpResponse { get; set; }
    }
}
