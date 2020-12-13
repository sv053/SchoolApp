using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для SalaryEditor.xaml
    /// </summary>
    public partial class SalaryEditor : Window
    {
        public List<TeacherSalary> salaryList { set; get; } = new List<TeacherSalary>();
        public SalaryEditor()
        {
            InitializeComponent();

            this.DataContext = this;
            Loaded += SalaryEditor_Loaded;
        }

        private void SalaryEditor_Loaded(object sender, RoutedEventArgs e)
        {
            School school = School.Instance;
            //    List<TeacherSalary> salaryList = new List<TeacherSalary>();
            //  DateTime dt = new DateTime(1 / 9 / 2019);
            //   Classes.Calendar calendar = new Classes.Calendar();

            //  DataGrid dg = new DataGrid();

            foreach (Teacher t in school.Teachers)
            {
                //        Label label = new Label();
                //       label.Content = t.F;
                //       sp.Children.Add(label);
                TeacherSalary ts = new TeacherSalary(t);
                ts.getTeacherGroupsCalendar();
                salaryList.Add(ts);

            }


            SalaryTable.ItemsSource = salaryList;

            List<string> test = new List<string>();


            //    DataGridTextColumn dataGridTextColumn = new DataGridTextColumn();
            //    dataGridTextColumn.
            //      sp.Children.Add(dg);

        }
    }
}
