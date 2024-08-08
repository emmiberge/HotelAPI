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
using HotelAPI.Model;



namespace HotelAPI.BookingAPI
{
    public class BookingFunctions
    {
        private readonly RoomService roomService;

        public BookingFunctions(RoomService roomService)
        {
            this.roomService = roomService;
        }

        
        /*
        [FunctionName("AddBooking")]
        public async Task<IActionResult> AddBooking(
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
            Response response = roomService.AddBooking(data);

            if (response.Status == 200)
            {
                return new OkObjectResult("Booking saved successfully");
            }

            else
            {
                return new BadRequestObjectResult(response.Message);
            }
        }*/

        [FunctionName("GetBookingById")]
        public async Task<IActionResult> GetBookingById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            int id = int.Parse(req.Query["id"]);

            Booking booking = roomService.GetBooking(id);

            if (booking == null)
            {
                return new NotFoundObjectResult($"Booking with id {id} not found");
            }

            Room room = roomService.GetRoomById(booking.RoomId);

            Console.WriteLine($"Called for room for booking {booking.Id} which has room id {booking.RoomId}");

            booking.Room = room;

            return new OkObjectResult(booking);


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
