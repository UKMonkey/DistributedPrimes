using System;
using DistributedSharedInterfaces.Jobs;

namespace ExampleShared
{
    public class JobData : IJobData
    {
        public long NumberToProcess { get; private set; }

        public byte[] Data
        {
            get
            {
                return BitConverter.GetBytes(NumberToProcess);
            }
            set
            {
                NumberToProcess = BitConverter.ToInt64(value, 0);
            }
        }

        public JobData(long numberToProcess)
        {
            NumberToProcess = numberToProcess;
        }

        public JobData()
        { }
    }
}
