using Castle.MicroKernel.Registration;
using WS.Template.Jobs.Demo.Business;
using WS.Template.Services;

namespace WS.Template.Windsor
{
    public class Installer : IWindsorInstaller
    {
        /// <summary>Registers all the Components to the container</summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The store.</param>
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component.For<IRunable>().ImplementedBy<ProcessEntry>().Named("Demo").LifestylePerThread()
            );
        }
    }
}
