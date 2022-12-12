using CAWStudios.DotNetTest;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CAWStudios.DotNetTest
{
    /// <summary>
    /// Represent a Job, all our jobs are CPU bound.
    /// you are not allowed to change this class at all nor any of its methods.
    /// </summary>
    internal class Job
    {

        private static volatile int _runningJobs;
        private static volatile int _idCounter;
        private Resource? _resource;
        private bool _isResourceUsed;
        public int TimeSeconds { get; }
        public int Id { get; }

        public Job(int timeSeconds)
        {
            TimeSeconds = timeSeconds;
            Id = _idCounter++;
        }

        /// <summary>
        /// Starting to run the job, the job is a CPU bound job.
        /// </summary>
        /// <returns>returns when the job is done.</returns>
        public async Task Start() 
        {
            Console.WriteLine($" { DateTime.Now}: Job {Id} started.");
            _runningJobs++;
            if (_runningJobs >= 4) throw new Exception("No more than 4 jobs are allowed to run on the same time!!!");
            await RunResourceRelatedJob();
            await Task.Delay(TimeSpan.FromSeconds(TimeSeconds)); // the actual long running CPU bound job.
            _runningJobs--;
            Console.WriteLine($"{ DateTime.Now}: Job {Id} finished after  {TimeSeconds} seconds.");
        }


        /// <summary>
        /// Using the resource for 1 seconds before we can start the actual job.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task RunResourceRelatedJob()
        {

            Console.WriteLine($" { DateTime.Now}: Job {Id} is using the resource.");
            _resource = ResourceManager.GetResource() ?? throw new Exception("Cannot get the resource");
            _resource.RegisterJob(this);
            await Task.Delay(TimeSpan.FromSeconds(1));
            _isResourceUsed = true;
            _resource.UnRegisterJob(this);
            ResourceManager.ReturnResource(_resource);
            Console.WriteLine($" { DateTime.Now}: Job {Id} releasing the resource.");
        }

       
    }
}
