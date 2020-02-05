using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bizland.Core.ViewModels
{
    public class OTPPageViewModel : ViewModelBase
    {
        public OTPPageViewModel(INavigationService navigationService)
           : base(navigationService)
        {

        }
    }
}
