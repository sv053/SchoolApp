using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Classes
{

    public class StudPayments
    {
        public Student Student;

        public string StudName { set; get; }

        public ObservableCollection<Payment> Payments { set; get; } = new ObservableCollection<Payment>();

     //   public float LastPayment { set; get; }

        public StudPayments()
        {

        }
        public StudPayments(Student st )
        {
            Student = st;
            StudName = st.F + st.I+ st.O;
        
        }

        public ObservableCollection<Payment> AddPayment(Payment payment)
        {
            Payments.Add(payment);
            return Payments;
        }
    }
}

