using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eyedentify.App_Code.DAL.SQLProvider
{
    class IncidentTypeDetails
    {
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
    }
}
