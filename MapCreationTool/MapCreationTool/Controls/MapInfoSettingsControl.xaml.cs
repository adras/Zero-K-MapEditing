using MapCreationTool.Models;
using System.Windows;
using System.Windows.Controls;

namespace MapCreationTool.Controls
{
    /// <summary>
    /// Interaction logic for MapInfoSettings.xaml
    /// </summary>
    public partial class MapInfoSettingsControl : UserControl
    {
        public ProjectSettings ProjectSettings
        {
            get { return (ProjectSettings)GetValue(ProjectSettingsProperty); }
            set { SetValue(ProjectSettingsProperty, value); }
        }

        public static readonly DependencyProperty ProjectSettingsProperty = DependencyProperty.Register(
            nameof(ProjectSettings),
            typeof(ProjectSettings),
            typeof(MapInfoSettingsControl),
            new PropertyMetadata(null)
        );

        public MapInfoSettingsControl()
        {
            InitializeComponent();
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            ProjectSettings.MapInformation = MapInformation.LoadFrom(ProjectSettings.MapPathInformation.mapInfoPath);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ProjectSettings.MapInformation.SaveAs(ProjectSettings.MapPathInformation.mapInfoPath);
        }
    }
}
