using System;
using System.Collections.Generic;
using System.Text;

namespace ScpiTest
{
    interface IOutput
    {
        void ShowCommand(ScpiCommand scpiCmd);
        void ShowResult(string scpiCmd);
        void ShowError(string errorStr);
        void ShowWait(ScpiCommand scpiCmd);
    }
}
