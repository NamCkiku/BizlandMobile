using Bizland.Core.Views;
using Bizland.Entities.ResponeEntity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Syncfusion.SfRotator.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bizland.Core.ViewModels
{
    public class OnBoardingPageViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<Boarding> boardings;

        private int selectedIndex;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="OnBoardingPageViewModel" /> class.
        /// </summary>
        public OnBoardingPageViewModel(INavigationService navigationService)
           : base(navigationService)
        {
            this.Boardings = new ObservableCollection<Boarding>
            {
                new Boarding
                {
                    ImagePath = "ChooseGradient.svg",
                    Header = "CHOOSE",
                    Content = "Pick the item that is right for you"
                },
                new Boarding
                {
                    ImagePath = "ConfirmGradient.svg",
                    Header = "ORDER CONFIRMED",
                    Content = "Your order is confirmed and will be on its way soon"
                },
                new Boarding
                {
                    ImagePath = "DeliverGradient.svg",
                    Header = "DELIVERY",
                    Content = "Your item will arrive soon. Email us if you have any issues"
                }
            };
        }
        #endregion

        #region Properties

        public ObservableCollection<Boarding> Boardings
        {
            get => boardings; set => SetProperty(ref boardings, value);
        }


        public int SelectedIndex
        {
            get => selectedIndex; set => SetProperty(ref selectedIndex, value);
        }

        #endregion

        #region Commands


        #endregion

        #region Methods

        private bool ValidateAndUpdateSelectedIndex(int itemCount)
        {
            if (this.SelectedIndex >= itemCount - 1)
            {
                return true;
            }

            this.SelectedIndex++;
            return false;
        }

        #endregion
    }
}
