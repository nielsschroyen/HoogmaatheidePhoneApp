using System;
using System.Windows;
using System.Windows.Controls;
using HoogmaatheideApp.Helpers;
using HoogmaatheideApp.Models;
using HoogmaatheideApp.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Nsharp.WP.Animations;

namespace HoogmaatheideApp
{
    public partial class MainPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel(this);

        }

        private bool _started = false;
        private bool _preAnimation;

        private void ListboxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_listbox.SelectedItem != null)
            {
                if (((Ras)_listbox.SelectedItem).Nesten.Count == 0)
                {
                    _listbox.SelectedItem = null;
                    return;
                }
                Animator.FadeOut(TitlePanel, 200);
                Animator.FadeOut(_listbox, 200);
                Animator.Wait(150, (o,s)=>GoToRasInfoPage(this, null));
            

            }
        }

        private void GoToRasInfoPage(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State[Constants.OpenRas] = _listbox.SelectedItem;
            ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(Constants.EditPageUri);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            _preAnimation = true;
            if (_started)
            {
             Animator.Wait(200,(o,s)=>  Animator.FadeIn(TitlePanel, 200));
             Animator.Wait(250 , (ooo, sss) =>
             {
                 for (int i = 0; i < _listbox.Items.Count; i++)
                 {
                     _listbox.Opacity = 1;
                     var lb = _listbox.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                     if (lb != null)
                     {
                         lb.Opacity = 0;
                         Animator.Wait(i * 20, (oooo, ssss) =>
                         {
                             lb.Opacity = 1;
                             CoolAnimations.SlideInner(lb, 150);
                         });
                     }
                 }
             });
            }
            _started = true;
            _listbox.SelectedItem = null;
            base.OnNavigatedTo(e);
        }

        public void DoStartupAnimations()
        {
            
            TitlePanel.Opacity = 0;
            int wait = 0;
            if (_preAnimation)
            {
                _preAnimation = false;
                wait = 400;
            }
            Animator.Wait(wait+150, (o, s) =>
                                   {
                                       CoolAnimations.SlideInner(TitlePanel, 150);
                                       TitlePanel.Opacity = 1;
                                       
                                   });
            _listbox.Opacity = 0;
            Animator.Wait(150+wait, (ooo, sss) =>
            {
                for (int i = 0; i < _listbox.Items.Count; i++)
                {
                    _listbox.Opacity = 1;
                    var lb = _listbox.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    if (lb != null)
                    {
                        lb.Opacity = 0;
                        Animator.Wait(i*20, (oooo, ssss) => { lb.Opacity = 1 ;
                        CoolAnimations.SlideInner(lb, 150);
                    });
                    }
                }
            });

        }

     
       
    }
}