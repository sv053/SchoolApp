using System;
using NUnit.Framework;
using SchoolApp.Classes;

namespace NUTestSchApp
{
    class StudentUnitTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

       
        //[TestCase("фамилия", "имя", "отчество", 145, "номер телефона", "группа", "имя родителя", "комментарий")]
        //[TestCase(25, 17, 4, "iuihiuhi", "")]
        //[TestCase("", "", "", 3.984, "", "", "", "")]
        //public void CreateNewStudentTest(string s, string n, string p, int age, string phone, string grNum, string parName, string comment)
        //{
            
        //    Student newSt = new Student(s, n, p, age, phone, grNum, parName, comment);

        //  //  if(newSt != null)
        //    Assert.IsNotNull(newSt);
        //}

        [Test]
        
        public void CreateNewStudentTest(string s, string n, string p, int age, string phone, string grNum, string parName, string comment)
        {

            Student newSt = new Student("фамилия", "имя", "отчество", 145, "номер телефона", "группа", "имя родителя", "комментарий");

            //  if(newSt != null)
            Assert.IsNull(newSt);
        }

        //[Test]
        //public void CreateNewStudentTest1(string s, string n, string p, int age, string phone, string grNum, string parName, string comment)
        //{

        //    Student newSt = new Student("", "", 25, 17, 4, "iuihiuhi", "");

        //    //  if(newSt != null)
        //    Assert.IsNotNull(newSt);
        //}

        [TestCase(25, 17, 4, "iuihiuhi", "")]
        [TestCase("", "", "", 3.984, "", "", "", "")]
        public void CreateNewStudentTest2(string s, string n, string p, int age, string phone, string grNum, string parName, string comment)
        {

            Student newSt = new Student(s, n, p, age, phone, grNum, parName, comment);

       //  Data data = new Data();
            object t = null;
            string e = string.Empty;

         //   Assert.Throws<ArgumentNullException>(() => data.Eval(t, e));
            //Assert.Throws<ArgumentNullException>(() => newSt.Eval(t, e));

            //  if(newSt != null)
            //Assert.Throws(Exception e, newSt);
        }

        [TestCase("фамилия", "имя", "отчество", "номер телефона", "дата", 500, "комментарий")]
        [TestCase(211.57, "", 17, 4, "iuihiuhi", "")]
        [TestCase("", "", "", "", "", "", "", "")]
        public void CreateNewTeacherTest3(string f, string i, string o, string phone, string bdate, int rate = 300, string st = "")
        {
         Teacher teacher = new Teacher(f,i,o,phone,bdate,rate,st);
            if(teacher != null)
                Assert.Pass();
        }
    }
}
