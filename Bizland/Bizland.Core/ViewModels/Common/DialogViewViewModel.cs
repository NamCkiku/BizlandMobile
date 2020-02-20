using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.Core.ViewModels
{
    public class DialogViewViewModel : ViewModelBase, IDialogAware
    {
        public DelegateCommand CloseCommand { get; }
        public DialogViewViewModel(INavigationService navigationService)
           : base(navigationService)
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
        }
        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Message"))
            {
                Message = parameters.GetValue<string>("Message");
            }
        }
    }
}
