using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SchoolApp.Classes
{

    [Serializable]
    public class Attendance
    {
        //   public Student Student { set; get; }
        public string StudName { set; get; }

        public bool _1 { set; get; }
        public bool _2 { set; get; }
        public bool _3 { set; get; }
        public bool _4 { set; get; }
        public bool _5 { set; get; }
        public bool _6 { set; get; }
        public bool _7 { set; get; }
        public bool _8 { set; get; }
        public bool _9 { set; get; }
        public bool _10 { set; get; }
        public bool _11 { set; get; }
        public bool _12 { set; get; }
        public bool _13 { set; get; }
        public bool _14 { set; get; }
        public bool _15 { set; get; }
        public bool _16 { set; get; }
        public bool _17 { set; get; }
        public bool _18 { set; get; }
        public bool _19 { set; get; }
        public bool _20 { set; get; }
        public bool _21 { set; get; }
        public bool _22 { set; get; }
        public bool _23 { set; get; }
        public bool _24 { set; get; }
        public bool _25 { set; get; }
        public bool _26 { set; get; }
        public bool _27 { set; get; }
        public bool _28 { set; get; }
        public bool _29 { set; get; }
        public bool _30 { set; get; }
        public bool _31 { set; get; }


        public Attendance() { }
        public Attendance(Student st)
        {
            //   Student = st;
            StudName = st.F + " " + st.I + " " + st.O;
        }



    }
}
