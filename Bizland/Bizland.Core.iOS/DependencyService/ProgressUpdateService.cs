using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bizland.Core.iOS.DependencyService;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ProgressUpdateService))]
namespace Bizland.Core.iOS.DependencyService
{
    public class ProgressUpdateService : IProgressUpdate
    {
        public void ProgressCancel(int id)
        {
            throw new NotImplementedException();
        }

        public void ProgressUpdate(int id, int ProgressPercentage)
        {
            throw new NotImplementedException();
        }
    }
}