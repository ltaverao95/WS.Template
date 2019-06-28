using System;
using System.Linq;
using WS.Template.InstanceResolver;

namespace WS.Template.Services
{
    public class SynchronizedController<T> where T : IRunable
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The run job.
        /// </summary>
        /// <param name="compName">
        /// The comp name.
        /// </param>
        public virtual void RunJob(string compName)
        {
            if (!ExecutionController.JobsRunning.Contains(compName))
            {
                try
                {
                    ExecutionController.JobsRunning.Add(compName);
                    using (var process = (IRunable)IocHelper.Instance.Resolve(compName, typeof(T)))
                    {
                        process.Run();
                    }

                    ContextProcessStore.DisposeContext();
                    ExecutionController.JobsRunning = ExecutionController.JobsRunning.Where(job => job != compName).ToList();
                }
                catch (Exception e)
                {
                    log.Error(string.Format("Error in Run Job ({0}):", compName), e);
                    ExecutionController.JobsRunning = ExecutionController.JobsRunning.Where(job => job != compName).ToList();
                    ContextProcessStore.DisposeContext();
                }
            }
            else
            {
                log.Warn(string.Format("Another execution of the same job ({0}) is being executed", compName));
            }
        }
    }
}