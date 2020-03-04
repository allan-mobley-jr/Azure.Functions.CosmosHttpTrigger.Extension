// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Azure.Cosmos;

namespace Mobsites.Azure.Functions.CosmosHttpTrigger.Extension
{
    public static partial class CosmosService
    {
        internal static CosmosClient Client { get; set; }

        public static CosmosContainer GetContainer(string database, string container) => Client.GetContainer(database, container);

        public static CosmosClient GetClient() => Client;
    }
}