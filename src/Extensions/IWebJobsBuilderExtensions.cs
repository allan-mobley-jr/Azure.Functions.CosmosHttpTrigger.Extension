// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mobsites.Azure.Functions.CosmosHttpTrigger.Extension
{
    /// <summary>
    /// Extension methods for Http integration
    /// </summary>
    public static class IWebJobsBuilderExtensions
    {
        /// <summary>
        /// Adds the HTTP services and extension to the provided <see cref="IWebJobsBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IWebJobsBuilder"/> to configure.</param>
        public static IWebJobsBuilder AddCosmos(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddExtension<CosmosExtensionConfigProvider>() 
                .ConfigureOptions<CosmosOptions>((config, path, options) =>
                {
                    options.ConnectionString = 
                        config[Constants.DefaultConnectionStringName] ?? config.GetConnectionString(Constants.DefaultConnectionStringName);
                    IConfigurationSection section = config.GetSection(path);
                    section.Bind(options);
                });

            return builder;
        }
    }
}