using MapCreationTool.Helpers;
using MapCreationTool.WPF;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
using IO = System.IO;

namespace MapCreationTool.Controls
{
    enum MapCompilerState
    {
        Running,
        Complete,
    }

    class MapCompiler
    {
        public delegate void OnCompilationResult(object sender, MapCompilerState state, string message);
        public event OnCompilationResult CompilationResult;
        Process pyProcess;

        public void Compile()
        {
            if (pyProcess == null)
                pyProcess = new Process();
            else
            {
                if (!pyProcess.HasExited)
                {
                    pyProcess.Kill();
                }
                pyProcess = new Process();
            }

            string pyConvPath = IO.Path.Combine(PathHelper.GetApplicationDirectory().FullName, @"Tools\PyMapConv\pymapconv.exe");
            IO.FileInfo pyMapConvFi = new IO.FileInfo(pyConvPath);

            string args = @" -o E:\Zero-K-Maps\Minimi\minimi.smf ";
            args += @"-t e:\Zero-K-Maps\medium\diffuse.bmp ";
            args += @"-a e:\Zero-K-Maps\medium\height.png ";
            args += @"-x 400 ";
            args += @"-n -150 ";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = pyMapConvFi.FullName,
                Arguments = args,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                WorkingDirectory = pyMapConvFi.Directory.FullName
            };

            Task.Run(() =>
            {
                pyProcess.OutputDataReceived += PyProcess_OutputDataReceived;
                pyProcess.StartInfo = startInfo;
                pyProcess.Start();

                CompilationResult?.Invoke(this, MapCompilerState.Running, "Started");

                pyProcess.BeginOutputReadLine();
                pyProcess.WaitForExit();
                CompilationResult?.Invoke(this, MapCompilerState.Complete, "Complete");
            });
        }

        private void PyProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            CompilationResult?.Invoke(this, MapCompilerState.Running, e.Data);
            Debug.WriteLine(e.Data);
        }
    }

    /// <summary>
    /// Interaction logic for CompileMapControl.xaml
    /// </summary>
    public partial class CompileMapControl : UserControl, INotifyPropertyChanged
    {
        string compilationResults;
        MapCompiler compiler;

        public string CompilationResults
        {
            get => compilationResults; set
            {
                compilationResults = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CompilationResults)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public CompileMapControl()
        {
            InitializeComponent();
            compiler = new MapCompiler();
            compiler.CompilationResult += Compiler_CompilationResult;
        }

        private void Compiler_CompilationResult(object sender, MapCompilerState state, string message)
        {
            CompilationResults += message + "\n";
        }

        private void CompileDeployControl_OnExecuteAction(object sender, ActionTypes actionType)
        {
            CompilationResults = "";
            compiler.Compile();
        }

    }
}
