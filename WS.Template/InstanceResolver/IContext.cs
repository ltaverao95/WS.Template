using System;
using System.Collections.Generic;

namespace WS.Template.InstanceResolver
{
    public interface IContext : IDisposable
    {
        event EventHandler Ended;

        IDictionary<string, object> Components { get; }

        void Init();
    }
}