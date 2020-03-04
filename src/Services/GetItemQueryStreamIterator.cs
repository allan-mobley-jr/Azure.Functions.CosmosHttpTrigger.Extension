// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Azure.Cosmos;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mobsites.Azure.Functions.CosmosHttpTrigger.Extension
{
    public static partial class CosmosService
    {
        internal static async Task<HttpResponseMessage> GetItemQueryStreamIterator(
            string database,
            string container,
            string partitionKey,
            Stream stream,
            string continuationToken,
            int? maxItemCount)
        {
            // Get QuerySpec object from request body.
            var querySpec = await JsonSerializer.DeserializeAsync<QuerySpec>(stream, Serialization.Options);

            var queryDefinition = new QueryDefinition(querySpec.Query);

            if (querySpec.Parameters.Length > 0)
            {
                foreach (var parameter in querySpec.Parameters)
                {
                    queryDefinition = queryDefinition.WithParameter(parameter.Name, parameter.Value);
                }
            }

            continuationToken = string.IsNullOrWhiteSpace(continuationToken)
                ? null
                : continuationToken;

            var responses = GetContainer(database, container)
                .GetItemQueryStreamIterator(
                    queryDefinition,
                    continuationToken,
                    new QueryRequestOptions
                    {
                        PartitionKey = new PartitionKey(partitionKey),
                        MaxItemCount = maxItemCount
                    });

            await foreach (var response in responses)
            {
                // Return only the first response.
                return response.ConvertHttpResponseMessage();
            }

            // Should never get here, but...
            return ErrorResponse("The query did not generate a response from Azure Cosmos.");
        }
    }
}