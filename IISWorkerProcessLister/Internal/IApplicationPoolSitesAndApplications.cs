using EvilBaschdi.Core;
using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Internal;

/// <summary>
/// </summary>
public interface IApplicationPoolSitesAndApplications : IValueFor2<IEnumerable<Site>, string, string>
{
}