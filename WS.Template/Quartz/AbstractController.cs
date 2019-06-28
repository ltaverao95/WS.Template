using Quartz;
using System;
using System.Globalization;

namespace WS.Template.Quartz
{
    public abstract class AbstractController : IJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Execute(IJobExecutionContext context)
        {
            log.Info(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Quartz: {0} executing at {1}", (object)context.JobDetail.Key, (object)DateTime.Now.ToString("r", (IFormatProvider)CultureInfo.InvariantCulture)));
            this.ExecuteBusinessJob();
        }

        public abstract void ExecuteBusinessJob();
    }
}