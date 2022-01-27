using OpenTK.Graphics.OpenGL;
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
		#endregion
	}
}
