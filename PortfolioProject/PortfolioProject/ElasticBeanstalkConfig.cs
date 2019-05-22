using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioProject
{
    public static class ElasticBeanstalkConfig
    {

        public static void SetEbConfig()
        {
            // workround for EB not acknowledging IIS Env variables
            // https://stackoverflow.com/questions/40127703/aws-elastic-beanstalk-environment-variables-in-asp-net-core-1-0

            var tempConfigBuilder = new ConfigurationBuilder();

            tempConfigBuilder.AddJsonFile(
                @"C:\Program Files\Amazon\ElasticBeanstalk\config\containerconfiguration",
                optional: true,
                reloadOnChange: true
            );

            var configuration = tempConfigBuilder.Build();

            var envSection = configuration.GetSection("iis:env");
            if (envSection == null)
            {
                return;
            }

            var ebEnv = envSection
                .GetChildren()
                .Where(pair => pair.Value != string.Empty)
                .Select(pair => pair.Value.Split(new[] { '=' }, 2))
                .ToDictionary(keypair => keypair[0], keypair => keypair[1]);

            foreach (var keyVal in ebEnv)
            {
                Environment.SetEnvironmentVariable(keyVal.Key, keyVal.Value);
            }
        }
    }
}
