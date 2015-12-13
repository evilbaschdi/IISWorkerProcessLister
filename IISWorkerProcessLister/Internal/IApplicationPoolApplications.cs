using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Internal
{
    /// <summary>
    /// </summary>
    public interface IApplicationPoolApplications
    {
        /// <summary>
        /// </summary>
        /// <param name="appPoolName"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        string Value(string appPoolName, Site site);
    }
}