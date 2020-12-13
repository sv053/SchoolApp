using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SchoolApp.Dialogs;
using SchoolApp.Classes;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace SchoolApp
{
    public partial class MainWindow : Window
    {
        School school = School.Instance;
        public ObservableCollection<Group> Groups { set; get; } = School.Instance.Groups;
        public ObservableCollection<Student> Students { set; get; } = School.Instance.Students;
        public ObservableCollection<Teacher> Teachers { set; get; } = School.Instance.Teachers;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += LoadLists;
         //   Loaded += LoadItinerary;
        }

        private void FormTimetable(object sender, RoutedEventArgs e)
        {

        }

        private void LoadLists(object sender, RoutedEventArgs e)
        {
            DirectoryInfo dir;
            dir = new DirectoryInfo(@"..\..\..");
            string uriGL = dir.FullName + "\\GroupsSerializeList.xml";


            FileInfo fiGL = new FileInfo(uriGL);
           //   docGL = docSL = docTL = new XDocument();

            // загрузка групп
            try
            {
                if (!fiGL.Exists || (fiGL.Length == 0))
                {
                    alarMsg.Visibility = Visibility.Visible;
                    MessageBox.Show("Groups file is empty or doesn't exist");
                }
                else
                {

                    using (Stream fStream = new FileStream(uriGL, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Group>));

                        Groups = (ObservableCollection<Group>)xmlFormat.Deserialize(fStream);

                    }
                    school.Groups = Groups;
                }
            }
            catch { }

            // загрузка учеников
            string uriSL = dir.FullName + "\\StudentsSerializeList.xml";
            FileInfo fiSL = new FileInfo(uriSL);

            try
            {

                if (!fiSL.Exists || (fiSL.Length == 0))
                {
                    alarMsg.Visibility = Visibility.Visible;
                    MessageBox.Show("Students file is empty or doesn't exist");
                }
                else
                {

                    using (Stream fStream1 = new FileStream(uriSL, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        XmlSerializer xmlFormat1 = new XmlSerializer(typeof(ObservableCollection<Student>));

                        Students = (ObservableCollection<Student>)xmlFormat1.Deserialize(fStream1);

                    }
                    school.Students = Students;
                }
            }
            catch { }
            //загрузка учителей

            string uriTL = dir.FullName + "\\TeachersSerializeList";
            FileInfo fiTL = new FileInfo(uriTL);

            //    if (!fiTL.Exists || (fiTL.Length == 0))
            try
            {
                if ((fiTL.Length == 0))
                {
                    alarMsg.Visibility = Visibility.Visible;
                    MessageBox.Show("Teachers file is empty or doesn't exist");
                }
                else
                {
                    using (Stream fStream2 = new FileStream(uriTL, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        XmlSerializer xmlFormat2 = new XmlSerializer(typeof(ObservableCollection<Teacher>));

                        Teachers = (ObservableCollection<Teacher>)xmlFormat2.Deserialize(fStream2);

                    }
                }
            }
            catch { }

            school.Teachers = Teachers;

            foreach (Teacher t in Teachers)
            {
                t.AssignGroups();
            }
        }
/*
        private void LoadItinerary(object sender, RoutedEventArgs e)
        {
            //  Array getDaysData = Enum.GetValues(dayOfWeek.GetType());

            ObservableCollection<ClassTimetable> test = new ObservableCollection<ClassTimetable>();

            foreach (Group gr in Groups)
            {
                test.Add(new ClassTimetable(gr));
            }

            dataGrid.ItemsSource = test;

        }
        */
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            StudentsEditor studentsEditor = new StudentsEditor();

            studentsEditor.ShowInTaskbar = false;
            studentsEditor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            studentsEditor.ShowDialog();
        }

        private void TeachersMenu_Click(object sender, RoutedEventArgs e)
        {
            TeachersEditor teachersEditor = new TeachersEditor();

            teachersEditor.ShowInTaskbar = false;
            teachersEditor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            teachersEditor.ShowDialog();
        }

        private void GroupsMenu_Click(object sender, RoutedEventArgs e)
        {
            GroupEditor groupEditor = new GroupEditor();

            groupEditor.ShowInTaskbar = false;
            groupEditor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            groupEditor.ShowDialog();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            AttendanceEditor attendanceEditor = new AttendanceEditor();

            attendanceEditor.ShowInTaskbar = false;
            attendanceEditor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            attendanceEditor.ShowDialog();
        }

        private void Attendance_Click(object sender, RoutedEventArgs e)
        {
            TimetableEditor timetableEditor = new TimetableEditor();
            timetableEditor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            timetableEditor.ShowDialog();
        }


        private void AttendanceMenu_Click(object sender, RoutedEventArgs e)
        {
            AttendanceEditor attendanceEditor = new AttendanceEditor();

            attendanceEditor.ShowInTaskbar = false;
            attendanceEditor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            attendanceEditor.ShowDialog();
        }

        private void PaymentsMenu_Click(object sender, RoutedEventArgs e)
        {
            PaymentsEditor paymentsEditor = new PaymentsEditor();

            paymentsEditor.ShowInTaskbar = false;
            paymentsEditor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            paymentsEditor.ShowDialog();
        }

        private void PriceMainpage_Click(object sender, RoutedEventArgs e)
        {
            PriceEditor priceEditor = new PriceEditor();

            priceEditor.ShowInTaskbar = false;
            priceEditor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            priceEditor.ShowDialog();
        }

        private void Salary_Click(object sender, RoutedEventArgs e)
        {
            SalaryEditor salaryEditor = new SalaryEditor();

            salaryEditor.ShowInTaskbar = false;
            salaryEditor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            salaryEditor.ShowDialog();
        }

        private void Timetable_Click(object sender, RoutedEventArgs e)
        {
            TimetableEditor timetableEditor = new TimetableEditor();

            timetableEditor.ShowInTaskbar = false;
            timetableEditor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            timetableEditor.ShowDialog();
        }
    }
}
