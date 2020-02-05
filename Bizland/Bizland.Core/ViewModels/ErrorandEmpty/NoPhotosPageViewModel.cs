﻿using Prism.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Bizland.Core.ViewModels
{
    /// <summary>
    /// ViewModel for no photos page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class NoPhotosPageViewModel : ViewModelBase
    {
        #region Fields

        private string imagePath;

        private string header;

        private string content;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="NoPhotosPageViewModel" /> class.
        /// </summary>
        public NoPhotosPageViewModel(INavigationService navigationService)
           : base(navigationService)
        {
            this.ImagePath = "NoPhotos.svg";
            this.Header = "NO PHOTOS";
            this.Content = "You haven’t selected any photos yet";
            this.GoBackCommand = new Command(this.GoBack);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the Go back button is clicked.
        /// </summary>
        public ICommand GoBackCommand { get; set; }

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
        /// Invoked when the Go back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void GoBack(object obj)
        {
            // Do something
        }

        #endregion
    }
}
