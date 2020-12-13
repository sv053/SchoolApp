using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace SchoolApp.Classes
{
    [Serializable]
    public class AttendDictSerial
    {
     
        public string GroupName { set; get; }
  
        ObservableCollection<Attendance>[] AttendanceList { set; get; } = new ObservableCollection<Attendance>[12];

    //    public ObservableCollection<string> studentsNames { set; get; } = new ObservableCollection<string>();

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
        
        public AttendDictSerial() { }
                
        public AttendDictSerial(string grName, Dictionary<int, ObservableCollection<Attendance>> yearGroupAtt)
        {
            GroupName = grName;

            foreach(int key in yearGroupAtt.Keys)
            {
               ObservableCollection<Attendance> monthGroupAtt = yearGroupAtt[key];
                AttendanceList[key-1] = monthGroupAtt;
            }

            JanAttendanceList = yearGroupAtt[1];
            FebAttendanceList = yearGroupAtt[2];
            MarAttendanceList = yearGroupAtt[3];
            AprAttendanceList = yearGroupAtt[4];
            MayAttendanceList = yearGroupAtt[5];
            JunAttendanceList = yearGroupAtt[6];
            JulAttendanceList = yearGroupAtt[7];
            AugAttendanceList = yearGroupAtt[8];
            SepAttendanceList = yearGroupAtt[9];
            OctAttendanceList = yearGroupAtt[10];
            NovAttendanceList = yearGroupAtt[11];
            DecAttendanceList = yearGroupAtt[12];

        }
    }
}
