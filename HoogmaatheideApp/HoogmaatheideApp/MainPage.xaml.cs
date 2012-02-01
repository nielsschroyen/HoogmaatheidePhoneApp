using HoogmaatheideApp.ViewModels;

namespace HoogmaatheideApp
{
    public partial class MainPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = new MainPageViewModel();

        }
    }
}