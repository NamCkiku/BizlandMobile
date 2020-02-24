using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bizland.Core.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private int _selectedViewModelIndex = 0;
        public MainPageViewModel()
        {

        }

        public int SelectedViewModelIndex
        {
            get => _selectedViewModelIndex; set => SetProperty(ref _selectedViewModelIndex, value);
        }
    }
}
