using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace MapCreationTool.Rendering
{
    class MouseOrbitCamera
    {
        private IInputElement uiElement;

        public PerspectiveCamera perspectiveCamera;

        private Point lastMousePos;
        private Point currentMousePos;

        private Point positionDelta;
        private int wheelDelta;

        const double ZOOM_FACTOR = 0.1;

        private double xAngle = 0;
        private double yAngle = 0;
        private double zoom = 0;

        public double XAngle
        {
            get
            {
                return xAngle;
            }

            set
            {
                xAngle = value;
            }
        }

        public double YAngle
        {
            get
            {
                return yAngle;
            }

            set
            {
                yAngle = value;
            }
        }

        public double Zoom
        {
            get
            {
                return zoom;
            }

            set
            {
                zoom = value;
            }
        }

        public MouseOrbitCamera(IInputElement uiElement)
        {
            this.uiElement = uiElement;

            perspectiveCamera = new PerspectiveCamera();
            perspectiveCamera.FieldOfView = 30;

            lastMousePos = Mouse.GetPosition(uiElement);
            currentMousePos = Mouse.GetPosition(uiElement);
            uiElement.MouseWheel += UiElement_MouseWheel;
        }

        private void UiElement_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            wheelDelta = e.Delta;
        }

        public void Update(DeltaTime delta)
        {
            lastMousePos = currentMousePos;
            currentMousePos = Mouse.GetPosition(uiElement);
            positionDelta = (Point)(currentMousePos - lastMousePos);

            UpdateCamera();

            wheelDelta = 0;
        }

        private void UpdateCamera()
        {

            if (wheelDelta != 0)
            {
                ZoomCamera();
            }

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                OrbitCamera();

            }

            UpdatePerspective();


            if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                PanCamera();
            }


        }
        Vector3D testPos = new Vector3D();
        private void UpdatePerspective()
        {
            Matrix3D m = new Matrix3D();
            m.Rotate(new Quaternion(new Vector3D(1, 0, 0), XAngle));
            m.Rotate(new Quaternion(new Vector3D(0, 0, 1), YAngle));
            var cameraPos = m.Transform(new Vector3D(0, 0, 1) * Zoom);

            //Vector3D lookDir = new Vector3D() - cameraPos;
            Vector3D lookDir = testPos - cameraPos;
            Vector3D up = Vector3D.CrossProduct(lookDir, new Vector3D(-lookDir.Y, lookDir.X, 0));

			lookDir.Normalize();
			up.Normalize();

			//Debug.WriteLine("xAngle: " + XAngle + " - yangle: " + YAngle);
			perspectiveCamera.Position = (Point3D)cameraPos;
            perspectiveCamera.LookDirection = lookDir;
            perspectiveCamera.UpDirection = up;
        }

        private void OrbitCamera()
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                YAngle += positionDelta.X;
                XAngle += positionDelta.Y;

            }
            XAngle = Math.Clamp(XAngle, 1, 180);
        }


        private void ZoomCamera()
        {
            //perspectiveCamera.Position += perspectiveCamera.LookDirection * (wheelDelta * ZOOM_FACTOR);
            Zoom -= wheelDelta * ZOOM_FACTOR;
            Zoom = Math.Clamp(Zoom, 1, 10000);
        }

        private void PanCamera()
        {
            Vector3D leftAxis = Vector3D.CrossProduct(perspectiveCamera.LookDirection, perspectiveCamera.UpDirection);
            Vector3D upAxis = perspectiveCamera.UpDirection;


            Vector3D translation = -leftAxis * positionDelta.X * 0.1 + upAxis * positionDelta.Y * 0.1;
            testPos += translation;
            //Debug.WriteLine("Translation: " + translation);
            //perspectiveCamera.Position += translation;
            //testPos += new Vector3D(positionDelta.X, positionDelta.Y, 0);
        }
    }
}
