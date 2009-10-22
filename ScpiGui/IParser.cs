using System;
using System.Collections.Generic;
using System.Text;

namespace ScpiTest
{
    interface IParser
    {
        bool GetNextLine(ref ScpiCommand scpiCmd);
    }
}
