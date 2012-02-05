using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HoogmaatheideApp.Controls;
using HoogmaatheideApp.Helpers;
using HoogmaatheideApp.Models;
using Microsoft.Phone.Shell;
using Nsharp.WP.Animations;

namespace HoogmaatheideApp
{
    public partial class RasInfoPage
    {
        private bool _preAnimation;
        private Ras _ras;

        public RasInfoPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            _preAnimation = true;
            if (PhoneApplicationService.Current.State.ContainsKey(Constants.OpenRas))
            {
                _ras = (Ras)PhoneApplicationService.Current.State[Constants.OpenRas];
                PhoneApplicationService.Current.State.Remove(Constants.OpenRas);
                DataContext = _ras;
                TitlePanel.Opacity = 0;
                _listbox.Opacity = 0;
                _beschrijving.Opacity = 0;
                _image.Opacity = 0;
                _link.Opacity = 0;
                LoadImage();
                var keywords = GenerateKeyWords();
                keywords.AddRange(_ras.Naam.Split(' '));

                ColorfullTextBlockMaker.MakeTextblockColorfull(_beschrijving, _ras.Beschrijving, keywords, FontWeights.Normal);
                Animator.Wait(100, (o, s) =>
                                       {
                                           CoolAnimations.SlideInner(TitlePanel, 150);
                                           TitlePanel.Opacity = 1;
                                       });
                Animator.Wait(200, (o, s) =>
                                       {
                                           CoolAnimations.SlideInner(_listbox, 150);
                                           _listbox.Opacity = 1;
                                       });
                Animator.Wait(250, (o, s) =>
                                       {
                                           CoolAnimations.SlideInner(_beschrijving, 150);
                                           _beschrijving.Opacity=1;

                                       });
                Animator.Wait(250, (o, s) =>
                {
                    CoolAnimations.SlideInner(_image, 150);
                    _image.Opacity = 1;

                });

                Animator.Wait(500, (o, s) =>
                {
                    CoolAnimations.MoveInner(_link, 550, MoveOrientation.Down);
                    _link.Opacity = 1;

                });
            }
            base.OnNavigatedTo(e);
      
        }

        private void LoadImage()
        {
            if (IsolatedStorageFile.GetUserStoreForApplication().FileExists(Constants.RasImageName(_ras.Naam)))
            {
                byte[] data;

                // Read the entire image (named in m_fileid) in one go into a byte array
                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    // Open the file - error handling omitted for brevity
                    using (
                        IsolatedStorageFileStream isfs = isf.OpenFile(Constants.RasImageName(_ras.Naam), FileMode.Open,
                                                                      FileAccess.Read))
                    {
                        data = new byte[isfs.Length];
                        isfs.Read(data, 0, data.Length);
                        isfs.Close();
                    }
                }

                // Create memory stream and bitmap
                var ms = new MemoryStream(data);
                var bi = new BitmapImage();

                // Set bitmap source to memory stream
                bi.SetSource(ms);

                _image.Source = bi;
            }

        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = _preAnimation;
            if (_preAnimation)
            {
                Animator.FadeOut(TitlePanel, 150);
                Animator.FadeOut(_listbox, 150);
                Animator.FadeOut(_beschrijving, 150);
                Animator.FadeOut(_image, 150);
                CoolAnimations.MoveOuter(_link, 500,MoveOrientation.Down);
                Animator.Wait(50, (o, s) =>
                                       {
                                           _preAnimation = false;
                                           NavigationService.GoBack();
                                       });
            }

            base.OnBackKeyPress(e);
        }

        private static List<string> GenerateKeyWords()
        {
            return new List<string>
                       {
                           "hond",
                           "grootte",
                           "vacht",
                           "schoft",
                           "kleur",
                           "bouw",
                           "spier",
                           "gewicht",
                           "reu",
                           "teef",
                           "neus",
                           "staart"
                       };
        }
   
    }
}