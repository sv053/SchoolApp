using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Classes
{
    public class YearGroupAtt
    {

        public ObservableCollection<Attendance> JanAttendanceList { set; get; } = new ObservableCollection<Attendance>();

        public ObservableCollection<Attendance> FebAttendanceList { set; get; } = new ObservableCollection<Attendance>();

        public ObservableCollection<Attendance> MarAttendanceList { set; get; } = new ObservableCollection<Attendance>();

        public ObservableCollection<Attendance> AprAttendanceList { set; get; } = new ObservableCollection<Attendance>();

        public ObservableCollection<Attendance> MayAttendanceList { set; get; } = new ObservableCollection<Attendance>();

        public ObservableCollection<Attendance> JunAttendanceList { set; get; } = new ObservableCollection<Attendance>();

        public ObservableCollection<Attendance> JulAttendanceList { set; get; } = new ObservableCollection<Attendance>();

        public ObservableCollection<Attendance> AugAttendanceList { set; get; } = new ObservableCollection<Attendance>();

        public ObservableCollection<Attendance> SepAttendanceList { set; get; } = new ObservableCollection<Attendance>();

        public ObservableCollection<Attendance> OctAttendanceList { set; get; } = new ObservableCollection<Attendance>();

        public ObservableCollection<Attendance> NovAttendanceList { set; get; } = new ObservableCollection<Attendance>();

        public ObservableCollection<Attendance> DecAttendanceList { set; get; } = new ObservableCollection<Attendance>();

        public ObservableCollection<string> stName { set; get; } = new ObservableCollection<string>();

        public Group Group { set; get; }
        public YearGroupAtt() { }

        /*
         public YearGroupAtt(Group gr)
         {
             Group = gr;

             foreach (Student st in gr.StudInGroup)
             {

                 stName.Add( st.F + " " + st.I + " " + st.O);

                 JanAttendanceList.Add(st.StudAtt[1]);
                 FebAttendanceList.Add(st.StudAtt[2]);
                 MarAttendanceList.Add(st.StudAtt[3]);
                 AprAttendanceList.Add(st.StudAtt[4]);
                 MayAttendanceList.Add(st.StudAtt[5]);
                 JunAttendanceList.Add(st.StudAtt[6]);
                 JulAttendanceList.Add(st.StudAtt[7]);
                 AugAttendanceList.Add(st.StudAtt[8]);
                 SepAttendanceList.Add(st.StudAtt[9]);
                 OctAttendanceList.Add(st.StudAtt[10]);
                 NovAttendanceList.Add(st.StudAtt[11]);
                 DecAttendanceList.Add(st.StudAtt[12]);
             }
         }
         */
        public void getYearGroupAtt(ObservableCollection<Student> stList)
        {
            //   Group = gr;

            foreach (Student st in stList)
            {

                stName.Add(st.F + " " + st.I + " " + st.O);
                //    st.SetStudentAttendance();

                JanAttendanceList.Add(st.StudAtt[0]);
                FebAttendanceList.Add(st.StudAtt[1]);
                MarAttendanceList.Add(st.StudAtt[2]);
                AprAttendanceList.Add(st.StudAtt[3]);
                MayAttendanceList.Add(st.StudAtt[4]);
                JunAttendanceList.Add(st.StudAtt[5]);
                JulAttendanceList.Add(st.StudAtt[6]);
                AugAttendanceList.Add(st.StudAtt[7]);
                SepAttendanceList.Add(st.StudAtt[8]);
                OctAttendanceList.Add(st.StudAtt[9]);
                NovAttendanceList.Add(st.StudAtt[10]);
                DecAttendanceList.Add(st.StudAtt[11]);
            }
        }
    }
}
