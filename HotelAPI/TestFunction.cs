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



namespace HotelAPI
{
    public static class TestFunction
    {
        [FunctionName("TestFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // default room
            Room TestRoom = new Room(1, 2, 300.00);
            return new OkObjectResult(TestRoom);
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
