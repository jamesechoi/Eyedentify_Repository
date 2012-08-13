using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eyedentify.App_Code;
using System.IO;


namespace Eyedentify
{
    public partial class ImagePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlIncidentProvider sq = new SqlIncidentProvider();
            string test = Request.QueryString["imgID"] == null ? string.Empty : Request.QueryString["imgID"];
            string imgSize = Request.QueryString["sz"] == null ? string.Empty : Request.QueryString["sz"];

            int _thumbnailSize = 0;
            if (imgSize.Equals(string.Empty))
            {
                _thumbnailSize = 0;
            }
            else
            {
                _thumbnailSize = Int32.Parse(imgSize); ///size of the image ie thumnail.
            }

            int _ImgID = int.Parse(test);

            System.Web.HttpContext.Current.Response.ContentType = "image/jpeg";

            IncidentImageDetails jid = sq.Incident_Image_Get(_ImgID);
            byte[] byteImage = jid.Image;
            MemoryStream ms = new MemoryStream(byteImage);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

            string imageType = Request.QueryString["type"] == null ? "small" : Request.QueryString["type"].ToString();

            if (imageType.Equals("small"))
            {
                System.Drawing.Image _newimage = image.GetThumbnailImage(100, 100, null, IntPtr.Zero);
                _newimage.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (imageType.Equals("medium"))
            {
                System.Drawing.Image _newimage = image.GetThumbnailImage(200, 200, null, IntPtr.Zero);
                _newimage.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (imageType.Equals("vsmall"))
            {
                System.Drawing.Image _newimage = image.GetThumbnailImage(60, 60, null, IntPtr.Zero);
                _newimage.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (_thumbnailSize == 60)
            {
                System.Drawing.Image _newimage = image.GetThumbnailImage(80, 80, null, IntPtr.Zero);
                _newimage.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else if (imageType.Equals("bid"))
            {
                int height = (int)(image.Size.Height * 1);
                int width = (int)(image.Size.Width * 1);
                if (height > 0 && width > 0)
                {
                    System.Drawing.Image _newimage = image.GetThumbnailImage(width, height, null, IntPtr.Zero);
                    _newimage.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            else if (imageType.Equals("full"))
            {
                //Graphics g = Graphics.FromImage(image);

                //Pen p = new Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Black));


                //System.Drawing.SolidBrush blackbrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                //System.Drawing.SolidBrush whitebrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);

                //g.FillRectangle(whitebrush, 0, 0, 100, 25);
                //g.DrawString("Cashies (" + jid.JobID+")", new Font("Verdana", 10, FontStyle.Bold), blackbrush, new Point(5, 5));

                //g.Save();

                image.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            }
            //}
            //else if (!test2.Equals("0"))
            //{
            //    int _ImgID = System.Convert.ToInt32(test2);

            //    System.Web.HttpContext.Current.Response.ContentType = "image/jpeg";

            //    UserImageDetails jid = sq2.GetUserImageDetails(_ImgID);
            //    byte[] byteImage = jid.UserImage;
            //    MemoryStream ms = new MemoryStream(byteImage);
            //    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

            //    System.Drawing.Image _newimage = image.GetThumbnailImage(100, 100, null, IntPtr.Zero);
            //    _newimage.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            //}
        }
    }
}