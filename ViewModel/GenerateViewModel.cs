using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AeternamDonaEis.Classes;
using Windows.UI.Xaml;

namespace AeternamDonaEis.ViewModel
{
    public class GenerateViewModel : BaseViewModel
    {

        private List<GenerateType> lstType;
        public List<GenerateType> LstType { get { return lstType; } set { SetValue(ref lstType, value); } }
        private GenerateType selType;
        public GenerateType SelType 
        { 
            get 
            { 
                return selType; 
            } 
            set 
            {
                SetValue(ref selType, value); 
                SelTitleOption = SelTitleOption;
                Quantity = 0;
            } 
        }
        private List<TitleOptions> lstTitleOption;
        public List<TitleOptions> LstTitleOption { get { return lstTitleOption; } set { SetValue(ref lstTitleOption, value); } }
        private TitleOptions selTitleOption;
        public TitleOptions SelTitleOption
        { 
            get 
            { 
                return selTitleOption; 
            } 
            set 
            {
                TitleOptions titles;
                switch (SelType)
                {
                    case GenerateType.Paragraphs:
                    case GenerateType.Lists:
                        titles = value;
                        break;
                    default:
                        titles = TitleOptions.WithoutTitles;
                        break;
                }
                SetValue(ref selTitleOption, titles); 
            } 
        }
        private List<TextOutput> lstOutput;
        public List<TextOutput> LstOutput { get { return lstOutput; } set { SetValue(ref lstOutput, value); } }
        private TextOutput selOutput;
        public TextOutput SelOutput { get { return selOutput; } set { SetValue(ref selOutput, value); } }
        private int quantity;
        public int Quantity 
        { 
            get
            { 
                return quantity; 
            } 
            set 
            {
                int w = value;
                if (value > 1)
                {
                    switch (SelType)
                    {
                        case GenerateType.Paragraphs:
                            w = 3;
                            break;
                        case GenerateType.Lists:
                            w = 9;
                            break;
                        case GenerateType.Words:
                            w = 36;
                            break;
                        case GenerateType.Letters:
                            w = 150;
                            break;
                        default:
                            w = 0;
                            break;
                    }
                }
                SetValue(ref quantity, w); 
            } 
        }
        private string result;
        public string Result { get { return result; } set { SetValue(ref result, value); } }

        public ICommand IncreaseCommand { get { return new RelayCommand(IncreaseQuantity); } }
        public ICommand DecreaseCommand { get { return new RelayCommand(DecreaseQuantity); } }

        private bool minify;
        public bool Minify { get { return minify; }set { SetValue(ref minify, value); } }

        private string minifyContent;
        public string MinifyContent { get { return minifyContent; } set { SetValue(ref minifyContent, value); } }

        public GenerateViewModel()
        {
            LstType = new List<GenerateType>();
            LstTitleOption = new List<TitleOptions>();
            LstOutput = new List<TextOutput>();
            foreach (GenerateType t in Enum.GetValues(typeof(GenerateType))) LstType.Add(t);
            foreach (TitleOptions to in Enum.GetValues(typeof(TitleOptions))) LstTitleOption.Add(to);
            foreach (TextOutput o in Enum.GetValues(typeof(TextOutput))) LstOutput.Add(o);

            Minify = false;

            SelType = GenerateType.Words;
            SelOutput = TextOutput.Raw;
            SelTitleOption = TitleOptions.WithoutTitles;
            Quantity = 0;
        }

        
        private void IncreaseQuantity()
        {
            Quantity++;
        }
        private void DecreaseQuantity()
        {
            Quantity--;
        }
        private void GenerateText()
        {
            switch (SelOutput)
            {
                case TextOutput.Json:
                case TextOutput.XML:
                    Minify = true;
                    break;
                default:
                    Minify = false;
                    break;
            }
            MinifyContent = Minify ? "Normalize" : "Minify";
            Result = Minify ? AeternamGenerator.Minify(AeternamGenerator.Generate(options), options.Output) : AeternamGenerator.Generate(options);
        }

    }
}
