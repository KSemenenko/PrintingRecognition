using System;
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
            target.Start();
            System.Threading.Thread.Sleep(10);
            target.Add("T");
            System.Threading.Thread.Sleep(12);
            target.Add("e");
            System.Threading.Thread.Sleep(14);
            target.Add("s");
            System.Threading.Thread.Sleep(13);
            target.Add("t");
            System.Threading.Thread.Sleep(16);
            target.Add("P");
            System.Threading.Thread.Sleep(12);
            target.Add("a");
            System.Threading.Thread.Sleep(11);
            target.Add("s");
            System.Threading.Thread.Sleep(18);
            target.Add("s");
            System.Threading.Thread.Sleep(15);
            target.Stop();

        }

        

    }
}
