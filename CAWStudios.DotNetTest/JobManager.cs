using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAWStudios.DotNetTest
{
    /// <summary>
    /// Job Manager
    /// you are not allowed to change this class nor any of its methods
    /// </summary>
    internal static class JobManager
    {
        public static List<Job> GetJobList()
        {
            const int size = 32;
            var list = new List<Job>(size);
            for (var i = 0; i < size; i++)
            {
                var time = i % 4 == 0 ? 5 : 2;
                list.Add(new Job(time));
            }
            return list;
        }
    }
}
