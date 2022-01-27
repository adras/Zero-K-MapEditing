using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.NewRendering
{
	class TerrainRenderer
	{
		private static readonly Stopwatch _stopwatch = Stopwatch.StartNew();
		Camera camera;
		Shader shader;
		int VertexArrayObject;

		public TerrainRenderer()
		{
			camera = new Camera();
			shader = new Shader();
		}

		int VertexBufferObject;
		public void Startup()
		{
			// Load shaders
			// Find a better place for the files, also the extension is weird, could be .glsl I guess
			shader.Load(@"NewRendering\shader.vert", @"NewRendering\shader.frag");

			// Setup vertex buffer
			// Create a new buffer
			VertexBufferObject = GL.GenBuffer();


			VertexArrayObject = GL.GenVertexArray();

			// todo, should come from image load
			float[] vertices = {
				-0.5f, -0.5f, 0.0f, // Bottom-left vertex
				 0.5f, -0.5f, 0.0f, // Bottom-right vertex
				 0.0f,  0.5f, 0.0f  // Top vertex
			};

			// ..:: Initialization code (done once (unless your object frequently changes)) :: ..
			// 1. bind Vertex Array Object
			GL.BindVertexArray(VertexArrayObject);
			// 2. copy our vertices array in a buffer for OpenGL to use
			GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
			GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
			// 3. then set our vertex attributes pointers
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);




			// Copy data to the ArrayBuffer
			// Usage Hint:
			// StaticDraw: the data will most likely not change at all or very rarely.
			// DynamicDraw: the data is likely to change a lot.
			// StreamDraw: the data will change every time it is drawn.

			// Later we would like to use dynamic or stream
			//GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

			// This breaks the current demo, do not use :D
			//Console.WriteLine("GlWpfControl is now ready");
			//GL.Enable(EnableCap.Blend);
			//GL.Enable(EnableCap.DepthTest);
			//GL.Enable(EnableCap.ScissorTest);
		}

		public void ShutDown()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.DeleteBuffer(VertexBufferObject);
		}


		public void Render()
		{
			GL.ClearColor(Color4.DarkGray);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			GL.LoadIdentity();

			Matrix4 view = camera.Update();
			GL.LoadMatrix(ref view);
			shader.Use();
			GL.BindVertexArray(VertexArrayObject);
			GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
		}

		//public void Render()
		//{
		//	// Before image loader is implemented, the vertex buffers should be tested
		//	// here with a simple triangle/rectangle
		//	// light and normals could also be tested
		//	// https://opentk.net/learn/chapter1/2-hello-triangle.html


		//	Matrix4 view = camera.Update();

		//	float hue = (float)_stopwatch.Elapsed.TotalSeconds * 0.15f % 1;
		//	Color4 c = Color4.FromHsv(new Vector4(hue, 0.75f, 0.75f, 1));
		//	GL.ClearColor(c);
		//	GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		//	GL.LoadIdentity();

		//	GL.LoadMatrix(ref view);

		//	GL.Begin(PrimitiveType.Triangles);

		//	GL.Color4(Color4.Red);
		//	GL.Vertex2(-0.20f, 0.12f);

		//	GL.Color4(Color4.Green);
		//	GL.Vertex2(0.28f, -0.12f);

		//	GL.Color4(Color4.Blue);
		//	GL.Vertex2(-0.28f, -0.12f);

		//	GL.End();

		//	//GL.Begin(PrimitiveType.Triangles);

		//	//GL.Color4(Color4.Red);
		//	//GL.Vertex2(-0.2, 0);

		//	//GL.Color4(Color4.Green);
		//	//GL.Vertex2(0.2, 0);

		//	//GL.Color4(Color4.Blue);
		//	//GL.Vertex2(0, 0.2);

		//	//GL.End();

		//	GL.Finish();
		//}

		internal void Move(Vector3 delta)
		{
			camera.position += delta;
		}
	}
}
