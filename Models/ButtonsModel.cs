using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASender.enums;

namespace WASender.Models
{
    public class ButtonsModel
    {
        public string id { get; set; }

        public string text { get; set; }

        public string url { get; set; }

        public string phoneNumber { get; set; }

        public bool editMode { get; set; }
        public ButtonTypeEnum buttonTypeEnum { get; set; }
    }
}
