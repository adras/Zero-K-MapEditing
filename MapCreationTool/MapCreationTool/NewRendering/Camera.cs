using OpenTK.Mathematics;
using System;
using System.Diagnostics;

namespace MapCreationTool.NewRendering
{
	// This is the camera class as it could be set up after the tutorials on the website.
	// It is important to note there are a few ways you could have set up this camera.
	// For example, you could have also managed the player input inside the camera class,
	// and a lot of the properties could have been made into functions.

	// TL;DR: This is just one of many ways in which we could have set up the camera.
	// Check out the web version if you don't know why we are doing a specific thing or want to know more about the code.
	public class Camera
	{
		// Those vectors are directions pointing outwards from the camera to define how it rotated.
		private Vector3 front = -Vector3.UnitZ;

		private Vector3 up = Vector3.UnitY;

		private Vector3 right = Vector3.UnitX;

		// Rotation around the X axis (radians)
		private float pitch;

		// Rotation around the Y axis (radians)
		private float yaw = -MathHelper.PiOver2; // Without this, you would be started rotated 90 degrees right.

		// The field of view of the camera (radians)
		private float fov = MathHelper.PiOver2;

		public Camera(Vector3 position, float aspectRatio)
		{
			Position = position;
			AspectRatio = aspectRatio;
		}

		// The position of the camera
		public Vector3 Position { get; set; }

		// This is simply the aspect ratio of the viewport, used for the projection matrix.
		public float AspectRatio { private get; set; }

		public Vector3 Front => front;

		public Vector3 Up => up;

		public Vector3 Right => right;

		// We convert from degrees to radians as soon as the property is set to improve performance.
		public float Pitch
		{
			get => MathHelper.RadiansToDegrees(pitch);
			set
			{
				// We clamp the pitch value between -89 and 89 to prevent the camera from going upside down, and a bunch
				// of weird "bugs" when you are using euler angles for rotation.
				// If you want to read more about this you can try researching a topic called gimbal lock
				var angle = MathHelper.Clamp(value, -179f, 0f);
				Debug.WriteLine($"Value: {value}");
				//float angle = value;
				pitch = MathHelper.DegreesToRadians(angle);
				UpdateVectors();
			}
		}

		// We convert from degrees to radians as soon as the property is set to improve performance.
		public float Yaw
		{
			get => MathHelper.RadiansToDegrees(yaw);
			set
			{
				yaw = MathHelper.DegreesToRadians(value);
				UpdateVectors();
			}
		}

		// The field of view (FOV) is the vertical angle of the camera view.
		// This has been discussed more in depth in a previous tutorial,
		// but in this tutorial, you have also learned how we can use this to simulate a zoom feature.
		// We convert from degrees to radians as soon as the property is set to improve performance.
		public float Fov
		{
			get => MathHelper.RadiansToDegrees(fov);
			set
			{
				var angle = MathHelper.Clamp(value, 1f, 45f);
				fov = MathHelper.DegreesToRadians(angle);
			}
		}

		// Get the view matrix using the amazing LookAt function described more in depth on the web tutorials
		public Matrix4 GetViewMatrix()
		{
			return Matrix4.LookAt(Position, Position + front, up);
		}

		// Get the projection matrix using the same method we have used up until this point
		public Matrix4 GetProjectionMatrix()
		{
			return Matrix4.CreatePerspectiveFieldOfView(fov, AspectRatio, 0.01f, 1000f);
		}

		private void UpdateVectors()
		{
			// I made it, I don't understand it, I won't explain it :P
			front.X = MathF.Sin(pitch) * MathF.Sin(yaw) ;
            front.Y = MathF.Sin(pitch) * MathF.Cos(yaw);
			front.Z = MathF.Cos(pitch);
            front.Normalize();

            right = Vector3.Cross(-Vector3.UnitZ, front).Normalized();
			up = Vector3.Cross(right, front).Normalized();
		}
	}
}