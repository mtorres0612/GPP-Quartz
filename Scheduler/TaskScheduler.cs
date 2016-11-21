/******************************************************
 * 
 *  Author          : Marvin D. Onofre
 *  Date Created    : 08/24/2016 
 *
 *  Description     : Implements Quartz.Net framework library with 
 *                    dynamic creation of jobs and triggers.
 * 
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Linq;
using System.Xml;

using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;

namespace GPPConsoleApplication
{
    public class TaskScheduler : ITaskScheduler
    {
        #region Declare class global variables

        private IScheduler _scheduler;
        private string[] JOBSNAME = new[] { "ABCD", "EFGH", "IJKL", "MNOP", "QRST", "UVWX", "YZ09" };

        // Class Properties
        public string Name { get { return GetType().Name; } }

        // Log4Net
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        public void Run()
        {
            try
            {
                /*
                Step 1
                 * Create Scheduled Jobs and Triggers (serialize)
                 * Load scheduled Jobs and Triggers (deserialize)
                 *  - Start jobs
                 *      - create message code transfer protocols
                 *          - move specific transfer protocols to destination protocol folder
                 *              - run protocols 
                 *               
                */

                ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
                _scheduler = schedulerFactory.GetScheduler();
                

                #region Load Jobs and Triggers via XML file

                _log.Info("Loading [GPP_JOBS.xml] scheduled jobs.");

                // Initialize XML object
                XmlDocument scheduledJobsXML = new XmlDocument();
                scheduledJobsXML.Load(@"\\WLAP0135\Shared\JobSchedulerXMLs\MESSAGECODES_" + DateTime.Now.ToString("MM-dd-yyyy") + ".xml");  // Load XML document

                _log.Info("Loading [GPP_JOBS.xml] scheduled jobs.Done.");
                
                // Loop on the job group
                foreach (string job in JOBSNAME)
                {
                    XmlNode jobs = scheduledJobsXML.SelectSingleNode(string.Format("GPP/Jobs/{0}", job.Trim()));        // Select the specific job group
                    XmlNodeList triggers = jobs.SelectNodes("Triggers/Trigger");                                        // Get the triggers of the selected job group


                    // Create Job Information
                    _log.Info("Creating job [" + job.ToString().Trim() + "].");

                    //.WithIdentity(job.ToString().Trim() + "-" + System.Guid.NewGuid().ToString().Trim().ToUpper(), job.ToString().Trim())
                    //.WithIdentity(System.Guid.NewGuid().ToString().Trim().ToUpper(), job.ToString().Trim())
                    IJobDetail jobInformation = JobBuilder.Create<Worker>()
                            .StoreDurably()
                            .WithIdentity(job.ToString().Trim(), job.ToString().Trim())
                            .Build();
                    _scheduler.AddJob(jobInformation, true);

                    _log.Info("Creating job [" + job.ToString().Trim() + "].Done.");


                    // Loop on the triggers of the selected job
                    foreach (XmlNode trigger in triggers)
                    {
                        // .WithCronSchedule("0 0/2 14 ? * MON,TUE,WED,THU,FRI *")              // Run interval 5 minutes from 2:00pm to 2:55pm every monday - friday
                        // .WithCronSchedule("0 0/5 0-23 ? * MON,TUE,WED,THU,FRI *")            // Run interval 5 minutes from 12:00 midnight to 11:55pm every monday - friday           

                        /*
                            Index   XML Element
                            =====   ===========
                            0       TriggerName
                            1       PrincipalCode
                            2       ERP
                            3       MessageCode
                            4       MsgMonday
                            5       MsgTuesday
                            6       MsgWednesday
                            7       MsgThursday
                            8       MsgFriday
                            9       MsgSaturday
                            10      MsgSunday
                            11      MsetRunTime
                            12      MsetStartTime
                            13      MsetEndTime
                            14      JobCronSchedule
                            15      RepeatForever
                            16      StartNow                     
                        */

                        // Check if the trigger have child nodes
                        if (trigger.HasChildNodes)
                        {
                            // Job Create Trigger
                            ITrigger jobTrigger = TriggerBuilder.Create()
                                    .WithIdentity(trigger.ChildNodes[0].InnerText.Trim(), job.ToString().Trim())
                                //.WithCronSchedule("0 0/1 * ? * MON,TUE,WED,THU,FRI *")              // Run interval 5 minutes from 2:00pm to 2:55pm every monday - friday                                
                                    .WithCronSchedule(trigger.ChildNodes[14].InnerText.Trim(),
                                        x => x.WithMisfireHandlingInstructionFireAndProceed())          // Get the JobCronSchedule Element                                   
                                    
                                    .StartNow()
                                    //.ForJob(job.ToString().Trim(), job.ToString().Trim())
                                    .ForJob(jobInformation)
                                    .Build();

                            _log.Info("Adding trigger [" + trigger.ChildNodes[0].ChildNodes[0].InnerText.Trim() + "].");      

                            // Add created job to the collection
                            _scheduler.ScheduleJob(jobTrigger);

                            _log.Info("Adding trigger [" + trigger.ChildNodes[0].ChildNodes[0].InnerText.Trim() + "]...Done.");

                           

                            // Clear the objects
                            // jobInformation = null;
                            jobTrigger = null;
                        }
                        else
                        {
                            Console.WriteLine("Job [" + job.ToString().Trim() + "] has no triggers. Skipping.");
                        }
                    }

                    // Clear the objects
                    jobs = null;
                    triggers = null;
                }

                // Start the schduler
                _scheduler.Start();

                // Close the XML object
                scheduledJobsXML = null;                                
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error: {0}", ex.ToString()));
            }

            #endregion

            #region Initialize Quartz.Net listener (scheduler, jobs and triggers) events
            // Do not add the trigger event listeners. the "Execute" method will not be fired.

            // Main Scheduler
            _scheduler.ListenerManager.AddSchedulerListener(new GPPSchedulerListener());

            // Job listener            
            //_scheduler.ListenerManager.AddJobListener(new GPPJobListener(), GroupMatcher<JobKey>.AnyGroup());
            _scheduler.ListenerManager.AddJobListener(new GPPJobListener(), EverythingMatcher<JobKey>.AllJobs());

            // Trigger listener
            //_scheduler.ListenerManager.AddTriggerListener(new GPPTriggerListener(), EverythingMatcher<JobKey>.AllTriggers());

            #endregion

        }

        #region Window Service Events

        public void Stop()
        {
            // Shutdown scheduler
            _scheduler.Shutdown();
        }

        #endregion
    }
}
