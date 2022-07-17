using Api.Common.Contracts;

namespace Api.Common.WorkerServices
{
    public class PingLookUpWorker : AddressLookUpWorkerBase<PingLookUpResult>
    {
        public PingLookUpWorker(string url) : base(url)
        {
        }
    }
}
