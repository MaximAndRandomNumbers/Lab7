using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorPick
{
    public partial class ColorPicker : Form
    {
        public ColorPicker()
        {
            InitializeComponent();
            btnGetColor.BackColor = Color.FromArgb(redBar.Value, greenBar.Value, blueBar.Value);
        }
  
        private String GetColor()
        {
            return "#"+ String.Format("{0:X}", btnGetColor.BackColor.ToArgb()).Substring(2);
        }
        private void btnGetColor_Hover(object sender, EventArgs e)
        {
            colorTip.Show(GetColor(), btnGetColor);
        }

        private void barScroll(object sender, EventArgs e)
        {
            btnGetColor.BackColor = Color.FromArgb(redBar.Value, greenBar.Value, blueBar.Value);
            Clipboard.SetText(GetColor());
        }
    }
}
