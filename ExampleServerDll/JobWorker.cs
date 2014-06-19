using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistributedServerInterfaces.Interfaces;
using DistributedSharedInterfaces.Jobs;

namespace ExampleServerDll
{
    public class JobWorker : MarshalByRefObject, IDllApi
    {
        public event DllApiCallback SupportingDataChanged;

        private IServerApi _server;

        private byte[] _supportingData = new byte[0];
        public byte[] SupportingData { get { return _supportingData; } }

        private long _count = 0;


        private class JobData : IJobData
        {
            public string DllName { get; set; }
            public long JobId { get; set; }
            public byte[] Data { get; set; } 
            public string SupportingDataMd5 { get; set; }
        }


        public void OnDllLoaded(IServerApi server)
        {
            _server = server;
        }


        public IJobGroup GetNextJobGroup(int jobCount)
        {
            IJobGroup ret = new JobGroup(_count, _count + jobCount);
            _count += jobCount;
            return ret;
        }


        public void DataProvided(IJobResultData request)
        {
            var result = BitConverter.ToInt64(request.Data, 0);
            Console.WriteLine("Got result for job {0}: {1}", request.JobId, result);
        }

        public void Dispose()
        {
        }
    }
}
