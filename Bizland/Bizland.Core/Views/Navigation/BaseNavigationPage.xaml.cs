using Xamarin.Forms;

namespace Bizland.Core.Views
{
    public partial class BaseNavigationPage : NavigationPage
    {
        public bool ClearNavigationStackOnNavigation
        {
            get { return true; }
        }

        public BaseNavigationPage()
        {
            InitializeComponent();

            SetBackButtonTitle(this, "");
        }
    }
}
