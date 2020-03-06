// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mobsites.Azure.Functions.CosmosHttpTrigger.Extension
{
    public static class HttpRequestExtensions
    {
        public static async Task<HttpResponseMessage> GetResponseAsync(this HttpRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                int parts = Helpers.ParseRoute(
                    request.Path,
                    out string database,
                    out string container,
                    out string partitionKey,
                    out string id,
                    out int maxItemCount);

                return (request.Method.ToLowerInvariant()) switch
                {
                    "get" => await CosmosService.ReadItemStreamAsync(database, container, partitionKey, id),
                    "post" => parts == 4
                        ? await CosmosService.CreateItemStreamAsync(database, container, partitionKey, request.Body)
                        : await CosmosService.GetItemQueryStreamIterator(database, container, partitionKey, request.Body, request.Headers["Continuation-Token"], maxItemCount),
                    "put" => parts == 4
                        ? await CosmosService.UpsertItemStreamAsync(database, container, partitionKey, request.Body)
                        : await CosmosService.ReplaceItemStreamAsync(database, container, partitionKey, id, request.Body),
                    "delete" => await CosmosService.DeleteItemStreamAsync(database, container, partitionKey, id),
                    _ => CosmosService.ErrorResponse("Request method did not match one of the Cosmos API method signatures.")
                };
            }
            catch (Exception ex)
            {
                return CosmosService.ErrorResponse(ex.Message);
            }
        }
    }
}