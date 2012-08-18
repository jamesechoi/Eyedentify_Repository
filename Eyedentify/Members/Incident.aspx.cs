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
using Artem.Google.UI;

namespace Eyedentify
{
    public partial class Incident : System.Web.UI.Page
    {
        private SqlIncidentProvider sip = new SqlIncidentProvider();
        private SqlStoreProvider ssp = new SqlStoreProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int incidentID = GetIncidentID();

                IncidentDetails id = sip.Incident_Get_Details(incidentID);

                CheckIncidentStatus(id);

                MembershipUser user = Membership.GetUser(User.Identity.Name);
                string userID = user.ProviderUserKey.ToString();

                AdddressDetails incidentAddr = sip.Incident_Get_Address(id.Incident_ID);
                StoreDetails loggedinUserAddress = ssp.Store_Get_User_Store_Info(userID);

                if (loggedinUserAddress.Store_ID == 0)
                {
                    Response.Redirect("ErrorPage.aspx?Error=MemberNoStore");
                }
                else
                {
                    if (userID.Equals(id.User_ID))
                    {
                        //PopulateIncidentReporterBox(id);
                    }
                    else
                    {
                        //OwnerPanel.Visible = false;
                    }
                    PopulateImageView(incidentID);
                    PopulateLabels(id, userID, incidentAddr);
                    PopulateCommentsList(incidentID);
                    PopulateMap(id, incidentAddr, loggedinUserAddress);
                }
            }

        }

        private void PopulateMap(IncidentDetails incidentDetails, AdddressDetails incidentAddr, StoreDetails loggedinUserAddress)
        {
            IncidentMap.MapType = MapType.Roadmap;
            IncidentMap.EnterpriseKey = Utility.GetGoogleAPIKey();
            IncidentMap.Latitude = incidentAddr.Latitude;
            IncidentMap.Longitude = incidentAddr.Longitude;
            IncidentMap.Zoom = Utility.GetGoogleZoomLevel(incidentAddr.Latitude, incidentAddr.Longitude, loggedinUserAddress.Store_Address.Latitude, loggedinUserAddress.Store_Address.Longitude);

            MarkerImage incidentMarkerIamge = new MarkerImage();
            incidentMarkerIamge.Url = "http://www.eyedentify.co.nz/Images/Map/map_Incident_Icon.jpg";

            MarkerImage homeMarkerIamge = new MarkerImage();
            homeMarkerIamge.Url = "http://www.eyedentify.co.nz/Images/Map/map_Home_Icon.png";

            Marker incidentMarker = new Marker();
            incidentMarker.Address = incidentAddr.Latitude + " " + incidentAddr.Longitude;
            incidentMarker.Info = incidentDetails.Description;
            incidentMarker.Icon = incidentMarkerIamge;
            IncidentMap.Markers.Add(incidentMarker);

            Marker yourAddressMarker = new Marker();
            yourAddressMarker.Address = loggedinUserAddress.Store_Address.Latitude + " " + loggedinUserAddress.Store_Address.Longitude;
            yourAddressMarker.Info = "Your Store";
            yourAddressMarker.Icon = homeMarkerIamge;
            IncidentMap.Markers.Add(yourAddressMarker);
        }

        private void PopulateCommentsList(int incidentID)
        {
            CommentsList.DataSource = sip.Incident_Get_Comments(incidentID);
            CommentsList.DataBind();
        }

        private void PopulateImageView(int incidentID)
        {
            DataTable imageList = sip.Incident_Images_Get(incidentID);
            DataGridImage.DataSource = imageList;
            DataGridImage.DataBind();

            DataGridThumbnail.DataSource = imageList;
            DataGridThumbnail.DataBind();

            PopulateImagePopupBox(imageList);
        }

        public string getSRC(object imgSRC)
        {
            DataRowView dRView = (DataRowView)imgSRC;
            return dRView["Incident_Image_ID"].ToString();

        }

        private void PopulateLabels(IncidentDetails id, string userID, AdddressDetails incidentAddr)
        {
            if (!id.Store_Name.Equals(string.Empty))
                LocationLabel.Text = id.Store_Name + ", ";
            else
                LocationLabel.Text = string.Empty;
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

        protected void SubmitComment_Click(object sender, EventArgs e)
        {
            int incident_ID = GetIncidentID();
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            string userID = user.ProviderUserKey.ToString();

            sip.Incident_Comment_Insert(incident_ID, CommentBox.Text.Trim(), userID);
            CommentBox.Text = string.Empty;
            PopulateCommentsList(incident_ID);
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