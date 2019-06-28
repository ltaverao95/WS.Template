namespace WS.Template
{
    using log4net.Config;
    using System;
    using System.Configuration;
    using Topshelf;
    using WS.Template.InstanceResolver;
    using WS.Template.Quartz;
    using WS.Template.Services;

    public class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main(string[] args)
        {
            try
            {
                XmlConfigurator.Configure();

                var ioc = IocHelper.Instance;
                ioc.Install(new Windsor.Installer());

                if (args.Length > 0 && args[0] == "now")
                {
                    try
                    {
                        AbstractController controller = new Jobs.Demo.QuartzController();
                        controller.ExecuteBusinessJob();
                        ioc.Dispose();
                    }
                    catch (Exception ex)
                    {
                        log.Error("An error has ocurred trying to execute job", ex);
                    }
                }
                else
                {
                    HostFactory.Run(hostConfigurator =>
                    {
                        hostConfigurator.SetServiceName(ConfigurationManager.AppSettings["ServiceName"]);
                        hostConfigurator.SetDisplayName(ConfigurationManager.AppSettings["ServiceDisplayName"]);
                        hostConfigurator.SetDescription(ConfigurationManager.AppSettings["ServiceDescription"]);

                        RunAsExtensions.RunAsLocalSystem(hostConfigurator);
                        StartModeExtensions.StartAutomatically(hostConfigurator);

                        hostConfigurator.Service<Service>(serviceConfigurator =>
                        {
                            serviceConfigurator.ConstructUsing(() => new Service());

                            serviceConfigurator.WhenStarted(service => service.Start());
                            serviceConfigurator.WhenStopped(service => service.Stop());
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                log.Error("An error has ocurred trying to execute job", ex);
            }
        }
    }
}