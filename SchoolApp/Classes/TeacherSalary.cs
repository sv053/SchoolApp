using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Classes
{
   public class TeacherSalary
    {
        Teacher teacher;
        public string Teacher { set; get; }

        public List<Salary> MonthsSalary { set; get; } = new List<Salary>();
        public Salary September { set; get; } 
        public Salary October { set; get; }
        public Salary November { set; get; }
        public Salary December { set; get; }
        public Salary January { set; get; }
        public Salary February { set; get; }
        public Salary March { set; get; }
        public Salary April { set; get; }
        public Salary May { set; get; }
        public Salary June { set; get; }
        public Salary July { set; get; }

        public Salary August { set; get; }

        public TeacherSalary(Teacher t)
        {
            teacher = t;
            Teacher = t.F;
            
        }

        public void getTeacherGroupsCalendar()
        {
            School school = School.Instance;
           Classes.Calendar calendar = new Classes.Calendar();


            var teacherGroups = school.Groups.Where(group => group.Teacher == Teacher).ToList(); // список групп учителя


            September = new Salary("Сентябрь", calendar.CountMonthGroupsLessons(teacherGroups, 9), teacher);
            October = new Salary("Октябрь", calendar.CountMonthGroupsLessons(teacherGroups, 10), teacher);
            November = new Salary("Ноябрь", calendar.CountMonthGroupsLessons(teacherGroups, 11), teacher);
            December = new Salary("Декабрь", calendar.CountMonthGroupsLessons(teacherGroups, 12), teacher);
            January = new Salary("Январь", calendar.CountMonthGroupsLessons(teacherGroups, 1), teacher);
            February = new Salary("Февраль", calendar.CountMonthGroupsLessons(teacherGroups, 2), teacher);
            March = new Salary("Март", calendar.CountMonthGroupsLessons(teacherGroups, 3), teacher);
            April = new Salary("Апрель", calendar.CountMonthGroupsLessons(teacherGroups, 4), teacher);
            May = new Salary("Май", calendar.CountMonthGroupsLessons(teacherGroups, 5), teacher);
            June = new Salary("Июнь", calendar.CountMonthGroupsLessons(teacherGroups, 6), teacher);
            July = new Salary("Июль", calendar.CountMonthGroupsLessons(teacherGroups, 7), teacher);
            August = new Salary("Август", calendar.CountMonthGroupsLessons(teacherGroups, 8), teacher);

            MonthsSalary.Add(September);
            MonthsSalary.Add(October);
            MonthsSalary.Add(November);
            MonthsSalary.Add(December);
            MonthsSalary.Add(January);
            MonthsSalary.Add(February);
            MonthsSalary.Add(March);
            MonthsSalary.Add(April);
            MonthsSalary.Add(May);
            MonthsSalary.Add(June);
            MonthsSalary.Add(July);
            MonthsSalary.Add(August);
          
        }
    }
}
