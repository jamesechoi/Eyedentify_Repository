using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Eyedentify.App_Code
{
    public class IncidentTypeDetails
    {
        public IncidentTypeDetails()
        {
        }

        public IncidentTypeDetails(int incident_Type_ID, string incident_Type_Name)
        {
            this.Incident_Type_ID = incident_Type_ID;
            this.Incident_Type_Name = incident_Type_Name;
        }

        private int _incident_Type_ID = 0;
        public int Incident_Type_ID
        {
            get { return _incident_Type_ID; }
            set { _incident_Type_ID = value; }
        }

        private string _incident_Type_Name = string.Empty;
        public string Incident_Type_Name
        {
            get { return _incident_Type_Name; }
            set { _incident_Type_Name = value; }
        }

        private static IncidentTypeDetails GetIncidentTypes(IDataReader reader)
        {
            IncidentTypeDetails incidentTypeD = new IncidentTypeDetails(
                int.Parse(reader["Incident_Type_ID"].ToString()),
                reader["Incident_Type_Name"].ToString()
                );
            return incidentTypeD;
        }

        public static List<IncidentTypeDetails> GetIncidentTypesCollectionFromReader(IDataReader reader)
        {
            List<IncidentTypeDetails> IncidentTypesCol = new List<IncidentTypeDetails>();
            while (reader.Read())
                IncidentTypesCol.Add(GetIncidentTypes(reader));
            return IncidentTypesCol;
        }
    }
}
