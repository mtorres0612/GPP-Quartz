/******************************************************
 * 
 *  Author          : Marvin D. Onofre
 *  Date Created    : 09/08/2016 
 *
 *  Description     : Subscribe to the Quartz.Net IJobListener events.
 * 
 * 
 * 
 */

using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GPPConsoleApplication
{
    public class GPPJobListener : IJobListener
    {

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string Name
        {
            get { return "GPPMainJobListener"; }
        }

        public void JobExecutionVetoed(IJobExecutionContext context)
        {
            _log.Info(string.Format("The scheduler called [{0}] for job {1}", MethodBase.GetCurrentMethod().Name, context.JobDetail.Key));
        }

        public void JobToBeExecuted(IJobExecutionContext context)
        {
            
            _log.Info(string.Format("The scheduler called [{0}] for job {1}", MethodBase.GetCurrentMethod().Name, context.JobDetail.Key));
        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {            
            _log.Info(string.Format("The scheduler called [{0}] for job {1} {2}", MethodBase.GetCurrentMethod().Name, context.JobDetail.Key, context.JobRunTime.ToString()));
        }        
    }
}
