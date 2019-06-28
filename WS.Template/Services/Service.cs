using System;
using System.Configuration;
using System.Threading;
using WS.Template.Jobs.Demo;
using WS.Template.Quartz;

namespace WS.Template.Services
{
    /// <summary>
    /// The service.
    /// </summary>
    public class Service : IService
    {
        /// <summary>
        /// The shutdown event.
        /// </summary>
        private readonly ManualResetEvent shutdownEvent = new ManualResetEvent(false);

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The thread.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            log.Info("Start Service.");
            try
            {
                this.thread = new Thread(this.Worker) { IsBackground = true };
                this.thread.Start();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            log.Info("Stop service.");
            try
            {
                QuartzJobs.StopJobs();
                this.shutdownEvent.Set();

                if (!this.thread.Join(3000))
                {
                    this.thread.Abort();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Workers this instance.
        /// </summary>
        private void Worker()
        {
            log.Debug("Enter in Worker... Installing");

            QuartzJobs.CreateJob(new JobDetails<QuartzController>
            {
                Name = "Viva.Template.WS.Job",
                CronExpression = ConfigurationManager.AppSettings["ExecCronExpression"]
            });

            // Add more jobs if is necessary before start.
            QuartzJobs.StartJobs();

            this.shutdownEvent.WaitOne();
        }
    }
}