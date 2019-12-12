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
        public List<GenerateType> LstType { get { return lstType; } set { if(lstType!=value)SetValue(ref lstType, value); } }
        private GenerateType selType;
        public GenerateType SelType 
        { 
            get 
            { 
                return selType; 
            } 
            set 
            {
                if(selType != value)
                {
                    SetValue(ref selType, value); 
                    SelTitleOption = SelTitleOption;
                    Quantity = 0;
                    GenerateText();
                }
            } 
        }
        private List<TitleOptions> lstTitleOption;
        public List<TitleOptions> LstTitleOption { get { return lstTitleOption; } set { if(lstTitleOption != value) SetValue(ref lstTitleOption, value); } }
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
                if(selTitleOption != titles)
                {
                    SetValue(ref selTitleOption, titles);
                    GenerateText();
                }
            } 
        }
        private List<TextOutput> lstOutput;
        public List<TextOutput> LstOutput { get { return lstOutput; } set { if(lstOutput != value) SetValue(ref lstOutput, value); } }
        private TextOutput selOutput;
        public TextOutput SelOutput
        { 
            get 
            { 
                return selOutput; 
            } 
            set 
            { 
                if(selOutput != value)
                {
                    SetValue(ref selOutput, value);
                    GenerateText();
                }
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
                int quantum = value;
                if (value < 1)
                {
                    switch (SelType)
                    {
                        case GenerateType.Paragraphs:
                            quantum = 3;
                            break;
                        case GenerateType.Lists:
                            quantum = 9;
                            break;
                        case GenerateType.Words:
                            quantum = 36;
                            break;
                        case GenerateType.Letters:
                            quantum = 150;
                            break;
                        default:
                            quantum = 0;
                            break;
                    }
                }
                if(quantity != quantum)
                {
                    SetValue(ref quantity, quantum);
                    GenerateText();
                }
            } 
        }
        private string result;
        public string Result { get { return result; } set { if(result != value) SetValue(ref result, value); } }

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
