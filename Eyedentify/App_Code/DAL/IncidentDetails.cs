using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Eyedentify.App_Code
{
    public class IncidentDetails
    {
        public IncidentDetails()
        {
        }

        public IncidentDetails(int incident_ID, string user_ID, string subject, string description, DateTime incident_datetime,
            List<IncidentTypeDetails> incident_types, List<IncidentPeopleDetails> incident_people, int people_involved, string other_incident_type,
            bool insert_status, bool delete_status, string delete_reason, int address_ID, string store_name)
        {
            this.Incident_ID = incident_ID;
            this.User_ID = user_ID;
            this.Subject = subject;
            this.Description = description;
            this.Incident_datetime = incident_datetime;
            this.Incident_types = incident_types;
            this.Incident_people = incident_people;
            this.People_Involved = people_involved;
            this.Other_Incident_Type = other_incident_type;
            this.Insert_Status = insert_status;
            this.Delete_Status = delete_status;
            this.Delete_Reason = delete_reason;
            this.Address_ID = address_ID;
            this.Store_Name = store_name;
        }

        private int _incident_ID = 0;
        public int Incident_ID
        {
            get { return _incident_ID; }
            set { _incident_ID = value; }
        }

        private string _user_ID = string.Empty;
        public string User_ID
        {
            get { return _user_ID; }
            set { _user_ID = value; }
        }

        private string _subject = string.Empty;
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private DateTime _incident_datetime = DateTime.MinValue;
        public DateTime Incident_datetime
        {
            get { return _incident_datetime; }
            set { _incident_datetime = value; }
        }

        private List<IncidentTypeDetails> _incident_types = new List<IncidentTypeDetails> { };
        public List<IncidentTypeDetails> Incident_types
        {
            get { return _incident_types; }
            set { _incident_types = value; }
        }

        private List<IncidentPeopleDetails> _incident_people = new List<IncidentPeopleDetails> { };
        public List<IncidentPeopleDetails> Incident_people
        {
            get { return _incident_people; }
            set { _incident_people = value; }
        }

        private int _people_involved = 0;
        public int People_Involved
        {
            get { return _people_involved; }
            set { _people_involved = value; }
        }

        private string _other_incident_type = string.Empty;
        public string Other_Incident_Type
        {
            get { return _other_incident_type; }
            set { _other_incident_type = value; }
        }

        private bool _insert_status = false;
        public bool Insert_Status
        {
            get { return _insert_status; }
            set { _insert_status = value; }
        }

        private string _delete_reason = string.Empty;
        public string Delete_Reason
        {
            get { return _delete_reason; }
            set { _delete_reason = value; }
        }

        private bool _delete_status = false;
        public bool Delete_Status
        {
            get { return _delete_status; }
            set { _delete_status = value; }
        }

        private int _address_ID = 0;
        public int Address_ID
        {
            get { return _address_ID; }
            set { _address_ID = value; }
        }

        private string _store_name = string.Empty;
        public string Store_Name
        {
            get { return _store_name; }
            set { _store_name = value; }
        }

        public static IncidentDetails GetIncidentDetailsFromReader(IDataReader reader)
        {
            IncidentDetails incidentDetailsCol = new IncidentDetails();
            while (reader.Read())
                incidentDetailsCol = GetIncidentDetails(reader);
            return incidentDetailsCol;
        }

        private static IncidentDetails GetIncidentDetails(IDataReader reader)
        {
            IncidentDetails incidentDetails = new IncidentDetails(
                int.Parse(reader["Incident_ID"].ToString()),
                reader["User_ID"].ToString(),
                reader["Subject"].ToString(),
                reader["Description"].ToString(),
               (DateTime)reader["Incident_DateTime"],
                 new List<IncidentTypeDetails> { },
                  new List<IncidentPeopleDetails> { },
                 int.Parse(reader["People_Involved"].ToString()),
                 reader["Other_Incident_Type"].ToString(),
                (bool)reader["Insert_Status"],
                (bool)reader["Delete_Status"],
                 reader["Delete_Reason"].ToString(),
                 int.Parse(reader["Address_ID"].ToString()),
                 reader["Store_Name"].ToString()
                );
            return incidentDetails;
        }
    }
}