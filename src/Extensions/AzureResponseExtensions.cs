// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Azure;
using System.Net;
using System.Net.Http;

namespace Mobsites.Azure.Functions.CosmosHttpTrigger.Extension
{
    public static class AzureResponseExtensions
    {
        public static HttpResponseMessage ConvertHttpResponseMessage(this Response response)
        {
            // Would be really nice if we could simply pass the Cosmos response directly back,
            // but, alas, we must reconstruct a return response.
            var result = new HttpResponseMessage();

            // Pass Cosmos response headers back.
            foreach (var headerName in response.Headers)
            {
                result.Headers.Add(headerName.Name, headerName.Value);
            }

            // Pass Cosmos status code back.
            result.StatusCode = (HttpStatusCode)response.Status;

            if (response.ContentStream != null)
                // Pass stream directly to response object, without deserializing.
                result.Content = new StreamContent(response.ContentStream);

            return result;
        }
    }
}