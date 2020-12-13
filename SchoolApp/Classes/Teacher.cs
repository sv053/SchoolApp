using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Classes
{
    public class Teacher //: IEquatable<Teacher>
    {
        School school = School.Instance;
        public string F { set; get; }
        public string I { set; get; }
        public string O { set; get; }
        public string Phone { set; get; }

        public string BDate { set; get; }

        public bool Edit { set; get; } = false;
        public bool Delete { set; get; } = false;

        public int HourlyRate { set; get; } = 300;

        public string AddData { set; get; }

        public List<string> AssignedGroups { set; get; } = new List<string>() { "tfytfyf", "dtrttu", "hohoioho" };

        public Teacher() { }
        public Teacher(string f, string i, string o, string phone, string bdate, int rate=300, string st ="")
        {
            F = f.Substring(0, 1).ToUpper() + f.Substring(1).ToLower();
            I = i.Substring(0, 1).ToUpper() + i.Substring(1).ToLower();
            O = o.Substring(0, 1).ToUpper() + o.Substring(1).ToLower();
            Phone = phone;
            BDate = bdate;
            HourlyRate = rate;
            AddData = st;
        }

        public List<string> AssignGroups()
        {
            AssignedGroups = new List<string>();

            if (AssignedGroups == null)
            {
                AssignedGroups = new List<string>();
            }
            else
                AssignedGroups.Clear();

            foreach(Group gr in school.Groups)
            {
                if (gr.Teacher == F)
                    AssignedGroups.Add(gr.Name);
            }
            return AssignedGroups;
        }

        /*
        public bool Equals(Teacher teer)
        {
            if (teer == null)
                return false;
            Teacher tmpTeacher = teer as Teacher;

            if (tmpTeacher == null)
                return false;
            else
                return Equals(tmpTeacher);
        }

       */
    }
}
