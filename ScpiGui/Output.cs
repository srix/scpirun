using System;
using System.Collections.Generic;
using System.Text;

namespace ScpiTest
{
    interface Output
    {
        void ShowCommand(ScpiCommand scpiCmd);
        void ShowResult(string scpiCmd);
        void ShowError(string errorStr);
    }
}
