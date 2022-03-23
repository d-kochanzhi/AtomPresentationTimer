using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AtomPresentationTimer.UserControls
{
    public partial class ScreenColorPicker : Form
    {

        Bitmap bitmap = null;

        public Color SelectedColor { get { return pictureBox1.BackColor; } }
        public ScreenColorPicker()
        {
            bitmap = CaptureFromScreen(Rectangle.Empty);

            InitializeComponent();
        }
        

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.TopMost = true;    
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.BackgroundImage = (Image)bitmap;
            this.MouseMove += ScreenColorPicker_MouseMove;
        }


        
      

        private void ScreenColorPicker_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                pictureBox1.BackColor = bitmap.GetPixel(e.Location.X, e.Location.Y);
            }
            catch (Exception)
            {                
            }
            
        }

        private Bitmap CaptureFromScreen(Rectangle rect)
        {
            Bitmap bmpScreenCapture = null;

            if (rect == Rectangle.Empty)//capture the whole screen
            {
                rect = Screen.PrimaryScreen.Bounds;
            }

            bmpScreenCapture = new Bitmap(rect.Width, rect.Height);

            Graphics p = Graphics.FromImage(bmpScreenCapture);


            p.CopyFromScreen(rect.X,
                     rect.Y,
                     0, 0,
                     rect.Size,
                     CopyPixelOperation.SourceCopy);


            p.Dispose();

            return bmpScreenCapture;
        }

        private void ScreenColorPicker_Click(object sender, EventArgs e)
        {            
            this.Close();            
        }

    }
}
