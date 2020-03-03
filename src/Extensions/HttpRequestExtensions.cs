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
                int parts = ParseRoute(
                    request.Path,
                    out string database,
                    out string container,
                    out string partitionKey,
                    out string id,
                    out int maxItemCount);

                return (request.Method.ToLowerInvariant()) switch
                {
                    "get" => await Responses.CreateItemStreamAsync(database, container, partitionKey, request.Body),
                    "post" => parts == 5
                        ? await Responses.CreateItemStreamAsync(database, container, partitionKey, request.Body)
                        : await Responses.GetItemQueryStreamIterator(database, container, partitionKey, request.Body, request.Headers["Continuation-Token"], maxItemCount),
                    "put" => parts == 5
                        ? await Responses.CreateItemStreamAsync(database, container, partitionKey, request.Body)
                        : await Responses.CreateItemStreamAsync(database, container, partitionKey, request.Body),
                    "delete" => await Responses.DeleteItemStreamAsync(database, container, partitionKey, id),
                    _ => Responses.ErrorResponse("Request method did not match one of the Cosmos API method signatures.")
                };
            }
            catch (Exception ex)
            {
                return Responses.ErrorResponse(ex.Message);
            }
            
        }

        private static int ParseRoute(
            string route,
            out string database,
            out string container,
            out string partitionKey,
            out string id,
            out int maxItemCount)
        {
            try
            {
                var dirs = route?.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if ((dirs?.Length ?? 0) < 5)
                    throw new Exception($"'{route}' must at the very least be a valid Cosmos API route beginning with the form '{Constants.CosmosAPIRouteForm}'.");

                if ($"{dirs[0].ToLowerInvariant()}/{dirs[1].ToLowerInvariant()}" != Constants.CosmosAPIRoutePrefix)
                    throw new Exception($"'{route}' must begin with '{Constants.CosmosAPIRoutePrefix}'.");

                database = dirs[2];
                container = dirs[3];
                partitionKey = dirs[4];
                id = dirs.Length == 6 ? dirs[5] : null;
                maxItemCount = -1;

                if (dirs.Length == 7)
                {
                    if (!int.TryParse(dirs[6], out maxItemCount))
                    {
                        maxItemCount = -1;
                    }
                }

                return dirs.Length;
            }
            catch (Exception ex)
            {
                // Rethrow it.
                throw ex;
            }
        }
    }
}