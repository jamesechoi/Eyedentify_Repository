using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Device.Location;

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

        internal static string GetGoogleAPIKey()
        {
            return WebConfigurationManager.AppSettings["GoogleAPI"].ToString();
        }

        internal static double GetLatitudeDeltaRangeInDegrees(double distance)
        {
            double earthRadius = 6371.0; //km
            double r = (double)distance / earthRadius;

            double RadiansToDegrees = (double)180 / Math.PI;

            return (double) r * RadiansToDegrees;
        }

        internal static double GetLongitudeDeltaRange(double distance, double latInDegrees)
        {
            double earthRadius = 6371.0; //km
            double r = (double)distance / earthRadius;

            double RadiansToDegrees = (double)180 / Math.PI;
            double latInRadians = (double)latInDegrees / RadiansToDegrees;

            double latT = Math.Asin(Math.Sin(latInRadians) / Math.Cos(r));

            double deltaLon = Math.Acos((Math.Cos(r) - Math.Sin(latT) * Math.Sin(latInRadians)) / (Math.Cos(latT) * Math.Cos(latInRadians)));
            return (double)deltaLon * RadiansToDegrees; ;
        }

        internal static int GetGoogleZoomLevel(double lat1, double lon1, double lat2, double lon2)
        {
            GeoCoordinate sCoord = new GeoCoordinate(lat1, lon1);
            GeoCoordinate eCoord = new GeoCoordinate(lat2, lon2);

            double distanceInKm = sCoord.GetDistanceTo(eCoord)/1000;

            if (distanceInKm < 0.1)
                return 18;
            else if (distanceInKm >= 0.1 && distanceInKm < 1.0)
                return 16;
            else if (distanceInKm >= 1.0 && distanceInKm < 2.0)
                return 14;
            else
                return 13;
        }
    }
}