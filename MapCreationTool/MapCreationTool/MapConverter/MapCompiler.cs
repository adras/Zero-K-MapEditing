using MapCreationTool.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.MapConverter
{
    /// <summary>
    /// 
    /// </summary>
    public class MapCompilerSetting
	{
		public string MAP_CONV_EXE_PATH = @"Tools\PyMapConv\pymapconv.exe";

		public readonly string identifier;
		public readonly string settingValue;
		public readonly string custom;

		// Use this overload for custom settings which are not composed out of identifier, and setting value
		public MapCompilerSetting(string custom)
		{
			this.custom = custom;
		}

		public MapCompilerSetting(string identifier, string settingValue)
		{
			this.identifier = identifier;
			this.settingValue = settingValue;
		}

		public override string ToString()
		{
			string result;
			if (string.IsNullOrEmpty(custom))
			{
				result = $"{identifier} {settingValue}";
			}
			else
			{
				result = $"{custom}";
			}
			return result;
		}
	}

	public class MapCompilerSettings
	{
		List<MapCompilerSetting> settings;
		public MapCompilerSettings()
		{
			settings = new List<MapCompilerSetting>();
		}

		public void AddSetting(MapCompilerSetting setting)
		{
			settings.Add(setting);
		}

		public void Clear()
		{
			settings.Clear();
		}

		public bool Remove(MapCompilerSetting setting)
		{
			bool result = settings.Remove(setting);
			return result;
		}

		public bool Remove(string identifier)
		{
			MapCompilerSetting? setting = settings.FirstOrDefault(s => s.identifier == identifier);
			if (setting == null)
				return false;

			bool result = settings.Remove(setting);
			return result;
		}

		public string GenerateParameterString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (MapCompilerSetting setting in settings)
			{
				// Using ToString overload
				sb.Append($" {setting}");
			}
			string result = sb.ToString();
			return result;
		}
	}

	public class MapCompiler
	{
		public delegate void OnCompilationResult(object sender, MapCompilerState state, string message);
		public event OnCompilationResult CompilationResult;
		Process pyProcess;

		public void Compile(MapCompilerSettings settings)
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

			string pyConvPath = Path.Combine(PathHelper.GetApplicationToolsDirectory().FullName, @"PyMapConv\pymapconv.exe");
			FileInfo pyMapConvFi = new FileInfo(pyConvPath);

			//string args = @" -o E:\Zero-K-Maps\mini\minimi.smf ";
			//args += @"-t e:\Zero-K-Maps\medium\diffuse.bmp ";
			//args += @"-a e:\Zero-K-Maps\medium\height.png ";
			//args += @"-x 400 ";
			//args += @"-n -150 ";
			string args = settings.GenerateParameterString();

			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = pyMapConvFi.FullName,
				Arguments = args,
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardOutput = true,
				WorkingDirectory = pyMapConvFi.Directory.FullName
			};

			//string args = @"d:\repos\OtherRepositories\Zero-K-MapEditing\springrts_smf_compiler\pymapconv.py ";
			//args += @" -o E:\Zero-K-Maps\Minimi\minimi.smf ";
			//args += @"-t e:\Zero-K-Maps\medium\diffuse.bmp ";
			//args += @"-a e:\Zero-K-Maps\medium\height.png ";
			//args += @"-x 400 ";
			//args += @"-n -150 ";


			//ProcessStartInfo startInfo = new ProcessStartInfo
			//{
			//    FileName = "python.exe",
			//    Arguments = args,
			//    CreateNoWindow = true,
			//    UseShellExecute = false,
			//    RedirectStandardOutput = true,
			//    WorkingDirectory = pyMapConvFi.Directory.FullName
			//};

			Debug.WriteLine("Using args: " + args);

			Task.Run(() =>
			{
				pyProcess.OutputDataReceived += PyProcess_OutputDataReceived;
				pyProcess.StartInfo = startInfo;
				pyProcess.Start();
				string executeText = $"Executing {startInfo.FileName} {startInfo.Arguments}";
				CompilationResult?.Invoke(this, MapCompilerState.Running, $"Executing: {executeText}");

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
}
