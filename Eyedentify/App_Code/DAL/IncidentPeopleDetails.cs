using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Eyedentify.App_Code
{
    public class IncidentPeopleDetails
    {
        public IncidentPeopleDetails(int people_ID, int incident_ID, string gender, string age_group, 
            string ethnicity, string height, string build, string description)
        {
            this.People_ID = people_ID;
            this.Incident_ID = incident_ID;
            this.Gender = gender;
            this.Age_Group = age_group;
            this.Ethnicity = ethnicity;
            this.Height = height;
            this.Build = build;
            this.Description = description;
        }

        private int _people_ID = 0;
        public int People_ID
        {
            get { return _people_ID; }
            set { _people_ID = value; }
        }

        private int _incident_ID = 0;
        public int Incident_ID
        {
            get { return _incident_ID; }
            set { _incident_ID = value; }
        }

        private string _gender = string.Empty;
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        private string _age_group = string.Empty;
        public string Age_Group
        {
            get { return _age_group; }
            set { _age_group = value; }
        }

        private string _ethnicity = string.Empty;
        public string Ethnicity
        {
            get { return _ethnicity; }
            set { _ethnicity = value; }
        }

        private string _height = string.Empty;
        public string Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private string _build = string.Empty;
        public string Build
        {
            get { return _build; }
            set { _build = value; }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


        private static IncidentPeopleDetails GetIncidentTypes(IDataReader reader)
        {
            IncidentPeopleDetails incidentPeople = new IncidentPeopleDetails(
                int.Parse(reader["Incident_People_ID"].ToString()),
                int.Parse(reader["Incident_ID"].ToString()),
                reader["Gender"].ToString(),
                reader["Age_Group"].ToString(),
                reader["Ethnicity"].ToString(),
                reader["Height"].ToString(),
                reader["Build"].ToString(),
                reader["Description"].ToString()
                );
            return incidentPeople;
        }

        public static List<IncidentPeopleDetails> GetIncidentTypesCollectionFromReader(IDataReader reader)
        {
            List<IncidentPeopleDetails> IncidentTypesCol = new List<IncidentPeopleDetails>();
            while (reader.Read())
                IncidentTypesCol.Add(GetIncidentTypes(reader));
            return IncidentTypesCol;
        }
    }
}