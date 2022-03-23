using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AtomPresentationTimer.UserControls
{
    public partial class FontComboBox : ComboBox
    {
        public FontComboBox()
        {
            InitializeComponent();
        }

        public FontComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            var fonts = new InstalledFontCollection();
            Name = "fontBox";
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DrawMode = DrawMode.OwnerDrawVariable;
            DataSource = fonts.Families.Select(FromFamily).ToArray();
            DisplayMember = "Name";
            ValueMember = "Name";

            this.DrawItem += OnDrawItem;
            this.MeasureItem += OnMeasureItem;
            this.SelectedIndexChanged += OnSelectedIndexChanged;
        }

        private Font FromFamily(FontFamily family)
        {
            var styles = Enum.GetValues(typeof(FontStyle)) as FontStyle[];
            var style = styles.First(family.IsStyleAvailable);

            return new Font(family, 10, style);
        }

        private void OnDrawItem(object sender, DrawItemEventArgs e)
        {
            var cb = sender as ComboBox;
            var fonts = cb.DataSource as Font[];
            var font = fonts[e.Index];

            e.DrawBackground();
            e.Graphics.DrawString(FormatFontName(font), font, Brushes.Black, e.Bounds);
            e.DrawFocusRectangle();
        }

        private void OnMeasureItem(object sender, MeasureItemEventArgs e)
        {
            var cb = sender as ComboBox;
            var fonts = cb.DataSource as Font[];
            var selectedFont = fonts[e.Index];

            var size = e.Graphics.MeasureString(FormatFontName(selectedFont), selectedFont);
            e.ItemHeight = (int)Math.Ceiling(size.Height);
            e.ItemWidth = (int)Math.Ceiling(size.Width);
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb.SelectedIndex > 0)
            {
                var fonts = cb.DataSource as Font[];
                cb.Font = fonts[cb.SelectedIndex];
            }
        }

        string FormatFontName(Font font)
        {
            return font.Name;
        }
    }
}
