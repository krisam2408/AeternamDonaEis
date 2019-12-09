using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeternamDonaEis.Classes
{
    public class GenDataTransfer
    {
        public GenerateType Type { get; }
        public int Quantity { get; }
        public TitleOptions TitleOption { get; }
        public TextOutput Output { get; }

        public GenDataTransfer(GenerateType type, int quantity, TitleOptions options, TextOutput output)
        {
            Type = type;
            Quantity = quantity;
            TitleOption = options;
            Output = output;
        }
    }
}
