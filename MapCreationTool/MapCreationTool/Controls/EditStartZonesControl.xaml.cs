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
    //class PolygonEditor
    //{
    //    Polyline polyline;
    //    bool isClosed;

    //    public PolygonEditor(Polyline polyline)
    //    {
    //        this.polyline = polyline;
    //    }

    //    public void AddPoint(Point p)
    //    {

    //    }

    //    public void Close()
    //    {
    //        if (isClosed)
    //            return;

    //        isClosed = true;
    //    }

    //    private void UpdatePolygon()
    //    {

    //    }
    //}

    /// <summary>
    /// Interaction logic for EditStartZonesControl.xaml
    /// </summary>
    public partial class EditStartZonesControl : UserControl, INotifyPropertyChanged
    {
        ObservableCollection<StartZoneInfo> startZones;
        private StartZoneInfo selectedStartZone;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<StartZoneInfo> StartZones { get => startZones; set => startZones = value; }
        public StartZoneInfo SelectedStartZone { 
            get => selectedStartZone; 
            set
            {
                selectedStartZone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedStartZone)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedBoxCoords)));
            }
        }

        public PointCollection SelectedBoxCoords
        {
            get => SelectedStartZone?.BoxCoords;
            set
            {
                if (SelectedStartZone == null)
                    return;
                SelectedStartZone.BoxCoords = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedBoxCoords)));
            }
        }

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

            // Wow, databinding these non observable pointcollections really sucks
            // Let's do it like this
            UpdatePolyLine();
        }

        private void UpdatePolyLine()
        {
            PointCollection newPoints = new PointCollection();
            foreach (Point point in selectedStartZone.BoxCoords)
                newPoints.Add(point);

            SelectedBoxCoords = newPoints;
        }

        private void cvsDraw_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Can't close a polygon with less or equal than 2 points
            if (SelectedBoxCoords.Count <= 2)
                return;

            Point firstPoint = SelectedBoxCoords[0];
            SelectedBoxCoords.Add(firstPoint);
            UpdatePolyLine();
        }

        private void cvsDraw_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
