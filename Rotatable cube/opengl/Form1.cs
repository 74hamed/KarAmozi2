using System;
using System.Windows.Forms;
using SharpGL;

namespace opengl

{
    public partial class Form1 : Form
    {
        private float rotationX = 0.0f;
        private float rotationY = 0.0f;
        private bool isDragging = false;
        private int lastX, lastY;

        public Form1()
        {
            InitializeComponent();
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);

            // Enable depth testing
            gl.Enable(OpenGL.GL_DEPTH_TEST);
        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            // Set the viewport
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);

            // Set the projection matrix
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(45.0f, (double)openGLControl.Width / (double)openGLControl.Height, 0.1, 100.0);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;

            // Clear the color and depth buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Load the identity matrix
            gl.LoadIdentity();

            // Move the cube back a bit
            gl.Translate(0.0f, 0.0f, -6.0f);

            // Rotate the cube
            gl.Rotate(rotationX, 1.0f, 0.0f, 0.0f);
            gl.Rotate(rotationY, 0.0f, 1.0f, 0.0f);

            // Draw the cube
            DrawCube(gl);

            // Flush the OpenGL buffers
            gl.Flush();
        }

        private void DrawCube(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_QUADS);

            // Front face
            gl.Color(1.0f, 0.0f, 0.0f); // Red
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(1.0f, 1.0f, 1.0f);
            gl.Color(1.0f, 1.0f, 0.0f); // Yellow
            gl.Vertex(-1.0f, 1.0f, 1.0f);

            // Back face
            gl.Color(0.0f, 1.0f, 0.0f); // Green
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f); // Red
            gl.Vertex(-1.0f, 1.0f, -1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(1.0f, 1.0f, -1.0f);
            gl.Color(1.0f, 1.0f, 0.0f); // Yellow
            gl.Vertex(1.0f, -1.0f, -1.0f);

            // Top face
            gl.Color(0.0f, 0.0f, 1.0f); // Blue
            gl.Vertex(-1.0f, 1.0f, -1.0f);
            gl.Color(1.0f, 1.0f, 0.0f); // Yellow
            gl.Vertex(-1.0f, 1.0f, 1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(1.0f, 1.0f, 1.0f);
            gl.Color(1.0f, 0.0f, 0.0f); // Red
            gl.Vertex(1.0f, 1.0f, -1.0f);

            // Bottom face
            gl.Color(0.0f, 0.0f, 1.0f); // Blue
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f); // Red
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(1.0f, 1.0f, 0.0f); // Yellow
            gl.Vertex(-1.0f, -1.0f, 1.0f);

            // Right face
            gl.Color(1.0f, 0.0f, 1.0f); // Magenta
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 1.0f, 0.0f); // Yellow
            gl.Vertex(1.0f, 1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f); // Red
            gl.Vertex(1.0f, 1.0f, 1.0f);
            gl.Color(0.0f, 0.0f, 1.0f); // Blue
            gl.Vertex(1.0f, -1.0f, 1.0f);

            // Left face
            gl.Color(0.0f, 1.0f, 1.0f); // Cyan
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f); // Red
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(1.0f, 0.0f, 1.0f); // Magenta
            gl.Vertex(-1.0f, 1.0f, 1.0f);
            gl.Color(0.0f, 0.0f, 1.0f); // Blue
            gl.Vertex(-1.0f, 1.0f, -1.0f);

            gl.End();
            
        }

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastX = e.X;
            lastY = e.Y;
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        DialogResult resault =  MessageBox.Show("Are you sure you want to exit ?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
            if(resault==DialogResult.Yes)
            Application.Exit();
            
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = e.X - lastX;
                int deltaY = e.Y - lastY;

                rotationX += deltaY * 0.5f;
                rotationY += deltaX * 0.5f;

                lastX = e.X;
                lastY = e.Y;

                openGLControl.Invalidate();
            }
        }
    }
}
