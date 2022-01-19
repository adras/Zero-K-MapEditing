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
        const int MINIMUM_POINT_DISTANCE = 10;

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

        public int Count { get { return points.Count; } }

        public Point? GetPointAtIndex (int idx)
        {
            if (points.Count == 0)
                return null;

            return points[idx];
        }

        public Point? GetLastPoint()
        {
            if (points.Count == 0)
                return null;

            return points[points.Count - 1];
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

        internal void Reset()
        {
            points.Clear();
            isClosed = false;
            UpdatePolygon();
        }

        internal void AddPointConsecutive(Point mousePos)
        {
            Point? lastPoint;
            // If it's the first point, just add it
            if ((lastPoint = GetLastPoint()) == null)
            {
                AddPoint(mousePos);
                return;
            }

            // Otherwise get distance, and only add it, if it's a little bit apart
            Vector delta = new Vector(mousePos.X - lastPoint.Value.X, mousePos.Y - lastPoint.Value.Y);
            if (delta.Length < MINIMUM_POINT_DISTANCE)
                return;

            AddPoint(mousePos);
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
                if (startZones.Count == 0)
                    return null;
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


        private void cvsDraw_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            CurrentEditor.Close();
        }

        private void cvsDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
                return;

            StartZoneInfo selectedZone = lvTeams.SelectedItem as StartZoneInfo;
            if (selectedZone == null)
                return;


            Point mousePos = e.GetPosition(cvsDraw);
            CurrentEditor?.AddPointConsecutive(mousePos);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            CurrentEditor?.Reset();
        }

        private void cvsDraw_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(cvsDraw);

            CurrentEditor?.AddPointConsecutive(mousePos);
        }
    }
}
