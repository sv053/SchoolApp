using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml.Serialization;
using SchoolApp.Classes;
using System.Reflection;

namespace SchoolApp.Dialogs
{
    public partial class GroupEditor : Window, INotifyPropertyChanged
    {
        //   School school = School.Instance;

        int groupsCount = 6;
        private Group selectedGroup;

        public Group SelectedGroup
        {
            get => selectedGroup;
            set
            {
                if (value != null)
                {
                    selectedGroup = value;
                }
            }
        }

        public int GroupsCount
        {
            get
            {
                return groupsCount;
            }
            set
            {
                groupsCount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("GroupsCount"));
                }
            }
        }

        public ObservableCollection<string> GroupsNames { set; get; }
        public ObservableCollection<string> TeachersNames { set; get; } = new ObservableCollection<string> { "gkhkjjkl", "dytdy", "iugig" };
        public ObservableCollection<Group> Groups { set; get; } = School.Instance.Groups;

        public ObservableCollection<Teacher> Teachers { set; get; } = School.Instance.Teachers;

        public event PropertyChangedEventHandler PropertyChanged;

        XDocument doc;
        DirectoryInfo dir;


        public GroupEditor()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += GroupEditor_Loaded;
            //   Loaded += Groups_CollectionChanged;
            Unloaded += GroupEditor_Unloaded;
        }

        private void GroupEditor_Unloaded(object sender, RoutedEventArgs e)
        {
            School school = School.Instance;
            //    ObservableCollection<Group> groups = school.Groups;
            Groups.CollectionChanged -= Groups_CollectionChanged;

            //   School.SaveInstance("groups.dat");
            SaveData(Groups);
        }

        public void SaveData(ObservableCollection<Group> gr)
        {
            dir = new DirectoryInfo(@"..\..\..");
            string uriGL = dir.FullName + "\\GroupsSerializeList.xml";

            using (Stream fStream = new FileStream(uriGL, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Group>));

                xmlFormat.Serialize(fStream, gr);
            }
        }

        private void GroupEditor_Loaded(object sender, RoutedEventArgs e)
        {


            School school = School.Instance;

            Groups.CollectionChanged += Groups_CollectionChanged;

            GroupsNames = school.GetGroupNames(school.Groups);
            TeachersNames = school.GetTeachersNames(school.Teachers);

            foreach (Group gr in Groups)
            {
             
                gr.GetStudentsList();
                gr.AllTeachers.Clear();
                gr.AllTeachers.Insert(0, gr.Teacher);

                foreach (string str in school.GetTeachersNames())
                {
                    if (str != gr.Teacher)
                        gr.AllTeachers.Add(str);
                }
            }
            LoadSources();
        }

        private void Groups_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            School school = School.Instance;
            GroupsCount = school.CountGroups;

        }

        private void BtnGuardarGroup_Click(object sender, RoutedEventArgs e)
        {
            School school = School.Instance;
            string grTeacher;

            if ((string)gTeacher.SelectedValue != "")
            {
                grTeacher = (string)gTeacher.SelectedValue;
            }
            else
                grTeacher = "Учитель не назначен";

                var Grs = (school.CreateGroup(Groups, (string)gHour1.SelectedValue, (string)gHour2.SelectedValue, (string)gWeekday1.SelectedValue,
                    (string)gWeekday2.SelectedValue, grTeacher, (int)gAge.SelectedValue));
            
            DirectoryInfo dir;


            dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            string uriGL = dir.FullName + "\\GroupsSerializeList.xml";

            using (Stream fStream = new FileStream(uriGL, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Group>));

                xmlFormat.Serialize(fStream, Grs);
            }


            if (MessageBox.Show($"Группа {Grs.Last().Name} сохранена", "Новая группа", MessageBoxButton.OK, MessageBoxImage.None).Equals(MessageBoxResult.OK))

            {
                gWeekday1.Text = gWeekday2.Text = gHour1.Text = gHour2.Text = gCount.Text = gAge.Text = gDuration.Text = gTeacher.Text = "";
            }
        }

        private void LoadSources()
        {
            School school = School.Instance;

            List<string> weekDays = new List<string>();

            foreach (var s in Enum.GetValues(typeof(DayOfWeek)))
            {
                weekDays.Add(s.ToString());
            }
            gWeekday1.ItemsSource = gWeekday2.ItemsSource = weekDays;


            List<string> hours = new List<string> { "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00" };

            gHour1.ItemsSource = gHour2.ItemsSource = hours;
            gCount.ItemsSource = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            gAge.ItemsSource = new List<int> { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
            gDuration.ItemsSource = new List<int> { 45, 60 };
            gTeacher.ItemsSource = TeachersNames;
        }

        private void SaveAllBtn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].Delete == true)
                {
                    if (Groups[i].StudInGroup.Count == 0)
                    {
                        if (MessageBox.Show("Удалить группу " + Groups[i].Name + " ?", "Удалить из списка", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            {
                                Groups.Remove(Groups[i]);
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show("Нельзя удалить непустую группу", "Ошибка удаления группы",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

            }
        }
        private void DataGridTemplateColumn_PastingCellClipboardContent(object sender, DataGridCellClipboardEventArgs e)
        {

        }

        private void TeacherCombobox_PastingCellClipboardContent(object sender, DataGridCellClipboardEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            var dgsc = dg.SelectedCells;
            MessageBox.Show("datagrid.SelectedCells - " + dgsc.ToString());

            var rowIndex = GroupsTable.SelectedIndex;
            var row = (DataGridRow)GroupsTable.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            //   int ind = 0;
            int SelectedIndex = row.GetIndex();
            MessageBox.Show("row.GetIndex - " + SelectedIndex.ToString());

            DataGridCellInfo dgcc = GroupsTable.CurrentCell;

            Teacher tr = (Teacher)dgcc.Item;

            MessageBox.Show("teacher - " + tr.F);

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //   DataGrid dg = sender as DataGrid;
            //   var dgsc = dg.SelectedCells;
            //   MessageBox.Show("datagrid.SelectedCells - " + dgsc.ToString());

            //    MessageBox.Show("dg");
        }

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("selected");
        }
    }
}

