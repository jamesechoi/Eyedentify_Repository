using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Eyedentify.App_Code
{
    public class SqlUserProvider : DataAccess
    {
        public SqlUserProvider()
        {
            this.ConnectionString = Utility.GetConnectionString();
            this.EnableCaching = false;
            this.CacheDuration = 100;
        }

        public int User_Get_Store_ID(string user_ID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_User_Get_Store_ID", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@user_ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(user_ID);
                cn.Open();

                DataTable dt = new DataTable();
                dt.Load(ExecuteReader(cmd));

                int store_ID = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                return store_ID;
            }
        }

        //internal void Incident_Delete_By_Owner(int incident_id, string deleteReasonText)
        //{
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("EyeD_Incident_Delete_By_Owner", cn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@incident_ID", SqlDbType.Int).Value = incident_id;
        //        cmd.Parameters.Add("@deleteReason", SqlDbType.VarChar).Value = deleteReasonText;
        //        cn.Open();
        //        ExecuteNonQuery(cmd);
        //    }
        //}
    }
}