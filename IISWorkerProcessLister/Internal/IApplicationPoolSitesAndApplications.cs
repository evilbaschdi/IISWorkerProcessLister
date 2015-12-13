using System.Collections.Generic;
using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Internal
{
    /// <summary>
    /// </summary>
    public interface IApplicationPoolSitesAndApplications
    {
        /// <summary>
        /// </summary>
        /// <param name="sites"></param>
        /// <param name="appPoolName"></param>
        /// <returns></returns>
        string Value(IEnumerable<Site> sites, string appPoolName);
    }
}