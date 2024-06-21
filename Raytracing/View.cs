using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using OpenTK.Input;

namespace OpenGL
{
    internal class View
    {
        int BasicProgramID;
        int BasicVertexShader;
        int BasicFragmentShader;
        public OpenTK.Vector3 CubeColor;
        public OpenTK.Vector3 CameraPosition;
        public OpenTK.Vector3 CameraView;
        public OpenTK.Vector3 CameraUp;
        public OpenTK.Vector3 CameraSide;
        public View()
        {
            CameraPosition = new OpenTK.Vector3(0.0f, 0.0f, -15.0f);
            CameraView = new OpenTK.Vector3(0.0f, 0.0f, 1.0f);
            CameraUp = new OpenTK.Vector3(0.0f, 1.0f, 0.0f);
            CameraSide = new OpenTK.Vector3(1.0f, 0.0f, 0.0f);

        }
        private void loadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        private void SetUniformVec3(string name, OpenTK.Vector3 value)
        {
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, name), value);
        }

        public void InitShaders()
        {
            BasicProgramID = GL.CreateProgram();
            loadShader("..\\..\\Shaders\\raytracing.vert", ShaderType.VertexShader, BasicProgramID, out BasicVertexShader);
            loadShader("..\\..\\Shaders\\raytracing.frag", ShaderType.FragmentShader, BasicProgramID, out BasicFragmentShader);
            GL.LinkProgram(BasicProgramID);
            int status = 0;
            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));
        }

        public bool Init()
        {
            GL.Enable(EnableCap.ColorMaterial);
            GL.ShadeModel(ShadingModel.Smooth);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            return true;
        }

        public void Draw()
        {
            GL.ClearColor(Color.AliceBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.UseProgram(BasicProgramID);
            SetUniformVec3("cube_color", CubeColor);
            SetUniformVec3("camera_position", CameraPosition);
            SetUniformVec3("camera_view", CameraView);
            SetUniformVec3("camera_up", CameraUp);
            SetUniformVec3("camera_side", CameraSide);
            // Quad
            GL.Color3(Color.White);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 1);
            GL.Vertex2(-1, -1);

            GL.TexCoord2(1, 1);
            GL.Vertex2(1, -1);

            GL.TexCoord2(1, 0);
            GL.Vertex2(1, 1);

            GL.TexCoord2(0, 0);
            GL.Vertex2(-1, 1);

            GL.End();
            GL.UseProgram(0);
        }
    }

    //1. Источник освещения, такой чтобы свет падал равномерно и интенсивность изменялась в зависимости от удаления
    //2. Добавить тетраэдр
    //3. Сделать все стены зеркальными
    //4. Шарик сделать диффузным, зеркальным и преломляющим
    //5. Кубик поправить

    //1.Генерация лучей, расчет пересечения точки с поверхностью
    /*
     2. Найти пересечение луча с картой плотностей уровня 100
    Построить изоповерхность уровня 100
     */
}
