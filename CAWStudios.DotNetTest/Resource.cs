namespace CAWStudios.DotNetTest
{
/// <summary>
/// Represent an expensive resource which only 1 (singleton) exists.
/// You are not allowed to change this class.
/// </summary>
    internal class Resource
    {
        private Job? _job;
        public static Resource Self { get; } = new();

        private Resource()
        {
        }
        public void RegisterJob(Job job)
        {
            if (_job != null) throw new Exception("The resource is used by some other job");
            _job = job;
        }

        public void UnRegisterJob(Job job)
        {
            if (_job == null || _job != job) throw new Exception("This resource is not registered to this job");
            _job = null;
        }
    }
}