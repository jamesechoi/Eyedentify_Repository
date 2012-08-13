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

                    IncidentDetails id = sip.Incident_Get_Details(incidentID);

                    if (usedID != id.User_ID || id.Insert_Status == true)
                        Response.Redirect("MemberHome.aspx");
                    else
                    {
                        DataTable imageList = sip.Incident_Images_Get(incidentID);
                        DataGridImage.DataSource = imageList;
                        DataGridImage.DataBind();
                        DataGridThumbnail.DataSource = imageList;
                        DataGridThumbnail.DataBind();
                        PopulateImagePopupBox(sip.Incident_Images_Get(incidentID));

                        PopulateLabels(id);
                        
                    }
                }
            }
        }


        public string getSRC(object imgSRC)
        {
            DataRowView dRView = (DataRowView)imgSRC;
            return dRView["Incident_Image_ID"].ToString();

        }


        private void PopulateLabels(IncidentDetails id)
        {
            LocationLabel.Text = "Bling, Newmarket, Auckland";
            TimeLabel.Text = id.Incident_datetime.ToShortDateString() + " " + id.Incident_datetime.ToShortTimeString();

            DescriptionLabel1.Text = id.Description;
            if (id.People_Involved == 0){
                PeopleInvolvedLabel.Text = "No details of involved offenders";
            }else{
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
                if (ipd.Gender.Equals("M")) ipd.Gender = "Male";
                else ipd.Gender = "Female";
                ((Label)PeopleInvolvedRepeater.Items[counter].FindControl("PersonGenderBox")).Text = ipd.Gender;
                ((Label)PeopleInvolvedRepeater.Items[counter].FindControl("PersonAgeGroupBox")).Text = ipd.Age_Group;
                ((Label)PeopleInvolvedRepeater.Items[counter].FindControl("PersonDescriptionBox")).Text = ipd.Description;
                counter++;
            }
        }

        private void PopulateRepeaters(int noOfRepeaters)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("person_order_id");
            dt.Columns.Add("Gender");
            dt.Columns.Add("Age_Group");
            dt.Columns.Add("Description");

            for (int i = 1; i <= noOfRepeaters; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i + "";
                dt.Rows.Add(dr);
            }
            PeopleInvolvedRepeater.DataSource = dt;
            PeopleInvolvedRepeater.DataBind();

        }

    }
}

