using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Configuration;

namespace Eyedentify.App_Code
{
    public class Utility
    {
        public Utility()
        {
        }

        internal static string GetConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        }

        internal static string ReadConfigSetting(string setting)
        {
            AppSettingsReader settings = new AppSettingsReader();
            string settingValue = (string)settings.GetValue(setting.ToString(), typeof(string));
            return settingValue;
        }
    }
}