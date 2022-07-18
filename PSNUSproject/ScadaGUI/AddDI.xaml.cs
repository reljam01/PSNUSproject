using DataConcentrator;
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

namespace ScadaGUI
{
    /// <summary>
    /// Interaction logic for AddDI.xaml
    /// </summary>
    public partial class AddDI : Window
    {
        public AddDI()
        {
            InitializeComponent();
            this.addrCmb.ItemsSource = new List<string> { "ADDR009", "ADDR010", "ADDR011", "ADDR012" };
        }

        private void AddDI_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                DigitalInput newDI = new DigitalInput(this.nameTxt.Text, this.descTxt.Text, this.addrCmb.Text, Double.Parse(this.scanTxt.Text));
                IOContext.Instance.DigitalInputs.Add(newDI);
                IOContext.Instance.SaveChanges();
                newDI.Load();
                this.Close();
            }
        }

        private bool ValidateInput()
        {
            bool retVal = true;
            if (String.IsNullOrWhiteSpace(nameTxt.Text))
            {
                nameValTxt.Text = "Required field!";
                nameTxt.BorderBrush = Brushes.Red;
                nameValTxt.Visibility = Visibility.Visible;

                retVal = false;
            }
            else
            {
                nameTxt.ClearValue(Border.BorderBrushProperty);
                nameValTxt.Visibility = Visibility.Hidden;
            }
            if (addrCmb.SelectedItem == null)
            {
                addrCmb.BorderBrush = Brushes.Red;
                retVal = false;
            }
            else
            {
                addrCmb.ClearValue(Border.BorderBrushProperty);
            }
            double result;
            if (String.IsNullOrWhiteSpace(scanTxt.Text))
            {
                scanValTxt.Text = "Required field!";
                scanTxt.BorderBrush = Brushes.Red;
                scanValTxt.Visibility = Visibility.Visible;

                retVal = false;
            }
            else
            {
                if (Double.TryParse(scanTxt.Text, out result))
                {
                    scanTxt.ClearValue(Border.BorderBrushProperty);
                    scanValTxt.Visibility = Visibility.Hidden;
                }
                else
                {
                    scanValTxt.Text = "Not a number!";
                    scanTxt.BorderBrush = Brushes.Red;
                    scanValTxt.Visibility = Visibility.Visible;
                    retVal = false;
                }
            }
            return retVal;
        }
    }
}
