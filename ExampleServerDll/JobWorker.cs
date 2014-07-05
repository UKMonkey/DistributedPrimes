using System;
using DistributedServerInterfaces.Interfaces;
using DistributedSharedInterfaces.Jobs;
using System.Collections.Generic;

namespace ExampleServerDll
{
    public class JobWorker : MarshalByRefObject, IDllApi
    {
        public event DataChangedCallback StatusDataChanged;
        public event DataChangedCallback SupportingDataChanged;

        private IServerApi _server;
        private long _count = 0;


        private const String CountKey = "CurrentCount";


        public void OnDllLoaded(IServerApi server, Dictionary<String, byte[]> supportingData, Dictionary<String, byte[]> status)
        {
            _server = server;
            if (status.ContainsKey(CountKey))
                _count = BitConverter.ToInt64(status[CountKey], 0);
        }


        public IJobGroup GetNextJobGroup(int jobCount)
        {
            IJobGroup ret = new JobGroup(_count, _count + jobCount);
            _count += jobCount;

            StatusDataChanged(CountKey, BitConverter.GetBytes(_count));

            return ret;
        }


        public void DataProvided(IJobResultData request)
        {
            var result = BitConverter.ToInt64(request.Data, 0);
            Console.WriteLine("Got result for job: {0}", result);
        }


        public IJobGroup GetCleanJobGroup()
        {
            return new JobGroup();
        }


        public void Dispose()
        {
        }
    }
}
