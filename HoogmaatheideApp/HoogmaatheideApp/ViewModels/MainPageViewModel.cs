using System.Collections.Generic;
using System.Windows.Media.Animation;
using HoogmaatheideApp.Helpers;
using HoogmaatheideApp.Models;

namespace HoogmaatheideApp.ViewModels
{
    public class MainPageViewModel:NotifyPropertyChangedBase
    {
        private readonly Storyboard _fadeIn;
        private List<Ras> _rassen;
        public List<Ras> Rassen
        {
            get { return _rassen; }
            set { _rassen = value;
                NotifyPropertyChanged(() => Rassen);
            }
        }

        public MainPageViewModel(Storyboard fadeIn)
        {
            _fadeIn = fadeIn;
            var webRequester = new WebRequester();

            webRequester.RetrieveNestenCompleted+=webRequester_RetrieveNestenCompleted;
            webRequester.GetNestenAsync(@"http://kennel.hoogmaatheide.be/scripts/json.php");
        }

        private void webRequester_RetrieveNestenCompleted(object sender, GetRassenCallbackArguments e)
        {
            ((WebRequester)sender).RetrieveNestenCompleted -= webRequester_RetrieveNestenCompleted;
            if (e.Exception == null)
            {
                Rassen = e.Rassen;
                _fadeIn.Begin();
            }
        }

        
    }
}
