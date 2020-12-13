using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Classes
{
   public class Salary
    {
        public string Month { set; get; }
        public int ClassesAmount {set; get;}

        public int MonthSalary { set; get; }
        public string MonthData { set; get; }


        public Salary(string month, int ca, Teacher t)
        {
            Month = month;
            ClassesAmount = ca;
            MonthSalary = ca*t.HourlyRate;

         //   MonthSalary = ca * 300;

            MonthData = ca.ToString() + "/" + MonthSalary;
        }

    }
}
