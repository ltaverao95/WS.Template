using WS.Template.Services;

namespace WS.Template.Jobs.Demo.Business
{
    public class ProcessEntry : IRunable
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProcessEntry()
        {
        }

        /// <summary>
        /// Dispose process code.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Entry point method.
        /// </summary>
        public void Run()
        {
            log.Info("[Demo] Begin run.");

            log.Info("[Demo] End run.");
        }
    }
}