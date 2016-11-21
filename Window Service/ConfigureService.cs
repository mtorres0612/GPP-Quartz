/******************************************************
 * 
 *  Author          : Marvin D. Onofre
 *  Date Created    : 08/08/2016 
 *
 *  Description     : Implements and configures Topshelf, Autofac and Quartz.Net framework.
 *  
 * 
 * 
 * 
 */


using Topshelf;
using Autofac;
//using Topshelf.Quartz;
//using Quartz;


namespace GPPConsoleApplication
{
    internal static class ConfigureService
    {
        internal static void Configure()
        {

            var builder = new Autofac.ContainerBuilder();
            builder.RegisterType<MainService>();
            var container = builder.Build();

            // Events
            // WhenContinued
            // WhenPaused
            // WhenSessionChanged
            // WhenShutdown
            // WhenStarted
            // WhenStopped

            HostFactory.Run(configure =>
            {
                configure.Service<MainService>(service =>
                {

                    //service.ConstructUsing(() => new MainService());   
                    service.ConstructUsing(() => container.Resolve<MainService>());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                    //                    service.WhenContinued(s => s.Continued());
                    //                    service.WhenShutdown(s => s.ShutDown());
                    //                    service.WhenPaused(s => s.Paused());                    
                });




                //Setup Account that window service use to run.  
                configure.RunAsLocalSystem();
                configure.SetServiceName("MyWindowServiceWithTopshelf");
                configure.SetDisplayName("MyWindowServiceWithTopshelf");
                configure.SetDescription("My .Net windows service with Topshelf");
            });
        }
    }


}
