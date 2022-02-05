using MapCreationTool.Models;
using System.Windows;
using System.Windows.Controls;

namespace MapCreationTool.Controls
{
    /// <summary>
    /// Interaction logic for MapSettingsControl.xaml
    /// </summary>
    public partial class CompilationSettingsControl : UserControl
    {
        public ProjectSettings ProjectSettings
        {
            get { return (ProjectSettings)GetValue(ProjectSettingsProperty); }
            set { SetValue(ProjectSettingsProperty, value); }
        }

        public static readonly DependencyProperty ProjectSettingsProperty = DependencyProperty.Register(
            nameof(ProjectSettings),
            typeof(ProjectSettings),
            typeof(CompilationSettingsControl),
            new PropertyMetadata(null)
        );


        public CompilationSettingsControl()
        {
            InitializeComponent();
        }
    }
}
