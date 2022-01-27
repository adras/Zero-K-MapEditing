using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.NewRendering
{
	public class Shader : IDisposable
	{
		int Handle;
		bool loaded = false;
		private  Dictionary<string, int> _uniformLocations;
		public Shader()
		{
		}

		public void Load(string vertexPath, string fragmentPath)
		{
			// Load shader files
			string VertexShaderSource;
			using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
			{
				VertexShaderSource = reader.ReadToEnd();
			}

			string FragmentShaderSource;
			using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
			{
				FragmentShaderSource = reader.ReadToEnd();
			}

			// Create GL Objects
			int VertexShader = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(VertexShader, VertexShaderSource);

			int FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(FragmentShader, FragmentShaderSource);

			// Compile
			GL.CompileShader(VertexShader);

			string infoLogVert = GL.GetShaderInfoLog(VertexShader);
			if (infoLogVert != string.Empty)
				Console.WriteLine(infoLogVert);

			GL.CompileShader(FragmentShader);

			string infoLogFrag = GL.GetShaderInfoLog(FragmentShader);

			if (infoLogFrag != System.String.Empty)
				Console.WriteLine(infoLogFrag);


			// Link
			Handle = GL.CreateProgram();
			GL.AttachShader(Handle, VertexShader);
			GL.AttachShader(Handle, FragmentShader);
			GL.LinkProgram(Handle);

			

			// Cleanup
			GL.DetachShader(Handle, VertexShader);
			GL.DetachShader(Handle, FragmentShader);
			GL.DeleteShader(FragmentShader);
			GL.DeleteShader(VertexShader);


			// The shader is now ready to go, but first, we're going to cache all the shader uniform locations.
			// Querying this from the shader is very slow, so we do it once on initialization and reuse those values
			// later.

			// First, we have to get the number of active uniforms in the shader.
			GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

			// Next, allocate the dictionary to hold the locations.
			_uniformLocations = new Dictionary<string, int>();

			// Loop over all the uniforms,
			for (var i = 0; i < numberOfUniforms; i++)
			{
				// get the name of this uniform,
				var key = GL.GetActiveUniform(Handle, i, out _, out _);

				// get the location,
				var location = GL.GetUniformLocation(Handle, key);

				// and then add it to the dictionary.
				_uniformLocations.Add(key, location);
			}

			loaded = true;
		}

		public void Use()
		{
			if (!loaded)
			{
				throw new NotSupportedException("Shader not loaded yet. Please call Load before");
			}

			GL.UseProgram(Handle);
		}

		#region IDisposable
		private bool disposedValue = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				GL.DeleteProgram(Handle);

				disposedValue = true;
			}
		}

		~Shader()
		{
			GL.DeleteProgram(Handle);
		}


		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		internal void SetMatrix4(string name, Matrix4 data)
		{
			GL.UseProgram(Handle);
			GL.UniformMatrix4(_uniformLocations[name], true, ref data);
		}
		#endregion
	}
}
