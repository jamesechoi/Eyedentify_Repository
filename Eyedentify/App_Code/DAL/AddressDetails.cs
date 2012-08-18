using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.SqlServer.Types;

namespace Eyedentify.App_Code
{
    public class AdddressDetails
    {
        public AdddressDetails()
        {
        }

        public AdddressDetails(int address_ID, string unit_number, int street_number, string street_name, string suburb, string city,
            string state, string country, string post_code, double latitude, double longitude)
        {
            this.Address_ID = address_ID;
            this.Unit_Number = unit_number;
            this.Street_Number = street_number;
            this.Street_Name = street_name;
            this.Suburb = suburb;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.Post_Code = post_code;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        private int _address_ID = 0;
        public int Address_ID
        {
            get { return _address_ID; }
            set { _address_ID = value; }
        }

        private string _unit_number = string.Empty;
        public string Unit_Number
        {
            get { return _unit_number; }
            set { _unit_number = value; }
        }

        private int _street_number = 0;
        public int Street_Number
        {
            get { return _street_number; }
            set { _street_number = value; }
        }

        private string _street_name = string.Empty;
        public string Street_Name
        {
            get { return _street_name; }
            set { _street_name = value; }
        }

        private string _suburb = string.Empty;
        public string Suburb
        {
            get { return _suburb; }
            set { _suburb = value; }
        }

        private string _city = string.Empty;
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _state = string.Empty;
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        private string _country = string.Empty;
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        private string _post_code = string.Empty;
        public string Post_Code
        {
            get { return _post_code; }
            set { _post_code = value; }
        }

        private double _latitude = 0.0;
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        private double _longitude = 0.0;
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        public static AdddressDetails GetAddressDetailsFromReader(IDataReader reader)
        {
            AdddressDetails addressDetailsCol = new AdddressDetails();
            while (reader.Read())
                addressDetailsCol = GetAddressDetails(reader);
            return addressDetailsCol;
        }

        public static AdddressDetails GetAddressDetails(IDataReader reader)
        {
            SqlGeography sgeo = SqlGeography.Parse(reader["Geo_Code"].ToString());
            double latitude = sgeo.Lat.Value;
            double longitude = sgeo.Long.Value;

            AdddressDetails addressDetails = new AdddressDetails(
                int.Parse(reader["Address_ID"].ToString()),
                reader["Unit_Number"].ToString(),
                int.Parse(reader["Street_Number"].ToString()),
                reader["Street_Name"].ToString(),
                reader["Suburb"].ToString(),
                reader["City"].ToString(),
                reader["State"].ToString(),
                reader["Country"].ToString(),
                reader["Post_Code"].ToString(),               
                latitude,
                longitude);
            return addressDetails;
        }
    }
}