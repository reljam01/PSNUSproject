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
    /// Interaction logic for AddDO.xaml
    /// </summary>
    public partial class AddDO : Window
    {
        public AddDO()
        {
            InitializeComponent();
            this.addrCmb.ItemsSource = new List<string> { "ADDR013", "ADDR014", "ADDR015", "ADDR016" };
        }

        private void AddDO_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                DigitalOutput newDO = new DigitalOutput(this.nameTxt.Text, this.descTxt.Text, this.addrCmb.Text, this.initTxt.Text);
                IOContext.Instance.DigitalOutputs.Add(newDO);
                IOContext.Instance.SaveChanges();
                newDO.Load();
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
            bool result;
            if (String.IsNullOrWhiteSpace(initTxt.Text))
            {
                initValTxt.Text = "Required field!";
                initTxt.BorderBrush = Brushes.Red;
                initValTxt.Visibility = Visibility.Visible;

                retVal = false;
            }
            else
            {
                if (Boolean.TryParse(initTxt.Text, out result))
                {
                    initTxt.ClearValue(Border.BorderBrushProperty);
                    initValTxt.Visibility = Visibility.Hidden;
                }
                else
                {
                    initValTxt.Text = "Must be True/False!";
                    initTxt.BorderBrush = Brushes.Red;
                    initValTxt.Visibility = Visibility.Visible;
                    retVal = false;
                }
            }
            return retVal;
        }
    }
}
