using Bizland.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bizland.Core.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {

        #region Service
        private readonly IEventAggregator _eventAggregator;
        #endregion
        #region Fields

        private CountryCode _countryCode;



        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="NoInternetConnectionPageViewModel" /> class.
        /// </summary>
        public RegisterPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
           : base(navigationService)
        {
            this._eventAggregator = eventAggregator;
            this.RegisterCommand = new Command(this.Register);
            this.PushCountryPageCommand = new Command(this.PushCountryPage);
            this.PushLoginPageCommand = new Command(this.PushLoginPage);
            _countryCode = new CountryCode("VN", "Viet Nam", "+84", "flag_vn.png");
        }

        #endregion

        #region override
        public override void Initialize(INavigationParameters parameters)
        {
            // sự kiện trả về
            _eventAggregator.GetEvent<SelectCountryEvent>().Subscribe(UpdateCountryCode);
        }

        public override void OnDestroy()
        {
            _eventAggregator.GetEvent<SelectCountryEvent>().Unsubscribe(UpdateCountryCode);
        }
        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the TryAgain button is clicked.
        /// </summary>
        public ICommand RegisterCommand { get; set; }

        public ICommand PushCountryPageCommand { get; set; }

        public ICommand PushLoginPageCommand { get; set; }

        #endregion

        #region Properties
        public ValidatableObject<string> UserName { get; set; }

        public ValidatableObject<string> Password { get; set; }


        public CountryCode CountryCode
        {
            get => _countryCode; set => SetProperty(ref _countryCode, value);
        }



        #endregion

        #region Methods

        private void UpdateCountryCode(CountryCode obj)
        {
            CountryCode = obj;
        }

        private void Register()
        {
            SafeExecute(async () =>
            {
                await NavigationService.NavigateAsync("OTPPage", useModalNavigation: true);
            });
        }

        private void PushCountryPage()
        {
            SafeExecute(async () =>
            {
                await NavigationService.NavigateAsync("CountryPage", useModalNavigation: true);
            });
        }

        private void PushLoginPage()
        {
            SafeExecute(async () =>
            {
                await NavigationService.NavigateAsync("LoginPage", useModalNavigation: true);
            });
        }

        #endregion
    }
}
