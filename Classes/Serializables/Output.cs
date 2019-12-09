using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AeternamDonaEis.Classes.Serializables
{
    [Serializable]
    [XmlInclude(typeof(Letters))]
    [XmlInclude(typeof(Lists))]
    [XmlInclude(typeof(Paragraphs))]
    [XmlInclude(typeof(Words))]
    public class Output
    {
        public List<IOutput> Text { get; set; }

        public Output() 
        {
            Text = new List<IOutput>();
        }
    }
}
