using Prism.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bizland.Core.ViewModels
{
    /// <summary>
    /// ViewModel for something went wrong page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SomethingWentWrongPageViewModel : ViewModelBase
    {
        #region Fields

        private string imagePath;

        private string header;

        private string content;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SomethingWentWrongPageViewModel" /> class.
        /// </summary>
        public SomethingWentWrongPageViewModel(INavigationService navigationService)
           : base(navigationService)
        {
            this.ImagePath = "SomethingWentWrong.svg";
            this.Header = "SOMETHING WENT WRONG";
            this.Content = "We’re not sure what happened, but we know an error occurred";
            this.TryAgainCommand = new Command(this.TryAgain);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the TryAgain button is clicked.
        /// </summary>
        public ICommand TryAgainCommand { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ImagePath.
        /// </summary>
        public string ImagePath
        {
            get => imagePath; set => SetProperty(ref imagePath, value);
        }

        /// <summary>
        /// Gets or sets the Header.
        /// </summary>
        public string Header
        {
            get => header; set => SetProperty(ref header, value);
        }

        /// <summary>
        /// Gets or sets the Content.
        /// </summary>
        public string Content
        {
            get => content; set => SetProperty(ref content, value);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the TryAgain button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void TryAgain(object obj)
        {
            // Do something
        }
        #endregion
    }
}
