/******************************************************
 * 
 *  Author          : Marvin D. Onofre
 *  Date Created    : 09/08/2016 
 *
 *  Description     : Subscribe to the Quartz.Net ISchedulerListener events.
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
    public class GPPSchedulerListener : ISchedulerListener
    {

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Jobs

        public void JobAdded(IJobDetail jobDetail)
        {
            _log.Info(string.Format("The scheduler called [{0}]", MethodBase.GetCurrentMethod().Name));            
        }

        public void JobDeleted(JobKey jobKey)
        {            
        }

        public void JobPaused(JobKey jobKey)
        {            
        }

        public void JobResumed(JobKey jobKey)
        {            
        }

        public void JobScheduled(ITrigger trigger)
        {            
            _log.Info(string.Format("The scheduler called [{0}]", MethodBase.GetCurrentMethod().Name));
        }

        public void JobUnscheduled(TriggerKey triggerKey)
        {         
        }

        public void JobsPaused(string jobGroup)
        {
            _log.Info(string.Format("The scheduler called [{0}]", MethodBase.GetCurrentMethod().Name));
        }

        public void JobsResumed(string jobGroup)
        {
            _log.Info(string.Format("The scheduler called [{0}]", MethodBase.GetCurrentMethod().Name));
        }

        public void SchedulerError(string msg, SchedulerException cause)
        {
            _log.Info(string.Format("The scheduler called [{0}]", MethodBase.GetCurrentMethod().Name));
        }

        #endregion

        #region Scheduler

        public void SchedulerInStandbyMode()
        {
            _log.Info(string.Format("The scheduler called [{0}]", MethodBase.GetCurrentMethod().Name));
        }

        public void SchedulerShutdown()
        {
            _log.Info(string.Format("The scheduler called [{0}]", MethodBase.GetCurrentMethod().Name));
        }

        public void SchedulerShuttingdown()
        {
            _log.Info(string.Format("The scheduler called [{0}]", MethodBase.GetCurrentMethod().Name));
        }

        public void SchedulerStarted()
        {
            _log.Info(string.Format("The scheduler called [{0}]", MethodBase.GetCurrentMethod().Name));
        }

        public void SchedulerStarting()
        {
            _log.Info(string.Format("The scheduler called [{0}]", MethodBase.GetCurrentMethod().Name));
        }

        public void SchedulingDataCleared()
        {            
        }

        #endregion

        #region Triggers

        public void TriggerFinalized(ITrigger trigger)
        {
            _log.Info(string.Format("The scheduler called [{0}]", MethodBase.GetCurrentMethod().Name));
        }

        public void TriggerPaused(TriggerKey triggerKey)
        {        
        }

        public void TriggerResumed(TriggerKey triggerKey)
        {        
        }

        public void TriggersPaused(string triggerGroup)
        {         
        }

        public void TriggersResumed(string triggerGroup)
        {        
        }

        #endregion

    }
}
