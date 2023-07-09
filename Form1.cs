using System;
using System.Drawing;
using System.Windows.Forms;

namespace smooth_drag_opensource
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            smoothMoveTimer = new System.Windows.Forms.Timer();
            smoothMoveTimer.Interval = 1; // Adjust this value if needed
            smoothMoveTimer.Tick += SmoothMoveTimer_Tick;
            smoothness = trackBar1.Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private bool isDragging = false;
        private Point mouseOffset;
        public static float smoothness = 4f; // Adjust this value to control the smoothness of dragging

        private System.Windows.Forms.Timer smoothMoveTimer;

        // when the mouse moves
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point mousePos = PointToScreen(e.Location);
                mousePos.Offset(-mouseOffset.X, -mouseOffset.Y);

                // calculate the distance to move in the x and y directions
                float targetX = mousePos.X;
                float targetY = mousePos.Y;

                float dx = targetX - Location.X;
                float dy = targetY - Location.Y;

                // start the smooth move timer and set the increment values
                smoothMoveTimer.Start();
                smoothMoveTimer.Tag = new PointF(dx / smoothness, dy / smoothness);
            }
        }

        // when the mouse button is released
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // stop dragging and the smooth move timer
            isDragging = false;
            smoothMoveTimer.Stop();
        }

        // when the mouse button is pressed down
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // start dragging and record the mouse offset
            isDragging = true;
            mouseOffset = new Point(e.X, e.Y);
        }

        // when the smooth move timer ticks
        private void SmoothMoveTimer_Tick(object sender, EventArgs e)
        {
            // get the increment values from the timer's tag
            PointF increment = (PointF)smoothMoveTimer.Tag;

            // update the form's location based on the increment values
            float newX = Location.X + increment.X;
            float newY = Location.Y + increment.Y;
            Location = new Point((int)newX, (int)newY);

            // if the increment values are very small, stop the timer
            if (Math.Abs(increment.X) < 1 && Math.Abs(increment.Y) < 1)
            {
                smoothMoveTimer.Stop();
            }
        }

        // when the trackbar value changes
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // update the smoothness value based on the trackbar value
            smoothness = trackBar1.Value;
        }
    }
}
