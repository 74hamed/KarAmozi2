using System;
using System.Drawing;
using System.Windows.Forms;
using SharpGL;

namespace _3DCamera
{
    public partial class Form1 : Form
    {
        private float rotationX = 0.0f;
        private float rotationY = 0.0f;
        private Timer renderTimer;
        private OpenGLControl openGLControl;
        private bool isRotating = false;
        private Point lastMousePosition;

        public Form1()
        {
            InitializeComponent();
            InitOpenGLControl();
            InitRenderLoop();
        }

        private void InitOpenGLControl()
        {
            openGLControl = new OpenGLControl();
            openGLControl.Dock = DockStyle.Fill;
            openGLControl.OpenGLInitialized += OpenGLControl_OpenGLInitialized;
            openGLControl.OpenGLDraw += OpenGLControl_OpenGLDraw;
            openGLControl.Resized += OpenGLControl_Resized;
            openGLControl.MouseDown += OpenGLControl_MouseDown;
            openGLControl.MouseMove += OpenGLControl_MouseMove;
            openGLControl.MouseUp += OpenGLControl_MouseUp;
            this.Controls.Add(openGLControl);
        }

        private void InitRenderLoop()
        {
            renderTimer = new Timer();
            renderTimer.Interval = 16; //  60 FPS
            renderTimer.Tick += (sender, e) => openGLControl.Invalidate();
            renderTimer.Start();
        }

        private void OpenGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            var gl = openGLControl.OpenGL;
            gl.ClearColor(0, 0, 0, 0);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
        }

        private void OpenGLControl_Resized(object sender, EventArgs e)
        {
            var gl = openGLControl.OpenGL;
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(45.0f, (double)Width / (double)Height, 0.1, 100.0);
            gl.LookAt(0, 0, 10, 0, 0, 0, 0, 1, 0);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void OpenGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            var gl = openGLControl.OpenGL;

            
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            //rotations
            gl.Translate(0.0f, 0.0f, -10.0f);
            gl.Rotate(rotationX, 1.0f, 0.0f, 0.0f);
            gl.Rotate(rotationY, 0.0f, 1.0f, 0.0f);

            
            DrawCamera(gl);

            gl.Flush();
        }

