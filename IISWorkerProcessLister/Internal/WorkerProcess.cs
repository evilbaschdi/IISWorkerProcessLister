using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using Microsoft.Web.Administration;
using Application = Microsoft.Web.Administration.Application;

namespace IISWorkerProcessLister.Internal
{
    public class WorkerProcesses
    {
        public string WorkerProcessShortInfo { get; set; }

        public BindingList<WorkerProcessItem> ItemsSource()
        {
            var serverManager = new ServerManager();
            WorkerProcessCollection workerProcessCollection = serverManager.WorkerProcesses;

            var itemsSource = new BindingList<WorkerProcessItem>();
            itemsSource.Clear();

            foreach (var process in workerProcessCollection)
            {
                string appPoolName = process.AppPoolName;
                int processId = process.ProcessId;

                WorkerProcessShortInfo += String.Format("App Pool: {0} | Process Id: {1}{2}", appPoolName, processId,
                    Environment.NewLine);

                var workerProcessItem = new WorkerProcessItem
                {
                    ProcessId = process.ProcessId,
                    AppPoolName = process.AppPoolName,
                    Applications = ReturnApplicationPoolSitesAndApplications(serverManager.Sites, appPoolName),
                    State = process.State.ToString()
                };
                itemsSource.Add(workerProcessItem);
            }
            return itemsSource;
        }

        public void CloseByProcessId(int processId)
        {
            Process process = Process.GetProcessById(processId);

            process.CloseMainWindow();
            process.WaitForExit(10000);

            if (!process.HasExited)
            {
                process.Kill();
            }
        }

        private string ReturnApplicationPoolSitesAndApplications(IEnumerable<Site> sites, string appPoolName)
        {
            string applicationPoolApplications = "";

            foreach (Site site in sites)
            {
                foreach (Application application in site.Applications)
                {
                    if (application.ApplicationPoolName == appPoolName)
                    {
                        applicationPoolApplications += String.Format("{0}{1}, ", site.Name, application.Path);
                    }
                }
                //applicationPoolApplications = applicationPoolApplications.Replace(site.Name + "/,", "");
            }

            return applicationPoolApplications.Remove(applicationPoolApplications.Trim().Length - 1, 1);
        }


        public void CloseWorkerProcess(DataGrid item)
        {//Get the underlying item, that you cast to your object that is bound
            //to the DataGrid (and has subject and state as property)
            var workerProcessItem = (WorkerProcessItem)item.SelectedCells[0].Item;

            CloseByProcessId(workerProcessItem.ProcessId);
        }
    }
}
