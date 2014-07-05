using System;
using DistributedClientInterfaces.Interfaces;
using DistributedSharedInterfaces.Jobs;
using System.Collections.Generic;

namespace ExampleClientDll
{
    public class DataWorker : MarshalByRefObject, IDllApi
    {
        private IClientApi _client;
        public Dictionary<string, byte[]> SupportingData { get; set; }

        public void OnDllLoaded(IClientApi client)
        {
            _client = client;
        }


        public byte[] ProcessJob(IJobData job)
        {
            return job.Data;
        }


        public void Dispose()
        {
        }
    }
}
