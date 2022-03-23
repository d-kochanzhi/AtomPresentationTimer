namespace AtomPresentationTimer
{
    partial class DisplayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDisplay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.Location = new System.Drawing.Point(12, 9);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(38, 15);
            this.lblDisplay.TabIndex = 0;
            this.lblDisplay.Text = "label1";
            this.lblDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblDisplay_MouseDown);
            this.lblDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblDisplay_MouseMove);
            this.lblDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblDisplay_MouseUp);
            // 
            // DisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 450);
            this.Controls.Add(this.lblDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DisplayForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DisplayForm";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.LocationChanged += new System.EventHandler(this.DisplayForm_LocationChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDisplay;
    }
}