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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateIncidentTypeBox();
                PopulateOtherIncidentBox();

                MembershipUser user = Membership.GetUser(User.Identity.Name);
                string usedID = user.ProviderUserKey.ToString();

                int incidentID = Request.QueryString["iID"] == null ? -1 : int.Parse(Request.QueryString["iID"].ToString());

                if (incidentID == -1)
                {
                    IncidentDetails id = new IncidentDetails(-1, usedID, string.Empty, string.Empty, DateTime.Now, new List<IncidentTypeDetails> { }, 
                        new List<IncidentPeopleDetails> { }, 0, string.Empty, false,false,string.Empty,-1);
                    IncidentID.Text = sip.Incident_Insert(id).ToString();
                }
                else
                {
                    IncidentDetails id = sip.Incident_Get_Details(incidentID);

                    if (usedID != id.User_ID || id.Insert_Status == true)
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

        private void PopulatePageDetails(IncidentDetails id)
        {
            //IncidentDate.Text = id.Incident_datetime.Date.ToString("dd/MM/yyyy");
            //IncidentTimeDropdown.Items.FindByText(id.Incident_datetime.ToString("HH:mm")).Selected = true;

            foreach(IncidentTypeDetails itd in id.Incident_types)
            {
                IncidentTypeListBox.Items.FindByText(itd.Incident_Type_Name).Selected = true;
            }

            if (IncidentTypeListBox.SelectedItem.Text == "Other")
            {
                OtherIncidentTypePanel.Visible = true;
                OtherIncidentyTypeBox.Text = id.Other_Incident_Type;
            }

            DescriptionBox.Text = id.Description;

            NoPeopleDropDown.Items.FindByValue(id.People_Involved + "").Selected = true;
            if (id.People_Involved > 0)
            {
                //AddPeopleLinkButton.Enabled = true;
                //ModalPopupExtender1.Enabled = true;

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
            //string subject = ""; // no longer used

            string datetime = IncidentDateTime.Text;

            //string datetime = Request.Form["IncidentDateTime"];
            string month = datetime.Split('/')[0];
            string day = datetime.Split('/')[1];
            string yearTime = datetime.Split('/')[2];
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

            int NoOfPeopleInvolved = int.Parse( NoPeopleDropDown.SelectedValue);

            IncidentDetails id = new IncidentDetails(incidentID, usedID, string.Empty, description, date, types, people,
                NoOfPeopleInvolved, otherIT, false, false, string.Empty, sup.User_Get_Store_ID(usedID));
            return id;
        }

        private List<IncidentPeopleDetails> GetPeopleDetails(int incidentID)
        {
            List<IncidentPeopleDetails> people = new List<IncidentPeopleDetails> { };

            foreach (RepeaterItem ri in PeopleInvolvedRepeater.Items)
            {
                string gender = ((DropDownList)ri.FindControl("PersonGenderBox")).SelectedItem.Value;
                string agegroup = ((DropDownList)ri.FindControl("PersonAgeGroupBox")).SelectedItem.Text;
                string persondesc = ((TextBox)ri.FindControl("PersonDescriptionBox")).Text;
                people.Add(new IncidentPeopleDetails(-1, incidentID, gender, agegroup, persondesc));
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
                AddImageLinkButton.Text = "Click here to add photos.";
            else if (imageCounter == 1)
                AddImageLinkButton.Text = "Click here to add photos. (" + imageCounter + " image added)";
            else
                AddImageLinkButton.Text = "Click here to add photos. (" + imageCounter + " images added)";
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

            int counter = 0;

            foreach (IncidentPeopleDetails ipd in list)
            {
                ((DropDownList)PeopleInvolvedRepeater.Items[counter].FindControl("PersonGenderBox")).SelectedValue = ipd.Gender;
                ((DropDownList)PeopleInvolvedRepeater.Items[counter].FindControl("PersonAgeGroupBox")).SelectedValue = ipd.Age_Group;
                ((TextBox)PeopleInvolvedRepeater.Items[counter].FindControl("PersonDescriptionBox")).Text = ipd.Description;
                counter++;
            }
        }

        protected void NoPeopleDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NoPeopleDropDown.SelectedIndex > 1)
            {
                PopulateRepeaters(int.Parse(NoPeopleDropDown.SelectedItem.Value));

                //AddPeopleLinkButton.Enabled = true;
                //ModalPopupExtender1.Enabled = true;
                peopletable.Visible = true;
                Label1.Visible = true;
            }
            else
            {
                peopletable.Visible = false;
                Label1.Visible = false;
                //AddPeopleLinkButton.Enabled = false;
                //ModalPopupExtender1.Enabled = false;

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
    }
}