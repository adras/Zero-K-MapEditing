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
		public Camera camera;
		Shader shader;
		int VertexArrayObject;
		int VertexBufferObject;
		int ElementBufferObject;

		public TerrainRenderer()
		{
			camera = new Camera(new Vector3(0, 0, 3), 4.0f / 3.0f);

			shader = new Shader();
		}

		private float _time;
		ImageData imageData;
		public void Startup(string imagePath)
		{
			// Load shaders
			// Find a better place for the files, also the extension is weird, could be .glsl I guess
			shader.Load(@"NewRendering\shader.vert", @"NewRendering\shader.frag");

			// Setup vertex buffer
			// Create a new buffer
			VertexBufferObject = GL.GenBuffer();
			VertexArrayObject = GL.GenVertexArray();


		float[] vertices =
		{
			 0.5f,  0.5f, 0.0f, // top right
             0.5f, -0.5f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, // top left
        };

		// Then, we create a new array: indices.
		// This array controls how the EBO will use those vertices to create triangles
		uint[] indices =
		{
            // Note that indices start at 0!
            0, 1, 3, // The first triangle will be the bottom-right half of the triangle
            1, 2, 3  // Then the second will be the top-right half of the triangle
        };



		//imageData = ImageLoader.LoadImage(imagePath, 0, 300);
		//vertices = imageData.vertices;
		//uint[] indices = imageData.indices;
		imageData = new ImageData();
			imageData.vertices = vertices;
			imageData.indices = indices;



			VertexBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
			GL.BufferData(BufferTarget.ArrayBuffer, imageData.vertices.Length * sizeof(float), imageData.vertices, BufferUsageHint.StaticDraw);

			VertexArrayObject = GL.GenVertexArray();
			GL.BindVertexArray(VertexArrayObject);

			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);

			// We create/bind the Element Buffer Object EBO the same way as the VBO, except there is a major difference here which can be REALLY confusing.
			// The binding spot for ElementArrayBuffer is not actually a global binding spot like ArrayBuffer is. 
			// Instead it's actually a property of the currently bound VertexArrayObject, and binding an EBO with no VAO is undefined behaviour.
			// This also means that if you bind another VAO, the current ElementArrayBuffer is going to change with it.
			// Another sneaky part is that you don't need to unbind the buffer in ElementArrayBuffer as unbinding the VAO is going to do this,
			// and unbinding the EBO will remove it from the VAO instead of unbinding it like you would for VBOs or VAOs.
			ElementBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
			// We also upload data to the EBO the same way as we did with VBOs.
			GL.BufferData(BufferTarget.ElementArrayBuffer, imageData.indices.Length * sizeof(uint), imageData.indices, BufferUsageHint.StaticDraw);
			
			shader.Use();
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

			shader.Use();

			Matrix4 model = Matrix4.Identity;//  * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time));
			Matrix4 view = camera.GetViewMatrix();
			Matrix4 proj = camera.GetProjectionMatrix();

			shader.SetMatrix4("model", model);
			shader.SetMatrix4("view", view);
			shader.SetMatrix4("projection", proj);

			GL.BindVertexArray(VertexArrayObject);

			GL.DrawElements(PrimitiveType.Triangles, imageData.indices.Length, DrawElementsType.UnsignedInt, 0);
			GL.Finish();
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
			camera.Position += delta;
			camera.Pitch += 4;
		}

		internal void MouseMove(Vector3 delta)
		{

			camera.Yaw += delta.X;
			camera.Pitch -= delta.Y;

		}
	}
}
