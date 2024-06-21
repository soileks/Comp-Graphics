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

namespace OpenGL
{
    public partial class Form1 : Form
    {
        View view;
        float yaw = -90.0f;
        float pitch = 0.0f;
        public Form1()
        {
            view= new View();
            InitializeComponent();
        }
        private void openGlControl_Paint(object sender, PaintEventArgs e)
        {
            view.Draw();
            openGlControl.SwapBuffers();
        }

        private void openGlControl_Load(object sender, EventArgs e)
        {
            view.Init();
            view.InitShaders();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            view.CubeColor.X = trackBar1.Value / 255.0f;
            openGlControl.Invalidate();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            view.CameraPosition.X = trackBar4.Value;
            openGlControl.Invalidate();
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            view.CameraPosition.Y = trackBar5.Value;
            openGlControl.Invalidate();
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            view.CameraPosition.Z = trackBar6.Value;
            openGlControl.Invalidate();
        }

        private void trackBar2_Scroll_1(object sender, EventArgs e)
        {
            view.CubeColor.Y = trackBar2.Value / 255.0f;
            openGlControl.Invalidate();
        }

        private void trackBar3_Scroll_1(object sender, EventArgs e)
        {
            view.CubeColor.Z = trackBar3.Value / 255.0f;
            openGlControl.Invalidate();
        }

        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            openGlControl.Invalidate();
        }

        private void trackBar8_Scroll(object sender, EventArgs e)
        {
            openGlControl.Invalidate();
        }
    }
}
