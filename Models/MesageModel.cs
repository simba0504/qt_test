﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASender.Models
{
    public class MesageModel
    {
        public string longMessage { get; set; }

        public List<FilesModel> files { get; set; }

        public List<ButtonsModel> buttons { get; set; }
    }
}
