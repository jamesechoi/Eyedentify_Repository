using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.SqlServer.Types;

namespace Eyedentify.App_Code
{
    public class StoreDetails
    {
        public StoreDetails()
        {
        }

        public StoreDetails(int store_ID, string store_name, AdddressDetails store_address)
        {
            this.Store_ID = store_ID;
            this.Store_Name = store_name;
            this.Store_Address = store_address;
        }

        private int _store_ID = 0;
        public int Store_ID
        {
            get { return _store_ID; }
            set { _store_ID = value; }
        }

        private string _store_name = string.Empty;
        public string Store_Name
        {
            get { return _store_name; }
            set { _store_name = value; }
        }

        private AdddressDetails _address = new AdddressDetails();
        public AdddressDetails Store_Address
        {
            get { return _address; }
            set { _address = value; }
        }


        public static StoreDetails GetStoreDetailsFromReader(IDataReader reader)
        {
            StoreDetails storeDetailsCol = new StoreDetails();
            while (reader.Read())
                storeDetailsCol = GetStoreDetails(reader);
            return storeDetailsCol;
        }

        private static StoreDetails GetStoreDetails(IDataReader reader)
        {
            AdddressDetails store_address = AdddressDetails.GetAddressDetails(reader);

            StoreDetails storeDetails = new StoreDetails(
                int.Parse(reader["Store_ID"].ToString()),
                reader["Store_Name"].ToString(),
                store_address);
            return storeDetails;
        }
    }
}