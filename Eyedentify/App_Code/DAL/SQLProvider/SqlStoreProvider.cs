using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Eyedentify.App_Code
{
    public class SqlStoreProvider : DataAccess
    {
        public SqlStoreProvider()
        {
            this.ConnectionString = Utility.GetConnectionString();
            this.EnableCaching = false;
            this.CacheDuration = 100;
        }

        internal StoreDetails Store_Get_User_Store_Info(string userID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Store_Get_User_Store_Info", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@user_ID", SqlDbType.UniqueIdentifier).Value = new Guid(userID);
                cn.Open();
                StoreDetails sDetails = StoreDetails.GetStoreDetailsFromReader(ExecuteReader(cmd));
                return sDetails;
            }
        }
    }
}