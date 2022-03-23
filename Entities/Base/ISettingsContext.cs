using System.Windows.Forms;

namespace AtomPresentationTimer.Entities.Base
{
    internal interface ISettingsContext
    {

        void LoadSettingToControls(Control.ControlCollection controls);

        void SaveSettings();
    }
}