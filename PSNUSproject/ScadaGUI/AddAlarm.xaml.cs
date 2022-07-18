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
    /// Interaction logic for AddAlarm.xaml
    /// </summary>
    public partial class AddAlarm : Window
    {
        public AddAlarm()
        {
            InitializeComponent();

            List<string> AIs = new List<string>();
            foreach(AnalogInput ai in IOContext.Instance.AnalogInputs.Local)
            {
                AIs.Add(ai.Name);
            }
            this.aiCmb.ItemsSource = AIs;
            this.priCmb.ItemsSource = new List<string> {"Low", "High"};
        }

        private void AddAlarm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                Alarm newAlarm = new Alarm();
                newAlarm.Name = nameTxt.Text;
                newAlarm.Message = messTxt.Text;
                newAlarm.Value = Double.Parse(valTxt.Text);
                newAlarm.OnUpperVal = (bool)upCb.IsChecked;
                if (priCmb.Text == "Low")
                {
                    newAlarm.HighPriority = false;
                }
                else
                {
                    newAlarm.HighPriority = true;
                }

                foreach (AnalogInput ai in IOContext.Instance.AnalogInputs)
                {
                    if (ai.Name == aiCmb.Text)
                    {
                        newAlarm.TagId = ai.ID;
                        newAlarm.TagName = ai.Name;
                    }
                }

                IOContext.Instance.Alarms.Add(newAlarm);
                IOContext.Instance.SaveChanges();

                this.Close();
            }
        }

        private bool ValidateInput()
        {
            bool retVal = true;
            if (String.IsNullOrWhiteSpace(messTxt.Text))
            {
                messValTxt.Text = "Required field!";
                messTxt.BorderBrush = Brushes.Red;
                messValTxt.Visibility = Visibility.Visible;

                retVal = false;
            }
            else
            {
                messTxt.ClearValue(Border.BorderBrushProperty);
                messValTxt.Visibility = Visibility.Hidden;
            }
            if (aiCmb.SelectedItem == null)
            {
                aiCmb.BorderBrush = Brushes.Red;
                retVal = false;
            }
            else
            {
                aiCmb.ClearValue(Border.BorderBrushProperty);
            }
            if (priCmb.SelectedItem == null)
            {
                priCmb.BorderBrush = Brushes.Red;
                retVal = false;
            }
            else
            {
                priCmb.ClearValue(Border.BorderBrushProperty);
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
            return retVal;
        }
    }
}
