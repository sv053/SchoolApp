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
    /// Логика взаимодействия для PriceEditor.xaml
    /// </summary>
    public partial class PriceEditor : Window
    {
        Pricelist pricelist = new Pricelist();
        public PriceEditor()
        {
            InitializeComponent();

            Loaded += PriceEditor_Loaded;
        }

        private void PriceEditor_Loaded(object sender, RoutedEventArgs e)
        {
            

            tbFor1hourPriceAdults.Text = pricelist.rateGroupPer1HourAdult.ToString();
            for1hourPriceAdults.Content = pricelist.rateGroupPer1HourAdult.ToString();

            tbFor8hourPriceAdults.Text = pricelist.rateGroupPer8hoursAdult.ToString();
            for8hourPriceAdults.Content = pricelist.rateGroupPer8hoursAdult.ToString();

            tbFor24hourPriceAdults.Text = pricelist.rateGroupPer24hoursAdult.ToString();
            for24hourPriceAdults.Content = pricelist.rateGroupPer24hoursAdult.ToString();

            tbFor1hourPricePrimary.Text = pricelist.rateGroupPer1HourPrimary.ToString();
            for1hourPricePrimary.Content = pricelist.rateGroupPer1HourPrimary.ToString();

            tbFor8hourPricePrimary.Text = pricelist.rateGroupPer8hoursPrimary.ToString();
            for8hourPricePrimary.Content = pricelist.rateGroupPer8hoursPrimary.ToString();

            tbFor24hourPricePrimary.Text = pricelist.rateGroupPer24hoursPrimary.ToString();
            for24hourPricePrimary.Content = pricelist.rateGroupPer24hoursPrimary.ToString();

            tbPrivateAdults.Text = pricelist.ratePrivatePer1HourAdult.ToString();
            forPrivateAdults.Content = pricelist.ratePrivatePer1HourAdult.ToString();

            tbPrivatePrim.Text = pricelist.ratePrivatePer1HourAdult.ToString();
            forPrivatePrim.Content = pricelist.ratePrivatePer1HourAdult.ToString();
        }

        private void RenewPricelistBtn_Click(object sender, RoutedEventArgs e)
        {
            if (for1hourPriceAdults.Content != null)
            {
                for1hourPriceAdults.Content = tbFor1hourPriceAdults.Text;

               if( int.TryParse(tbFor1hourPriceAdults.Text, out int f))
                pricelist.rateGroupPer1HourAdult = f;
            }

        }
    }
}
