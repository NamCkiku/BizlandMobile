using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bizland.Core.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExtendedNavigationPage : SharedTransitionNavigationPage
    {
        public ExtendedNavigationPage(Page page) : base(page)
        {
            InitializeComponent();
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}