using MapCreationTool.Models;
using MapCreationTool.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace MapCreationTool.Controls
{
    /// <summary>
    /// Interaction logic for EditStartZonesControl.xaml
    /// </summary>
    public partial class EditStartZonesControl : UserControl, INotifyPropertyChanged
    {
        ObservableCollection<StartZoneInfo> startZones;
        //private StartZoneInfo selectedStartZone;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<StartZoneInfo> StartZones { get => startZones; set => startZones = value; }
        //public StartZoneInfo SelectedStartZone { get => selectedStartZone; 
        //    set
        //    {
        //        selectedStartZone = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedStartZone)));
        //    }

        //}


        public EditStartZonesControl()
        {
            InitializeComponent();
            StartZones = new ObservableCollection<StartZoneInfo>();
        }


        private void btnAddTeam_Click(object sender, RoutedEventArgs e)
        {
            StartZoneInfo startZoneInfo = new StartZoneInfo();
            startZoneInfo.Name = $"Team-{StartZones.Count + 1}";
            startZoneInfo.ShortName = $"T-{StartZones.Count + 1}";

            StartZones.Add(startZoneInfo);
        }

        private void cvsDraw_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StartZoneInfo selectedZone = lvTeams.SelectedItem as StartZoneInfo;
            if (selectedZone == null)
                return;

            Point mousePos = e.GetPosition(cvsDraw);
            selectedZone.BoxCoords.Add(mousePos);
            selectedZone.BoxCoords = selectedZone.BoxCoords;

            selectedZone.AddPointNotify(mousePos);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BoxCoords"));

        }
    }
}
