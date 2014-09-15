using System;
using System.Collections.Generic;
using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Internal
{
    public class ReturnApplicationPoolSitesAndApplications : IApplicationPoolSitesAndApplications
    {
        private readonly IApplicationPoolApplications _applicationPoolApplications;

        public ReturnApplicationPoolSitesAndApplications(IApplicationPoolApplications applicationPoolApplications)
        {
            if (applicationPoolApplications == null)
            {
                throw new ArgumentNullException("applicationPoolApplications");
            }
            _applicationPoolApplications = applicationPoolApplications;
        }

        public string Value(IEnumerable<Site> sites, string appPoolName)
        {
            var applicationPoolApplications = "";

            foreach (var site in sites)
            {
                //var tempString = GetApplicationPoolApplications(appPoolName, site);
                //applicationPoolApplications += tempString.Replace(site.Name + "/,", "");

                applicationPoolApplications += _applicationPoolApplications.Value(appPoolName, site);
            }

            return applicationPoolApplications.Remove(applicationPoolApplications.Trim().Length - 1, 1);
        }
    }
}