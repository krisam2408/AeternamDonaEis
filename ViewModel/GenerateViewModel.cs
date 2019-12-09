using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AeternamDonaEis.Classes;

namespace AeternamDonaEis.ViewModel
{
    public class GenerateViewModel : BaseViewModel
    {
        private GenerateOption option;
        private bool minify;

        public List<GenerateType> LstType { get; set; }
        public GenerateType SelType { get; set; }
        public List<TitleOptions> LstTitleOption { get; set; }
        public TitleOptions SelTitleOption { get; set; }
        public List<TextOutput> LstOutput { get; set; }
        public TextOutput SelOutput { get; set; }
        public int Quantity { get; set; }

        public ICommand IncreaseQuantityCommand { get { return new Command(); } }

        public GenerateViewModel()
        {
            LstType = new List<GenerateType>();
            LstTitleOption = new List<TitleOptions>();
            LstOutput = new List<TextOutput>();
            foreach (GenerateType t in Enum.GetValues(typeof(GenerateType))) LstType.Add(t);
            foreach (TitleOptions to in Enum.GetValues(typeof(TitleOptions))) LstTitleOption.Add(to);
            foreach (TextOutput o in Enum.GetValues(typeof(TextOutput))) LstOutput.Add(o);

            option = new GenerateOption();
            minify = false;

            SelType = option.Type;
            SelTitleOption = option.Titles;
            SelOutput = option.Output;
            Quantity = option.Quantity;
        }

        

    }
}
