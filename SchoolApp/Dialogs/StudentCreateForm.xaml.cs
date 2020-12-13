using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SchoolApp.Classes;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Windows.Media;

namespace SchoolApp.Dialogs
{

    public partial class StudentCreateForm : Window
    //       public partial class StudentCreateForm : StudentsEditor
    {
        School school = School.Instance;
        ObservableCollection<Student> students;
        //   XDocument doc;

        public ObservableCollection<Student> Students { set { students = value; } get { return students; } }

        DirectoryInfo dir = new DirectoryInfo(@"..\..\..");

        public StudentCreateForm()
        {
            InitializeComponent();

            Unloaded += UnloadData;
            Loaded += LoadData;
        }

        // вывод имён групп в комбобокс
        private void LoadData(object sender, RoutedEventArgs e)
        {
            Students = school.Students;
            ObservableCollection<string> groupNames = school.GetGroupNames(school.Groups);

            sGroup.ItemsSource = groupNames;

        }

        private void BtnSaveAll_Click(object sender, RoutedEventArgs e)
        {
            Students = school.Students;

            if (sSurname.Text == "")
            {
                sSurname.Background = Brushes.MistyRose;

            }
            if (sName.Text == "")
            {
                sName.Background = Brushes.MistyRose;
            }
            if (sPathr.Text == "")
            {
                sPathr.Background = Brushes.MistyRose;
            }

            if (sPhone.Text == "")
            {
                sPhone.Background = Brushes.MistyRose;
            }

            if ((sSurname.Text != "") && (sName.Text != "") && (sPathr.Text != "") && (sPhone.Text != ""))
            {
                var lastSt = (school.AddStudent(new Student(sSurname.Text, sName.Text, sPathr.Text, 12, sPhone.Text, sGroup.Text, sParent.Text, sComment.Text), school.Students, out bool hts).Last());

                if (MessageBox.Show($"Ученик {lastSt.F} {lastSt.I} {lastSt.O} добавлен!", "Новый ученик", MessageBoxButton.OK, MessageBoxImage.None).Equals(MessageBoxResult.OK))

                {
                    sClass.Text = sSurname.Text = sName.Text = sPathr.Text = sPhone.Text = sParent.Text = "";
                    sSurname.Background = Brushes.White;
                    sName.Background = Brushes.White;
                    sPathr.Background = Brushes.White;
                }
                SaveData();
            }
        }

        public void SaveData()
        {

            string uriSL = dir.FullName + "\\StudentsSerializeList.xml";

            using (Stream fStream = new FileStream(uriSL, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Student>));

                xmlFormat.Serialize(fStream, Students);
            }
        }
        public void UnloadData(object sender, RoutedEventArgs e)
        {
            SaveData();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
