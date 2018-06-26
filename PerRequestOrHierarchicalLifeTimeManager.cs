using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangfire.Unity4
{
    /// <summary>
    /// https://stackoverflow.com/a/33198612/136961
    /// </summary>
    public class PerRequestOrHierarchicalLifeTimeManager : LifetimeManager
    {
        private readonly PerRequestLifetimeManager _perRequestLifetimeManager = new PerRequestLifetimeManager();
        private readonly HierarchicalLifetimeManager _hierarchicalLifetimeManager = new HierarchicalLifetimeManager();

        private LifetimeManager GetAppropriateLifetimeManager()
        {
            if (System.Web.HttpContext.Current == null)
                return _hierarchicalLifetimeManager;

            return _perRequestLifetimeManager;
        }

        public override object GetValue()
        {
            return GetAppropriateLifetimeManager().GetValue();
        }

        public override void SetValue(object newValue)
        {
            GetAppropriateLifetimeManager().SetValue(newValue);
        }

        public override void RemoveValue()
        {
            GetAppropriateLifetimeManager().RemoveValue();
        }
    }
}