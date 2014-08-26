using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace IISWorkerProcessLister.Internal
{
    public class WorkerProcesses
    {
        public string WorkerProcessInfo { get; set; }

        public string WorkerProcessShortInfo { get; set; }

        public BindingList<WorkerProcessItem> ItemsSource()
        {
            var serverManager = new ServerManager();
            var workerProcesses = serverManager.WorkerProcesses;

            var itemsSource = new BindingList<WorkerProcessItem>();
            itemsSource.Clear();
            WorkerProcessInfo = string.Empty;
            WorkerProcessShortInfo = string.Empty;

            foreach (var process in workerProcesses)
            {
                var appPoolName = process.AppPoolName;
                var applicationPoolSitesAndApplications = ReturnApplicationPoolSitesAndApplications(
                    serverManager.Sites, appPoolName);
                var processId = process.ProcessId;
                var state = process.State.ToString();

                var workerProcessItem = new WorkerProcessItem
                {
                    ProcessId = processId,
                    AppPoolName = appPoolName,
                    Applications = applicationPoolSitesAndApplications,
                    State = state,
                };

                var workerProcessInfo = string.Format("App Pool: {0} | Process ID: {1} | State: {2}{3}", appPoolName,
                    processId, state, Environment.NewLine);
                var workerPorocessShortInfo = string.Format("{0} | {1}{2}", appPoolName, processId, Environment.NewLine);

                //File.AppendAllText(@"c:\temp\test.txt", shortInfo);

                WorkerProcessInfo += workerProcessInfo;
                WorkerProcessShortInfo += workerPorocessShortInfo;

                itemsSource.Add(workerProcessItem);
            }

            return itemsSource;
        }

        public void CloseByProcessId(int processId)
        {
            var process = Process.GetProcessById(processId);

            process.CloseMainWindow();
            process.WaitForExit(10000);

            if (!process.HasExited)
            {
                process.Kill();
            }
        }

        private string ReturnApplicationPoolSitesAndApplications(IEnumerable<Site> sites, string appPoolName)
        {
            var applicationPoolApplications = "";

            foreach (var site in sites)
            {
                //var tempString = GetApplicationPoolApplications(appPoolName, site);
                //applicationPoolApplications += tempString.Replace(site.Name + "/,", "");

                applicationPoolApplications += GetApplicationPoolApplications(appPoolName, site);
            }

            return applicationPoolApplications.Remove(applicationPoolApplications.Trim().Length - 1, 1);
        }

        private string GetApplicationPoolApplications(string appPoolName, Site site)
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

        internal void CloseWorkerProcess(DataGrid item)
        {
            CloseByProcessId(GetWorkerProcessItem(item).ProcessId);
        }

        private static WorkerProcessItem GetWorkerProcessItem(DataGrid item)
        {
            //Get the underlying item, that you cast to your object that is bound
            //to the DataGrid (and has subject and state as property)
            var workerProcessItem = (WorkerProcessItem)item.SelectedCells[0].Item;
            return workerProcessItem;
        }

        internal void RecycleApplicationPool(DataGrid item)
        {
            var serverManager = new ServerManager();
            serverManager.ApplicationPools[GetWorkerProcessItem(item).AppPoolName].Recycle();
        }
    }
}