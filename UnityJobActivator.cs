﻿using Hangfire;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hangfire.Unity4
{
    public class UnityJobActivator : JobActivator
    {
        private readonly IUnityContainer container;

        /// <summary>
        /// Initialize a new instance of the <see cref="T:UnityJobActivator"/> class
        /// </summary>
        /// <param name="container">The unity container to be used</param>
        public UnityJobActivator(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this.container = container;
        }

        /// <inheritdoc />
        public override object ActivateJob(Type jobType)
        {
            return this.container.Resolve(jobType);
        }
        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            return new UnityScope(container.CreateChildContainer());
        }
        class UnityScope : JobActivatorScope
        {
            private readonly IUnityContainer container;

            public UnityScope(IUnityContainer container)
            {
                this.container = container;
            }

            public override object Resolve(Type type)
            {
                return container.Resolve(type);
            }

            public override void DisposeScope()
            {
                container.Dispose();
            }
        }
    }
}