using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Classes
{
   public class Payment
    {
        //     public DateTime DateTime { set; get; } 
        public string DateTime { set; get; } 
        public string StudName { set; get; }
     //  
        public float APayment { set; get; } 

        public Payment()
        {

        }

        public Payment(Student st, float payment, string dt )
        {
            StudName = st.F + st.I + st.O;
            DateTime = dt;
            APayment = payment;
        }

       
    }
}
