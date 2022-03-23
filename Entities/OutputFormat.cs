using System;
using System.Collections.Generic;
using System.Text;

namespace AtomPresentationTimer.Entities
{
    public class OutputFormat
    {
        public string Text { get; set; }
    
        public System.Drawing.Color Color { get; set; }

        public OutputFormat(string text, System.Drawing.Color color)
        {
            Text = text;         
            Color = color;
        }
    }
}
