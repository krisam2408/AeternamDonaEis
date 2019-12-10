using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeternamDonaEis.Classes
{
    public enum GenerateType
    {
        Paragraphs, Words, Letters, Lists
    }

    public enum TitleOptions
    {
        [Display(Name="With Titles")]WithTitles,
        [Display(Name ="Without Titles")]WithoutTitles
    }

    public enum TextOutput
    {
        HTML, Raw, Json//, XML
    }
}
