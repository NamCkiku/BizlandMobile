using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.Core
{
    public interface IProgressUpdate
    {
        void ProgressUpdate(int id, int ProgressPercentage);
        void ProgressCancel(int id);
    }
}
