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

namespace Eyedentify
{
    public partial class ReportIncident : System.Web.UI.Page
    {
        private SqlIncidentProvider sip = new SqlIncidentProvider();
        private SqlUserProvider sup = new SqlUserProvider();
        private SqlStoreProvider ssp = new SqlStoreProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MembershipUser user = Membership.GetUser(User.Identity.Name);
                string userID = user.ProviderUserKey.ToString();

                StoreDetails loggedInUserStore = ssp.Store_Get_User_Store_Info(userID);

                PopulateLocationLabel(loggedInUserStore);
                PopulateIncidentTypeBox();
                PopulateOtherIncidentBox();

                int incidentID = Request.QueryString["iID"] == null ? -1 : int.Parse(Request.QueryString["iID"].ToString());

                if (loggedInUserStore.Store_ID == 0)
                {
                    Response.Redirect("ErrorPage.aspx?Error=MemberNoStore");
                }
                else
                {

                    if (incidentID == -1)
                    {
                        IncidentDetails id = new IncidentDetails(-1, userID, string.Empty, string.Empty, DateTime.Now, new List<IncidentTypeDetails> { },
                            new List<IncidentPeopleDetails> { }, 0, string.Empty, false, false, string.Empty, -1, string.Empty);
                        IncidentID.Text = sip.Incident_Insert(id).ToString();
                    }
                    else
                    {
                        IncidentDetails id = sip.Incident_Get_Details(incidentID);

                        if (userID != id.User_ID || id.Insert_Status == true)
                            Response.Redirect("MemberHome.aspx");
                        else
                        {
                            IncidentID.Text = incidentID + "";
                            BindImageGridData();

                            PopulatePageDetails(id);
                        }
                    }
                }
            }
        }

        private void PopulateLocationLabel(StoreDetails loggedInUserStore)
        {
            AddressIDLabel.Text = loggedInUserStore.Store_Address.Address_ID.ToString();

            LocationLabel.Text = loggedInUserStore.Store_Name + ", " + loggedInUserStore.Store_Address.Suburb + ", " + loggedInUserStore.Store_Address.City + ", " + loggedInUserStore.Store_Address.Country;
        }

        private void PopulatePageDetails(IncidentDetails id)
        {
            IncidentDateTime.Text = id.Incident_datetime.Day + "-" + id.Incident_datetime.Month + "-" + id.Incident_datetime.Year + " " + id.Incident_datetime.ToShortTimeString().Replace(".", "");

            foreach (IncidentTypeDetails itd in id.Incident_types)
            {
                IncidentTypeListBox.Items.FindByText(itd.Incident_Type_Name).Selected = true;
            }

            if (IncidentTypeListBox.SelectedIndex > 0)
            {
                if (IncidentTypeListBox.SelectedItem.Text == "Other")
                {
                    OtherIncidentTypePanel.Visible = true;
                    OtherIncidentyTypeBox.Text = id.Other_Incident_Type;
                }
            }

            DescriptionBox.Text = id.Description;

            NoPeopleDropDown.Items.FindByValue(id.People_Involved + "").Selected = true;
            if (id.People_Involved > 0)
            {
                PeopleBindData(id.Incident_people);
            }

            DataTable dt = sip.Incident_Get_Main_Incident(id.Incident_ID);

            if (dt.Rows.Count > 0)
            {
                OtherIncidentsListBox.SelectedValue = dt.Rows[0]["Main_Incident_ID"].ToString();

                string otherIncident = OtherIncidentsListBox.SelectedItem.Text;
                LinkOtherIncidentLinkButton.Text = "Click here to link to other incident. (Currently linked to \"" + otherIncident + "\" incident)";
            }
        }

        private void PopulateOtherIncidentBox()
        {
            int incidentID = int.Parse(IncidentID.Text);
            OtherIncidentsListBox.DataSource = sip.Incident_Incident_Get_Valid(incidentID);
            OtherIncidentsListBox.DataValueField = "Incident_ID";
            OtherIncidentsListBox.DataTextField = "Subject";
            OtherIncidentsListBox.DataBind();
        }

        private void PopulateIncidentTypeBox()
        {
            IncidentTypeListBox.DataSource = sip.Incident_Type_Get_All();
            IncidentTypeListBox.DataValueField = "Incident_Type_ID";
            IncidentTypeListBox.DataTextField = "Incident_Type_Name";
            IncidentTypeListBox.DataBind();
        }

        private IncidentDetails PopulateIncidentDetails(int incidentID)
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            string usedID = user.ProviderUserKey.ToString();
            string datetime = IncidentDateTime.Text;

            string day = datetime.Split('-')[0];
            string month = datetime.Split('-')[1];
            string yearTime = datetime.Split('-')[2];
            datetime = day + "/" + month + "/" + yearTime;
            DateTime date = DateTime.Parse(datetime);

            int[] selectedtypes = IncidentTypeListBox.GetSelectedIndices();
            List<IncidentTypeDetails> types = new List<IncidentTypeDetails> { };

            foreach (int selectedIndex in selectedtypes)
            {
                types.Add(new IncidentTypeDetails(int.Parse(IncidentTypeListBox.Items[selectedIndex].Value), IncidentTypeListBox.Items[selectedIndex].Text));
            }

            List<IncidentPeopleDetails> people = GetPeopleDetails(incidentID);

            string otherIT = "";
            if (IncidentTypeListBox.SelectedIndex > -1)
            {
                if (IncidentTypeListBox.SelectedItem.Text == "Other")
                    otherIT = OtherIncidentyTypeBox.Text.Trim();
                else
                    otherIT = string.Empty;
            }

            string description = DescriptionBox.Text.ToString().Trim();

            string storeName = LocationLabel.Text.Substring(0, LocationLabel.Text.IndexOf(','));

            int NoOfPeopleInvolved = 0;

            if (NoPeopleDropDown.SelectedIndex > 0)
                NoOfPeopleInvolved = int.Parse(NoPeopleDropDown.SelectedValue);

            IncidentDetails id = new IncidentDetails(incidentID, usedID, string.Empty, description, date, types, people,
                NoOfPeopleInvolved, otherIT, false, false, string.Empty, int.Parse(AddressIDLabel.Text), storeName);
            return id;
        }

        private List<IncidentPeopleDetails> GetPeopleDetails(int incidentID)
        {
            List<IncidentPeopleDetails> people = new List<IncidentPeopleDetails> { };

            foreach (RepeaterItem ri in PeopleInvolvedRepeater.Items)
            {
                string gender = ((DropDownList)ri.FindControl("PersonGenderDropDown")).SelectedIndex > 1 ? ((DropDownList)ri.FindControl("PersonGenderDropDown")).SelectedItem.Text : null;
                string agegroup = ((DropDownList)ri.FindControl("PersonAgeGroupDropDown")).SelectedIndex > 1 ? ((DropDownList)ri.FindControl("PersonAgeGroupDropDown")).SelectedItem.Text : null;
                string ethnicity = ((DropDownList)ri.FindControl("PersonEthnicityDropDown")).SelectedIndex > 1 ? ((DropDownList)ri.FindControl("PersonEthnicityDropDown")).SelectedItem.Text : null;
                string height = ((DropDownList)ri.FindControl("PersonHeightDropDown")).SelectedIndex > 1 ? ((DropDownList)ri.FindControl("PersonHeightDropDown")).SelectedItem.Text : null;
                string build = ((DropDownList)ri.FindControl("PersonBuildDropDown")).SelectedIndex > 1 ? ((DropDownList)ri.FindControl("PersonBuildDropDown")).SelectedItem.Text : null;
                string persondesc = ((TextBox)ri.FindControl("PersonDescriptionBox")).Text;
                people.Add(new IncidentPeopleDetails(-1, incidentID, gender, agegroup, ethnicity, height, build, persondesc));
            }
            return people;
        }

        private void BindImageGridData()
        {
            int incidentID = int.Parse(IncidentID.Text);
            IncidentImageGrid.DataSource = sip.Incident_Images_Get(incidentID);
            IncidentImageGrid.DataBind();

            int imageCounter = IncidentImageGrid.Rows.Count;
            if (imageCounter == 0)
            {
                AddImageLinkButton.Text = "Click here to add photos.";
                IncidentImageGrid.Visible = false;
                lblMsg.Visible = false;
            }
            else if (imageCounter == 1)
            {
                AddImageLinkButton.Text = "Click here to add photos. (" + imageCounter + " image added)";
                IncidentImageGrid.Visible = true;
                lblMsg.Visible = true;
            }
            else
            {
                AddImageLinkButton.Text = "Click here to add photos. (" + imageCounter + " images added)";
                IncidentImageGrid.Visible = true;
                lblMsg.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                byte[] image = FileUpload1.FileBytes;
                string imageDescription = string.Empty; // ImageDescriptionBox.Text.Trim();
                /*Validation for file extension*/
                bool gif = FileUpload1.FileName.ToLower().Contains(".gif");
                bool png = FileUpload1.FileName.ToLower().Contains(".png");
                bool jpg = FileUpload1.FileName.ToLower().Contains(".jpg");
                bool jpeg = FileUpload1.FileName.ToLower().Contains(".jpeg");
                if (gif || png || jpg || jpeg)
                {
                    int s = IncidentImageGrid.Rows.Count;

                    bool mainPhoto = MainPhotoCheckBox.Checked;
                    if (s == 0)
                        mainPhoto = true;

                    int incidentID = int.Parse(IncidentID.Text);
                    sip.Incident_Image_Insert(incidentID, image, imageDescription, mainPhoto);

                    lblMsg.Text = "File Upload Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMsg.Text = "Valid File Extension allowed are {.gif,.png,jpg,.jpeg}";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "File Not Upload";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            BindImageGridData();
        }

        private void SaveIncident()
        {
            int incidentID = int.Parse(IncidentID.Text);
            IncidentDetails id = PopulateIncidentDetails(incidentID);

            sip.Incident_Update(id);

            if (OtherIncidentsListBox.SelectedIndex > -1)
            {
                sip.lnk_Incident_Incident_Delete(incidentID);
                sip.lnk_Incident_Incident_Insert(int.Parse(OtherIncidentsListBox.SelectedItem.Value), incidentID, "");
            }
        }

        protected void IncidentImageGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int imageID = int.Parse(e.Keys[0].ToString());
            sip.Incident_Image_Delete(imageID);
            BindImageGridData();
        }

        protected void IncidentImageGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName;

            if (command.Equals("MainPhoto"))
            {
                int imageID = int.Parse(e.CommandArgument.ToString());
                int incidentID = int.Parse(IncidentID.Text);
                sip.Incident_Image_Set_Main_Photo(incidentID, imageID);
            }
            BindImageGridData();
        }

        protected void CancelandDeleteButton_Click(object sender, EventArgs e)
        {
            sip.Incident_Cancel_and_Delete(int.Parse(IncidentID.Text));
            Response.Redirect("MemberHome.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            SaveIncident();
            Response.Redirect("MemberHome.aspx");
        }

        protected void SubmitButtom_Click(object sender, EventArgs e)
        {
            SaveIncident();
            Response.Redirect("ConfirmIncident.aspx?iID=" + IncidentID.Text);
        }

        protected void IncidentTypeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IncidentTypeListBox.SelectedItem.Text == "Other")
            {
                IncidentTypeListBox.SelectedIndex = IncidentTypeListBox.Items.Count - 1;
                OtherIncidentTypePanel.Visible = true;
            }
            else
                OtherIncidentTypePanel.Visible = false;
        }

        private void PeopleBindData(List<IncidentPeopleDetails> list)
        {
            PopulateRepeaters(list.Count);

            if (list.Count > 0)
            {
                PeopleTable.Visible = true;
                PeopleInvolvedInfoLabel.Visible = true;
            }

            int counter = 0;

            foreach (IncidentPeopleDetails ipd in list)
            {
                DropDownList genderDB = ((DropDownList)PeopleInvolvedRepeater.Items[counter].FindControl("PersonGenderDropDown"));
                DropDownList ageDB = ((DropDownList)PeopleInvolvedRepeater.Items[counter].FindControl("PersonAgeGroupDropDown"));
                DropDownList ethDB = ((DropDownList)PeopleInvolvedRepeater.Items[counter].FindControl("PersonEthnicityDropDown"));
                DropDownList heightDB = ((DropDownList)PeopleInvolvedRepeater.Items[counter].FindControl("PersonHeightDropDown"));
                DropDownList buildDB = ((DropDownList)PeopleInvolvedRepeater.Items[counter].FindControl("PersonBuildDropDown"));

                genderDB.Items.FindByText(ipd.Gender).Selected = true;
                ageDB.Items.FindByText(ipd.Age_Group).Selected = true;
                ethDB.Items.FindByText(ipd.Ethnicity).Selected = true;
                heightDB.Items.FindByText(ipd.Height).Selected = true;
                buildDB.Items.FindByText(ipd.Build).Selected = true;
                ((TextBox)PeopleInvolvedRepeater.Items[counter].FindControl("PersonDescriptionBox")).Text = ipd.Description;

                if (genderDB.SelectedIndex == 1)
                    genderDB.SelectedIndex = 0;
                if (ageDB.SelectedIndex == 1)
                    ageDB.SelectedIndex = 0;
                if (ethDB.SelectedIndex == 1)
                    ethDB.SelectedIndex = 0;
                if (heightDB.SelectedIndex == 1)
                    heightDB.SelectedIndex = 0;
                if (buildDB.SelectedIndex == 1)
                    buildDB.SelectedIndex = 0;

                counter++;
            }
        }

        protected void NoPeopleDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NoPeopleDropDown.SelectedIndex > 1)
            {
                PopulateRepeaters(int.Parse(NoPeopleDropDown.SelectedItem.Value));
                PeopleTable.Visible = true;
                PeopleInvolvedInfoLabel.Visible = true;
            }
            else
            {
                PeopleTable.Visible = false;
                PeopleInvolvedInfoLabel.Visible = false;
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

        protected void LinkRelatedIncidentButton_Click(object sender, EventArgs e)
        {
            if (OtherIncidentsListBox.SelectedIndex > -1)
            {
                string otherIncident = OtherIncidentsListBox.SelectedItem.Text;
                LinkOtherIncidentLinkButton.Text = "Click here to link to other incident. (Currently linked to \"" + otherIncident + "\" incident)";
            }
            else
            {
                LinkOtherIncidentLinkButton.Text = "Click here to link to other incident.";
            }

            ModalPopupExtender2.Hide();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int incidentID = int.Parse(IncidentID.Text);
            sip.Incident_Delete_By_Owner(incidentID, string.Empty);
        }
    }
}