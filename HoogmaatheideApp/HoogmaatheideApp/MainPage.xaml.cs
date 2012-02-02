using System;
using System.Windows;
using HoogmaatheideApp.Helpers;
using HoogmaatheideApp.Models;
using HoogmaatheideApp.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HoogmaatheideApp
{
    public partial class MainPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel(_fadeIn);

        }

        private void ListboxSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_listbox.SelectedItem != null)
            {
                if (((Ras)_listbox.SelectedItem).Nesten.Count == 0)
                {
                    _listbox.SelectedItem = null;
                    return;
                }
                _fadeAllOut.Completed+=FadeAllOutCompleted;
                _fadeAllOut.Begin();
               
                
            }
        }

        private void FadeAllOutCompleted(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State[Constants.OpenRas] = _listbox.SelectedItem;
            ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(Constants.EditPageUri);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            _listbox.SelectedItem = null;
            _fadeAllIn.Begin();
            base.OnNavigatedTo(e);
        }
    }
}