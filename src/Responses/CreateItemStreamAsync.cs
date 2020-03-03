// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Azure.Cosmos;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mobsites.Azure.Functions.CosmosHttpTrigger.Extension
{
    public static partial class Responses
    {
        public static async Task<HttpResponseMessage> CreateItemStreamAsync(string database, string container, string partitionKey, Stream stream)
        {
            return (await GetContainer(database, container)
                .CreateItemStreamAsync(stream, new PartitionKey(partitionKey)))
                .ConvertHttpResponseMessage();
        }
    }
}