        private void DrawCamera(OpenGL gl)
        {
            // main body of the camera
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(0.3, 0.3, 0.3);

            // Front face
            gl.Vertex(-2.0, -1.0, 1.0);
            gl.Vertex(2.0, -1.0, 1.0);
            gl.Vertex(2.0, 1.0, 1.0);
            gl.Vertex(-2.0, 1.0, 1.0);

            // Back face
            gl.Vertex(-2.0, -1.0, -1.0);
            gl.Vertex(2.0, -1.0, -1.0);
            gl.Vertex(2.0, 1.0, -1.0);
            gl.Vertex(-2.0, 1.0, -1.0);

            // Top face
            gl.Vertex(-2.0, 1.0, -1.0);
            gl.Vertex(2.0, 1.0, -1.0);
            gl.Vertex(2.0, 1.0, 1.0);
            gl.Vertex(-2.0, 1.0, 1.0);

            // Bottom face
            gl.Vertex(-2.0, -1.0, -1.0);
            gl.Vertex(2.0, -1.0, -1.0);
            gl.Vertex(2.0, -1.0, 1.0);
            gl.Vertex(-2.0, -1.0, 1.0);

            // Right face
            gl.Vertex(2.0, -1.0, -1.0);
            gl.Vertex(2.0, 1.0, -1.0);
            gl.Vertex(2.0, 1.0, 1.0);
            gl.Vertex(2.0, -1.0, 1.0);

            // Left face
            gl.Vertex(-2.0, -1.0, -1.0);
            gl.Vertex(-2.0, 1.0, -1.0);
            gl.Vertex(-2.0, 1.0, 1.0);
            gl.Vertex(-2.0, -1.0, 1.0);

            gl.End();

            // Draw the lens
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(0.1, 0.1, 0.1);

            // Front face
            gl.Vertex(-0.5, -0.5, 1.5);
            gl.Vertex(0.5, -0.5, 1.5);
            gl.Vertex(0.5, 0.5, 1.5);
            gl.Vertex(-0.5, 0.5, 1.5);

            // Back face
            gl.Vertex(-0.5, -0.5, 1.0);
            gl.Vertex(0.5, -0.5, 1.0);
            gl.Vertex(0.5, 0.5, 1.0);
            gl.Vertex(-0.5, 0.5, 1.0);

            // Top face
            gl.Vertex(-0.5, 0.5, 1.0);
            gl.Vertex(0.5, 0.5, 1.0);
            gl.Vertex(0.5, 0.5, 1.5);
            gl.Vertex(-0.5, 0.5, 1.5);

            // Bottom face
            gl.Vertex(-0.5, -0.5, 1.0);
            gl.Vertex(0.5, -0.5, 1.0);
            gl.Vertex(0.5, -0.5, 1.5);
            gl.Vertex(-0.5, -0.5, 1.5);

            // Right face
            gl.Vertex(0.5, -0.5, 1.0);
            gl.Vertex(0.5, 0.5, 1.0);
            gl.Vertex(0.5, 0.5, 1.5);
            gl.Vertex(0.5, -0.5, 1.5);

            // Left face
            gl.Vertex(-0.5, -0.5, 1.0);
            gl.Vertex(-0.5, 0.5, 1.0);
            gl.Vertex(-0.5, 0.5, 1.5);
            gl.Vertex(-0.5, -0.5, 1.5);

            gl.End();

            // Draw the handle
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(1, 0.2, 0.2);

            // Front face
            gl.Vertex(2.0, 0.5, 0.5);
            gl.Vertex(2.5, 0.5, 0.5);
            gl.Vertex(2.5, 1.5, 0.5);
            gl.Vertex(2.0, 1.5, 0.5);

            // Back face
            gl.Vertex(2.0, 0.5, -0.5);
            gl.Vertex(2.5, 0.5, -0.5);
            gl.Vertex(2.5, 1.5, -0.5);
            gl.Vertex(2.0, 1.5, -0.5);

            // Top face
            gl.Vertex(2.0, 1.5, -0.5);
            gl.Vertex(2.5, 1.5, -0.5);
            gl.Vertex(2.5, 1.5, 0.5);
            gl.Vertex(2.0, 1.5, 0.5);

            // Bottom face
            gl.Vertex(2.0, 0.5, -0.5);
            gl.Vertex(2.5, 0.5, -0.5);
            gl.Vertex(2.5, 0.5, 0.5);
            gl.Vertex(2.0, 0.5, 0.5);

            // Right face
            gl.Vertex(2.5, 0.5, -0.5);
            gl.Vertex(2.5, 1.5, -0.5);
            gl.Vertex(2.5, 1.5, 0.5);
            gl.Vertex(2.5, 0.5, 0.5);

            // Left face
            gl.Vertex(2.0, 0.5, -0.5);
            gl.Vertex(2.0, 1.5, -0.5);
            gl.Vertex(2.0, 1.5, 0.5);
            gl.Vertex(2.0, 0.5, 0.5);

            gl.End();

            // Draw the film reel
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(0.4, 0.4, 0.4);

            // Front face
            gl.Vertex(-2.5, 0.5, 1.0);
            gl.Vertex(-1.5, 0.5, 1.0);
            gl.Vertex(-1.5, 1.5, 1.0);
            gl.Vertex(-2.5, 1.5, 1.0);

            // Back face
            gl.Vertex(-2.5, 0.5, 0.5);
            gl.Vertex(-1.5, 0.5, 0.5);
            gl.Vertex(-1.5, 1.5, 0.5);
            gl.Vertex(-2.5, 1.5, 0.5);

            // Top face
            gl.Vertex(-2.5, 1.5, 0.5);
            gl.Vertex(-1.5, 1.5, 0.5);
            gl.Vertex(-1.5, 1.5, 1.0);
            gl.Vertex(-2.5, 1.5, 1.0);

            // Bottom face
            gl.Vertex(-2.5, 0.5, 0.5);
            gl.Vertex(-1.5, 0.5, 0.5);
            gl.Vertex(-1.5, 0.5, 1.0);
            gl.Vertex(-2.5, 0.5, 1.0);

            // Right face
            gl.Vertex(-1.5, 0.5, 0.5);
            gl.Vertex(-1.5, 1.5, 0.5);
            gl.Vertex(-1.5, 1.5, 1.0);
            gl.Vertex(-1.5, 0.5, 1.0);

            // Left face
            gl.Vertex(-2.5, 0.5, 0.5);
            gl.Vertex(-2.5, 1.5, 0.5);
            gl.Vertex(-2.5, 1.5, 1.0);
            gl.Vertex(-2.5, 0.5, 1.0);

            gl.End();
        }

        private void OpenGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isRotating = true;
                lastMousePosition = e.Location;
            }
        }

        private void OpenGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRotating)
            {
                var deltaX = e.X - lastMousePosition.X;
                var deltaY = e.Y - lastMousePosition.Y;

                rotationY += deltaX * 0.5f;
                rotationX -= deltaY * 0.5f;

                lastMousePosition = e.Location;
            }
        }

        private void OpenGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isRotating = false;
            }
        }
    }
}
