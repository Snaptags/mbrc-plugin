﻿namespace MusicBeeRemoteCore
{
    using System.Diagnostics;

    using MusicBeePlugin.Rest.ServiceInterface;

    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Bootstrappers.Ninject;
    using Nancy.Diagnostics;

    using Ninject;

    class Bootstrapper : NinjectNancyBootstrapper
    {
        private IKernel container;

        public Bootstrapper(IKernel existingContainer)
        {
            this.container = existingContainer;
            this.container.Load<FactoryModule>();
        }

        protected override IKernel GetApplicationContainer()
        {
            return this.container;
        }

        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
#if DEBUG
            StaticConfiguration.EnableRequestTracing = true;
#endif
            base.ApplicationStartup(container, pipelines);
            
        }
       
 
        protected override DiagnosticsConfiguration DiagnosticsConfiguration => new DiagnosticsConfiguration
                                                                                    {
                                                                                        Password = "12345"
                                                                                    };
    }
}