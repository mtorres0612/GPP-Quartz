using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IAPL.Transport.Transactions;
namespace GPPConsoleApplication
{
    public class Worker : IJob
    {
        //private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Execute(IJobExecutionContext context)
        {
            string name = context.Trigger.Key.Name;
            string[] arrContext = name.Split('-');

            MessageTransaction messageTransaction = new MessageTransaction();

            messageTransaction.GetFileListItem(arrContext[0], arrContext[1], arrContext[2]);


            //Console.WriteLine(string.Format("{0} {1}", "First Task is executing.", DateTime.Now.ToString()));
            //_log.Info(string.Format("{0} {1}", "First Task is executing.", DateTime.Now.ToString()));

            // Get time base ex. 2:15am => 2:00am
            // Get list from FileListProc where from 2:00am between (based time + 1)
            // Get list from FileListProc where from 2:00am between 3:00am

        }
    }
}
