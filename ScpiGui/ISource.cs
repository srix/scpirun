using System;
using System.Collections.Generic;
using System.Text;

namespace ScpiTest
{
    interface ISource
    {
        //void ReadNextLine(ref string readStr, ref bool status);
      bool GetNextLine(ref string readStr);
    }
}
