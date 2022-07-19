using Api.Common.Contracts;

namespace Api.Common.WorkerServices
{
    public class DomainAvailabilityWorker : AddressLookUpWorkerBase<DomainAvailabilityResult>
    {
        public DomainAvailabilityWorker(string url) : base(url)
        {
        }
    }
}
