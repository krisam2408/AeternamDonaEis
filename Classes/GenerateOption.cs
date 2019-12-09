using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeternamDonaEis.Classes
{
    public class GenerateOption
    {
        private GenerateType type;
        public GenerateType Type
        {
            get { return type; }
            set
            {
                type = value;
                Titles = Titles;
                Quantity = 0;
            }
        }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value > 1)
                {
                    switch (Type)
                    {
                        case GenerateType.Paragraphs:
                            quantity = 3;
                            break;
                        case GenerateType.Lists:
                            quantity = 9;
                            break;
                        case GenerateType.Words:
                            quantity = 36;
                            break;
                        case GenerateType.Letters:
                            quantity = 150;
                            break;
                        default:
                            quantity = 0;
                            break;
                    }
                }
                else quantity = value;
            }
        }
        private TitleOptions titles;
        public TitleOptions Titles
        {
            get { return titles; }
            set
            {
                switch (Type)
                {
                    case GenerateType.Paragraphs:
                    case GenerateType.Lists:
                        titles = value;
                        break;
                    default:
                        titles = TitleOptions.WithoutTitles;
                        break;
                }
            }
        }
        public TextOutput Output { get; set; }

        public GenerateOption()
        {
            Type = GenerateType.Words;
            Quantity = 0;
            Titles = TitleOptions.WithoutTitles;
            Output = TextOutput.Raw;
        }
    }
}
