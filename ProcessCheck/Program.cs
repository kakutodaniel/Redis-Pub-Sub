using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace ProcessCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            

            while(true)
            {
                var proc = Process.GetProcesses();

                var processExists = proc.Any(p => p.ProcessName.Contains("corona"));

                var processExists2 = proc.Any(p => p.MainWindowTitle.Contains("corona"));

                Console.WriteLine(processExists);
                Console.WriteLine(processExists2);

                Thread.Sleep(2000);
            }

           
        }
    }
}
