using Api.Common.Contracts;

namespace Api.Common.WorkerServices;

public class RdapLookUpWorker : AddressLookUpWorkerBase<RdapLookUpResult>
{
    public RdapLookUpWorker(string url) : base(url)
    {
    }
}
