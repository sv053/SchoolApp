using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Xml.Linq;
using SchoolApp.Classes;

namespace SchoolApp.Dialogs
{
    
    public partial class StudentsEditor : Window
    {
        School school = School.Instance;
     
        public ObservableCollection<Student> Students { set; get; } = School.Instance.Students;
        public StudentsEditor()
        {
            InitializeComponent();
        //    this.DataContext = DataContext;
            this.DataContext = this;
            Loaded += StudentsEditor_Loaded;
           Unloaded += StudentsEditor_Unloaded;
        }


        private void StudentsEditor_Loaded(object sender, RoutedEventArgs e)
        {
         //    StudentsTable.ItemsSource = Students;
            
             school.Students = Students;


        }

        private void StudentsEditor_Unloaded(object sender, RoutedEventArgs e)
        {
            school.Students = Students;

        }

        private void CreateStudBtn_Click(object sender, RoutedEventArgs e)
        {
           
            StudentCreateForm scf = new StudentCreateForm();
         //   this.Close();
            scf.ShowInTaskbar = false;
            scf.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            scf.ShowDialog();
    
        }


        private void UpdateStudBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            for(int i=0; i< Students.Count; i++)
            {
                if (Students[i].Delete == true)
                {
                    MessageBox.Show(Students[i].F);
                    Students.Remove(Students[i]);
                }
                    
            }
        }
    }
}