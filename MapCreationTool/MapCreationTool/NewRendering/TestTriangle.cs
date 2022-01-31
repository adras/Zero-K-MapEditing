using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.NewRendering
{
	class TestTriangle
	{
		float[] vertices = {
			-0.5f, -0.5f, 0.0f, //Bottom-left vertex
			 0.5f, -0.5f, 0.0f, //Bottom-right vertex
			 0.0f,  0.5f, 0.0f  //Top vertex
		};

		int vertexBufferObject;
		int vertexArrayObject;

		Shader shader;

		public void Start()
		{
			//GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
			vertexBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
			GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

			vertexArrayObject = GL.GenVertexArray();
			GL.BindVertexArray(vertexArrayObject);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);



			shader = new Shader();
			shader.Load(@"NewRendering\triangle.vert", @"NewRendering\triangle.frag");
			shader.Use();

		}

		public void Render()
		{
			//GL.Clear(ClearBufferMask.ColorBufferBit);
			shader.Use();

			GL.BindVertexArray(vertexArrayObject);
			GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
		}

		public void Unload()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.DeleteBuffer(vertexBufferObject);
		}
	}
}
