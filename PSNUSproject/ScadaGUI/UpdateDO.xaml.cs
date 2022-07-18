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
    /// Interaction logic for UpdateDO.xaml
    /// </summary>
    public partial class UpdateDO : Window
    {
        private DigitalOutput UpdatedDO = new DigitalOutput();

        private int currentID = -1;

        public UpdateDO(DigitalOutput DO)
        {
            InitializeComponent();

            currentID = DO.ID;
            UpdatedDO.Name = DO.Name;
            UpdatedDO.Description = DO.Description;
            UpdatedDO.Address = DO.Address;
            UpdatedDO.Value = DO.Value;

            this.addrCmb.ItemsSource = new List<string> { "ADDR013", "ADDR014", "ADDR015", "ADDR016" };
            this.addrCmb.SelectedValue = DO.Address;
            this.DataContext = UpdatedDO;
        }

        private void UpdateDO_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                foreach (DigitalOutput dio in IOContext.Instance.DigitalOutputs.Local)
                {
                    if (dio.ID == currentID)
                    {
                        dio.Name = nameTxt.Text;
                        dio.Description = descTxt.Text;
                        dio.Address = addrCmb.Text;
                        dio.Value = Boolean.Parse(valTxt.Text);
                        IOContext.Instance.Entry(dio).State = System.Data.Entity.EntityState.Modified;
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
            bool result;
            if (String.IsNullOrWhiteSpace(valTxt.Text))
            {
                valValTxt.Text = "Required field!";
                valTxt.BorderBrush = Brushes.Red;
                valValTxt.Visibility = Visibility.Visible;

                retVal = false;
            }
            else
            {
                if (Boolean.TryParse(valTxt.Text, out result))
                {
                    valTxt.ClearValue(Border.BorderBrushProperty);
                    valValTxt.Visibility = Visibility.Hidden;
                }
                else
                {
                    valValTxt.Text = "Must be True/False!";
                    valTxt.BorderBrush = Brushes.Red;
                    valValTxt.Visibility = Visibility.Visible;
                    retVal = false;
                }
            }
            return retVal;
        }
    }
}
