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

		public void Ready()
		{
            // This breaks the current demo, do not use :D
			//Console.WriteLine("GlWpfControl is now ready");
			//GL.Enable(EnableCap.Blend);
			//GL.Enable(EnableCap.DepthTest);
			//GL.Enable(EnableCap.ScissorTest);
		}

		public void Render()
		{
            var hue = (float)_stopwatch.Elapsed.TotalSeconds * 0.15f % 1;
            var c = Color4.FromHsv(new Vector4(hue, 0.75f, 0.75f, 1));
            GL.ClearColor(c);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.LoadIdentity();
            GL.Begin(PrimitiveType.Triangles);

            GL.Color4(Color4.Red);
            GL.Vertex2(-0.20f, 0.12f);

            GL.Color4(Color4.Green);
            GL.Vertex2(0.28f, -0.12f);

            GL.Color4(Color4.Blue);
            GL.Vertex2(-0.28f, -0.12f);

            GL.End();
            
            //GL.Begin(PrimitiveType.Triangles);

            //GL.Color4(Color4.Red);
            //GL.Vertex2(-0.2, 0);

            //GL.Color4(Color4.Green);
            //GL.Vertex2(0.2, 0);

            //GL.Color4(Color4.Blue);
            //GL.Vertex2(0, 0.2);

            //GL.End();
            
            GL.Finish();
        }
	}
}
