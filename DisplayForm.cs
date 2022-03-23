using AtomPresentationTimer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AtomPresentationTimer
{
    public partial class DisplayForm : Form
    {
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);

        public DisplayForm()
        {
            InitializeComponent();
            this.Location = Settings.TimerSettings.Default.DisplayPosition;
        }

        public void Output(OutputFormat content) {
            lblDisplay.Text = content.Text;
            lblDisplay.Font = new Font((string)Settings.TimerSettings.Default.FontName, (float)Settings.TimerSettings.Default.FontSize);
            lblDisplay.ForeColor = content.Color;
        }

        private void lblDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void lblDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void lblDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);

            }
        }

        private void DisplayForm_LocationChanged(object sender, EventArgs e)
        {
            Debug.WriteLine($"Change position: {this.Location.X} {this.Location.Y} ");
            Settings.TimerSettings.Default.DisplayPosition = this.Location;
        }
    }
}
