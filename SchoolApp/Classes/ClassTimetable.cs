using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Classes
{
    enum Days
    {
        Monday = 0,
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4,
        Saturday = 5,
        Sunday = 6
    }

    class DailyClasses
    {
        public string DayOfWeek { set; get; }
        public ObservableCollection<string> DayClasses { set; get; }

        public enum Dayw { mon, tue, wed }

        public DailyClasses(ObservableCollection<Group> grps, string dayOfWeek)
        {
            DayOfWeek = dayOfWeek;
            GetDailyTimeTable(grps, DayOfWeek);
        }
        public DailyClasses()
        {

        }

        public ObservableCollection<string> GetDailyTimeTable(ObservableCollection<Group> grps, string weekday)
        {
            DayClasses = new ObservableCollection<string>();
            DayOfWeek = weekday;

            foreach (Group gr in grps)
            {
                if (gr.Day1 == weekday || gr.Day2 == weekday)
                {
                    DayClasses.Add(gr.Name);
                }
            }

            return DayClasses;
        }
    }
    class ClassTimetable
    {

        public string Monday { set; get; }
        public string Tuesday { set; get; }
        public string Wednesday { set; get; }
        public string Thursday { set; get; }
        public string Friday { set; get; }
        public string Saturday { set; get; }
        public string Sunday { set; get; }

        public ClassTimetable()
        {

        }
        public ClassTimetable(Group group)
        {
            int i = 1;
            if (group.Day1 == group.Day2)
                i = 2;

            if (group.Day1 == "Monday" || group.Day2 == "Monday")
                Monday = group.Name + "(" + i + ")";
            if (group.Day1 == "Tuesday" || group.Day2 == "Tuesday")
                Tuesday = group.Name + "(" + i + ")";
            if (group.Day1 == "Wednesday" || group.Day2 == "Wednesday")
                Wednesday = group.Name + "(" + i + ")";
            if (group.Day1 == "Thursday" || group.Day2 == "Thursday")
                Thursday = group.Name + "(" + i + ")";
            if (group.Day1 == "Friday" || group.Day2 == "Friday")
                Friday = group.Name + "(" + i + ")";
            if (group.Day1 == "Saturday" || group.Day2 == "Saturday")
                Saturday = group.Name + "(" + i + ")";
            if (group.Day1 == "Sunday" || group.Day2 == "Sunday")
                Sunday = group.Name + "(" + i + ")";
        }

        /*
        public Dictionary<string, ObservableCollection<string>> FormTimetable(ObservableCollection<Group> groups)
        {
            Days dayOfWeek = new Days();
            Array getDaysData = Enum.GetValues(dayOfWeek.GetType());
         //   School school = School.Instance;
         
            for (int i=0; i< getDaysData.Length; i++)
            {
                ObservableCollection<string> dayLessons = new ObservableCollection<string>();

                string tmp = getDaysData.GetValue(i).ToString();
                foreach (Group gr in groups)
                {
                    if (tmp == gr.Day1 || tmp == gr.Day2)
                        dayLessons.Add(gr.Name);
                }
                Tt.Add(tmp, dayLessons);
            }

            return Tt;
        }
       
        */


    }
}
