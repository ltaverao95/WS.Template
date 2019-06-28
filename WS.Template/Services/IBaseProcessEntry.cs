using System;

namespace WS.Template.Services
{
    /// <summary>
    /// Generic IProcessEntry to be used in SynchronizedController.
    /// </summary>
    public interface IRunable : IDisposable
    {
        /// <summary>
        /// The run.
        /// </summary>
        void Run();
    }
}