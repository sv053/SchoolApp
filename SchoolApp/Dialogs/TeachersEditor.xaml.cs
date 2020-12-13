using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Linq;
using System.Xml.Serialization;
using SchoolApp.Classes;

namespace SchoolApp.Dialogs
{
    public partial class TeachersEditor : Window
    {
        //   public ObservableCollection<Student> Students { set; get; } = School.Instance.Students;
        public ObservableCollection<Teacher> Teachers { set; get; } = new ObservableCollection<Teacher>();//School.Instance.Teachers;

        Teacher selectedTeacher;
        public Teacher SelectedTeacher
        {
            get => selectedTeacher;
            set
            {
                if (value != null)
                {
                    selectedTeacher = value;
                }
            }
            /*
        public Group SelectedGroup
        {
            get => selectedGroup;
            set
            {
                if (value != null)
                {
                    selectedGroup = value;
                    /*
                    ObservableCollection<Group> groups = School.Instance.Groups;

                    Group group = groups.Where(i => i.Name == selectedGroup.Name).FirstOrDefault();

                    groups.Remove(group);

                    if(group != null)
                    {

                        group.GroupID = 888;
                        groups.Insert(group.GroupID, group);
                    }
                    
                    groupStack.DataContext = value;
                }
            }*/
        }

        School school = School.Instance;
        DirectoryInfo dir = new DirectoryInfo(@"..\..\..");

        public TeachersEditor()
        {
            InitializeComponent();

            //   Teachers = school.Teachers;
            this.DataContext = this;
            Loaded += TeachersEditor_Loaded;
            Unloaded += TeachersEditor_Unloaded;
        }

        private void TeachersEditor_Unloaded(object sender, RoutedEventArgs e)
        {
            //   School school = School.Instance;
            //   School school = School.Instance;
            school.Teachers = Teachers;

            SaveData(Teachers);
        }

        private void SaveData(ObservableCollection<Teacher> teachs)
        {

            string uriTL = dir.FullName + "\\TeachersSerializeList";

            using (Stream fStream = new FileStream(uriTL, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Teacher>));

                xmlFormat.Serialize(fStream, teachs);
            }

        }

        private void TeachersEditor_Loaded(object sender, RoutedEventArgs e)
        {

            foreach (Teacher t in school.Teachers)
            //    foreach (Teacher t in Teachers)
            {

                t.AssignGroups();
                Teachers.Add(t);
            }

        }

        private void CreateTeacherBtn_Click(object sender, RoutedEventArgs e)
        {
           ugCreateEditTeacher.Visibility = Visibility.Visible;

        }

        private void BtnCancelTeacher_Click(object sender, RoutedEventArgs e)
        {
            tSurname.Text = "";
            tName.Text = "";
            tPath.Text = "";
            tTeleph.Text = "";
            tRate.Text = "";
            tAddData.Text = "";
        }

        private void BtnSaveTeacher_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(tRate.Text, out int trate);
            //   var lastT = (school.AddTeacher(new Teacher(tSurname.Text, tName.Text, tPath.Text, tTeleph.Text, "", trate), school.Teachers, out bool save)).Last();

            var tchs = (school.AddTeacher(new Teacher(tSurname.Text, tName.Text, tPath.Text, tTeleph.Text, "", trate, tAddData.Text), school.Teachers, out bool save)).Last();

            Teachers.Add(tchs);

            //   Teachers.Clear();
            SaveData(Teachers);
     
            if (MessageBox.Show("Учитель добавлен!", "Новый учитель", MessageBoxButton.OK, MessageBoxImage.None).Equals(MessageBoxResult.OK))

