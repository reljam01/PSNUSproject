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
    /// Interaction logic for AddAO.xaml
    /// </summary>
    public partial class AddAO : Window
    {
        public AddAO()
        {
            InitializeComponent();
            this.addrCmb.ItemsSource = new List<string> { "ADDR005", "ADDR006", "ADDR007", "ADDR008" };
        }

        private void AddAO_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                AnalogOutput newAO = new AnalogOutput();
                newAO.Name = this.nameTxt.Text;
                newAO.Description = this.descTxt.Text;
                newAO.Address = this.addrCmb.Text;
                newAO.InitialValue = Double.Parse(this.initTxt.Text);
                newAO.LowLimit = Double.Parse(this.lowTxt.Text);
                newAO.HighLimit = Double.Parse(this.upTxt.Text);
                newAO.Units = this.unitTxt.Text;
                if (newAO.InitialValue > newAO.HighLimit)
                {
                    newAO.Value = newAO.HighLimit;
                }
                else if (newAO.InitialValue < newAO.LowLimit)
                {
                    newAO.Value = newAO.LowLimit;
                }
                else
                {
                    newAO.Value = newAO.InitialValue;
                }

                IOContext.Instance.AnalogOutputs.Add(newAO);
                IOContext.Instance.SaveChanges();
                newAO.Load();
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
            if (String.IsNullOrWhiteSpace(initTxt.Text))
            {
                initValTxt.Text = "Required field!";
                initTxt.BorderBrush = Brushes.Red;
                initValTxt.Visibility = Visibility.Visible;

                retVal = false;
            }
            else
            {
                if (Double.TryParse(initTxt.Text, out result))
                {
                    initTxt.ClearValue(Border.BorderBrushProperty);
                    initValTxt.Visibility = Visibility.Hidden;
                }
                else
                {
                    initValTxt.Text = "Not a number!";
                    initTxt.BorderBrush = Brushes.Red;
                    initValTxt.Visibility = Visibility.Visible;
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
