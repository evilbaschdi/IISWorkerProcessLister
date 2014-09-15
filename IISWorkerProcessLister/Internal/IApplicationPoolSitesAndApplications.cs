using System.Collections.Generic;
using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Internal
{
    public interface IApplicationPoolSitesAndApplications
    {
        string Value(IEnumerable<Site> sites, string appPoolName);
    }
}