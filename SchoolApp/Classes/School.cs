using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace SchoolApp.Classes
{
    [Serializable]
    public class School
    {
        private static School instance = null;
        private ObservableCollection<Student> students = new ObservableCollection<Student>();
        private ObservableCollection<Teacher> teachers = new ObservableCollection<Teacher>();
        private ObservableCollection<Group> groups = new ObservableCollection<Group>();

        public ObservableCollection<Student> Students // { set; get; }
    /* */   {
            get
            {
                return students;
            }
            set
            {
                students = value;
            }
        }

        public ObservableCollection<Teacher> Teachers // { set; get; }
     /**/   {
            get
            {
                return teachers;
            }
            set
            {
                teachers = value;
            }
        }

        public ObservableCollection<Group> Groups //{ set; get; }
   /* */    {
            get
            {
                return groups;
            }
            set
            {
                groups = value;
            }
        }

 /* */
          private School() {} 
          public static School Instance
          {
              get
              {
                  if (School.instance == null)
                  {
                      School.instance = new School();
                  }

                  return School.instance;

              }

          }
  
        //-------------------------------------------------
        /*
        public School() { }
        public static School Instance
        {
            get
            {
                if (School.instance == null)
                {
                    School.instance = new School();
                }

                return School.instance;

            }
            private set { }
        }

        public static void LoadInstance(string fileName)
        {
            using (var st = File.OpenRead(fileName))
                Instance = (School)new BinaryFormatter().Deserialize(st);
        }

        public static void SaveInstance(string fileName)
        {
            using (var f = File.OpenWrite(fileName))
                new BinaryFormatter().Serialize(f, Instance);
        }
        */
        FileStream log = null;
        StreamWriter logWriter = null;
        public int CountGroups
        {
            get
            {
                return Groups.Count;
            }
        }

        public int CountStudents
        {
            get
            {
                return Students.Count;
            }
        }

        public int CountStudentsInGroups
        {
            set
            {
             //   int studNumber = 0;
                    foreach (Group group in Groups)
                    {
                        CountStudentsInGroups += group.StudentsNumber;
                    }
            }
            get
            {
               return CountStudentsInGroups;
            }
        }

        // если в списке нет с таким именем, записать в список
        public ObservableCollection<Teacher> AddTeacher(Teacher newTeacher, ObservableCollection<Teacher> teachers, out bool forsave)
        {
         var teacherExists = Teachers.Where(s => s.F.Contains(newTeacher.F)).FirstOrDefault();
            forsave = false;
            //    MessageBox.Show(teacherExists.ToString());

            if (teacherExists == null)
            {
                Teachers.Add(newTeacher);
                forsave = true;
            }
                
            else
                MessageBox.Show("Учитель уже есть в списке");

            return Teachers;
        }

        public ObservableCollection<Teacher> DropTeacher(Teacher teacher)
        {
           
         //   var teacherExists = Teachers.Where(s => s.F.Contains(newTeacher.F)).FirstOrDefault();
            //    forsave = false;
            //    MessageBox.Show(teacherExists.ToString());

            this.Teachers.Remove(teacher);

                MessageBox.Show($"Учитель {teacher.F+teacher.I+teacher.O} удалён из списка");

            return Teachers;
        }

        public ObservableCollection<Student> AddStudent(Student newStudent, ObservableCollection<Student> students, out bool forsave)
        {
           //    if (!Students.Contains(s => s.F == newStudent.F)==false)
            forsave = false;
            var studExists = Students.Where( s => s.F.Contains(newStudent.F)).FirstOrDefault();

            if (studExists == null)
            {
                Students.Add(newStudent);
                forsave = true;
            }
            
            else
                MessageBox.Show("Ученик уже есть в списке");

            return Students;
        }

        public ObservableCollection<Student> DropStudent(Student student)
        {

            //   var teacherExists = Teachers.Where(s => s.F.Contains(newTeacher.F)).FirstOrDefault();
            //    forsave = false;
            //    MessageBox.Show(teacherExists.ToString());

            this.Students.Remove(student);

            MessageBox.Show($"Ученик {student.F + student.I + student.O} удалён из списка");

            return Students;
        }

        public ObservableCollection<string> GetGroupNames(ObservableCollection<Group> grps)
        {
            ObservableCollection<string> groupsNames = new ObservableCollection<string>();

            foreach (Group gr in grps)
                groupsNames.Add(gr.Name);

            return groupsNames;
        }

        public ObservableCollection<string> GetTeachersNames(ObservableCollection<Teacher> tchs)
        {
            ObservableCollection<string> teachersNames = new ObservableCollection<string>();

            foreach (Teacher tr in tchs)
                teachersNames.Add(tr.F);

            return teachersNames;
        }

        public ObservableCollection<string> GetTeachersNames()
        {
            ObservableCollection<string> teachersNames = new ObservableCollection<string>();

            foreach (Teacher tr in Teachers)
                teachersNames.Add(tr.F);

            return teachersNames;
        }
        /*
                public bool CheckIfExistsGroupName(string name, List<Group> grps)
                {
                    if (GetGroupNames(grps).Contains(name))
                        return true;
                    else
                        return false;
                }
        */
     

        public ObservableCollection<Group> CreateGroup(ObservableCollection<Group> grps, string ts1, string ts2, string dow1, string dow2, string teacher, int age)
        {
           string name = dow1.ToString().Substring(0, 3)+ ts1 + dow2.ToString().Substring(0, 3) + ts2;
            int groupId = grps.Count;

            var groupExists = grps.Where(i => i.Name.Equals(name)).FirstOrDefault();

            if (groupExists == null || groupExists.Teacher != teacher)
            {
                groupId ++;

                Groups.Add(new Group( dow1, ts1, dow2, ts2, teacher,age, groupId));
            }
            else
            {
                //   logWrite("группа уже существует");
                MessageBox.Show("группа уже существует");
            }
            return Groups;
        }

        public void DropGroup(Group group, ObservableCollection<Student> stud, ObservableCollection<Group> groups)
        {
            int countGroupStudents = stud.Where(i => i.Group == group.Name).Count();

            if (countGroupStudents == 0 && group.StudentsNumber == 0)
            {
                if(MessageBox.Show("Вы уверены, что хотите удалить группу?", "Удаление группы", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question ) == MessageBoxResult.Yes)
               groups.Remove(group);
            }
                
            else
            {
                MessageBox.Show("Нельзя удалить непустую группу", "Ошибка удаления группы", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                
            }
               
        }

        public void logWrite(string msg)
        {
            logWriter.WriteLine($"{DateTime.Now.ToString()}: {msg}");
        }

        public void LoadAllLists()
        {

        }

        public void SaveAllLists()
        {
            //   logWriter.Close();
            //    log.Close();
        }

       
    }

   
}
