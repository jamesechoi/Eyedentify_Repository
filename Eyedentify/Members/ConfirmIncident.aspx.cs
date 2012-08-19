using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eyedentify.App_Code;
using System.Web.Security;
using System.IO;
using System.Data;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Eyedentify
{
    public partial class ConfirmIncident : System.Web.UI.Page
    {
        private SqlIncidentProvider sip = new SqlIncidentProvider();
                private SqlStoreProvider ssp = new SqlStoreProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MembershipUser user = Membership.GetUser(User.Identity.Name);
                string usedID = user.ProviderUserKey.ToString();

                int incidentID = Request.QueryString["iID"] == null ? -1 : int.Parse(Request.QueryString["iID"].ToString());

                if (incidentID == -1)
                {
                    Response.Redirect("MemberHome.aspx");
                }
                else
                {
                    DataTable imageTable = sip.Incident_Images_Get(incidentID);
                    DataGridImage.DataSource = imageTable;
                    DataGridImage.DataBind();

                    DataGridThumbnail.DataSource = imageTable;
                    DataGridThumbnail.DataBind();

                    IncidentDetails id = sip.Incident_Get_Details(incidentID);

                    PopulateImagePopupBox(imageTable);

                    if (usedID != id.User_ID || id.Insert_Status == true)
                        Response.Redirect("MemberHome.aspx");
                    else
                    {
                        PopulateLabels(id, usedID);
                    }
                }
            }
        }

        public string getSRC(object imgSRC)
        {
            DataRowView dRView = (DataRowView)imgSRC;
            return dRView["Incident_Image_ID"].ToString();

        }

        private void PopulateLabels(IncidentDetails id, string userID)
        {
            AdddressDetails incidentAddr = sip.Incident_Get_Address(id.Incident_ID);

            StoreDetails sd = ssp.Store_Get_User_Store_Info(userID);
            LocationLabel.Text = string.Empty;

            if (incidentAddr.Address_ID.Equals(sd.Store_Address.Address_ID))
                LocationLabel.Text = sd.Store_Name + ", ";

            LocationLabel.Text = LocationLabel.Text + incidentAddr.Suburb + ", " + incidentAddr.City + ", " + incidentAddr.Country;
            
            TimeLabel.Text = id.Incident_datetime.ToShortDateString() + " " + id.Incident_datetime.ToShortTimeString();

            DescriptionLabel.Text = id.Description;
            if (id.People_Involved == 0)
            {
                PeopleInvolvedLabel.Text = "No details of involved offenders";
            }
            else
            {
                PeopleInvolvedLabel.Text = id.People_Involved + "";
                PeopleBindData(id.Incident_people);
            }

            List<IncidentTypeDetails> itds = sip.Incident_Get_Types(id.Incident_ID);
            string incidentTypeString = string.Empty;
            if (itds.Count == 0)
            {
                incidentTypeString = "type of incident not defined";
            }
            else
            {
                foreach (IncidentTypeDetails itd in itds)
                {
                    incidentTypeString += itd.Incident_Type_Name + ", ";
                }
            }
            TypeLabel.Text = incidentTypeString.Substring(0, incidentTypeString.Length - 2);
        }



        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportIncident.aspx?iID=" + Request.QueryString["iID"].ToString());
        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            int incidentID = int.Parse(Request.QueryString["iID"].ToString());
            sip.Incident_Insert_Finalise(incidentID);
            Response.Redirect("MemberHome.aspx");
        }

        private void PeopleBindData(List<IncidentPeopleDetails> list)
        {
            PopulateRepeaters(list.Count);

            int counter = 0;

            foreach (IncidentPeopleDetails ipd in list)
            {
                if (!ipd.Gender.Equals(string.Empty))
                    ((Label)PeopleInvolvedRepeater.Items[counter].FindControl("PersonGenderLabel")).Text = "<b>Gender:</b> " + ipd.Gender + " ";
                if (!ipd.Age_Group.Equals(string.Empty))
                    ((Label)PeopleInvolvedRepeater.Items[counter].FindControl("PersonAgeGroupLabel")).Text = "<b>Age Group:</b> " + ipd.Age_Group + " ";
                if (!ipd.Ethnicity.Equals(string.Empty))
                    ((Label)PeopleInvolvedRepeater.Items[counter].FindControl("PersonEthnicityLabel")).Text = "<b>Ethnicity:</b> " + ipd.Ethnicity + " ";
                if (!ipd.Height.Equals(string.Empty))
                    ((Label)PeopleInvolvedRepeater.Items[counter].FindControl("PersonHeightLabel")).Text = "<b>Height:</b> " + ipd.Height + " ";
                if (!ipd.Build.Equals(string.Empty))
                    ((Label)PeopleInvolvedRepeater.Items[counter].FindControl("PersonBuildLabel")).Text = "<b>Build:</b> " + ipd.Build + " ";
                if (!ipd.Description.Equals(string.Empty))
                    ((Label)PeopleInvolvedRepeater.Items[counter].FindControl("PersonDescriptionLabel")).Text = "<b>Description:</b> " + ipd.Description + " ";
                counter++;
            }
        }

        private void PopulateRepeaters(int noOfRepeaters)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("person_order_id");

            for (int i = 1; i <= noOfRepeaters; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i + "";
                dt.Rows.Add(dr);
            }
            PeopleInvolvedRepeater.DataSource = dt;
            PeopleInvolvedRepeater.DataBind();
        }

        private void PopulateImagePopupBox(DataTable dt)
        {
            int counter = dt.Rows.Count;
            if (counter > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("BYLINE_POSITION_RIGHT = 150;");
                sb.Append("BYLINE_POSITION_BOTTOM = 50;");
                sb.Append("var viewer = new PhotoViewer();");
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("viewer.add('ImagePage.aspx?type=full&imgID=" + dr["Incident_Image_ID"].ToString() + "');");
                }
                ClientScript.RegisterStartupScript(typeof(Page), "MyScript", sb.ToString(), true);
            }
        }
    }
}

