using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Classes
{
    class Calendar
    {

        public Dictionary<string, List<DateTime>> calendar = new Dictionary<string, List<DateTime>>();



        public Dictionary<string, List<DateTime>> GetGroupYearCalendar(Group group)
        {

            calendar.Clear();

            for (int i = 1; i <= 12; i++)
            {
                /*          int daysInMonth = System.DateTime.DaysInMonth(2019, i);

                      List<DateTime> dtl = new List<DateTime>();

                              for (int j = 1; j <= daysInMonth; j++)
                              {
                                  DateTime checkingDate = new DateTime(2019, i, j);

                                  if (group.Day1 == checkingDate.DayOfWeek.ToString()||group.Day2 == checkingDate.DayOfWeek.ToString())
                                  dtl.Add(new DateTime(2019, i, j));
                              }*/
                DateTime date = new DateTime(2019, i, 1);

                //   GetGroupMonthCalendar(group, i);

                //    calendar.Add(date.Month.ToString(), dtl);
                calendar.Add(date.Month.ToString(), GetGroupMonthCalendar(group, i));
            }
            return calendar;
        }

        public List<DateTime> GetGroupMonthCalendar(Group group, int month)
        {

            //   calendar.Clear();


            int daysInMonth = System.DateTime.DaysInMonth(2019, month);

            List<DateTime> dtl = new List<DateTime>();

            for (int j = 1; j <= daysInMonth; j++)
            {
                DateTime checkingDate = new DateTime(2019, month, j);

                if (group.Day1 == checkingDate.DayOfWeek.ToString() || group.Day2 == checkingDate.DayOfWeek.ToString())
                    dtl.Add(new DateTime(2019, month, j));
            }
            //  DateTime date = new DateTime(2019, month, 1);

            //   calendar.Add(date.Month.ToString(), dtl);

            return dtl;
        }

        public int CountMonthGroupsLessons(List<Group> lg, int month)
        {
            int sum = 0;

            foreach (Group g in lg)
            {
                sum += GetGroupMonthCalendar(g, month).Count;
            }
            return sum;
        }
    }
}


