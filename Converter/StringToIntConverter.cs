using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AeternamDonaEis.Converter
{
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string str = value.ToString();
            char[] dots = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            bool valid;
            if (str.Length > 0) valid = true;
            else valid = false;
            foreach(char c in str) if (!dots.Contains(c)) valid = false;
            if (valid) return int.Parse(str);
            return 0;
        }
    }
}
