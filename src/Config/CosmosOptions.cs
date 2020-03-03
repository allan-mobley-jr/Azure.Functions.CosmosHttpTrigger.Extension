// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Azure.Cosmos;
using Microsoft.Azure.WebJobs.Hosting;

namespace Mobsites.Azure.Functions.CosmosHttpTrigger.Extension
{
    public class CosmosOptions : IOptionsFormatter
    {
        /// <summary>
        /// Gets or sets the Cosmos connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the connection mode used in the CosmosClient instances.
        /// </summary>
        public ConnectionMode? ConnectionMode { get; set; }

        /// <summary>
        /// Gets or sets the user-agent suffix to include with every Azure Cosmos service interaction.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the preferred geo-replicated region to be used for Azure Cosmos service interaction.
        /// </summary>
        public string ApplicationRegion { get; set; }

        public string Format() => string.Empty;
    }
}