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
    /// Interaction logic for AddAI.xaml
    /// </summary>
    public partial class AddAI : Window
    {
        public AddAI()
        {
            InitializeComponent();
            this.addrCmb.ItemsSource = new List<string> { "ADDR001", "ADDR002", "ADDR003", "ADDR004" };
        }

        private void AddAI_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                AnalogInput newAI = new AnalogInput();
                newAI.Name = this.nameTxt.Text;
                newAI.Description = this.descTxt.Text;
                newAI.Address = this.addrCmb.Text;
                newAI.ScanTime = Double.Parse(this.scanTxt.Text);
                newAI.LowLimit = Double.Parse(this.lowTxt.Text);
                newAI.HighLimit = Double.Parse(this.upTxt.Text);
                newAI.Units = this.unitTxt.Text;
                newAI.Alarming = 0;

                IOContext.Instance.AnalogInputs.Add(newAI);
                IOContext.Instance.SaveChanges();
                newAI.Load();
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
            if (String.IsNullOrWhiteSpace(lowTxt.Text))
            {
                lowValTxt.Text = "Required field!";
                lowTxt.BorderBrush = Brushes.Red;
                lowValTxt.Visibility = Visibility.Visible;

                retVal = false;
            }
            else
            {
                if (Double.TryParse(lowTxt.Text, out result))
                {
                    lowTxt.ClearValue(Border.BorderBrushProperty);
                    lowValTxt.Visibility = Visibility.Hidden;
                }
                else
                {
                    lowValTxt.Text = "Not a number!";
                    lowTxt.BorderBrush = Brushes.Red;
                    lowValTxt.Visibility = Visibility.Visible;
                    retVal = false;
                }
            }
            if (String.IsNullOrWhiteSpace(upTxt.Text))
            {
                upValTxt.Text = "Required field!";
                upTxt.BorderBrush = Brushes.Red;
                upValTxt.Visibility = Visibility.Visible;

                retVal = false;
            }
            else
            {
                if (Double.TryParse(upTxt.Text, out result))
                {
                    upTxt.ClearValue(Border.BorderBrushProperty);
                    upValTxt.Visibility = Visibility.Hidden;
                }
                else
                {
                    upValTxt.Text = "Not a number!";
                    upTxt.BorderBrush = Brushes.Red;
                    upValTxt.Visibility = Visibility.Visible;
                    retVal = false;
                }
            }
            return retVal;
        }
    }
}
