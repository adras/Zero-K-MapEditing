using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.NewRendering.Lights
{
    public static class ShaderExtensions
    {

        public static void SetPointLights(this Shader shader, List<PointLight> lights)
        {
            GL.UseProgram(shader.Handle);
            shader.SetInt("pointLightCount", lights.Count);
            for (int i = 0; i < lights.Count; i++)
            {
                SetPointLight(shader, i, lights[i]);
            }
        }
        public static void SetDirLights(this Shader shader, List<DirLight> lights)
        {
            GL.UseProgram(shader.Handle);
            shader.SetInt("dirLightCount", lights.Count);
            for (int i = 0; i < lights.Count; i++)
            {
                SetDirLight(shader, i, lights[i]);
            }
        }
        public static void SetSpotLights(this Shader shader, List<SpotLight> lights)
        {
            GL.UseProgram(shader.Handle);
            shader.SetInt("spotLightCount", lights.Count);
            for (int i = 0; i < lights.Count; i++)
            {
                SetSpotLight(shader, i, lights[i]);
            }
        }

        public static void SetPointLight(this Shader shader, int idx, PointLight data)
        {
            GL.UseProgram(shader.Handle);

            GL.Uniform3(shader._uniformLocations[$"pointLights[{idx}].ambient"], data.ambient);
            GL.Uniform1(shader._uniformLocations[$"pointLights[{idx}].constant"], data.constant);
            GL.Uniform3(shader._uniformLocations[$"pointLights[{idx}].diffuse"], data.diffuse);
            GL.Uniform1(shader._uniformLocations[$"pointLights[{idx}].linear"], data.linear);
            GL.Uniform3(shader._uniformLocations[$"pointLights[{idx}].position"], data.position);
            GL.Uniform1(shader._uniformLocations[$"pointLights[{idx}].quadratic"], data.quadratic);
            GL.Uniform3(shader._uniformLocations[$"pointLights[{idx}].specular"], data.specular);
        }

        public static void SetDirLight(this Shader shader, int idx, DirLight data)
        {
            GL.UseProgram(shader.Handle);

            GL.Uniform3(shader._uniformLocations[$"dirLights[{idx}].ambient"], data.ambient);
            GL.Uniform3(shader._uniformLocations[$"dirLights[{idx}].diffuse"], data.diffuse);
            GL.Uniform3(shader._uniformLocations[$"dirLights[{idx}].direction"], data.direction);
            GL.Uniform3(shader._uniformLocations[$"dirLights[{idx}].specular"], data.specular);
        }

        public static void SetSpotLight(this Shader shader, int idx, SpotLight data)
        {
            GL.UseProgram(shader.Handle);

            GL.Uniform3(shader._uniformLocations[$"spotLights[{idx}].position"], data.position);
            GL.Uniform3(shader._uniformLocations[$"spotLights[{idx}].direction"], data.direction);
            GL.Uniform1(shader._uniformLocations[$"spotLights[{idx}].cutOff"], data.cutOff);
            GL.Uniform1(shader._uniformLocations[$"spotLights[{idx}].outerCutOff"], data.outerCutOff);

            GL.Uniform3(shader._uniformLocations["spotLights[{idx}].specular"], data.specular);
            GL.Uniform3(shader._uniformLocations["spotLights[{idx}].diffuse"], data.diffuse);
            GL.Uniform3(shader._uniformLocations["spotLights[{idx}].ambient"], data.ambient);

            GL.Uniform1(shader._uniformLocations["spotLights[{idx}].constant"], data.constant);
            GL.Uniform1(shader._uniformLocations["spotLights[{idx}].linear"], data.linear);
            GL.Uniform1(shader._uniformLocations["spotLights[{idx}].quadratic"], data.quadratic);
        }

    }
}
