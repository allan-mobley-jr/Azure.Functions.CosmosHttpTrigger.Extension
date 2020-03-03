// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Azure.Cosmos;

namespace Mobsites.Azure.Functions.CosmosHttpTrigger.Extension
{
    public static partial class Responses
    {
        public static CosmosClient CosmosClient { get; set; }
        
        public static CosmosContainer GetContainer(string database, string container)
        {
            return CosmosClient.GetContainer(database, container);
        }
    }
}