using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeternamDonaEis.Classes.Serializables
{
    [Serializable]
    public class Paragraphs:IOutput
    {
        public string Title { get; set; }
        public List<string> Body { get; set; }

        public Paragraphs()
        {
            Body = new List<string>();
        }
    }
}
