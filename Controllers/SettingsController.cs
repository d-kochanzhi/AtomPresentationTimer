using AtomPresentationTimer.Entities.Base;
using AtomPresentationTimer.Settings;
using AtomPresentationTimer.UserControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AtomPresentationTimer.Controllers
{
    internal class SettingsController : ISettingsContext
    {
        #region singleton

        private static SettingsController _instance;
        public static SettingsController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SettingsController();
            }
            return _instance;
        }

        #endregion

        string[] PropertyNames
        {
            get
            {
                return TimerSettings.Default.Properties.Cast<SettingsProperty>()
                       .Select(p => p.Name).ToArray();
            }
        }

        public void LoadSettingToControls(Control.ControlCollection controls)
        {

            foreach (Control control in controls)
            {

                if (control.Tag != null && PropertyNames.Contains(control.Tag.ToString()))
                {

                    if (control is NumericUpDown)
                    {
                        ((NumericUpDown)control).Value = decimal.Parse(TimerSettings.Default[control.Tag.ToString()].ToString());
                        ((NumericUpDown)control).ValueChanged += NumericUpDown_ValueChanged;
                    }

                    if (control is Button)
                    {
                        ((Button)control).BackColor = (Color)TimerSettings.Default[control.Tag.ToString()];
                        ((Button)control).BackColorChanged += Button_BackColorChanged;
                    }

                    if (control is FontComboBox)
                    {
                        var cb = control as ComboBox;
                        var fonts = cb.DataSource as Font[];
                        var selected = fonts.FirstOrDefault(f => f.Name.Equals((string)TimerSettings.Default[control.Tag.ToString()], StringComparison.InvariantCultureIgnoreCase));

                        ((FontComboBox)control).SelectedItem = selected;
                        ((FontComboBox)control).SelectedIndexChanged += FontCombo_SelectedIndexChanged;
                    }
                    else if (control is ComboBox)
                    {

                        ((ComboBox)control).SelectedItem = TimerSettings.Default[control.Tag.ToString()].ToString();
                        ((ComboBox)control).SelectedIndexChanged += Combo_SelectedIndexChanged;
                    }

                    if (control is TabControl)
                    {
                        ((TabControl)control).SelectedIndex = int.Parse(TimerSettings.Default[control.Tag.ToString()].ToString());
                        ((TabControl)control).SelectedIndexChanged += Tab_SelectedIndexChanged;
                    }
                }

                if (control.HasChildren)
                    LoadSettingToControls(control.Controls);
            }
        }

        public void SaveSettings()
        {
            TimerSettings.Default.Save();
        }

        private void Tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            var control = sender as TabControl;

            TimerSettings.Default[control.Tag.ToString()] = control.SelectedIndex;
            Debug.WriteLine($"[{control.Name}] changed value to: {control.SelectedIndex}");
        }
        private void Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var control = sender as ComboBox;

            TimerSettings.Default[control.Tag.ToString()] = control.SelectedItem;
            Debug.WriteLine($"[{control.Name}] changed value to: {control.SelectedItem}");
        }
        private void FontCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var control = sender as FontComboBox;

            TimerSettings.Default[control.Tag.ToString()] = control.SelectedValue;
            Debug.WriteLine($"[{control.Name}] changed value to: {control.SelectedValue}");
        }
        private void Button_BackColorChanged(object sender, EventArgs e)
        {
            var control = sender as Button;
            TimerSettings.Default[control.Tag.ToString()] = control.BackColor;
            Debug.WriteLine($"[{control.Name}] changed value to: {control.BackColor}");
        }
        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            TimerSettings.Default[control.Tag.ToString()] = control.Value;
            Debug.WriteLine($"[{control.Name}] changed value to: {control.Value}");


        }


    }
}
