using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserAuth;

namespace UserAuth.Tests
{


    [TestClass]
    public partial class DetectorTest
    {

        [TestMethod]
        public void ConstructorTest()
        {
            Detector target = new Detector();
            target.Run();
            System.Threading.Thread.Sleep(10);
            target.NewLetter("T");
            System.Threading.Thread.Sleep(12);
            target.NewLetter("e");
            System.Threading.Thread.Sleep(14);
            target.NewLetter("s");
            System.Threading.Thread.Sleep(13);
            target.NewLetter("t");
            System.Threading.Thread.Sleep(16);
            target.NewLetter("P");
            System.Threading.Thread.Sleep(12);
            target.NewLetter("a");
            System.Threading.Thread.Sleep(11);
            target.NewLetter("s");
            System.Threading.Thread.Sleep(18);
            target.NewLetter("s");
            System.Threading.Thread.Sleep(15);
            target.Stop();

        }

        

    }
}
