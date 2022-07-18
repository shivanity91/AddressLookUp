using Api.Common.Contracts;

namespace Api.Common.WorkerServices
{
    public class GeoIpLookUpWorker : AddressLookUpWorkerBase<GeoIpLookUpResult>
    {
        public GeoIpLookUpWorker(string url) : base(url)
        {
        }
    }
}
