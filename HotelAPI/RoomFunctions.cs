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
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;



namespace HotelAPI
{
    public class RoomFunctions
    {
        private readonly RoomService roomService;

        public RoomFunctions(RoomService roomService)
        {
            this.roomService = roomService;
        }

        [FunctionName("Ping")]
        public Task<IActionResult> Ping(
             [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
             ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return Task.FromResult<IActionResult>(new OkObjectResult("Pong"));

        }

        [FunctionName("GetAllRooms")]
        public Task<IActionResult> GetAllRooms(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            var rooms = roomService.GetAllRooms();
            return Task.FromResult<IActionResult>(new OkObjectResult(rooms));

        }

        [FunctionName("GetRoomByID")]
        public async Task<IActionResult> GetRoomByID(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            int id = int.Parse(req.Query["id"]);

            Room room = roomService.GetRoomById(id);

            if(room == null)
            {
                return new NotFoundObjectResult($"Room with id {id} not found");
            }

            return new OkObjectResult(room);


        }

        [FunctionName("DeleteRoomByID")]
        public async Task<IActionResult> DeleteRoomByID(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "delete")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            int id = int.Parse(req.Query["id"]);

            log.LogInformation("Id :" + id);

            var context = new RoomDbContext();
            var room = await context.RoomsTestTable.FindAsync(id);

            // No room found
            if (room == null)
            {
                return new NotFoundResult();
            }

            // Delete room
            context.RoomsTestTable.Remove(room);
            context.SaveChanges();

            return new OkObjectResult("Deleted room with id " + room.Id);

        }

        [FunctionName("AddRoom")]
        public async Task<IActionResult> AddRoom(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            if (string.IsNullOrEmpty(requestBody))
            {
                return new BadRequestObjectResult("Request body is empty. Please provide valid data.");
            }

            dynamic data = JsonConvert.DeserializeObject(requestBody);

            
            // Call service 
            Response response = roomService.AddRoom(data);

            if(response.Status  == 200)
            {
                return new OkObjectResult("Room saved successfully");
            }

            else
            {
                return new BadRequestObjectResult(response.Message);
            }
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
