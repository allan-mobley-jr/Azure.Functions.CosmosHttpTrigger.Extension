// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Azure.Cosmos;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mobsites.Azure.Functions.CosmosHttpTrigger.Extension
{
    public static partial class CosmosService
    {
        internal static async Task<HttpResponseMessage> UpsertItemStreamAsync(
            string database,
            string container,
            string partitionKey,
            Stream stream) => (await GetContainer(database, container)
                .UpsertItemStreamAsync(stream, new PartitionKey(partitionKey)))
                .ConvertHttpResponseMessage();
    }
}