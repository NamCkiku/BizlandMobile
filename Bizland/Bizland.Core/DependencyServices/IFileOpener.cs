using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.Core
{
    public interface IFileOpener
    {
        void OpenFile(byte[] data, string name);
    }
}
