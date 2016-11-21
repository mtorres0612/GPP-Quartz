/******************************************************
 * 
 *  Author          : Marvin D. Onofre
 *  Date Created    : 08/24/2016 
 *
 *  Description     : Interface for class TaskScheduler.
 * 
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPPConsoleApplication
{
    public interface ITaskScheduler
    {
        string Name { get; }
        void Run();
        void Stop();
    }
}
