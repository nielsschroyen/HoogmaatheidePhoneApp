using System;

namespace HoogmaatheideApp.Helpers
{
    public class Constants
    {
        public static readonly string OpenRas = "openRas";
        public static readonly string HoogmaaheideData = "hoogmaatheideData" ;
        public static readonly Uri EditPageUri = new Uri("/RasInfoPage.Xaml", UriKind.RelativeOrAbsolute);

        public static string RasImageName(string naam)
        {
            return "pic_" + naam.Replace(' ' , '_') + ".png";
        }
    }
}
