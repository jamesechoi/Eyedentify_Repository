using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eyedentify.App_Code;
using System.Web.Security;

namespace Eyedentify
{
    public partial class MemberHome : System.Web.UI.Page
    {
        private SqlIncidentProvider sip = new SqlIncidentProvider();
        private SqlStoreProvider ssp = new SqlStoreProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string searchType = GetSearchType();

                MembershipUser user = Membership.GetUser(User.Identity.Name);
                string userID = user.ProviderUserKey.ToString();

                PopulateIncidentTypeBox();

                if (searchType.Equals("myAcct"))
                {
                    string searchString = GetSearchString();
                    if (searchString.Equals("myIncidents"))
                    {
                        PopulateMyIncidentsGridView(userID);
                    }
                    else if (searchString.Equals("unfinished"))
                    {
                        PopulateMyUnfinishedIncidentsGridView(userID);
                    }
                }
                else if (searchType.Equals("filter"))
                {
                    PopulateFilterResultIncidentsGridView(userID);                    
                }
                else
                {
                    PopulateNoSearchCategoryGridView();
                }
            }
        }

        private void PopulateFilterResultIncidentsGridView(string userID)
        {
            string distanceString = GetDistanceString();
            if (!distanceString.Equals(string.Empty))
            {
                StoreDetails loggedInUserStore = ssp.Store_Get_User_Store_Info(userID);

                double latDeltaRange = Utility.GetLatitudeDeltaRangeInDegrees(double.Parse(distanceString));
                double lonDeltaRange = Utility.GetLongitudeDeltaRange(double.Parse(distanceString), loggedInUserStore.Store_Address.Latitude);

                double lat_min = loggedInUserStore.Store_Address.Latitude - latDeltaRange;
                double lat_max = loggedInUserStore.Store_Address.Latitude + latDeltaRange;

                double lon_min = loggedInUserStore.Store_Address.Longitude - lonDeltaRange;
                double lon_max = loggedInUserStore.Store_Address.Longitude + lonDeltaRange;

                GridViewList.DataSource = sip.Incident_Get_For_Filtered_Incidents(lat_min, lat_max, lon_min, lon_max);
                GridViewList.DataBind();
            }
        }

        private void PopulateIncidentTypeBox()
        {
            IncidentTypeListBox.DataSource = sip.Incident_Type_Get_All();
            IncidentTypeListBox.DataValueField = "Incident_Type_ID";
            IncidentTypeListBox.DataTextField = "Incident_Type_Name";
            IncidentTypeListBox.DataBind();
        }

        private void PopulateMyIncidentsGridView(string userID)
        {
            GridViewList.DataSource = sip.Incident_Get_For_My_Incidents(userID);
            GridViewList.DataBind();
        }

        private void PopulateMyUnfinishedIncidentsGridView(string userID)
        {
            GridViewList.DataSource = sip.Incident_Get_For_Unfinished_Incidents(userID);
            GridViewList.DataBind();
        }

        private void PopulateNoSearchCategoryGridView()
        {
            GridViewList.DataSource = sip.Incident_Get_For_Home_Page();
            GridViewList.DataBind();
        }

        private string GetDistanceString()
        {
            return Request.QueryString["dist"] == null ? string.Empty : Request.QueryString["dist"];
        }

        private string GetSearchString()
        {
            return Request.QueryString["search"] == null ? string.Empty : Request.QueryString["search"];
        }

        private string GetSearchType()
        {
            return Request.QueryString["type"] == null ? string.Empty : Request.QueryString["type"];
        }

        public string GetHyperLink()
        {
            string searchType = GetSearchType();
            string searchString = GetSearchString();

            MembershipUser user = Membership.GetUser(User.Identity.Name);
            string userID = user.ProviderUserKey.ToString();

            if (searchType.Equals("myAcct") && searchString.Equals("unfinished"))
            {
                return "ReportIncident.aspx?iID=";
            }
            else
            {
                return "Incident.aspx?iID=";
            }
        }

        public string GetImageString(object imageID)
        {
            if (imageID.ToString().Trim() == string.Empty)
                return "../Images/no-image.gif";
            else
                return "ImagePage.aspx?imgID=" + imageID.ToString() + "&type=medium";
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            string queryString = string.Empty;

            int[] selectedtypes = IncidentTypeListBox.GetSelectedIndices();
            string incidentTypeQueryString = string.Empty;
            int counter = 0;
            foreach (int selectedIndex in selectedtypes)
            {
                if (counter == 0)
                    incidentTypeQueryString = "iTypes=";
                incidentTypeQueryString = incidentTypeQueryString + IncidentTypeListBox.Items[selectedIndex].Value + ",";
                counter++;
            }
            incidentTypeQueryString = incidentTypeQueryString.TrimEnd(',');

            string distanceQueryString = string.Empty;
            if (DistanceFilterDropdown.SelectedIndex > 0)
            {
                distanceQueryString = "dist=" + DistanceFilterDropdown.SelectedValue;
            }

            queryString = (incidentTypeQueryString + "&" + distanceQueryString).TrimEnd('&').TrimStart('&');

            if (!queryString.Equals(string.Empty))
                Response.Redirect("MemberHome.aspx?type=filter&" + queryString);
        }
    }
}