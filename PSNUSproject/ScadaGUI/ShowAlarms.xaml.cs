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
    /// Interaction logic for ShowAlarms.xaml
    /// </summary>
    public partial class ShowAlarms : Window
    {
        public Alarm SelectedAlarm { get; set; }

        public ShowAlarms(int CurrentTag)
        {
            InitializeComponent();

            AlarmsGrid.ItemsSource = IOContext.Instance.Alarms.Local.Where(alarm => alarm.TagId == CurrentTag);

            this.DataContext = this;
        }

        private void ConfirmAlarm_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAlarm.Activated)
            {
                bool HighAlarms = false;
                bool LowAlarms = false;
                foreach (Alarm al in SelectedAlarm.Tag.Alarms)
                {
                    if (al.Id != SelectedAlarm.Id)
                    {
                        if (al.Activated == true)
                        {
                            if (al.HighPriority)
                            {
                                HighAlarms = true;
                            }
                            else
                            {
                                LowAlarms = true;
                            }
                        }
                    }
                }
                if (SelectedAlarm.OnUpperVal)
                {
                    if (SelectedAlarm.Value > SelectedAlarm.Tag.Value)
                    {
                        SelectedAlarm.Activated = false;
                        if (HighAlarms == false)
                        {
                            if (LowAlarms)
                            {
                                SelectedAlarm.Tag.Alarming = 1;
                                IOContext.Instance.Entry(SelectedAlarm.Tag).State = System.Data.Entity.EntityState.Modified;
                            }
                            else
                            {
                                SelectedAlarm.Tag.Alarming = 0;
                                IOContext.Instance.Entry(SelectedAlarm.Tag).State = System.Data.Entity.EntityState.Modified;
                            }
                        }
                        IOContext.Instance.Entry(SelectedAlarm).State = System.Data.Entity.EntityState.Modified;
                        IOContext.Instance.SaveChanges();
                        AlarmsGrid.Items.Refresh();
                        //this.Close();
                    }
                }
                else
                {
                    if (SelectedAlarm.Value < SelectedAlarm.Tag.Value)
                    {
                        SelectedAlarm.Activated = false;
                        if ((HighAlarms == false) && (LowAlarms == false))
                        {
                            SelectedAlarm.Tag.Alarming = 0;
                            IOContext.Instance.Entry(SelectedAlarm.Tag).State = System.Data.Entity.EntityState.Modified;
                        }
                        IOContext.Instance.Entry(SelectedAlarm).State = System.Data.Entity.EntityState.Modified;
                        IOContext.Instance.SaveChanges();
                        AlarmsGrid.Items.Refresh();
                        //this.Close();
                    }
                }
            }
        }

        private void DeleteAlarm_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            // ako je korisnik kliknuo na Yes, obrisati alarm iz kolekcije
            if (result == MessageBoxResult.Yes)
            {
                bool HighAlarms = false;
                bool LowAlarms = false;
                foreach (Alarm al in SelectedAlarm.Tag.Alarms)
                {
                    if (al.Id != SelectedAlarm.Id)
                    {
                        if (al.Activated == true)
                        {
                            if (al.HighPriority)
                            {
                                HighAlarms = true;
                                break;
                            }
                            else
                            {
                                LowAlarms = true;
                            }
                        }
                    }
                }
                if (HighAlarms)
                {
                    SelectedAlarm.Tag.Alarming = 2;
                }
                else
                {
                    if (LowAlarms)
                    {
                        SelectedAlarm.Tag.Alarming = 1;
                    }
                    else
                    {
                        SelectedAlarm.Tag.Alarming = 0;
                    }
                }
                IOContext.Instance.Entry(SelectedAlarm.Tag).State = System.Data.Entity.EntityState.Modified;
                IOContext.Instance.Alarms.Remove(SelectedAlarm);
                IOContext.Instance.SaveChanges();
                AlarmsGrid.Items.Refresh();
                //this.Close();
            }
        }
    }
}
