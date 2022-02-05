using OpenTK.Mathematics;

namespace MapCreationTool.NewRendering.HitTest
{
    public static class CameraExtensions
    {
        public static Vector3 ScreenToPointRay(this Camera camera, Vector3 mousePos, Matrix4 matrix)
        {
            Matrix4 proj = camera.GetProjectionMatrix();
            Matrix4 view = camera.GetViewMatrix();


            //Matrix4 newMatrix = Matrix4.Invert(view * proj);

            //Vector4 v4 = new Vector4(mousePos);
            //v4.W = 1;
            //Quaternion quat = Quaternion.FromMatrix(new Matrix3(newMatrix));
            //Vector4 result = Vector4.Transform(v4, quat);

            Vector4 mouse4 = new Vector4(mousePos);
            //mouse4.Y = mouse4.Y;
            mouse4.Z = 0;
            mouse4.W = 1000f;



            Vector4 test = mouse4 * matrix;
            //Vector4 test = Vector4.TransformColumn(newMatrix, mouse4);

            //test.W = 1.0f / test.W;
            //test.X *= test.W;
            //test.Y *= test.W;
            //test.Z *= test.W;





            //Quaternion quat = Quaternion.FromMatrix(new Matrix3(newMatrix));
            //Vector4 result = Vector4.Transform(mousePos, quat);

            return new Vector3(test);
        }
    }
}
