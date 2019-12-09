using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeternamDonaEis.ViewModel
{
    public class MainViewModel
    {
        public GenerateViewModel Generate { get; set; }


        #region Singleton
        private static MainViewModel singleton;
        public static MainViewModel Instance
        {
            get
            {
                if (singleton == null) singleton = new MainViewModel();
                return singleton;
            }
        }

        #endregion
    }
}
