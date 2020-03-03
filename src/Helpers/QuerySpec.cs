// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Mobsites.Azure.Functions.CosmosHttpTrigger.Extension
{
    internal class QuerySpec
    {
        public string Query { get; set; }
        public QueryParam[] Parameters { get; set; } = new QueryParam[]{};
    }

    internal class QueryParam
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}