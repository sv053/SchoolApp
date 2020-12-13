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
    /// Логика взаимодействия для PaymentsEditor.xaml
    /// </summary>
    public partial class PaymentsEditor : Window
    {
        ObservableCollection<Student> students = School.Instance.Students;
        ObservableCollection<Payment> payments = new ObservableCollection<Payment>();
        ObservableCollection<StudPayments> allsStudPayments = new ObservableCollection<StudPayments>();
        DirectoryInfo dir = new DirectoryInfo(@"..\..\..");

        public ObservableCollection<StudPayments> AllsStudPayments { get => allsStudPayments; set => allsStudPayments = value; }

        public PaymentsEditor()
        {
            InitializeComponent();

            Loaded += PaymentsEditor_Loaded;
            Loaded += DownloadPaymentsList;
            Unloaded += PaymentsEditor_Unloaded;

        }

        private void PaymentsEditor_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveData();
        }

        public void SaveData()
        {
            string uriPL = dir.FullName + "\\PaymentsSerializeList2.xml";
            using (Stream fStream = new FileStream(uriPL, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Payment>));

                xmlFormat.Serialize(fStream, payments);
            }
        }
        private void DownloadPaymentsList(object sender, RoutedEventArgs e)
        {
            string uriPL = dir.FullName + "\\PaymentsSerializeList2.xml";

            //      if (fiPSL.Exists && (fiPSL.Length > 0))
            //     {
            try
            {
                using (Stream fStream = new FileStream(uriPL, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Payment>));

                    payments = (ObservableCollection<Payment>)xmlFormat.Deserialize(fStream);
                }
            }
            catch
            {

            }
            paymentsList.ItemsSource = payments;
        }

        private void CreateStudPaymentsList(ObservableCollection<Payment> paymentsColl)
        {

            foreach (Student st in students)
            {
                StudPayments studPayments = new StudPayments(st);

                string stName = st.F + st.I + st.O;

                var paymentsPerStudent = paymentsColl.Where(p => p.StudName == stName);

                foreach (var pps in paymentsPerStudent)
                {
                    studPayments.AddPayment(pps);
                }
                AllsStudPayments.Add(studPayments);
            }
            studPaymentsDatagrid.ItemsSource = AllsStudPayments;

        }

        private void PaymentsEditor_Loaded(object sender, RoutedEventArgs e)
        {

            studFIO.ItemsSource = students.OrderBy(s => s.F);


            Pricelist pricelist = new Pricelist();
            Type Pricelist = pricelist.GetType();
            PropertyInfo[] properties = Pricelist.GetProperties();

            List<string> prices = new List<string>();

            foreach (PropertyInfo pi in properties)
            {
                string tmp = pi.GetValue(pricelist).ToString();
                prices.Add(tmp);
            }

            sum.ItemsSource = prices;
        }


        private void BtnSavePayment_Click(object sender, RoutedEventArgs e)
        {
            var cbi1 = (Student)studFIO.SelectedValue;

            string StudFio = cbi1.F + cbi1.I + cbi1.O;

            string cbi2 = (string)sum.Text;

            Student student = students.Where(s => s.F + s.I + s.O == StudFio).FirstOrDefault();

            var sd = datepicker.SelectedDate;
            //---------- no need         
            if (float.TryParse(cbi2, out float summ))
            {
                //       studPayments.AddPayment(new Payment(cbi1, summ, sd.Value.Date.ToShortDateString()));

            }
            payments.Add(new Payment(cbi1, summ, sd.Value.Date.ToShortDateString()));
            paymentsList.ItemsSource = payments;
            sum.Text = "";
            studFIO.Text = "";
        }

        private void StudPaymentsTabitem_GotFocus(object sender, RoutedEventArgs e)
        {
            AllsStudPayments.Clear();
            CreateStudPaymentsList(payments);
        }
    }
}
