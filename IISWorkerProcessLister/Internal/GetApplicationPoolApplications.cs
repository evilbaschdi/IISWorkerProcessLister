using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Internal
{
    public class GetApplicationPoolApplications : IApplicationPoolApplications
    {
        public string Value(string appPoolName, Site site)
        {
            var applicationPoolApplications = "";

            foreach (var application in site.Applications)
            {
                if (!string.IsNullOrWhiteSpace(appPoolName) &&
                    !string.IsNullOrWhiteSpace(application.ApplicationPoolName) &&
                    (application.ApplicationPoolName.Trim() == appPoolName.Trim()))
                {
                    applicationPoolApplications += string.Format("{0}{1}, ", site.Name, application.Path);
                }
            }

            return applicationPoolApplications;
        }
    }
}