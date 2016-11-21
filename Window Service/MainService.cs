/******************************************************
 * 
 *  Author          : Marvin D. Onofre
 *  Date Created    : 08/08/2016 
 *
 *  Description     : Main GPP window service initialization and configuration.
 * 
 * 
 * 
 */

using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPPConsoleApplication
{
    public class MainService
    {

        #region Declare variables

        //readonly int _workerThreads, _complete;
        //readonly bool _success;
        ITaskScheduler _scheduler;

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        public MainService(ILifetimeScope lifetimescope)
        {
            // Set min and max thread pools
            //ThreadPool.GetMaxThreads(out _workerThreads, out _complete);
            //bool _success = ThreadPool.SetMinThreads(_workerThreads, _complete);
        }

        #region Window Service Events

        public void Start()
        {
            _scheduler = new TaskScheduler();
            _scheduler.Run();
            Console.WriteLine(string.Format("{0} {1}", "Task start.", DateTime.Now.ToString()));
            _log.Info("Application is working");
        }

        public void Stop()
        {
            if (_scheduler != null)
            {
                Console.WriteLine(string.Format("{0} {1}", "Task stop.", DateTime.Now.ToString()));
                _scheduler.Stop();
            }

            Console.ReadLine();
        }

        public void Paused()
        {            
        }

        public void ShutDown()
        {
        }

        public void Continued()
        {
        }

        #endregion
    }
}
