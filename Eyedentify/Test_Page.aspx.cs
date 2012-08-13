using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eyedentify.App_Code;

namespace Eyedentify
{
    public partial class Test_Page : System.Web.UI.Page
    {
        SqlIncidentProvider sip = new SqlIncidentProvider();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //IncidentDetails id = new IncidentDetails(1, "C2AE38F1-6EDC-4BE6-B2D4-704B77BCEC11", "test subject", "test description", DateTime.Now,
            //    new List<string> { "1", "2", "3" }, false);

            //int s = sip.Incident_Insert(id);
            //Response.Write(s+"sss");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            sip.Incident_Insert_Finalise(int.Parse(TextBox1.Text));
            Response.Write("Status Updated");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int incidentID = int.Parse(TextBox2.Text);
            byte[] image = FileUpload1.FileBytes;
            sip.Incident_Image_Insert(incidentID, image, "test description", true);
            Response.Write("Status Updated");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            EmailHelper eh = new EmailHelper();
            eh.Send_Registration_Email("James", "Choi", "jameschoi", "james.e.choi@gmail.com");
        }
    }
}