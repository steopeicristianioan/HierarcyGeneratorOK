using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using FontAwesome.Sharp;

namespace hieracyGenerator.useful
{
    static class Useful
    {
        public static void placeInParent(Control control, Control parent, int x, int y)
        {
            control.Parent = parent;
            control.Location = new Point(x, y);
            
        }
        public static void customiseIconButton(ref IconButton button, string foreColor, string backColor, Size size)
        {
            button.ForeColor = button.IconColor = ColorTranslator.FromHtml(foreColor);
            button.BackColor = ColorTranslator.FromHtml(backColor);
            button.Size = size;
        }
        public static void setNormalIconBUtton(ref IconButton button)
        {
            button.TabStop = false;
            button.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            button.TextImageRelation = TextImageRelation.ImageBeforeText;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseDownBackColor = button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button.ImageAlign = button.TextAlign = ContentAlignment.MiddleLeft;
            button.BackColor = Color.Transparent;

        }
    }
}
