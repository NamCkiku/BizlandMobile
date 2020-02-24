using Bizland.Core.Extensions;
using Bizland.Core.Helpers;
using Bizland.Core.Resource;
using Bizland.Entities;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
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
        private readonly IDialogService dialogService;
        private readonly IFacebookClient _facebookService;
        private readonly IGoogleClientManager _googleService;
        /// <summary>
        /// Initializes a new instance for the <see cref="NoInternetConnectionPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel(INavigationService navigationService, IDialogService dialogService)
           : base(navigationService)
        {
            this._googleService = CrossGoogleClient.Current;
            this._facebookService = CrossFacebookClient.Current;
            this.dialogService = dialogService;
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
            SafeExecute(async () =>
            {
                if (IsConnected)
                {
                    if (_facebookService.IsLoggedIn)
                    {
                        _facebookService.Logout();
                    }

                    EventHandler<FBEventArgs<string>> userDataDelegate = null;

                    userDataDelegate = async (object sender, FBEventArgs<string> e) =>
                    {
                        switch (e.Status)
                        {
                            case FacebookActionStatus.Completed:
                                var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));
                                break;
                            case FacebookActionStatus.Canceled:
                                await App.Current.MainPage.DisplayAlert("Facebook Auth", "Canceled", "Ok");
                                break;
                            case FacebookActionStatus.Error:
                                await App.Current.MainPage.DisplayAlert("Facebook Auth", "Error", "Ok");
                                break;
                            case FacebookActionStatus.Unauthorized:
                                await App.Current.MainPage.DisplayAlert("Facebook Auth", "Unauthorized", "Ok");
                                break;
                        }

                        _facebookService.OnUserData -= userDataDelegate;
                    };

                    _facebookService.OnUserData += userDataDelegate;

                    string[] fbRequestFields = { "email", "first_name", "picture", "gender", "last_name" };
                    string[] fbPermisions = { "email" };
                    await _facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
                }
                else
                {
                    DisplayMessage.ShowMessageInfo("No internet", 5000);
                }
            });
        }

        private void LoginWithGoogle()
        {
            SafeExecute(async () =>
            {
                if (IsConnected)
                {
                    if (!string.IsNullOrEmpty(_googleService.ActiveToken))
                    {
                        //Always require user authentication
                        _googleService.Logout();
                    }

                    EventHandler<GoogleClientResultEventArgs<GoogleUser>> userLoginDelegate = null;
                    userLoginDelegate = async (object sender, GoogleClientResultEventArgs<GoogleUser> e) =>
                    {
                        switch (e.Status)
                        {
                            case GoogleActionStatus.Completed:
                                var googleUserString = JsonConvert.SerializeObject(e.Data);
                                await App.Current.MainPage.DisplayAlert("Google Auth Complate", "Canceled", "Ok");
                                break;
                            case GoogleActionStatus.Canceled:
                                await App.Current.MainPage.DisplayAlert("Google Auth", "Canceled", "Ok");
                                break;
                            case GoogleActionStatus.Error:
                                await App.Current.MainPage.DisplayAlert("Google Auth", "Error", "Ok");
                                break;
                            case GoogleActionStatus.Unauthorized:
                                await App.Current.MainPage.DisplayAlert("Google Auth", "Unauthorized", "Ok");
                                break;
                        }

                        _googleService.OnLogin -= userLoginDelegate;
                    };

                    _googleService.OnLogin += userLoginDelegate;

                    await _googleService.LoginAsync();
                }
                else
                {
                    DisplayMessage.ShowMessageInfo("No internet", 5000);
                }
            });
        }
        void CloseDialogCallback(IDialogResult dialogResult)
        {

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
