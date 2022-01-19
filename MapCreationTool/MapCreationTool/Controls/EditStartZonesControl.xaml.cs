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
    class PolygonEditor
    {
        bool isClosed;
        PointCollection points;
        EditStartZonesControl control;

        public PolygonEditor(EditStartZonesControl control, PointCollection points = null)
        {
            this.points = points;
            this.control = control;

            if (this.points == null)
                this.points = new PointCollection();
        }

        public void AddPoint(Point p)
        {
            if (isClosed)
                return;

            points.Add(p);
            UpdatePolygon();
        }

        public PointCollection GetPoints()
        {
            return points;
        }

        public void Close()
        {
            if (isClosed)
                return;

            // Can't close a polygon with less or equal than 2 points
            if (points.Count <= 2)
                return;

            // Add the first point to close the polygon
            AddPoint(points[0]);

            isClosed = true;
        }

        private void UpdatePolygon()
        {
            PointCollection newPoints = new PointCollection();
            foreach (Point point in points)
                newPoints.Add(point);

            control.SelectedBoxCoords = newPoints;
        }
    }

    /// <summary>
    /// Interaction logic for EditStartZonesControl.xaml
    /// </summary>
    public partial class EditStartZonesControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<StartZoneInfo> startZones;
        private StartZoneInfo selectedStartZone;
        private Dictionary<StartZoneInfo, PolygonEditor> editors;

        private PolygonEditor CurrentEditor
        {
            get
            {
                return editors[SelectedStartZone];
            }
        }

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
            editors = new Dictionary<StartZoneInfo, PolygonEditor>();
        }


        private void btnAddTeam_Click(object sender, RoutedEventArgs e)
        {
            StartZoneInfo startZoneInfo = new StartZoneInfo();
            startZoneInfo.Name = $"Team-{StartZones.Count + 1}";
            startZoneInfo.ShortName = $"T-{StartZones.Count + 1}";

            editors.Add(startZoneInfo, new PolygonEditor(this));

            StartZones.Add(startZoneInfo);
        }

        private void cvsDraw_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StartZoneInfo selectedZone = lvTeams.SelectedItem as StartZoneInfo;
            if (selectedZone == null)
                return;

            Point mousePos = e.GetPosition(cvsDraw);
            CurrentEditor?.AddPoint(mousePos);
        }


        private void cvsDraw_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            CurrentEditor.Close();
        }

        private void cvsDraw_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
