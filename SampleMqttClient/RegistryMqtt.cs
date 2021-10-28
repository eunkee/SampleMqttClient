using Microsoft.Win32;
using System;

namespace SampleMqttClient
{
    class RegistryMqtt
    {
        public static RegistryKey RegKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\SampleMqttClient", true);
        private static readonly object regObject = new();

        public static string Address
        {
            get
            {
                string rslt = string.Empty;
                if (RegKey != null)
                {
                    lock (regObject)
                    {
                        try
                        {
                            rslt = Convert.ToString(RegKey.GetValue("Address", rslt));
                        }
                        catch { }
                    }
                }
                return rslt;
            }
            set
            {
                lock (regObject)
                {
                    if (RegKey != null)
                    {
                        try
                        {
                            RegKey.SetValue("Address", value);
                        }
                        catch { }
                    }
                }
            }
        }

        public static int Port
        {
            get
            {
                int rslt = 9002;
                if (RegKey != null)
                {
                    lock (regObject)
                    {
                        try
                        {
                            rslt = Convert.ToInt32(RegKey.GetValue("Port", rslt));
                        }
                        catch { }
                    }
                }
                return rslt;
            }
            set
            {
                lock (regObject)
                {
                    if (RegKey != null)
                    {
                        try
                        {
                            RegKey.SetValue("Port", value.ToString());
                        }
                        catch { }
                    }
                }
            }
        }

        public static string Id
        {
            get
            {
                string rslt = string.Empty;
                if (RegKey != null)
                {
                    lock (regObject)
                    {
                        try
                        {
                            rslt = Convert.ToString(RegKey.GetValue("Id", rslt));
                        }
                        catch { }
                    }
                }
                return rslt;
            }
            set
            {
                lock (regObject)
                {
                    if (RegKey != null)
                    {
                        try
                        {
                            RegKey.SetValue("ID", value);
                        }
                        catch { }
                    }
                }
            }
        }

        public static string Password
        {
            get
            {
                string rslt = string.Empty;
                if (RegKey != null)
                {
                    lock (regObject)
                    {
                        try
                        {
                            rslt = Convert.ToString(RegKey.GetValue("Password", rslt));
                        }
                        catch { }
                    }
                }
                return rslt;
            }
            set
            {
                lock (regObject)
                {
                    if (RegKey != null)
                    {
                        try
                        {
                            RegKey.SetValue("Password", value);
                        }
                        catch { }
                    }
                }
            }
        }

        public static string Topic
        {
            get
            {
                string rslt = string.Empty;
                if (RegKey != null)
                {
                    lock (regObject)
                    {
                        try
                        {
                            rslt = Convert.ToString(RegKey.GetValue("Topic", rslt));
                        }
                        catch { }
                    }
                }
                return rslt;
            }
            set
            {
                lock (regObject)
                {
                    if (RegKey != null)
                    {
                        try
                        {
                            RegKey.SetValue("Topic", value);
                        }
                        catch { }
                    }
                }
            }
        }
    }
}
