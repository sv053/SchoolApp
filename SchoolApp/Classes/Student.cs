using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Classes
{
  public class Student
    {
        public string F { set; get; } = "ффф";
        public string I { set; get; } = "иии";
        public string O { set; get; } = "ооо";
        public string Group { set; get; } = "не назначена";
        public int Age { set; get; } = 10;
        public string PhoneNumber { set; get; } = "-----------";
        public string Addata { set; get; }
        public string Parent { set; get; }

        public ObservableCollection<Attendance> StudAtt { set; get; } = new ObservableCollection<Attendance>();
        //    Attendance StudAtt { set; get; }

        //   public bool Edit { set; get; }
        public bool Delete { set; get; }

        public Student() { }
        public Student(string f, string i, string o, int age, string phone, string group, string parent, string addata = "", bool edit = false, bool del = false )
        {
           try
            {
                  F =  f.Substring(0, 1).ToUpper() + f.Substring(1).ToLower();
                  I = i.Substring(0, 1).ToUpper() + i.Substring(1).ToLower();
                  O = o.Substring(0, 1).ToUpper() + o.Substring(1).ToLower();

                Age = age;
                PhoneNumber = phone;
                Group = group;
                Parent = parent.ToUpper();
                Addata = addata;

                for (int j = 0; j < 12; j++)
                {
                    StudAtt.Add(new Attendance());
                }

                if (f == null)  
                    {
                        throw new Exception("surname is empty");
                    }
                else if(i == null)
                {
                    throw new Exception("name is empty");
                }
                else if (o == null)
                {
                    throw new Exception("pathronymic is empty");
                }
                else if(phone == null)
                {
                    throw new Exception("phone is empty");
                }
            }
            catch(Exception e)
            {
                // throw new Exception();
                string errText = e.Message;
                throw;
            }
        }

        public string PutIntoGroup(Student st, string group)
        {
            return st.Group = group;
        }
        
        public string AddParent(Student st, string parent)
        {
            return st.Parent = parent;
        }

        /*
        public ObservableCollection<Attendance> SetStudentAttendance()
        {
            StudAtt = new ObservableCollection<Attendance>();

            for (int i = 0; i<12; i++)
            {
                StudAtt.Add(new Attendance());
            }
            
            return StudAtt;
        }
        */
    }
}
