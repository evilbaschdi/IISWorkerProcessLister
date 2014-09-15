using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Internal
{
    public interface IApplicationPoolApplications
    {
        string Value(string appPoolName, Site site);
    }
}