using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistributedSharedInterfaces.Jobs;

namespace ExampleShared
{
    public class JobData : IJobData
    {
        public long NumberToProcess { get; private set; }

        public String SupportingDataMd5 { get; set; }
        public String DllName { get; set; }
        public long JobId { get; set; }
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
