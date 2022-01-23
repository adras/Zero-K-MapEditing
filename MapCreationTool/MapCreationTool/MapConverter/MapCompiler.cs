using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.MapConverter
{
    internal class MapCompiler
    {
        public delegate void OnCompilationResult(object sender, MapCompilerState state, MapCompilerMessageType messageType, string message);
        public event OnCompilationResult CompilationResult;

        PyMapCompiler pyMapCompiler;
        MapInfoEditor mapInfoEditor;

        public MapCompiler(PyMapCompiler pyMapCompiler, MapInfoEditor mapInfoEditor)
        {
            this.pyMapCompiler = pyMapCompiler;
            this.mapInfoEditor = mapInfoEditor;

            pyMapCompiler.CompilationResult += PyMapCompiler_CompilationResult;
        }

        public void Compile(PyMapCompilerSettings pyCompilerSettings)
        {
            pyMapCompiler.Compile(pyCompilerSettings);
        }

        private void PyMapCompiler_CompilationResult(object sender, MapCompilerState state, MapCompilerMessageType messageType, string message)
        {
            CompilationResult?.Invoke(sender, state, messageType, message);
        }
    }
}
