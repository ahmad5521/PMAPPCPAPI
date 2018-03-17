using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PMAPP.Models
{
    public class WorkOrdersPhotos
    {
        public int id { get; set; }

        public int WOID_FK { get; set; }

        public Byte[] imageData { get; set; }

        public DateTime date { get; set; }

        public ImageSource imageSource { get; set; }
    }
}
