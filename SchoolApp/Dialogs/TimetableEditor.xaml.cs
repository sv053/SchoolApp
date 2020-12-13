using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SchoolApp.Classes;

namespace SchoolApp.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для TimetableEditor.xaml
    /// </summary>
    public partial class TimetableEditor : Window
    {
        public TimetableEditor()
        {
            InitializeComponent();

            Loaded += LoadItinerary;
            Loaded += TimetableEditor_Loaded;
        }

        private void LoadItinerary(object sender, RoutedEventArgs e)
        {
            //  Array getDaysData = Enum.GetValues(dayOfWeek.GetType());
            School school = School.Instance;
            ObservableCollection<Group> groups = school.Groups;
            ObservableCollection<ClassTimetable> tt = new ObservableCollection<ClassTimetable>();

            foreach (Group gr in groups)
            {
                tt.Add(new ClassTimetable(gr));
            }

            dg1.ItemsSource = tt;

        }

        private void TimetableEditor_Loaded(object sender, RoutedEventArgs e)
        {
            School school = School.Instance;
            ObservableCollection < Group > groups = school.Groups;
            ObservableCollection<Student> students = school.Students;
         //   ObservableCollection<Attendance> groupAttendance = new ObservableCollection<Attendance>();

            List<DailyClasses> ldc = new List<DailyClasses>();
        //    ObservableCollection<Student> studInGroup = new ObservableCollection<Student>();


            Days dayOfWeek = new Days();
            Array getDaysData = Enum.GetValues(dayOfWeek.GetType());

            for (int i = 0; i < getDaysData.Length; i++)
            {
                string tmp = getDaysData.GetValue(i).ToString();

                DailyClasses dc = new DailyClasses();
                dc.GetDailyTimeTable(groups, tmp);

                ldc.Add(dc);
            }
            /*
            for (int i = 0; i<groups.Count; i++)
            {
                //    groupAttendance.Clear();
                //   studInGroup.Clear();
                ObservableCollection<Student> studInGroup = new ObservableCollection<Student>();
                ObservableCollection<Attendance> groupAttendance = new ObservableCollection<Attendance>();


                studInGroup = groups[i].GetStudentsList();
            
                    foreach (Student st in studInGroup)
                    {
                     //   if (st.Group == ldc[i].DayClasses[i])
                            groupAttendance.Add(new Attendance(st));
                    }

                        Label label = new Label();
                        label.Foreground = Brushes.Black;
                        label.FontWeight = FontWeights.Bold;
                        label.FontStyle = FontStyles.Italic;
                        label.FontSize = 14;
                label.Background = Brushes.MistyRose;
                label.FontFamily = new FontFamily("Arial");
                label.Content = "Группа "+ groups[i].Name;
                            mainStackpanel.Children.Add(label);
                //   lbl.Content = label;
             
                mainStackpanel.Children.Add(new DataGrid()
                {
                    ItemsSource = groupAttendance,
                    SelectionMode = DataGridSelectionMode.Extended,
                    AutoGenerateColumns = true,
                    FontWeight = FontWeights.Bold,
                    FontFamily = new FontFamily("Arial"),
                    RowBackground = Brushes.LightSteelBlue,
                    
                });

            } */
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Attendance> generalAttendance = new ObservableCollection<Attendance>();

        }
    }
}
