using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistributedSharedInterfaces.Jobs;
using ExampleShared;

namespace ExampleServerDll
{
    public class JobGroup : IJobGroup
    {
        // jobs are done from Start (inclusive) to End (exclusive)
        public long StartValue { get; private set;}
        public long EndValue { get; private set;}
        public long GroupId { get; set; }
        public long SupportingDataVersion { get; set; }
        public int JobCount { get { return (int) (EndValue - StartValue); } }

        public byte[] Data
        {
            get
            {
                return BitConverter.GetBytes(StartValue).Concat(
                          BitConverter.GetBytes(EndValue)).ToArray();
            }
            set
            {
                StartValue = BitConverter.ToInt64(value, 0);
                EndValue = BitConverter.ToInt64(value, 8);
            }
        }

        public JobGroup(long startValue, long endValue)
        {
            StartValue = startValue;
            EndValue = endValue;
        }

        public JobGroup()
        {}

        public IEnumerable<IJobData> GetJobs()
        {
            for (var i = StartValue; i < EndValue; ++i)
            {
                yield return new JobData(i);
            }
        }
    }
}
