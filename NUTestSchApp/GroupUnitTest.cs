using NUnit.Framework;
using SchoolApp.Classes;
using SchoolApp.Dialogs;
using System.IO;

namespace NUTestSchApp
{
    public class Tests : Group
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [TestCase(0)]
        [TestCase(25)]
        [TestCase(3)]
        //public void Test2(int p)
        //{
        //    GroupEditor groupEditor = new GroupEditor();

        //    if (groupEditor != null)
        //        Assert.Pass();
        //}

        public void Test2(int p)
        {
          
                Assert.Pass();
        }
    }
}