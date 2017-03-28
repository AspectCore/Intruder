using System;
using AspectCore.Abstractions;
using AspectCore.Extensions.Configuration;

namespace AspectCore.Extensions.Intruder
{
    public static class ConfigurationExtensions
    {
        public static IAspectConfigure UseAspectIntruder(this IAspectConfigure configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            return configure.Use<AspectIntruderAttribute>();
        }
    }
}