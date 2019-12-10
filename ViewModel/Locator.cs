using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeternamDonaEis.ViewModel
{
    public class Locator
    {
        public TextGeneratorViewModel TextGenerate { get; set; }
        public MinifyViewModel Minify { get; set; }


        #region Singleton
        private static Locator singleton;
        public static Locator Instance
        {
            get
            {
                if (singleton == null) singleton = new Locator();
                return singleton;
            }
        }

        public Locator()
        {
            TextGenerate = new TextGeneratorViewModel();
            Minify = new MinifyViewModel();
        }

        #endregion
    }
}
