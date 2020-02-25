/* Mary Jeffries 
 * Feb 14 2020
 * Pizza App
 */
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _219076PizzaApp 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Net.WebClient webClient = new System.Net.WebClient();
        string strExchangeRate;
        public MainWindow()
        {
            InitializeComponent();
            strExchangeRate = webClient.DownloadString("https://api.exchangeratesapi.io/latest?base=USD");
            MessageBox.Show(strExchangeRate);
            int indexOfCad = strExchangeRate.IndexOf("CAD");
            int endOfCad = strExchangeRate.IndexOf(",", indexOfCad + 1);

            MessageBox.Show(indexOfCad.ToString() + ", " + endOfCad.ToString());
            string rate = strExchangeRate.Substring(indexOfCad + 5, endOfCad - indexOfCad - 5);
            MessageBox.Show(rate);
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            //Base pizza price is $10
            //Each topping is $1.00
            double pizzaPrice = 10.00;
            string output = "You ordered: " + Environment.NewLine + Environment.NewLine;
            if ((bool)chkBoxMushrooms.IsChecked)
            {
                pizzaPrice += 1.00;
                output += "Mushrooms" + Environment.NewLine;
            }
            if ((bool)chkBoxPineapple.IsChecked)
            {
                pizzaPrice += 1.00;
                output += "Pineapple (seriously, gross)" + Environment.NewLine;
            }

            if ((bool)rbDelivery.IsChecked)
            {
                pizzaPrice += 5.00;
                output += "Delivery" + Environment.NewLine;
            }
            else
            {
                output += "Pickup" + Environment.NewLine;
            }

            if (comboCountry.SelectedValue.ToString().Contains("USA"))
            {
                pizzaPrice = pizzaPrice / 1.3256;
                output += pizzaPrice.ToString("$#.00") + " USD";
            }
            else
            {
                output += pizzaPrice.ToString("$#.00");
            }

            MessageBox.Show("Mushrooms: " + chkBoxMushrooms.IsChecked.ToString()
                + Environment.NewLine + "Pineapple: " + chkBoxPineapple.IsChecked.ToString()
                 + Environment.NewLine + "Price: " + pizzaPrice.ToString("$#.00"));

            lblOutput.Content = output;
        }

        private void comboCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(comboCountry.SelectedValue.ToString());
        }

        private void rbPickUp_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Pick up: " + rbPickUp.IsChecked.ToString());
        }

        private void rbDelivery_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delivery: " + rbDelivery.IsChecked.ToString());
        }
    }
}
