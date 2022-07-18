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
    /// Interaction logic for UpdateAI.xaml
    /// </summary>
    public partial class UpdateAI : Window
    {
        private AnalogInput CurrentAI = new AnalogInput();
        private int CurrentID = -1;
        public UpdateAI(AnalogInput AI, bool isUpdate)
        {
            InitializeComponent();
            CurrentID = AI.ID;
            CurrentAI.Name = AI.Name;
            CurrentAI.Description = AI.Description;
            CurrentAI.Address = AI.Address;
            CurrentAI.HighLimit = AI.HighLimit;
            CurrentAI.LowLimit = AI.LowLimit;
            CurrentAI.ScanTime = AI.ScanTime;
            CurrentAI.Units = AI.Units;
            CurrentAI.Value = AI.Value;

            this.addrCmb.ItemsSource = new List<string> { "ADDR001", "ADDR002", "ADDR003", "ADDR004" };
            this.addrCmb.SelectedValue = AI.Address;
            this.DataContext = CurrentAI;

            if(isUpdate)
            {
                UpdateBtn.Visibility = System.Windows.Visibility.Visible;
                nameTxt.IsEnabled = true;
                descTxt.IsEnabled = true;
                addrCmb.IsEnabled = true;
                upTxt.IsEnabled = true;
                lowTxt.IsEnabled = true;
                scanTxt.IsEnabled = true;
                unitTxt.IsEnabled = true;
            }
            else
            {
                UpdateBtn.Visibility = System.Windows.Visibility.Hidden;
                nameTxt.IsEnabled = false;
                descTxt.IsEnabled = false;
                addrCmb.IsEnabled = false;
                upTxt.IsEnabled = false;
                lowTxt.IsEnabled = false;
                scanTxt.IsEnabled = false;
                unitTxt.IsEnabled = false;
            }
        }

        private void UpdateAI_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                foreach (AnalogInput ai in IOContext.Instance.AnalogInputs.Local)
                {
                    if (ai.ID == CurrentID)
                    {
                        ai.Name = nameTxt.Text;
                        ai.Description = descTxt.Text;
                        ai.Address = addrCmb.Text;
                        ai.ScanTime = Double.Parse(scanTxt.Text);
                        ai.HighLimit = Double.Parse(upTxt.Text);
                        ai.LowLimit = Double.Parse(lowTxt.Text);
                        ai.Units = unitTxt.Text;
                        IOContext.Instance.Entry(ai).State = System.Data.Entity.EntityState.Modified;
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
