﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeternamDonaEis.Classes.Serializables
{
    [Serializable]
    public class Lists:IOutput
    {
        public string Title { get; set; }
        public List<string> ListItems { get; set; }

        public Lists()
        {
            ListItems = new List<string>();
        }
    }
}
