using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.Azure.WebJobs.Host;


namespace Company.Function
{
    public static class sampleFunction
    {
        [FunctionName("sampleFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "ToDoList",
                containerName: "Items",
                Connection = "CosmosDBConnection")]IAsyncCollector<dynamic> document,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = string.IsNullOrEmpty(req.Query["name"])?"default":req.Query["name"];

            await document.AddAsync(new { Name = name, id = Guid.NewGuid() });

            return new OkObjectResult("success");

        }
    }
}
