using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eyedentify.App_Code;

namespace Eyedentify
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            double lat = double.Parse(TextBox1.Text);
            double lon = double.Parse(TextBox2.Text);

            double latDeltaRange = Utility.GetLatitudeDeltaRangeInDegrees(0.5);
            double lonDeltaRange = Utility.GetLongitudeDeltaRange(0.5, lat);

            Label1.Text = latDeltaRange + " " + lonDeltaRange;
        }
    }
}