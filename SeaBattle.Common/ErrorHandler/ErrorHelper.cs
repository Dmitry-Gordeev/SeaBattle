using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SeaBattle.Common.ErrorHandler
{
    public class ErrorHelper
    {
        public static void FatalError(Exception e)
        {
            Trace.WriteLine(e);
        }
    }
}
