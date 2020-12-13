using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Xml.Serialization;
using SchoolApp.Classes;

namespace SchoolApp.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AttendanceEditor.xaml
    /// формируется ведомость посещаемости по всем группам, загружается сохранённая для групп, 
    /// которые уже существовали, для новых формируется новая ведомость
    /// </summary>
    /// 

    public partial class AttendanceEditor : Window
    {
        static DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        static string uriAL = dir.FullName + "\\AttendanceSerializeList.xml";

        delegate ObservableCollection<Attendance> SaveAttendance(ObservableCollection<Attendance> att);

        Dictionary<string, List<DateTime>> calendar = new Dictionary<string, List<DateTime>>();

        Attendance selectedAtt;
        public Attendance SelectedAtt
        {
            get => selectedAtt;
            set
            {
                if (value != null)
                {
                    selectedAtt = value;
                   //   attDatagrid.DataContext = value;
                }
            }
        }

        public Dictionary<int, ObservableCollection<Attendance>> YearGroupAttendance { get; set; }
        //  public Dictionary<string, ObservableCollection<Attendance>> Attendances { get; set; } = new Dictionary<string, ObservableCollection<Attendance>>();
        public ObservableCollection<Group> Groups { get; set; } = School.Instance.Groups;

        public ObservableCollection<AttendDictSerial> ADS { set; get; } = new ObservableCollection<AttendDictSerial>();
        public ObservableCollection<AttendDictSerial> exADS { set; get; } = new ObservableCollection<AttendDictSerial>();

        //   public ObservableCollection<YearGroupAtt> exADS { set; get; } = new ObservableCollection<YearGroupAtt>();
        public ObservableCollection<YearGroupAtt> allAtt { set; get; } = new ObservableCollection<YearGroupAtt>();

        //   public AttendDictSerial AdsGroup { set; get; } 

        public AttendanceEditor()
        {
            InitializeComponent();

            //   
            Loaded += getAttendance;
            Loaded += OutputData;
            this.DataContext = this;

        }

        private void getAttendance(object sender, RoutedEventArgs e)
        {

            foreach (Group gr in Groups)
            {
                ObservableCollection<Student> studs = gr.GetStudentsList();
                //    Attendances.Add(gr.Name, gr.GetGroupAttendance());
            }
        }

        private void OutputData(object sender, RoutedEventArgs e)
        {
            /*
            School school = School.Instance;
            List<DailyClasses> ldc = new List<DailyClasses>();
            ObservableCollection<ObservableCollection<string>> dtt = new ObservableCollection<ObservableCollection<string>>();

            Days dayOfWeek = new Days();
            Array getDaysData = Enum.GetValues(dayOfWeek.GetType());
         
            for (int i = 0; i < getDaysData.Length; i++)
            {
               string tmp = getDaysData.GetValue(i).ToString();

                DailyClasses dc = new DailyClasses();
                dc.GetDailyTimeTable(Groups, tmp);

                ldc.Add(dc);
             }

            foreach(DailyClasses dcl in ldc)
            {
                dtt.Add(dcl.DayClasses);
           }
           */
            //-----------------------------------------------------

            foreach (Group gr in Groups)
            {
                TreeViewItem treeViewItem = new TreeViewItem()
                {
                    Header = gr.Name,
                    Tag = gr.Teacher,
                    BorderThickness = new Thickness(1),
                    Foreground = Brushes.LightBlue,
                    Cursor = Cursors.Hand


                };
                treeViewItem.Selected += treeViewItemSelected;

                foreach (Student st in gr.GetStudentsList())
                {
                    TreeViewItem tvi = new TreeViewItem();
                    tvi.Header = st.F + " " + st.I + " " + st.O;
                    tvi.Cursor = Cursors.No;
                    tvi.Foreground = Brushes.LightCoral;
                    treeViewItem.Items.Add(tvi);
                }
                mainTreeView.Items.Add(treeViewItem);
            }


            FileInfo fi = new FileInfo(uriAL);

            // создать годовой список посещаемости группы
            //   YearGroupAttendance.Clear();

            //    if (ADS.Count == 0 || !fi.Exists)

            if (ADS.Count == 0)
            {

                foreach (Group gr in Groups)
                {

                    YearGroupAttendance = new Dictionary<int, ObservableCollection<Attendance>>();

                    for (int i = 1; i < 13; i++)
                    {
                        ObservableCollection<Attendance> att = new ObservableCollection<Attendance>();

                        foreach (Student st in gr.StudInGroup)
                        {
                            att.Add(new Attendance(st));
                        }
                        YearGroupAttendance.Add(i, att);
                    }
                    ADS.Add(new AttendDictSerial(gr.Name, YearGroupAttendance));

                }
            }

            try
            {
                // загрузить сохранённую ранее посещаемость 

                using (Stream fStream = new FileStream(uriAL, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<AttendDictSerial>));

                    exADS = (ObservableCollection<AttendDictSerial>)xmlFormat.Deserialize(fStream);

                }
            }
            catch { MessageBox.Show("can't read data"); }

            // проверить соответствие текущему списку групп, если группа уже была, загрузить для неё сохранённые данные

            //    allAtt = new ObservableCollection<YearGroupAtt>();

            foreach (Group gr in Groups)
            {

                //     gr.GetGroupAtt();
                //    gr.GroupAttendance = new YearGroupAtt(gr.StudInGroup);
                //    allAtt.Add(new YearGroupAtt(gr.StudInGroup));
                //   YearGroupAtt tmpYearGroupAtt = new YearGroupAtt(gr);
                var grAtt = exADS.Where(a => a.GroupName == gr.Name).FirstOrDefault();

                /// проверить, не изменился ли состав группы, если изменился, для существующих учеников
                /// добавить сохранённую посещаемость, а для новых оставить пустую
                if (grAtt != null)
                {

                    //   gr.GroupAttendance = grAtt;
                    //   allAtt.Add(grAtt);
                    var group = Groups.Where(g => g.Name == grAtt.GroupName).FirstOrDefault();
                    ADS.Remove(ADS.Where(d => d.GroupName == gr.Name).FirstOrDefault());
                    ADS.Add(grAtt);

                    /*
                  foreach (Student st in gr.StudInGroup)
                  {

                      string stName = st.F + " " + st.I + " " + st.O;

                      if (grAtt.stName.Where(sname => sname == stName).FirstOrDefault() != null)
                      {
                          allAtt.Remove(ADS.Where(d => d.GroupName == gr.Name).FirstOrDefault());
                          ADS.Add(grAtt);

                      }
                    //       att.Add(new Attendance(st));
                  }
                  */



                }
            }
        }
        // построить каталог групп с учениками 
        private void treeViewItemSelected(object sender, RoutedEventArgs e)
        {

            TreeViewItem send = sender as TreeViewItem;
            string str = send.Header.ToString();

            //    TheGroupAttendance.Clear();

            Group gr = Groups.Where(g => g.Name.Contains(str)).FirstOrDefault();
            GetCalendar(gr);

            Attendance attendance = new Attendance();
            Type Attendance = attendance.GetType();
            PropertyInfo[] properties = Attendance.GetProperties();

            AttendDictSerial ads = ADS.Where(a => a.GroupName == gr.Name).FirstOrDefault();



            var yearGroupAtt1 = allAtt.Where(a => a.Group.Name.Contains(str)).FirstOrDefault();

            // получить годовой календарь для выбранной группы


            if (ads != null)
            {
                sepGrid.Children.Add(GetMonthCalendarGrid(gr, properties, 9, ads.SepAttendanceList));
                octGrid.Children.Add(GetMonthCalendarGrid(gr, properties, 10, ads.OctAttendanceList));
                novGrid.Children.Add(GetMonthCalendarGrid(gr, properties, 11, ads.NovAttendanceList));
                decGrid.Children.Add(GetMonthCalendarGrid(gr, properties, 12, ads.DecAttendanceList));
                janGrid.Children.Add(GetMonthCalendarGrid(gr, properties, 1, ads.JanAttendanceList));
                febGrid.Children.Add(GetMonthCalendarGrid(gr, properties, 2, ads.FebAttendanceList));
                marGrid.Children.Add(GetMonthCalendarGrid(gr, properties, 3, ads.MarAttendanceList));
                aprGrid.Children.Add(GetMonthCalendarGrid(gr, properties, 4, ads.AprAttendanceList));
                mayGrid.Children.Add(GetMonthCalendarGrid(gr, properties, 5, ads.MayAttendanceList));
                junGrid.Children.Add(GetMonthCalendarGrid(gr, properties, 6, ads.JunAttendanceList));
                julGrid.Children.Add(GetMonthCalendarGrid(gr, properties, 7, ads.JulAttendanceList));
                augGrid.Children.Add(GetMonthCalendarGrid(gr, properties, 8, ads.AugAttendanceList));

                cancelBtn.Visibility = Visibility.Visible;
                saveBtn.Visibility = Visibility.Visible;

            }
            else
            {
                MessageBox.Show("group doesn't exists");
            }

            //    saveBtn.Click += new RoutedEventHandler(groupAttendance, saveAttendance);
        }

        // нарисовать таблицу посещаемости для выбранной группы
        private DataGrid GetMonthCalendarGrid(Group gr, PropertyInfo[] props, int month, ObservableCollection<Attendance> atts)
        {

            DataGrid dGrid = new DataGrid()
            {

                AutoGenerateColumns = false,
                ItemsSource = atts,
                //   FontWeight = FontWeights.Bold,
                //   FontFamily = new FontFamily("Arial"),
                RowBackground = Brushes.Aquamarine,
                AlternatingRowBackground = Brushes.Gainsboro,
                CanUserAddRows = false
            };

            Binding binding = new Binding("StudName");
            binding.Mode = BindingMode.OneWay;
            DataGridTextColumn col = new DataGridTextColumn();
            col.Header = "Ученик";
            col.Binding = binding;
            dGrid.Columns.Add(col);


            foreach (DateTime date in calendar[month.ToString()])
            {
                int i = 0;
                foreach (PropertyInfo pi in props)
                {
                    i++;

                    if (i == date.Day)
                    {
                        Binding binding1 = new Binding(pi.Name);
                        binding1.Mode = BindingMode.TwoWay;

                        DataGridCheckBoxColumn col1 = new DataGridCheckBoxColumn();
                        col1.Header = date.Day;
                        col1.Binding = binding1;
                        dGrid.Columns.Add(col1);
                    }
                }
            }
            return dGrid;
        }

        // получить календарь на год
        private void GetCalendar(Group group)
        {
            calendar.Clear();

            for (int i = 1; i <= 12; i++)
            {
                int daysInMonth = System.DateTime.DaysInMonth(2019, i);

                List<DateTime> dtl = new List<DateTime>();

                for (int j = 1; j <= daysInMonth; j++)
                {
                    DateTime checkingDate = new DateTime(2019, i, j);

                    if (group.Day1 == checkingDate.DayOfWeek.ToString() || group.Day2 == checkingDate.DayOfWeek.ToString())
                        dtl.Add(new DateTime(2019, i, j));
                }
                DateTime date = new DateTime(2019, i, 1);

                calendar.Add(date.Month.ToString(), dtl);
            }
        }


        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить все данные?", "Отмена радактирования", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                FileInfo fi = new FileInfo(uriAL);
                fi.Delete();
                this.Close();
            }
            else
            {

            }
        }

        // 
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Stream fStream = new FileStream(uriAL, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<AttendDictSerial>));
                    xmlFormat.Serialize(fStream, ADS);


                    //   XmlSerializer xmlFormat = new XmlSerializer(typeof(AttendDictSerial[]), new XmlRootAttribute() { ElementName = "groups" });
                    //   xmlFormat.Serialize(fStream, Attendances.Select(ga => new AttendDictSerial() { Name = ga.Key, AttList = ga.Value }).ToArray());
                }
                MessageBox.Show("Изменения сохранены");

            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
            }


        }

        // общий свод посещаемости за год по месяцам
        private void GenAtt_GotFocus(object sender, RoutedEventArgs e)
        {
            /*
         
            Days dayOfWeek = new Days();
            Array getDaysData = Enum.GetValues(dayOfWeek.GetType());

            for (int i = 0; i < getDaysData.Length; i++)
            {
                string tmp = getDaysData.GetValue(i).ToString();

                DailyClasses dc = new DailyClasses();
                dc.GetDailyTimeTable(groups, tmp);

                ldc.Add(dc);
            }
            */

            string[] months = { "сентябрь", "октябрь", "ноябрь" };

            foreach (string st in months)
            {

                TabItem ti = new TabItem()
                {
                    Header = st,
                };
                StackPanel sp = new StackPanel();

                ti.Content = sp;

                foreach (var v in ADS)       // имя очередной группы добавляем в стекпанель
                {
                    Label label = new Label();
                    label.Foreground = Brushes.Black;
                    label.FontWeight = FontWeights.Bold;
                    label.FontStyle = FontStyles.Italic;
                    label.FontSize = 12;
                    label.Background = Brushes.LightCoral;
                      label.Content = "Группа " + v.GroupName;
                  //   mainStackpanel.Children.Add(label);
                    //   lbl.Content = label;    
                    //    label.FontFamily = new FontFamily("Arial");
                    sp.Children.Add(label);

                    sp.Children.Add(new DataGrid()   //рисуем таблицу для очередной группы
                    {
                        ItemsSource = v.SepAttendanceList,
                        CanUserAddRows = false,
                        AutoGenerateColumns = true,
                        FontWeight = FontWeights.Bold,
                     //   RowBackground = Brushes.Gainsboro,
                        AlternatingRowBackground = Brushes.Lavender
                    }); ;

                }
                   tc.Items.Add(ti);
            }

        }
    }
}
