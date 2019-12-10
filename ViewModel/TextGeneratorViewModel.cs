using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AeternamDonaEis.Classes;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;

namespace AeternamDonaEis.ViewModel
{
    public class TextGeneratorViewModel : BaseViewModel
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
                GenerateText();
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
                GenerateText();
            } 
        }
        private List<TextOutput> lstOutput;
        public List<TextOutput> LstOutput { get { return lstOutput; } set { SetValue(ref lstOutput, value); } }
        private TextOutput selOutput;
        public TextOutput SelOutput
        { 
            get 
            { 
                return selOutput; 
            } 
            set 
            { 
                SetValue(ref selOutput, value);
                GenerateText();
            } 
        }
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
                if (value < 1)
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
                GenerateText();
            } 
        }
        private string result;
        public string Result { get { return result; } set { SetValue(ref result, value); } }

        public ICommand IncreaseCommand { get { return new RelayCommand(IncreaseQuantity); } }
        public ICommand DecreaseCommand { get { return new RelayCommand(DecreaseQuantity); } }
        public ICommand CopyCommand { get { return new RelayCommand(Copy); } }

        public TextGeneratorViewModel()
        {
            LstType = new List<GenerateType>();
            LstTitleOption = new List<TitleOptions>();
            LstOutput = new List<TextOutput>();
            foreach (GenerateType t in Enum.GetValues(typeof(GenerateType))) LstType.Add(t);
            foreach (TitleOptions to in Enum.GetValues(typeof(TitleOptions))) LstTitleOption.Add(to);
            foreach (TextOutput o in Enum.GetValues(typeof(TextOutput))) LstOutput.Add(o);

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
        private void Copy()
        {
            var data = new DataPackage();
            data.SetText(Result);
            Clipboard.SetContent(data);
        }
        private void GenerateText()
        {
            Result = Generator.Generate(new GenDataTransfer(SelType, Quantity, SelTitleOption, SelOutput));
        }

    }
}
