using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Classes
{
    class Pricelist
    {
        public int rateGroupPer1HourAdult { set; get; } = 300;
        public int rateGroupPer8hoursAdult { set; get; } = 2200;
        public int rateGroupPer24hoursAdult { set; get; } = 5400;
        public int rateGroupPer1HourPrimary { set; get; } = 250;
        public int rateGroupPer8hoursPrimary { set; get; } = 1600;
        public int rateGroupPer24hoursPrimary { set; get; } = 4200;
        public int ratePrivatePer1HourAdult { set; get; } = 500;

    }
}
