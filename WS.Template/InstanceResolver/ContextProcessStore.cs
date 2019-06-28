using System.Threading;

namespace WS.Template.InstanceResolver
{
    public static class ContextProcessStore
    {
        private const string Context = "Context";

        public static void SaveContext(IContext context)
        {
            Thread.SetData(Thread.GetNamedDataSlot("Context"), (object)context);
        }

        public static IContext GetContext()
        {
            return (IContext)Thread.GetData(Thread.GetNamedDataSlot("Context"));
        }

        public static void DisposeContext()
        {
            ContextProcessStore.GetContext()?.Dispose();
        }
    }
}