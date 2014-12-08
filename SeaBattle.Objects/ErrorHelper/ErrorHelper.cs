using System;
using System.Diagnostics;

namespace SeaBattle.Service.ErrorHelper
{
    public class ErrorHelper
    {
        public static void FatalError(Exception e)
        {
            Trace.WriteLine(e);
        }
    }
}
