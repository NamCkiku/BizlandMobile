using Bizland.Core.Extensions;
using Bizland.Core.Resource;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bizland.Core.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Fields

        private bool rememberme;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="NoInternetConnectionPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel(INavigationService navigationService)
           : base(navigationService)
        {
            this.LoginCommand = new Command(this.Login);
            this.PushForgotPasswordCommand = new Command(this.ForgotPassword);
            this.PushRegisterCommand = new Command(this.PushRegisterPage);
            this.LoginWithFaceBookCommand = new Command(this.LoginWithFacebook);
            this.LoginWithGoogleCommand = new Command(this.LoginWithGoogle);

            InitValidations();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the TryAgain button is clicked.
        /// </summary>
        public ICommand LoginCommand { get; set; }

        public ICommand LoginWithFaceBookCommand { get; set; }

        public ICommand LoginWithGoogleCommand { get; set; }

        public ICommand PushForgotPasswordCommand { get; set; }

        public ICommand PushRegisterCommand { get; set; }

        #endregion

        #region Properties
        public ValidatableObject<string> UserName { get; set; }

        public ValidatableObject<string> Password { get; set; }


        public bool Rememberme { get => rememberme; set => SetProperty(ref rememberme, value); }


        #endregion

        #region Methods
        private void InitValidations()
        {
            UserName = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();

            UserName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = MobileResource.Common_Property_NullOrEmpty("User name") });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = MobileResource.Common_Property_NullOrEmpty("Password") });
        }

        private bool Validate()
        {
            return UserName.Validate() && Password.Validate();
        }


        private void Login()
        {
            SafeExecute(async () =>
            {
                if (IsConnected)
                {
                    bool isValid = Validate();
                    if (isValid)
                    {

                        await NavigationService.NavigateAsync("/RootPage?selectedTab=HomePage");
                    }
                    else
                    {
                        DisplayMessage.ShowMessageInfo("Login Error", 5000);
                    }
                }
                else
                {
                    DisplayMessage.ShowMessageInfo("No internet", 5000);
                }
            });
        }

        private void LoginWithFacebook()
        {
            SafeExecute(() =>
            {
                if (IsConnected)
                {
                    DisplayMessage.ShowMessageInfo("Login With Facebook", 5000);
                }
                else
                {
                    DisplayMessage.ShowMessageInfo("No internet", 5000);
                }
            });
        }

        private void LoginWithGoogle()
        {
            SafeExecute(() =>
            {
                if (IsConnected)
                {
                    DisplayMessage.ShowMessageInfo("Login With Google", 5000);
                }
                else
                {
                    DisplayMessage.ShowMessageInfo("No internet", 5000);
                }
            });
        }

        private void ForgotPassword()
        {
            SafeExecute(async () =>
            {
                if (IsConnected)
                {
                    bool isValid = Validate();
                    if (isValid)
                    {
                        await NavigationService.NavigateAsync(new System.Uri("/NavigationPage/RootPage?selectedTab=MainPage", System.UriKind.Absolute));
                    }
                }
                else
                {
                    DisplayMessage.ShowMessageInfo("No internet", 5000);
                }
            });
        }
        private void PushRegisterPage()
        {
            SafeExecute(async () =>
            {
                await NavigationService.NavigateAsync("RegisterPage", useModalNavigation: true);
            });
        }

        #endregion
    }
}
