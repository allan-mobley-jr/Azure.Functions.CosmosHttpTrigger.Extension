// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Text.Json;

namespace Mobsites.Azure.Functions.CosmosHttpTrigger.Extension
{
    internal static class Serialization
    {
        static Serialization()
        {
            Options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public static JsonSerializerOptions Options { get; set; }
    }
}