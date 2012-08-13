using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eyedentify.App_Code;

namespace Eyedentify
{
    public partial class MemberHome : System.Web.UI.Page
    {
        SqlIncidentProvider sip = new SqlIncidentProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridView();
            }
        }

        private void PopulateGridView()
        {
            GridViewList.DataSource = sip.Incident_Get_For_Home_Page();
            GridViewList.DataBind();
        }

        public string GetImageString(object imageID)
        {
            if (imageID.ToString().Trim() == string.Empty)
                return "../Images/no-image.gif";
            else
                return "ImagePage.aspx?imgID=" + imageID.ToString() + "&type=medium";
        }
    }
}