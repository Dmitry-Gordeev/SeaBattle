using System;
using System.Diagnostics;

namespace SeaBattle.Objects.ErrorHelper
{
    public class ErrorHelper
    {
        public static void FatalError(Exception e)
        {
            Trace.WriteLine(e);
        }
    }
}