            {
                tSurname.Text = tName.Text = tPath.Text = tTeleph.Text = tBdate.Text = tRate.Text = tAddData.Text = "";
            }

        }

        private void MainDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            School school = School.Instance;
            // получить индекс строки коллекции

            DataGridCellInfo dgsc = mainDataGrid.CurrentCell;

            Teacher tr = (Teacher)dgsc.Item;

            MessageBox.Show("cellColumn " + dgsc.Column);
            MessageBox.Show("cellItem " + tr.F + tr.I + tr.O);
            MessageBox.Show("cellValid " + dgsc.IsValid);
            //     }

            MessageBox.Show("SelectedTeacher  " + SelectedTeacher.F);
            MessageBox.Show("tr  " + tr.F);

            var rowIndex = mainDataGrid.SelectedIndex;
            var row = (DataGridRow)mainDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            //   int ind = 0;
            int SelectedIndex = row.GetIndex();
            string fi = tr.F + tr.I + tr.O;

            if (school.Teachers.Where(t => t.F + t.I + t.O == fi).FirstOrDefault() != null)
            {
                if (SelectedTeacher.AssignedGroups.Count == 0)
                {
                    if (MessageBox.Show("Удалить " + SelectedTeacher.F, "Удалить из списка", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {

                        school.DropTeacher(school.Teachers.Where(t => t.F + t.I + t.O == fi).FirstOrDefault());
                        //     Teachers.Remove(Teachers[SelectedIndex]);
                        if (SelectedTeacher != null && Teachers.Contains(SelectedTeacher))
                        {
                            MessageBox.Show("bingo!!  ");
                            Teachers.RemoveAt(Teachers.IndexOf(SelectedTeacher));
                            return;
                        }

                    }
                }
                else
                    MessageBox.Show("Нельзя удалить преподавателя, пока у него есть группы ");
            }
            //   SaveData(Teachers);
            //   Teachers = school.Teachers;
        }

        private void SaveAllBtn_Click(object sender, RoutedEventArgs e)
        {

            //   var tsr = school.Teachers.Where(te => te.Delete == true).ToList().RemoveAll( t=> DeleteTeacher(t));

            //   foreach(Teacher tr in Teachers)
            for (int i = 0; i < Teachers.Count; i++)
            {
                //    if (tr.Delete == true)
                if (Teachers[i].Delete == true)
                {
                    if (SelectedTeacher.AssignedGroups.Count == 0)
                    {
                        if (MessageBox.Show("Удалить " + Teachers[i].F, "Удалить из списка", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            {
                                Teachers.Remove(Teachers[i]);
                            }
                        }
                    }
                }
                //    school.Teachers = Teachers;
            }
            //   SaveData(Teachers);
        }

        private void CheckColumn_PastingCellClipboardContent(object sender, DataGridCellClipboardEventArgs e)
        {
            School school = School.Instance;
            // получить индекс строки коллекции

            DataGridCellInfo dgsc = mainDataGrid.CurrentCell;

            Teacher tr = (Teacher)dgsc.Item;

            MessageBox.Show("cellColumn " + dgsc.Column);
            MessageBox.Show("cellItem " + tr.F + tr.I + tr.O);


            var rowIndex = mainDataGrid.SelectedIndex;
            var row = (DataGridRow)mainDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            //   int ind = 0;
            int SelectedIndex = row.GetIndex();
            //   string fi = SelectedTeacher.F + SelectedTeacher.I + SelectedTeacher.O;
            string fi = tr.F + tr.I + tr.O;

            if (school.Teachers.Where(t => t.F + t.I + t.O == fi).FirstOrDefault() != null)
            {
                if (SelectedTeacher.AssignedGroups.Count == 0)
                {
                    if (MessageBox.Show("Удалить " + SelectedTeacher.F, "Удалить из списка", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {

                        school.DropTeacher(school.Teachers.Where(t => t.F + t.I + t.O == fi).FirstOrDefault());
                        //     Teachers.Remove(Teachers[SelectedIndex]);
                        if (SelectedTeacher != null && Teachers.Contains(SelectedTeacher))
                        {
                            MessageBox.Show("bingo!!  ");
                            Teachers.RemoveAt(Teachers.IndexOf(SelectedTeacher));
                            return;
                        }

                    }
                }
                else
                    MessageBox.Show("Нельзя удалить преподавателя, пока у него есть группы ");
            }

        }
    }
}
