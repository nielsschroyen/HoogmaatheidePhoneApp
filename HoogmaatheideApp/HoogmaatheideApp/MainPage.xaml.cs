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
            DataContext = new MainPageViewModel();

        }

        private void ListboxSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_listbox.SelectedItem != null)
            {
                PhoneApplicationService.Current.State[Constants.OpenRas] = _listbox.SelectedItem;
                ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(Constants.EditPageUri);
            }
        }
    }
}