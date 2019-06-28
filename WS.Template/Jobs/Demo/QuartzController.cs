using WS.Template.Quartz;
using WS.Template.Services;

namespace WS.Template.Jobs.Demo
{
    public class QuartzController : AbstractController
    {
        /// <summary>
        /// Executes the specified context.
        /// </summary>
        public override void ExecuteBusinessJob()
        {
            new SynchronizedController<IRunable>().RunJob("Demo");
        }
    }
}