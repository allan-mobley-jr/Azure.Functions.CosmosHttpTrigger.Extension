// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Net;
using System.Net.Http;

namespace Mobsites.Azure.Functions.CosmosHttpTrigger.Extension
{
    public static partial class Responses
    {
        public static HttpResponseMessage ErrorResponse(string message) => new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest,
            Content = new StringContent(message)
        };
    }
}