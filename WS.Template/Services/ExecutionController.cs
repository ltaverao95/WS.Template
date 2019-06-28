using System.Collections.Generic;

namespace WS.Template.Services
{
    public static class ExecutionController
    {
        /// <summary>
        /// Initializes static members of the <see cref="ExecutionController"/> class.
        /// </summary>
        static ExecutionController()
        {
            JobsRunning = new List<string>();
        }

        /// <summary>
        /// Gets or sets the jobs name that are currently running in the process.
        /// </summary>
        /// <value>
        /// The jobs running.
        /// </value>
        public static List<string> JobsRunning { get; set; }
    }
}