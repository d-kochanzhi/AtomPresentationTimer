using AtomPresentationTimer.Controllers;
using AtomPresentationTimer.Entities;
using AtomPresentationTimer.Settings;
using Bluegrams.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtomPresentationTimer
{
    public partial class MainForm : Form
    {
        DisplayForm form = new DisplayForm();
        public MainForm()
        {
            InitializeComponent();

            form.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SettingsController.GetInstance().LoadSettingToControls(this.Controls);
            LoadStrategy();
        }

        private void Strategy_OnTick(object sender, TimerEventArgs e)
        {
            statusLabel.Text = e.Message;
            updateButtonsState();
        }

        private void Strategy_OnStateChanged(object sender, TimerEventArgs e)
        {
            statusLabel.Text = e.Message;
            updateButtonsState();

        }

        private void updateButtonsState()
        {
            if (TimerController.GetInstance().Strategy == null) {
                btnRun.Enabled = false;
                btnPause.Enabled = false;
                btnStop.Enabled = false;
                btnReset.Enabled = false;
                return;
            }
            
            Enums.TimerStatus state = TimerController.GetInstance().Strategy.Status;

            var run = Enums.TimerStatus.Paused;
            var pause = Enums.TimerStatus.Running;
            var stop = Enums.TimerStatus.Running;
            var reset = Enums.TimerStatus.Paused | Enums.TimerStatus.Stopped;

            btnRun.Enabled = run.HasFlag(state);
            btnPause.Enabled = pause.HasFlag(state);
            btnStop.Enabled = stop.HasFlag(state);
            btnReset.Enabled = reset.HasFlag(state);

            var output = TimerController.GetInstance().Strategy.Output();
            lblMonitor.Text = output.Text;
            lblMonitor.ForeColor = output.Color;
            form.Output(output);

        }

        private void LoadStrategy() {
            switch (tabControlTimers.SelectedIndex)
            {
                case 0:
                    TimerController.GetInstance().SetStrategy(new CountDownStrategy());
                    break;
                case 1:
                    TimerController.GetInstance().SetStrategy(new CountUpStrategy());
                    break;
                case 2:
                    TimerController.GetInstance().SetStrategy(new ClockStrategy());
                    break;
            }


            TimerController.GetInstance().Strategy.OnStateChanged += Strategy_OnStateChanged;
            TimerController.GetInstance().Strategy.OnTick += Strategy_OnTick;
            
            updateButtonsState();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingsController.GetInstance().SaveSettings();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            TimerController.GetInstance().Strategy.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            TimerController.GetInstance().Strategy.Pause();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            TimerController.GetInstance().Strategy.Reset();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            TimerController.GetInstance().Strategy.Stop();

        }

       

        private void btnColor_Click(object sender, EventArgs e)
        {  
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

     

  

        private void tabControlTimers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStrategy();
        }

       
        private void resetWindowPosition_Click(object sender, EventArgs e)
        {
            form.Location= new Point(0, 0);
        }

        private void contextScreenColorPicker_Click(object sender, EventArgs e)
        {
            var menuItem = (sender as ToolStripMenuItem);

            if (menuItem != null)
            {
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    Control sourceControl = owner.SourceControl;

                    UserControls.ScreenColorPicker cp = new UserControls.ScreenColorPicker();
                    cp.Closed += (o, args) => { sourceControl.BackColor = cp.SelectedColor; };
                    cp.Show();

                }
            }
 
           
        }

      

       
    }
}
