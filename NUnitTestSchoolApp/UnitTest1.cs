using NUnit.Framework;

using SchoolApp.Dialogs;


namespace NUnitTestSchoolApp
{

    public class Tests : GroupEditor
    {
        public Tests()
        {
        }

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
        [TestCase(1)]
        [TestCase(-1)]
        public void Test2(int p)
        {
            Assert.AreEqual(0, p);
        }
    }
}