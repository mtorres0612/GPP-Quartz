/******************************************************
 * 
 *  Author          : Marvin D. Onofre
 *  Date Created    : 09/08/2016 
 *
 *  Description     : Subscribe to the Quartz.Net ITriggerListener events.
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
    
    public class GPPTriggerListener : ITriggerListener
    {
        //private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string Name
        {
            get { return "GPPMainTriggerListener"; }
        }

        public void TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode)
        {            
            try
            {
                //_log.Info(string.Format("The scheduler called {0} for trigger {1}", MethodBase.GetCurrentMethod().Name, trigger.Key));
                //Console.WriteLine(string.Format("TriggerFired - The scheduler called {0} for trigger {1} - {2}", MethodBase.GetCurrentMethod().Name, trigger.Key, context.ToString()));
                Console.WriteLine(string.Format("The scheduler called [{0}] for trigger {1}", MethodBase.GetCurrentMethod().Name, trigger.Key));

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void TriggerFired(ITrigger trigger, IJobExecutionContext context)
        {
            try
            {
                //_log.Info(string.Format("The scheduler called {0} for trigger {1}", MethodBase.GetCurrentMethod().Name, trigger.Key));
                //Console.WriteLine(string.Format("TriggerFired - The scheduler called {0} for trigger {1} - {2}", MethodBase.GetCurrentMethod().Name, trigger.Key, context.ToString()));
                Console.WriteLine(string.Format("The scheduler called [{0}] for trigger {1}", MethodBase.GetCurrentMethod().Name, trigger.Key));
                
                context.JobInstance.Execute(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            
        }

        public void TriggerMisfired(ITrigger trigger)
        {
            //_log.Info(string.Format("The scheduler called {0} for trigger {1}", MethodBase.GetCurrentMethod().Name, trigger.Key));
            Console.WriteLine(string.Format("The scheduler called [{0}] for trigger {1}", MethodBase.GetCurrentMethod().Name, trigger.Key));
        }

        public bool VetoJobExecution(ITrigger trigger, IJobExecutionContext context)
        {
            try { 
                //_log.Info(string.Format("The scheduler called {0} for trigger {1}", MethodBase.GetCurrentMethod().Name, trigger.Key));
                Console.WriteLine(string.Format("The scheduler called [{0}] for trigger {1}", MethodBase.GetCurrentMethod().Name, trigger.Key));
            }
            catch (Exception ex)
            {

            }
            return true;
        }        
    }
}
