using DataConcentrator;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace ScadaGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AnalogInput SelectedAI { get; set; }
        public AnalogOutput SelectedAO { get; set; }
        public DigitalInput SelectedDI { get; set; }
        public DigitalOutput SelectedDO { get; set; }
        public Alarm SelectedAlarm { get; set; }
        public AlarmHistory SelectedHistory { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            IOContext.Instance.DigitalInputs.Load();
            IOContext.Instance.DigitalOutputs.Load();
            IOContext.Instance.AnalogInputs.Load();
            IOContext.Instance.AnalogOutputs.Load();
            IOContext.Instance.AlarmHistories.Load();
            IOContext.Instance.Alarms.Load();

            foreach(DigitalInput di in IOContext.Instance.DigitalInputs)
            {
                if (di.OnOffScan)
                {
                    di.Load();
                }
            }

            foreach(AnalogInput ai in IOContext.Instance.AnalogInputs)
            {
                if(ai.OnOffScan)
                {
                    ai.Load();
                }
            }

            DIGrid.ItemsSource = IOContext.Instance.DigitalInputs.Local;
            DOGrid.ItemsSource = IOContext.Instance.DigitalOutputs.Local;
            AIGrid.ItemsSource = IOContext.Instance.AnalogInputs.Local;
            AOGrid.ItemsSource = IOContext.Instance.AnalogOutputs.Local;
            AlarmsGrid.ItemsSource = IOContext.Instance.Alarms.Local;
            HistoryGrid.ItemsSource = IOContext.Instance.AlarmHistories.Local;

            Input.ValueChanged += RefreshInputs;
            Alarm.AlarmTriggered += ActivatedAlarm;

            this.DataContext = this;
        }

        public void RefreshInputs()
        {
            DIGrid.Dispatcher.Invoke(() => { DIGrid.Items.Refresh(); });
            AIGrid.Dispatcher.Invoke(() => { AIGrid.Items.Refresh(); });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (DigitalInput DI in IOContext.Instance.DigitalInputs.Local)
            {
                DI.Abort();
            }
            foreach (AnalogInput AI in IOContext.Instance.AnalogInputs.Local)
            {
                AI.Abort();
            }
            DictionaryThreads.PLCsim.Abort();
        }

        private void AddDI_Click(object sender, RoutedEventArgs e)
        {
            AddDI addDI = new AddDI();
            addDI.ShowDialog();
        }

        private void ContinueDI_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedDI.OnOffScan == false)
            {
                SelectedDI.Load();
                IOContext.Instance.Entry(SelectedDI).State = System.Data.Entity.EntityState.Modified;
                IOContext.Instance.SaveChanges();
                DIGrid.Items.Refresh();
            }
        }

        private void PauseDI_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDI.OnOffScan)
            {
                SelectedDI.Unload();
                IOContext.Instance.Entry(SelectedDI).State = System.Data.Entity.EntityState.Modified;
                IOContext.Instance.SaveChanges();
                DIGrid.Items.Refresh();
            }
        }

        private void UpdateDI_Click(object sender, RoutedEventArgs e)
        {
            UpdateDI updatedi = new UpdateDI(SelectedDI);
            updatedi.ShowDialog();
            DIGrid.Items.Refresh();
        }

        private void DeleteDI_Click(object sender, RoutedEventArgs e)
        {
            // otvaranje Yes/No dijaloga
            var result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            // ako je korisnik kliknuo na Yes, obrisati DI iz kolekcije
            if (result == MessageBoxResult.Yes)
            {
                SelectedDI.Abort();
                IOContext.Instance.DigitalInputs.Remove(SelectedDI);
                IOContext.Instance.SaveChanges();
                DIGrid.Items.Refresh();
            }
        }

        private void AddDO_Click(object sender, RoutedEventArgs e)
        {
            AddDO addDO = new AddDO();
            addDO.ShowDialog();
        }

        private void UpdateDO_Click(object sender, RoutedEventArgs e)
        {
            UpdateDO updatedo = new UpdateDO(SelectedDO);
            updatedo.ShowDialog();
            DOGrid.Items.Refresh();
        }

        private void DeleteDO_Click(object sender, RoutedEventArgs e)
        {
            // otvaranje Yes/No dijaloga
            var result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            // ako je korisnik kliknuo na Yes, obrisati DO iz kolekcije
            if (result == MessageBoxResult.Yes)
            {
                IOContext.Instance.DigitalOutputs.Remove(SelectedDO);
                IOContext.Instance.SaveChanges();
            }
        }

        private void AddAI_Click(object sender, RoutedEventArgs e)
        {
            AddAI addAI = new AddAI();
            addAI.ShowDialog();
        }

        private void ContinueAI_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedAI.OnOffScan == false)
            {
                SelectedAI.Load();
                IOContext.Instance.Entry(SelectedAI).State = System.Data.Entity.EntityState.Modified;
                IOContext.Instance.SaveChanges();
                AIGrid.Items.Refresh();
            }
        }

        private void PauseAI_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedAI.OnOffScan)
            {
                SelectedAI.Unload();
                IOContext.Instance.Entry(SelectedAI).State = System.Data.Entity.EntityState.Modified;
                IOContext.Instance.SaveChanges();
                AIGrid.Items.Refresh();
            }
        }

        private void DetailsAI_Click(object sender, RoutedEventArgs e)
        {
            UpdateAI detailsAI = new UpdateAI(SelectedAI, false);
            detailsAI.ShowDialog();
        }

        private void UpdateAI_Click(object sender, RoutedEventArgs e)
        {
            UpdateAI changeAI = new UpdateAI(SelectedAI, true);
            changeAI.ShowDialog();
            AIGrid.Items.Refresh();
        }

        private void AlarmsAI_Click(object sender, RoutedEventArgs e)
        {
            ShowAlarms showAlarm = new ShowAlarms(SelectedAI.ID);
            showAlarm.ShowDialog();
        }

        private void DeleteAI_Click(object sender, RoutedEventArgs e)
        {
            // otvaranje Yes/No dijaloga
            var result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            // ako je korisnik kliknuo na Yes, obrisati AI iz kolekcije
            if (result == MessageBoxResult.Yes)
            {
                SelectedAI.Abort();
                IOContext.Instance.AnalogInputs.Remove(SelectedAI);
                IOContext.Instance.SaveChanges();
            }
        }

        private void AddAO_Click(object sender, RoutedEventArgs e)
        {
            AddAO addAO = new AddAO();
            addAO.ShowDialog();
        }

        private void DetailsAO_Click(object sender, RoutedEventArgs e)
        {
            UpdateAO detailsAO = new UpdateAO(SelectedAO, false);
            detailsAO.ShowDialog();
        }

        private void UpdateAO_Click(object sender, RoutedEventArgs e)
        {
            UpdateAO updateAO = new UpdateAO(SelectedAO, true);
            updateAO.ShowDialog();
            AOGrid.Items.Refresh();
        }

        private void DeleteAO_Click(object sender, RoutedEventArgs e)
        {
            // otvaranje Yes/No dijaloga
            var result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            // ako je korisnik kliknuo na Yes, obrisati AO iz kolekcije
            if (result == MessageBoxResult.Yes)
            {
                IOContext.Instance.AnalogOutputs.Remove(SelectedAO);
                IOContext.Instance.SaveChanges();
            }
        }

        private void AddAlarm_Click(object sender, RoutedEventArgs e)
        {
            AddAlarm addAlarm = new AddAlarm();
            addAlarm.ShowDialog();
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
                        AIGrid.Items.Refresh();
                        AlarmsGrid.Items.Refresh();
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
                        AIGrid.Items.Refresh();
                        AlarmsGrid.Items.Refresh();
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
                                LowAlarms = false;
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
                AIGrid.Items.Refresh();
            }
        }

        private void ActivatedAlarm(Alarm alarm)
        {
            AIGrid.Dispatcher.Invoke(() => {
                if (alarm.HighPriority)
                {
                    alarm.Tag.Alarming = 2;
                }
                else
                {
                    if (alarm.Tag.Alarming == 0)
                    {
                        alarm.Tag.Alarming = 1;
                    }
                }
                IOContext.Instance.Entry(alarm.Tag).State = System.Data.Entity.EntityState.Modified;
                IOContext.Instance.SaveChanges();
            });

            AlarmHistory alarmHistory = null;
            alarmHistory = new AlarmHistory(alarm.Id, alarm.Tag.Name, alarm.Message, DateTime.Now);
            if(alarm.HighPriority)
            {
                alarmHistory.Acknowledged = false;
            }
            else
            {
                alarmHistory.Acknowledged = true;
            }
            HistoryGrid.Dispatcher.Invoke(() => {
                if (alarmHistory != null)
                {
                    IOContext.Instance.AlarmHistories.Add(alarmHistory);
                    IOContext.Instance.SaveChanges();
                }});

            AIGrid.Dispatcher.Invoke(() => AIGrid.Items.Refresh());
            AlarmsGrid.Dispatcher.Invoke(() => AlarmsGrid.Items.Refresh());
            HistoryGrid.Dispatcher.Invoke(() => HistoryGrid.Items.Refresh());
        }

        private void ConfirmHistory_Click(object sender, RoutedEventArgs e)
        {
            SelectedHistory.Acknowledged = true;
            IOContext.Instance.Entry(SelectedHistory).State = System.Data.Entity.EntityState.Modified;
            IOContext.Instance.SaveChanges();
            HistoryGrid.Items.Refresh();
        }

        private void EraseHistory_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            // ako je korisnik kliknuo na Yes, obrisati istoriju iz kolekcije
            if (result == MessageBoxResult.Yes)
            {
                IOContext.Instance.AlarmHistories.Remove(SelectedHistory);
                IOContext.Instance.SaveChanges();
            }
        }
    }
}
