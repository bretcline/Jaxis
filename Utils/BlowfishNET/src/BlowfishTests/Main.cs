using System;
using System.Collections.Generic;
using System.Text;

namespace BlowfishNET.Tests
{
    /// <summary>Playground to debug single tests in Visual C# Express Edition,
    /// where class libraries cannot be associated with a calling process.</summary>
    /// <remarks>For own experiments make sure that the output type is changed
    /// from "Class Library" to "Console Application".</remarks>
    class Application
    {
        static void Main()
        {
            TestBlowfishAlgorithm tbfa = new TestBlowfishAlgorithm();
            tbfa.TestCryptoStreams();
            TestJavaInterop tji = new TestJavaInterop();
            tji.TestBlowfishStream();
            tji.TestBlowfishJXchg();
            TestCore tc = new TestCore();
            tc.Test();
            tc.TestCFBOpenSSL();
            TestBlowfishSimple tbfs = new TestBlowfishSimple();
            tbfs.Test();
        }
    }
}
