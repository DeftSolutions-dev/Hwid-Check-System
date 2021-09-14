using System.Management;
using System.Security.Principal;

namespace HWID
{
    public class Hwid
    {
        string Result;
        public string GetOwnerIdentity(bool ReturnDashes = false)
        {
            if (ReturnDashes)
            {
                return WindowsIdentity.GetCurrent().Owner.Value.ToString();
            }
            else
            {
                return WindowsIdentity.GetCurrent().Owner.Value.ToString().Replace("-", "");
            }
        }

        public string GetCPUID(string ID = null)
        {
            return GetWin32("Win32_Processor", ID ?? "ProcessorId");
        }

        public string GetDiskID(string ID = null)
        {
            return GetWin32("Win32_DiskDrive", ID ?? "SerialNumber");
        }

        public string GetUSBID(string ID = null)
        {
            return GetWin32("Win32_USBHub", ID ?? "PNPDeviceID");
        }

        public string GetBaseboardID(string ID = null)
        {
            return GetWin32("Win32_BaseBoard", ID ?? "SerialNumber");
        }

        public string GetMemoryID(string ID = null)
        {
            return GetWin32("Win32_PhysicalMemory", ID ?? "PartNumber");
        }

        public string GetGPUID(string ID = null)
        {
            return GetWin32("Win32_VideoController", ID ?? "PNPDeviceID");
        }

        public string GetMonitorID(string ID = null)
        {
            return GetWin32("Win32_DesktopMonitor", ID ?? "PNPDeviceID");
        }

        public string GetWin32(string Selection, string ID)
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM {Selection}"))
            {
                try
                {
                    var collection = searcher.Get().GetEnumerator();
                    return collection.MoveNext() ? collection.Current[ID].ToString() : "0";
                }
                catch (ManagementException Error)
                {
                    switch (Error.ErrorCode)
                    {
                        case ManagementStatus.NotFound:
                            return "ID Not Found";
                        case ManagementStatus.InvalidClass:
                            return "Invalid Class";
                        default:
                            return "0";

                    }
                }
            }

        }
    }
}