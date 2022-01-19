using MapCreationTool.Configuration;
using MapCreationTool.Rendering;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapCreationTool.Controls
{
    /// <summary>
    /// Interaction logic for TerrainControl.xaml
    /// </summary>
    public partial class TerrainControl : UserControl
    {
        Testing testing;

        public ProjectSettings ProjectSettings
        {
            get { return (ProjectSettings)GetValue(ProjectSettingsProperty); }
            set { SetValue(ProjectSettingsProperty, value); }
        }

        public static readonly DependencyProperty ProjectSettingsProperty = DependencyProperty.Register(
            nameof(ProjectSettings),
            typeof(ProjectSettings),
            typeof(TerrainControl),
            new PropertyMetadata(null)
        );

        public TerrainControl()
        {
            InitializeComponent();

            testing = new Testing(this);
        }

        private async void viewPortMain_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            bool newVisibility = (bool)e.NewValue;
            if (!newVisibility)
                return;
            //Dispatcher.Invoke(() =>
            //    {
                    MeshGeometry3D geom = testing.LoadHeightmap();
                    testing.SetMeshGeometry(geom);
                //});
        }
    }
}
