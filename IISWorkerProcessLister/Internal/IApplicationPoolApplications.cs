using EvilBaschdi.Core;
using Microsoft.Web.Administration;

namespace IISWorkerProcessLister.Internal;

/// <summary>
/// </summary>
public interface IApplicationPoolApplications : IValueFor2<string, Site, string>
{
}