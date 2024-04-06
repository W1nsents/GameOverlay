using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayGameNet.Utils
{
    class Configuration
    {
        static Form1 frm = new Form1();

        public static string WINDOW_NAME = ""; // OVERLAY APP
        public static DllImport.RECT rect;
        public static IntPtr handle = DllImport.FindWindow(null, WINDOW_NAME);

        public static IntPtr hand;
        public static bool threadwire = true;
    }
}
