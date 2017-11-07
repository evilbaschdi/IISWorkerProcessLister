using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Internal
{
    /// <summary>
    /// </summary>
    public class ReturnApplicationPoolSitesAndApplications : IApplicationPoolSitesAndApplications
    {
        private readonly IApplicationPoolApplications _applicationPoolApplications;

        /// <summary>
        /// </summary>
        /// <param name="applicationPoolApplications"></param>
        public ReturnApplicationPoolSitesAndApplications(IApplicationPoolApplications applicationPoolApplications)
        {
            _applicationPoolApplications = applicationPoolApplications ?? throw new ArgumentNullException(nameof(applicationPoolApplications));
        }

        /// <summary>
        /// </summary>
        /// <param name="sites"></param>
        /// <param name="appPoolName"></param>
        /// <returns></returns>
        public string Value(IEnumerable<Site> sites, string appPoolName)
        {
            var applicationPoolApplications = sites.Aggregate("", (current, site) => $"{current}{_applicationPoolApplications.Value(appPoolName, site)}");

            return applicationPoolApplications.Remove(applicationPoolApplications.Trim().Length - 1, 1);
        }
    }
}