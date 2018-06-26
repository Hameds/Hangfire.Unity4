using Hangfire;
using Hangfire.Annotations;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangfire.Unity4
{
    public static class GlobalConfigurationExtensions
    {
        public static IGlobalConfiguration<UnityJobActivator> UseUnityActivator(
            [NotNull] this IGlobalConfiguration configuration, IUnityContainer container)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            if (container == null) throw new ArgumentNullException("container");

            return configuration.UseActivator(new UnityJobActivator(container));
        }
    }
}