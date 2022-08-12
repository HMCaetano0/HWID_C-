using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Management;
using System.Windows;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace HWID
{
    internal class Program
    {

        public static string getHWID()
        {
            string HWID = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value.Replace("-", "");

            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";

            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }
            string aux = HWID + id, hwid_xor = "";
            for (int i = 0; i < aux.Length; i++)
                hwid_xor += aux[i] ^ 0xA;

            return hwid_xor.Replace("1", "i").Replace("4", "A").Replace("3", "e").Replace("5", "S");
        }

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("HWID gerado com sucesso!");
            Clipboard.SetText(getHWID());
            Console.Read();
        }
    }
}
