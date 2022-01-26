using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.NewRendering
{
	// https://stackoverflow.com/questions/31211689/opentk-model-view-projection-problems
	// https://stackoverflow.com/questions/25065671/glloadmatrix-replacement
	// https://stackoverflow.com/questions/45374853/opentk-gl-matrixmode-and-gl-loadmatrix-not-found
	public class Camera
	{
		Matrix4 modelMatrix;
		Matrix4 viewMatrix;
		Matrix4 projectionMatrix;

		public Vector3 position;
		public Vector3 lookAt;


		public Camera()
		{
			position = new Vector3(0, 0, 1);
		}

		public Matrix4 Update()
		{
			modelMatrix = Matrix4.Identity;

			//Vector3 up = Vector3.Cross(position, lookAt);
			Vector3 up = new Vector3(0, 1, 0);
			viewMatrix = Matrix4.LookAt(position, lookAt, up);

			projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
				MathHelper.DegreesToRadians(45),
				4.0f / 3.0f, 0.01f, 100f);

			Matrix4 result = modelMatrix * viewMatrix * projectionMatrix;

			return result;
		}

	}
}
