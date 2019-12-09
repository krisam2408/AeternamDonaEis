using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeternamDonaEis.Classes.Serializables
{
    [Serializable]
    public class Letters:IOutput
    {
        public string Body { get; set; }
    }
}
