using MapCreationTool.Helpers;
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
    public class PyMapCompilerSetting
    {
        public string MAP_CONV_EXE_PATH = @"Tools\PyMapConv\pymapconv.exe";

        public readonly string identifier;
        public readonly string settingValue;
        public readonly string custom;

        // Use this overload for custom settings which are not composed out of identifier, and setting value
        public PyMapCompilerSetting(string custom)
        {
            this.custom = custom;
        }

        public PyMapCompilerSetting(string identifier, string settingValue)
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

    public class PyMapCompilerSettings
    {
        List<PyMapCompilerSetting> settings;
        public PyMapCompilerSettings()
        {
            settings = new List<PyMapCompilerSetting>();
        }

        public void AddSetting(PyMapCompilerSetting setting)
        {
            settings.Add(setting);
        }

        public void Clear()
        {
            settings.Clear();
        }

        public bool Remove(PyMapCompilerSetting setting)
        {
            bool result = settings.Remove(setting);
            return result;
        }

        public bool Remove(string identifier)
        {
            PyMapCompilerSetting? setting = settings.FirstOrDefault(s => s.identifier == identifier);
            if (setting == null)
                return false;

            bool result = settings.Remove(setting);
            return result;
        }

        public string GenerateParameterString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (PyMapCompilerSetting setting in settings)
            {
                // Using ToString overload
                sb.Append($" {setting}");
            }
            string result = sb.ToString();
            return result;
        }
    }

    public class PyMapCompiler
    {
        public delegate void OnCompilationResult(object sender, MapCompilerState state, MapCompilerMessageType messageType, string message);
        public event OnCompilationResult CompilationResult;
        Process pyProcess;

        public void Compile(PyMapCompilerSettings settings)
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
                RedirectStandardError = true,
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
                pyProcess.ErrorDataReceived += PyProcess_ErrorDataReceived;
                pyProcess.StartInfo = startInfo;
                pyProcess.Start();
                string executeText = $"Executing {startInfo.FileName} {startInfo.Arguments}";
                CompilationResult?.Invoke(this, MapCompilerState.Running, MapCompilerMessageType.Output, $"Executing: {executeText}");

                pyProcess.BeginOutputReadLine();
                pyProcess.BeginErrorReadLine();
                pyProcess.WaitForExit();
                CompilationResult?.Invoke(this, MapCompilerState.Complete, MapCompilerMessageType.Output, "Complete");
            });
        }

        private void PyProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            CompilationResult?.Invoke(this, MapCompilerState.Running, MapCompilerMessageType.Error, e.Data);
            Debug.WriteLine(e.Data);
        }

        private void PyProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            CompilationResult?.Invoke(this, MapCompilerState.Running, MapCompilerMessageType.Output, e.Data);
            Debug.WriteLine(e.Data);
        }
    }
}
