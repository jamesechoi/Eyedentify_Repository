using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eyedentify.App_Code
{
    public class IncidentImageDetails
    {
        public IncidentImageDetails()
        {
        }

        public IncidentImageDetails(int image_ID, int incident_ID, byte[] image, string description, bool main_photo)
        {
            this.Image_ID = image_ID;
            this.Incident_ID = incident_ID;
            this.Image = image;
            this.Description = description;
            this.Main_Photo = main_photo;
        }

        private int _image_ID = 0;
        public int Image_ID
        {
            get { return _image_ID; }
            set { _image_ID = value; }
        }

        private int _incident_ID = 0;
        public int Incident_ID
        {
            get { return _incident_ID; }
            set { _incident_ID = value; }
        }

        private byte[] _image = null;
        public byte[] Image
        {
            get { return _image; }
            set { _image = value; }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private bool _main_photo = false;
        public bool Main_Photo
        {
            get { return _main_photo; }
            set { _main_photo = value; }
        }        
    }
}