using System.Linq;
using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Internal
{
    /// <summary>
    /// </summary>
    public class GetApplicationPoolApplications : IApplicationPoolApplications
    {
        /// <summary>
        /// </summary>
        /// <param name="appPoolName"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        public string Value(string appPoolName, Site site)
        {
            return
                site.Applications.Where(
                        application =>
                            !string.IsNullOrWhiteSpace(appPoolName) && !string.IsNullOrWhiteSpace(application.ApplicationPoolName) &&
                            application.ApplicationPoolName.Trim() == appPoolName.Trim())
                    .Aggregate("", (current, application) => current + $"{site.Name}{application.Path}, ");
        }
    }
}