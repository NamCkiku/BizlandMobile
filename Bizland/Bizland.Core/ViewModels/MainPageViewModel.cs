using Prism;
using Prism.Navigation;
using System;

namespace Bizland.Core.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IActiveAware
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }

        public bool _isActive;
        public bool IsActive { get => _isActive; set => SetProperty(ref _isActive, value, RaiseIsActiveChanged); }

        public event EventHandler IsActiveChanged;


        protected virtual void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}