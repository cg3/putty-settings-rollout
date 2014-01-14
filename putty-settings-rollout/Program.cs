using Microsoft.Win32;
using System.Collections.Generic;

namespace putty_settings_rollout
{
    class Program
    {
        static string PuttyKeyPath = @"Software\SimonTatham\PuTTY\Sessions";

        static void changeSessionSettingsToDefault(List<string> sessions)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(PuttyKeyPath + @"\Default%20Settings");
            foreach (var session in sessions)
            {
                RegistryKey actSession = Registry.CurrentUser.OpenSubKey(PuttyKeyPath + @"\" + session, true);
                foreach (string s in rk.GetValueNames())
                {
                    if (!(s.Equals("UserName") || s.Equals("HostName")))
                    {
                        actSession.SetValue(s, rk.GetValue(s), rk.GetValueKind(s));
                    }
                }
            }
        }

        static List<string> getSessions()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(PuttyKeyPath);
            List<string> results = new List<string>(rk.GetSubKeyNames());
            results.Remove(@"Default%20Settings");
            return results;
        }

        static void Main(string[] args)
        {
            changeSessionSettingsToDefault(getSessions());
        }
    }
}
