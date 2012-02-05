using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;
using HoogmaatheideApp.Helpers;
using HoogmaatheideApp.Models;
using Nsharp.WP.Animations;
using System.Linq;

namespace HoogmaatheideApp.ViewModels
{
    [DataContract]
    public class MainPageViewModel : NotifyPropertyChangedBase
    {
        private readonly MainPage _mainPage;
        private List<Ras> _rassen;

        public List<Ras> Rassen
        {
            get { return _rassen; }
            set
            {
                _rassen = value;
                NotifyPropertyChanged(() => Rassen);
            }
        }

        public MainPageViewModel(MainPage mainPage)
        {
            _mainPage = mainPage;
            var webRequester = new WebRequester();

            if (IsolatedStorageSettings.ApplicationSettings.Contains(Constants.HoogmaaheideData))
            {
                Rassen = IsolatedStorageSettings.ApplicationSettings[Constants.HoogmaaheideData] as List<Ras>;

            }
            else
            {
                Rassen = new List<Ras>();
                IsolatedStorageSettings.ApplicationSettings.Add(Constants.HoogmaaheideData, Rassen);
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
            _mainPage.DoStartupAnimations();
            Animator.Wait(2000, (o, s) =>
                                    {
                                        webRequester.RetrieveNestenCompleted +=
                                            WebRequesterRetrieveNestenCompleted;
                                        webRequester.GetNestenAsync(
                                            @"http://kennel.hoogmaatheide.be/scripts/json.php");
                                    });
        }

        private void WebRequesterRetrieveNestenCompleted(object sender, GetRassenCallbackArguments e)
        {
            ((WebRequester) sender).RetrieveNestenCompleted -= WebRequesterRetrieveNestenCompleted;
            //Add nesten
            if (e.Exception == null)
            {
                var newRassen = e.Rassen;
                foreach (var ras in newRassen)
                {
                    AddPicture(ras);
                    var ra = Rassen.SingleOrDefault(r => r.Naam == ras.Naam);
                    if (ra != null)
                    {
                        ra.Nesten = ras.Nesten;
                    }
                    else
                    {
                        Rassen.Add(ras);
                    }
                }
                //Save new info
                IsolatedStorageSettings.ApplicationSettings[Constants.HoogmaaheideData] = newRassen;
                IsolatedStorageSettings.ApplicationSettings.Save();

            }
        }

        private void AddPicture(Ras ras)
        {
            if (!IsolatedStorageFile.GetUserStoreForApplication().FileExists(Constants.RasImageName(ras.Naam)))
            {
                var webClient = new WebClient();
                webClient.OpenReadCompleted += (s,e)=>bitmapImageDownloaded(ras.Naam, e);
                webClient.OpenReadAsync(new Uri(ras.Foto));
            }
        }

        private void bitmapImageDownloaded(string naam, OpenReadCompletedEventArgs e)
        {
            // Read complete
            var buffer = new byte[1024];

            // Create (or replace) file and write image to it
            var stream = e.Result;
            using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var isfs = new IsolatedStorageFileStream(Constants.RasImageName(naam), FileMode.Create, isf))
                {
                    int count;
                    while (0 < (count = stream.Read(buffer, 0, buffer.Length)))
                    {
                        isfs.Write(buffer, 0, count);
                    }

                    stream.Close();
                    isfs.Close();
                }
            }
        }
    }
}
