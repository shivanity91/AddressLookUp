using Api.Common.Contracts;

namespace Api.Common.WorkerServices
{
    public class ReverseDnsLookUpWorker : AddressLookUpWorkerBase<ReverseDnsLookUpResult>
    {
        public ReverseDnsLookUpWorker(string url) : base(url)
        {
        }
    }
}
