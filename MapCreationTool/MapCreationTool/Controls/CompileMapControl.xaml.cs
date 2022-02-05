using MapCreationTool.MapConverter;
using MapCreationTool.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MapCreationTool.Controls
{
    /// <summary>
    /// Interaction logic for CompileMapControl.xaml
    /// </summary>
    public partial class CompileMapControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        string compilationResults;
        //PyMapCompiler compiler;
        MapCompiler mapCompiler;

        public string CompilationResults
        {
            get => compilationResults; set
            {
                compilationResults = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CompilationResults)));
            }
        }


        public ProjectSettings ProjectSettings
        {
            get { return (ProjectSettings)GetValue(ProjectSettingsProperty); }
            set { SetValue(ProjectSettingsProperty, value); }
        }

        public static readonly DependencyProperty ProjectSettingsProperty = DependencyProperty.Register(
            nameof(ProjectSettings),
            typeof(ProjectSettings),
            typeof(CompileMapControl),
            new PropertyMetadata(null)
        );

        public CompileMapControl()
        {
            InitializeComponent();
            PyMapCompiler pyMapCompiler = new PyMapCompiler();
            MapInfoEditor mapInfoEditor = new MapInfoEditor();
            mapCompiler = new MapCompiler(pyMapCompiler, mapInfoEditor);

            mapCompiler.CompilationResult += Compiler_CompilationResult;
        }

        private void Compiler_CompilationResult(object sender, MapCompilerState state, MapCompilerMessageType messageType, string message)
        {
            CompilationResults += message + "\n";
            Dispatcher.Invoke(() => scrollViewer.ScrollToBottom());
        }

        private void CompileDeployControl_OnExecuteAction(object sender, ActionTypes actionType)
        {
            CompilationResults = "";
            PyMapCompilerSettings compileSettings = ProjectSettingsToCompilerSettingsConverter.Convert(ProjectSettings);

            mapCompiler.Compile(compileSettings, ProjectSettings);
        }

    }
}
