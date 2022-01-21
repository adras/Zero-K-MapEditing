using MapCreationTool.Helpers;
using MapCreationTool.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
	/// <summary>
	/// Interaction logic for CompileMapControl.xaml
	/// </summary>
	public partial class CompileMapControl : UserControl, INotifyPropertyChanged
	{
		string compilationResults;
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
		}



		private void CompileDeployControl_OnExecuteAction(object sender, ActionTypes actionType)
		{
			CompilationResults = "Starte\n\nd";
			string pyConvPath = IO.Path.Combine(PathHelper.GetApplicationDirectory().FullName, @"Tools\PyMapConv\pymapconv.exe");
			IO.FileInfo pyMapConvFi = new IO.FileInfo(pyConvPath);
			
			string args = "-o sadf";
		
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = pyMapConvFi.FullName,
				Arguments = args,
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardOutput = true,
				WorkingDirectory = pyMapConvFi.Directory.FullName
			};

			Process pyProcess = new Process();
			pyProcess.OutputDataReceived += PyProcess_OutputDataReceived;
			pyProcess.StartInfo = startInfo;
			pyProcess.Start();
		}

		private void PyProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			CompilationResults += e.Data + "\n";
		}
	}
}
