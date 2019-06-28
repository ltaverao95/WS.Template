namespace WS.Template.Quartz
{
    public class JobDetails<T>
    {
        public string Name { get; set; }

        public T Controller { get; set; }

        public string CronExpression { get; set; }
    }
}