using Processor.Management.Cpu.Service;
using System;
using System.Security.Principal;

namespace Decaffeinated
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            if (isElevated)
            {

                if (args.Length == 0)
                {
                    Console.WriteLine("Missing argument!\r\nHow to use: Decaffeinated.exe <false,true,status>");
                }
                else if (args.Length == 1)
                {
                    if (args[0] == "status")
                    {
                        ProcessorInformationProvider pip = new ProcessorInformationProvider();
                        pip.LoadDriver();
                       var _status = pip.GetCpuC1EStatus();
                        pip.UnloadDriver();

                        Console.WriteLine("Current C1E status: " +_status.CurrentStatus.ToString());

                    }
                    else
                    {

                        bool IsEnabled = false;

                        if (bool.TryParse(args[0], out IsEnabled))
                        {
                            ProcessorInformationProvider pip = new ProcessorInformationProvider();
                            pip.LoadDriver();
                            pip.SetCpuC1EStatus(IsEnabled);
                            pip.UnloadDriver();

                            Console.WriteLine("C1E set to " + IsEnabled);

                        }
                        else
                        {
                            Console.WriteLine("Error in argument! use true or false.");

                        }
                    }

                }
                else
                {
                    Console.WriteLine("Too many arguments");

                }
            }
            else
            {

                Console.WriteLine("You need admin rights to run this program.");
            }
           

        }
    }
}
