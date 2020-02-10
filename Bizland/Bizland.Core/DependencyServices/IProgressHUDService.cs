using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.Core
{
    public interface IProgressHUDService
    {
        void StartHUD(string message);

        void DisposeHUD();
    }
}
