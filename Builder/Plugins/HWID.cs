using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace Builder.Plugins
{
    public static class HWID
    {
        public static string GetHardwareId()
        {
            string processorId = "";
            string diskDriveSignature = "";

            using (var searcherProcessor = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor"))
            using (var searcherDiskDrive = new ManagementObjectSearcher("SELECT Signature FROM Win32_DiskDrive"))
            {
                var processor = searcherProcessor.Get().Cast<ManagementObject>().FirstOrDefault();
                var diskDrive = searcherDiskDrive.Get().Cast<ManagementObject>().FirstOrDefault();

                if (processor != null)
                    processorId = processor["ProcessorId"]?.ToString();

                if (diskDrive != null)
                    diskDriveSignature = diskDrive["Signature"]?.ToString();
            }

            return  $"{processorId}{diskDriveSignature}";
        }
    }
}
