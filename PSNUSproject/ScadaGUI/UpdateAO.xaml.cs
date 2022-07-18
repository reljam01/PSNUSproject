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
    /// Interaction logic for UpdateAO.xaml
    /// </summary>
    public partial class UpdateAO : Window
    {
        private AnalogOutput CurrentAO = new AnalogOutput();
        private int CurrentID = -1;
        public UpdateAO(AnalogOutput AO, bool isUpdate)
        {
            InitializeComponent();

            CurrentID = AO.ID;
            CurrentAO.Name = AO.Name;
            CurrentAO.Description = AO.Description;
            CurrentAO.Address = AO.Address;
            CurrentAO.HighLimit = AO.HighLimit;
            CurrentAO.LowLimit = AO.LowLimit;
            CurrentAO.Units = AO.Units;
            CurrentAO.Value = AO.Value;

            this.addrCmb.ItemsSource = new List<string> { "ADDR005", "ADDR006", "ADDR007", "ADDR008" };
            this.addrCmb.SelectedValue = AO.Address;
            this.DataContext = CurrentAO;

            if (isUpdate)
            {
                UpdateBtn.Visibility = System.Windows.Visibility.Visible;
                nameTxt.IsEnabled = true;
                descTxt.IsEnabled = true;
                addrCmb.IsEnabled = true;
                upTxt.IsEnabled = true;
                lowTxt.IsEnabled = true;
                unitTxt.IsEnabled = true;
                valTxt.IsEnabled = true;
            }
            else
            {
                UpdateBtn.Visibility = System.Windows.Visibility.Hidden;
                nameTxt.IsEnabled = false;
                descTxt.IsEnabled = false;
                addrCmb.IsEnabled = false;
                upTxt.IsEnabled = false;
                lowTxt.IsEnabled = false;
                unitTxt.IsEnabled = false;
                valTxt.IsEnabled = false;
            }
        }

        private void UpdateAO_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                foreach (AnalogOutput ao in IOContext.Instance.AnalogOutputs.Local)
                {
                    if (ao.ID == CurrentID)
                    {
                        ao.Name = nameTxt.Text;
                        ao.Description = descTxt.Text;
                        ao.Address = addrCmb.Text;
                        ao.HighLimit = Double.Parse(upTxt.Text);
                        ao.LowLimit = Double.Parse(lowTxt.Text);
                        ao.Units = unitTxt.Text;
                        double val = Double.Parse(valTxt.Text);
                        if (val < ao.LowLimit)
                        {
                            ao.Value = ao.LowLimit;
                        }
                        else if (val > ao.HighLimit)
                        {
                            ao.Value = ao.HighLimit;
                        }
                        else
                        {
                            ao.Value = val;
                        }
                        IOContext.Instance.Entry(ao).State = System.Data.Entity.EntityState.Modified;
                        IOContext.Instance.SaveChanges();
                    }
                }
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
            if (String.IsNullOrWhiteSpace(valTxt.Text))
            {
                valValTxt.Text = "Required field!";
                valTxt.BorderBrush = Brushes.Red;
                valValTxt.Visibility = Visibility.Visible;

                retVal = false;
            }
            else
            {
                if (Double.TryParse(valTxt.Text, out result))
                {
                    valTxt.ClearValue(Border.BorderBrushProperty);
                    valValTxt.Visibility = Visibility.Hidden;
                }
                else
                {
                    valValTxt.Text = "Not a number!";
                    valTxt.BorderBrush = Brushes.Red;
                    valValTxt.Visibility = Visibility.Visible;
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
