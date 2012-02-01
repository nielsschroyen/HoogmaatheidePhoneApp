using HoogmaatheideApp.Helpers;
using HoogmaatheideApp.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HoogmaatheideApp
{
    public partial class RasInfoPage
    {
        public RasInfoPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey(Constants.OpenRas))
            {
                var ras = (Ras)PhoneApplicationService.Current.State[Constants.OpenRas];
                PhoneApplicationService.Current.State.Remove(Constants.OpenRas);
                DataContext = ras;
            }
            base.OnNavigatedTo(e);
        }

   
    }
}