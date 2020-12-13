using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace SchoolApp.Classes
{
    public class Group
    {
        int groupID;


        public string Name { set; get; }

        public int GroupID
        {
            set
            {
                groupID = value;

            }

            get { return groupID; }
        }

        public string Day1 { set; get; }
        public string Hour1 { set; get; }
        public string Day2 { set; get; }
        public string Hour2 { set; get; }
        public bool Delete { set; get; } = false;
        public string Teacher { set; get; }
        public int StudentsNumber { set; get; }
        //    public int ChildAge { set; get; }
        public int MaxChildAge { set; get; }
        public int MaxChildrenCount { set; get; } = 6;

        public YearGroupAtt GroupAttendance { set; get; } = new YearGroupAtt();
        //   public ObservableCollection<Attendance> GroupStudAttendance { set; get; } = new ObservableCollection<Attendance>();

        public int ClassDuration { set; get; } = 60;

        ObservableCollection<Student> studInGroup = new ObservableCollection<Student>();
        ObservableCollection<string> allTeachersNames = new ObservableCollection<string>();

        public ObservableCollection<string> AllTeachers { set; get; } = School.Instance.GetTeachersNames();

        /*  public ObservableCollection<Teacher> AllTeachers { set; get; } = School.Instance.Teachers;

          public ObservableCollection<string> AllTeachersNames
          {
              set
              {
                  value = School.Instance.GetTeachersNames(School.Instance.Teachers);
                  allTeachersNames = value;
              }
              get => allTeachersNames;
          } 
  */

        //   public DateTime DateTime { set; get; }
        //   public TimeSpan TimeSpan { set; get; }
        public string TimeSpan { set; get; }

        public DayOfWeek DayOfWeek { set; get; }
        public ObservableCollection<Student> StudInGroup
        {
            get => studInGroup;
            set
            {
                value = GetStudentsList();
                studInGroup = value;
            }
        }

        public Group() { }
        public Group(int age, DayOfWeek dow1, string time1, DayOfWeek dow2, string time2, string teacher, int idGroup)
        {
            Name = dow1.ToString().Substring(0, 3) + time1 + dow2.ToString().Substring(0, 3) + time2;
            Teacher = teacher;
            MaxChildAge = age;
            GroupID = idGroup;
            Day1 = dow1.ToString();
            Day2 = dow2.ToString();
            Hour1 = time1;
            Hour2 = time2;

        }


        public Group(string dow1, string time1, string dow2, string time2, string teacher, int age, int idGroup)
        {
            Name = dow1.Substring(0, 3) + time1 + dow2.Substring(0, 3) + time2;
            Day1 = dow1;
            Day2 = dow2;
            Hour1 = time1;
            Hour2 = time2;
            Teacher = teacher;
            MaxChildAge = age;
            GroupID = idGroup;

        }
        /* */
        public ObservableCollection<Student> AssignStudentsToGroup(Student st)
        {
            StudInGroup.Add(st);

            return StudInGroup;
        }

        public int SetClassDuration(int dur)
        {
            return ClassDuration = dur;
        }

        public ObservableCollection<Student> GetStudentsList()
        {
            School school = School.Instance;
            StudInGroup.Clear();

            foreach (Student stu in school.Students)
            {
                //    string fullStudName = new string();

                if (stu.Group == Name)
                {
                    StudInGroup.Add(stu);
                }
            }
            return StudInGroup;
        }
         /*
        public YearGroupAtt GetGroupAtt()
        {
            if (StudInGroup.Count > 0)
                GroupAttendance.getYearGroupAtt(StudInGroup);

            return GroupAttendance;
        }
       
        public ObservableCollection<Attendance> GetStudentsGroupAtt()
        {
            if (studInGroup.Count > 0)

                foreach (Student st in StudInGroup)
                    GroupStudAttendance.Add(st.StudAtt);

            return GroupAttendance;
        }
        */
    }
}
