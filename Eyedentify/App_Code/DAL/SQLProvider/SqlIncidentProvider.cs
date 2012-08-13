using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Eyedentify.App_Code
{
    public class SqlIncidentProvider : DataAccess
    {
        public SqlIncidentProvider()
        {
            this.ConnectionString = Utility.GetConnectionString();
            this.EnableCaching = false;
            this.CacheDuration = 100;
        }

        public IncidentDetails Incident_Get_Details(int incident_ID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Get_Details", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@incident_ID", SqlDbType.Int).Value = incident_ID;
                cn.Open();
                IncidentDetails iDetails = IncidentDetails.GetIncidentDetailsFromReader(ExecuteReader(cmd));
                iDetails.Incident_types = Incident_Get_Types(incident_ID);
                iDetails.Incident_people = Incident_Get_People(incident_ID);
                return iDetails;
            }
        }

        private List<IncidentPeopleDetails> Incident_Get_People(int incident_ID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Get_People", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@incident_ID", SqlDbType.Int).Value = incident_ID;
                cn.Open();
                return IncidentPeopleDetails.GetIncidentTypesCollectionFromReader(ExecuteReader(cmd));
            }
        }

        public List<IncidentTypeDetails> Incident_Get_Types(int incident_ID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Get_Types", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@incident_ID", SqlDbType.Int).Value = incident_ID;
                cn.Open();
                return IncidentTypeDetails.GetIncidentTypesCollectionFromReader(ExecuteReader(cmd));
            }
        }

        

        public int Incident_Insert(IncidentDetails incident)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(incident.User_ID);
                cmd.Parameters.Add("@subject", SqlDbType.VarChar).Value = incident.Subject;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = incident.Description;
                cmd.Parameters.Add("@incident_datetime", SqlDbType.DateTime).Value = incident.Incident_datetime;
                cmd.Parameters.Add("@insert_status", SqlDbType.Bit).Value = incident.Insert_Status;
                if (incident.Address_ID == -1)
                    cmd.Parameters.Add("@addr_ID", SqlDbType.Int).Value = null;
                else
                    cmd.Parameters.Add("@addr_ID", SqlDbType.Int).Value = incident.Address_ID;
                cn.Open();

                DataTable dt = new DataTable();
                dt.Load(ExecuteReader(cmd));

                int incidentID = int.Parse(dt.Rows[0].ItemArray[0].ToString());

                foreach (IncidentTypeDetails iType in incident.Incident_types)
                {
                    cmd = new SqlCommand("EyeD_lnk_Incident_Type_Insert", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@incidentID", SqlDbType.Int).Value = incidentID;
                    cmd.Parameters.Add("@typeID", SqlDbType.Int).Value = iType.Incident_Type_ID;

                    ExecuteNonQuery(cmd);
                }

                return incidentID;
            }
        }

        public void Incident_Update(IncidentDetails incident)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Update", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@incidentID", SqlDbType.Int).Value = incident.Incident_ID;
                cmd.Parameters.Add("@subject", SqlDbType.VarChar).Value = incident.Subject;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = incident.Description;
                cmd.Parameters.Add("@incident_datetime", SqlDbType.DateTime).Value = incident.Incident_datetime;
                cmd.Parameters.Add("@people_involved", SqlDbType.Int).Value = incident.People_Involved;
                cmd.Parameters.Add("@other_incident_type", SqlDbType.VarChar).Value = incident.Other_Incident_Type;
                cmd.Parameters.Add("@delete_status", SqlDbType.Bit).Value = incident.Delete_Status;
                cmd.Parameters.Add("@addr_ID", SqlDbType.Int).Value = incident.Address_ID;
                cn.Open();

                ExecuteNonQuery(cmd);

                cmd = new SqlCommand("EyeD_lnk_Incident_Type_Delete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@incidentID", SqlDbType.Int).Value = incident.Incident_ID;
                ExecuteNonQuery(cmd);
                

                foreach (IncidentTypeDetails iType in incident.Incident_types)
                {
                    cmd = new SqlCommand("EyeD_lnk_Incident_Type_Insert", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@incidentID", SqlDbType.Int).Value = incident.Incident_ID;
                    cmd.Parameters.Add("@typeID", SqlDbType.Int).Value = iType.Incident_Type_ID;
                    ExecuteNonQuery(cmd);
                }

                cmd = new SqlCommand("EyeD_Incident_People_Delete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@incidentID", SqlDbType.Int).Value = incident.Incident_ID;
                ExecuteNonQuery(cmd);

                foreach (IncidentPeopleDetails iPeople in incident.Incident_people)
                {
                    cmd = new SqlCommand("EyeD_Incident_People_Insert", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@incidentID", SqlDbType.Int).Value = incident.Incident_ID;
                    cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = iPeople.Gender;
                    cmd.Parameters.Add("@age_group", SqlDbType.VarChar).Value = iPeople.Age_Group;
                    cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = iPeople.Description;     
                    ExecuteNonQuery(cmd);
                }
            }
        }

        public DataTable Incident_Incident_Get_Valid(int incident_ID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Incident_Get_Valid", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@incidentID", SqlDbType.Int).Value = incident_ID;
                cn.Open();

                DataTable dt = new DataTable();
                dt.Load(ExecuteReader(cmd));

                return dt;
            }
        }

        public int Incident_Image_Insert(int incident_ID, byte[] incident_Image, string incident_description, bool main_photo)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Image_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@incidentID", SqlDbType.Int).Value = incident_ID;
                cmd.Parameters.Add("@incidentImage", SqlDbType.Image).Value = incident_Image;
                cmd.Parameters.Add("@incidentdescription", SqlDbType.VarChar).Value = incident_description;
                cmd.Parameters.Add("@main_photo", SqlDbType.Bit).Value = main_photo;                
                cn.Open();

                DataTable dt = new DataTable();
                dt.Load(ExecuteReader(cmd));

                int incident_image_ID = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                return incident_image_ID;
            }
        }

        public void Incident_Image_Update(int image_ID, string incident_description, bool main_photo)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Image_Update", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@imageID", SqlDbType.Int).Value = image_ID;
                cmd.Parameters.Add("@incidentdescription", SqlDbType.VarChar).Value = incident_description;
                cmd.Parameters.Add("@main_photo", SqlDbType.Bit).Value = main_photo;
                cn.Open();

                ExecuteNonQuery(cmd);
            }
        }

        public IncidentImageDetails Incident_Image_Get(int imageID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Image_Get", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@imageID", SqlDbType.Int).Value = imageID;
                cn.Open();

                DataTable dt = new DataTable();
                dt.Load(ExecuteReader(cmd));

                IncidentImageDetails iid = new IncidentImageDetails();
                iid.Image_ID = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                iid.Incident_ID = int.Parse(dt.Rows[0].ItemArray[1].ToString());
                iid.Image = (byte[])dt.Rows[0].ItemArray[2];
                iid.Description = dt.Rows[0].ItemArray[3].ToString();
                iid.Main_Photo = (bool)dt.Rows[0].ItemArray[4];

                return iid;
            }
        }        

        public DataTable Incident_Images_Get(int incident_ID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Images_Get", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@incident_ID", SqlDbType.Int).Value = incident_ID;
                cn.Open();

                DataTable dt = new DataTable();
                dt.Load(ExecuteReader(cmd));
                return dt;
            }
        }

        public DataTable Incident_Get_For_Home_Page()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Get_For_Home_Page", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                DataTable dt = new DataTable();
                dt.Load(ExecuteReader(cmd));
                return dt;
            }
        }

        public DataTable Incident_Type_Get_All()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Type_Get_All", cn);
                cn.Open();

                DataTable dt = new DataTable();
                dt.Load(ExecuteReader(cmd));
                return dt;
            }
        }

        public void Incident_Image_Delete(int imageID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Image_Delete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@imageID", SqlDbType.Int).Value = imageID;
                cn.Open();

                ExecuteNonQuery(cmd);
            }
        }

        public void Incident_Image_Set_Main_Photo(int incidentID,int imageID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Image_Set_Main_Photo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@incidentID", SqlDbType.Int).Value = incidentID;
                cmd.Parameters.Add("@imageID", SqlDbType.Int).Value = imageID;
                cn.Open();

                ExecuteNonQuery(cmd);
            }
        }

        public void Incident_Insert_Finalise(int Incident_ID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Insert_Finalise", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@incidentID", SqlDbType.Int).Value = Incident_ID;
                cn.Open();
                ExecuteNonQuery(cmd);
            }
        }

        public void lnk_Incident_Incident_Insert(int mainIncidentID, int relatedIncidentID, string linkageComment)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_lnk_Incident_Incident_Insert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@mainIncidentID", SqlDbType.Int).Value = mainIncidentID;
                cmd.Parameters.Add("@relatedIncidentID", SqlDbType.Int).Value = relatedIncidentID;
                cmd.Parameters.Add("@linkageComment", SqlDbType.VarChar).Value = linkageComment;
                cn.Open();
                ExecuteNonQuery(cmd);
            }
        }

        public void lnk_Incident_Incident_Approve(int mainIncidentID, int relatedIncidentID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_lnk_Incident_Incident_Approve", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@mainIncidentID", SqlDbType.Int).Value = mainIncidentID;
                cmd.Parameters.Add("@relatedIncidentID", SqlDbType.Int).Value = relatedIncidentID;
                cn.Open();
                ExecuteNonQuery(cmd);
            }
        }

        public void lnk_Incident_Incident_Delete(int relatedIncidentID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_lnk_Incident_Incident_Delete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@relatedIncidentID", SqlDbType.Int).Value = relatedIncidentID;
                cn.Open();
                ExecuteNonQuery(cmd);
            }
        }

        internal void Incident_Cancel_and_Delete(int incidentID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Cancel_and_Delete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@incident_ID", SqlDbType.Int).Value = incidentID;
                cn.Open();
                ExecuteNonQuery(cmd);
            }
        }

        internal DataTable Incident_Get_Main_Incident(int incident_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Get_Main_Incident", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@related_incident_ID", SqlDbType.Int).Value = incident_id;                
                cn.Open();

                DataTable dt = new DataTable();
                dt.Load(ExecuteReader(cmd));
                return dt;
            }
        }

        internal void Incident_Delete_By_Owner(int incident_id, string deleteReasonText)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("EyeD_Incident_Delete_By_Owner", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@incident_ID", SqlDbType.Int).Value = incident_id;
                cmd.Parameters.Add("@deleteReason", SqlDbType.VarChar).Value = deleteReasonText;
                cn.Open();
                ExecuteNonQuery(cmd);
            }
        }
    }
}