using Quartz;
using Quartz.Impl;
using System;
using System.Globalization;

namespace WS.Template.Quartz
{
    public static class QuartzJobs
    {
        private static IScheduler sched = new StdSchedulerFactory().GetScheduler();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void SetScheduler(IScheduler scheduler)
        {
            QuartzJobs.sched = scheduler;
        }

        public static void CreateJob<T>(JobDetails<T> jobDetails) where T : class, IJob, new()
        {
            try
            {
                log.Info(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Quartz: Create Job {0}", (object)jobDetails.Name));
                IJobDetail jobDetail = JobBuilder.Create<T>().WithIdentity(jobDetails.Name, string.Format((IFormatProvider)CultureInfo.CurrentCulture, "{0}_Group", (object)jobDetails.Name)).Build();
                ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create().WithIdentity(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "{0}_Trigger", (object)jobDetails.Name), string.Format((IFormatProvider)CultureInfo.CurrentCulture, "{0}_Group", (object)jobDetails.Name)).WithCronSchedule(jobDetails.CronExpression).Build();
                DateTimeOffset dateTimeOffset = QuartzJobs.sched.ScheduleJob(jobDetail, (ITrigger)cronTrigger);
                log.Info(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Quartz: {0} has been scheduled to run at: {1} and repeat based on expression: {2}", (object)jobDetail.Key, (object)dateTimeOffset.ToLocalTime(), (object)cronTrigger.CronExpressionString));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }
        }

        public static void StartJobs()
        {
            log.Info("Quartz: Start Job");
            QuartzJobs.sched.Start();
        }

        public static void StopJobs()
        {
            log.Info("Quartz: Stop Job");
            QuartzJobs.sched.Shutdown(true);
            log.Info(string.Format(CultureInfo.CurrentCulture, "Quartz: Executed {0} Jobs.", (object)QuartzJobs.sched.GetMetaData().NumberOfJobsExecuted));
        }

        public static void PauseJobs()
        {
            log.Info("Quartz: Pause Job");
            QuartzJobs.sched.PauseAll();
        }

        public static void ContinueJobs()
        {
            log.Info("Quartz: Continue Job");
            QuartzJobs.sched.ResumeAll();
        }
    }
}