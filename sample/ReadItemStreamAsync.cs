using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net.Http;
using System.Threading.Tasks;


namespace Mobsites.Azure.Functions.CosmosHttpTrigger.Extension.Sample
{
    public static class ReadItemStreamAsync
    {
        [FunctionName("ReadItemStreamAsync")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cosmos/{database}/{container}/{partitionKey}/{id}")] HttpRequest req) =>
                await req.GetResponseAsync();
    }
}