using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Diagnostics;
using System.Windows.Input;

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
        TestTriangle triangle;
        public bool useTexture = true;

        public TerrainRenderer()
        {
            // Zoom out, set aspect ratio
            // Screen size will be updated by control's resize event during initialization
            camera = new Camera(new Vector3(0, 0, 2), 400, 300);

            // Look down
            camera.Pitch = -180;

            // Look ???
            camera.Yaw = 180;

            shader = new Shader();


            triangle = new TestTriangle();
        }

        public VertexData imageData;
        Texture diffuseTexture;
        public void Startup(string imagePath, string diffusePath)
        {

            // Load shaders
            // Find a better place for the files, also the extension is weird, could be .glsl I guess
            shader.Load(@"NewRendering\shader.vert", @"NewRendering\lighting.frag");

            imageData = ImageLoader.LoadImage(imagePath, 0, 300);
            diffuseTexture = Texture.LoadImage(diffusePath);

            float[] _vertices =
            {
				// Position         normal
				 0.5f,  0.5f, 0.0f, 0f, 0, -1.0f, // top right
				 0.5f, -1.0f, 0.0f, 0f, 0, -1.0f, // bottom right
				-0.5f, -0.5f, 0.0f, 0f, 0, -1.0f, // bottom left
				-0.5f,  0.5f, 0.0f, 0f, 0, -1.0f  // top left
			};

            uint[] _indices =
            {
                0, 1, 3,
				//1, 2, 3
				0, 1, 3
            };
            //imageData.vertices = _vertices;
            //imageData.indices = _indices;

            // AdrAs: Enable the depth buffer to make sure that triangles behind other triangles are not rendered
            GL.Enable(EnableCap.DepthTest);

            // AdrAs: Just for fun also make the front faces filled, and the back faces wireframe, to help identify future issues
            GL.PolygonMode(MaterialFace.Back, PolygonMode.Fill);
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Line);


            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, imageData.vertices.Length * sizeof(float), imageData.vertices, BufferUsageHint.StaticDraw);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            int positionLocation = shader.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

            int normalLocation = shader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

            int texCoordLocation = shader.GetAttribLocation("aTexCoords");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));


            //VertexArrayObject = GL.GenVertexArray();
            //GL.BindVertexArray(VertexArrayObject);

            //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            //GL.EnableVertexAttribArray(0);

            // We create/bind the Element Buffer Object EBO the same way as the VBO, except there is a major difference here which can be REALLY confusing.
            // The binding spot for ElementArrayBuffer is not actually a global binding spot like ArrayBuffer is. 
            // Instead it's actually a property of the currently bound VertexArrayObject, and binding an EBO with no VAO is undefined behaviour.
            // This also means that if you bind another VAO, the current ElementArrayBuffer is going to change with it.
            // Another sneaky part is that you don't need to unbind the buffer in ElementArrayBuffer as unbinding the VAO is going to do this,
            // and unbinding the EBO will remove it from the VAO instead of unbinding it like you would for VBOs or VAOs.
            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            // We also upload data to the EBO the same way as we did with VBOs.
            GL.BufferData(BufferTarget.ElementArrayBuffer, imageData.indices.Length * sizeof(uint), imageData.indices, BufferUsageHint.DynamicDraw);

            diffuseTexture.Use(TextureUnit.Texture0);
            shader.Use();


            triangle.Start();
        }

        internal void Update()
        {
            Vector3 delta = Vector3.Zero;
            float speed = 0.5f;
            if (Keyboard.IsKeyDown(Key.LeftShift))
                speed = 1.5f;

            if (Keyboard.IsKeyDown(Key.A))
            {
                delta -= camera.Right * speed;
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                delta += camera.Right * speed;
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                delta += camera.Front * speed;
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                delta -= camera.Front * speed;
            }
            Move(delta);

        }

        public void ShutDown()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
        }


        public void Render()
        {

            GL.ClearColor(Color4.AntiqueWhite);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.LoadIdentity();
            diffuseTexture.Use(TextureUnit.Texture0);
            diffuseTexture.Use(TextureUnit.Texture1);
            shader.Use();

            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 proj = camera.GetProjectionMatrix();

            //model = Matrix4.Identity;
            //view  = Matrix4.Identity;
            //proj  = Matrix4.Identity;

            //view = Matrix4.LookAt(new Vector3(0, 0, 2), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            //proj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 4.0f / 3.0f, 0.01f, 10f);

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", proj);

            // No texture yet, when texture is implemented, enable this again
            shader.SetVector3("viewPos", camera.Position);
            shader.SetInt("material.diffuse", 0);
            shader.SetInt("material.specular", 1);
            shader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            shader.SetFloat("material.shininess", 32.0f);

            shader.SetBool("useTexture", useTexture);
            shader.SetVector3("noTextureColor", new Vector3(1.0f, 1.0f, 0.7f));


            // Directional light needs a direction, in this example we just use (-0.2, -1.0, -0.3f) as the lights direction
            shader.SetVector3("light.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            shader.SetVector3("light.ambient", new Vector3(0.6f));
            shader.SetVector3("light.diffuse", new Vector3(1.0f));
            shader.SetVector3("light.specular", new Vector3(1.0f));

            GL.BindVertexArray(VertexArrayObject);

            GL.DrawElements(PrimitiveType.Triangles, imageData.indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.Finish();

            //triangle.Render();
        }

        internal void UpdateImageData()
        {
            // https://www.khronos.org/opengl/wiki/Vertex_Specification_Best_Practices#Dynamic_VBO
            // Using BufferSubData might be a good idea for performance improvement
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, imageData.vertices.Length * sizeof(float), imageData.vertices, BufferUsageHint.DynamicDraw);
        }

        internal void Move(Vector3 delta)
        {
            camera.Position += delta;
            //camera.Pitch += 4;
        }

        internal void MouseMove(Vector3 delta)
        {
            camera.Yaw += delta.X;
            camera.Pitch -= delta.Y;

        }
    }
}
