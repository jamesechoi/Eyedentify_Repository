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
    public partial class Incident : System.Web.UI.Page
    {
        private SqlIncidentProvider sip = new SqlIncidentProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int incidentID = GetIncidentID();

                IncidentDetails id = sip.Incident_Get_Details(incidentID);

                CheckIncidentStatus(id);

                MembershipUser user = Membership.GetUser(User.Identity.Name);
                string userID = user.ProviderUserKey.ToString();

                if (userID.Equals(id.User_ID))
                {
                    //PopulateIncidentReporterBox(id);
                }
                else
                {
                    //OwnerPanel.Visible = false;
                }

                DataTable imageList = sip.Incident_Images_Get(incidentID);
                DataGridImage.DataSource = imageList;
                DataGridImage.DataBind();
                DataGridThumbnail.DataSource = imageList;
                DataGridThumbnail.DataBind();
                PopulateImagePopupBox(sip.Incident_Images_Get(incidentID));
                PopulateLabels(id);
            }

        }



        public string getHREF(object sURL)
        {
            DataRowView dRView = (DataRowView)sURL;
            return dRView["ImageID"].ToString();
        }

        public string getSRC(object imgSRC)
        {
            DataRowView dRView = (DataRowView)imgSRC;
            return dRView["Incident_Image_ID"].ToString();

        }
        public string getHREF1(object sURL)
        {
            DataRowView dRView = (DataRowView)sURL;
            return dRView["ImageID"].ToString();
        }

        public string getSRC1(object imgSRC)
        {
            DataRowView dRView = (DataRowView)imgSRC;
            return dRView["ImageID"].ToString();

        }

        private void PopulateLabels(IncidentDetails id)
        {
            LocationLabel.Text = "Bling, Newmarket, Auckland";
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
            Response.Redirect("MemberHome.aspx");
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportIncident.aspx?iID=" + Request.QueryString["iID"].ToString());
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

        //private void PopulateXMLFile(DataTable dt)
        //{
        //    using (XmlWriter writer = XmlWriter.Create(@"C:/Users/James/Desktop/roject - Eyedentify/Code - Eyedentify/Eyedentify/Members/ImageXMLFile.xml"))
        //    {
        //        writer.WriteStartDocument();
        //        writer.WriteStartElement("PageTitle");

        //        foreach (DataRow dr in dt.Rows)
        //        {


        //            writer.WriteStartElement("Image");

        //            writer.WriteElementString("ImageID", dr["Incident_Image_ID"].ToString());

        //            writer.WriteEndElement();
        //        }

        //        writer.WriteEndElement();
        //        writer.WriteEndDocument();
        //    }
        //}


        private void CheckIncidentStatus(IncidentDetails id)
        {
            if (id.Delete_Status || !id.Insert_Status)
                Response.Redirect("MemberHome.aspx");
        }

        private int GetIncidentID()
        {
            string test = Request.QueryString["iID"] == null ? string.Empty : Request.QueryString["iID"];
            return int.Parse(test);
        }

        /*
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            if (DeleteReasonTextBox.Text.Trim().Length > 0)
            {
                sip.Incident_Delete_By_Owner(GetIncidentID(), DeleteReasonTextBox.Text.Trim());
                Response.Redirect("MemberHome.aspx");
            }
        }
         * */
        protected void SubmitComment_Click(object sender, EventArgs e)
        {
            //1. Save the comment to the database
            //2. Retrive all comments associated with this incident and display them - use <asp:Repeater>
            string commentText = comment.Text;
            commentLabel.Text = commentText;
            comment.Text = "";
        }
    }

}