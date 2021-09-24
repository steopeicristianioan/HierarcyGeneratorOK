using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using hieracyGenerator.model;
using hieracyGenerator.forms;
using FontAwesome.Sharp;
using hieracyGenerator.control;

namespace hieracyGenerator.view
{
    class ArchiveCard : Panel
    {
        private Panel parent;
        private Archive archive;

        public ArchiveCard(Panel parent, Archive archive)
        {
            this.parent = parent;
            this.Parent = this.parent;
            this.archive = archive;

            this.Size = new Size(300, 300);
            this.BackColor = ColorTranslator.FromHtml("#4DA8DA");
        }
        public void setLocation(int x, int y)
        {
            this.Location = new Point(x, y);
        }

        private IconButton display;
        private Label nametime;

        private void loadButton()
        {
            display = new IconButton();
            useful.Useful.placeInParent(display, this, 50, 200);
            useful.Useful.setNormalIconBUtton(ref display);
            useful.Useful.customiseIconButton(ref display, "#12232E", "", new Size(200, 90));
            display.Font = new Font("Consolas", 13, FontStyle.Bold);
            display.IconChar = IconChar.Eye;
            display.Text = "Display";

            display.MouseHover += new EventHandler(this.button_Hover);
            display.MouseLeave += new EventHandler(this.button_Leave);
            display.Click += new EventHandler(this.button_Click);
        }
        private void button_Hover(object sender, EventArgs e)
        {
            display.ForeColor = display.IconColor = ColorTranslator.FromHtml("#EEFBFB");
        }
        private void button_Leave(object sender, EventArgs e)
        {
            display.ForeColor = display.IconColor = ColorTranslator.FromHtml("#12232E");
        }
        private void button_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain(archive.Username, archive.Size, 1, this.archive);
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.loadArchieve(this.archive);
            frm.ShowDialog();

            ControlArchive control = new ControlArchive(10);
            control.loadHistory(this.archive.Username, this.parent);
        }
        private void loadNameTime()
        {
            nametime = new Label();
            useful.Useful.placeInParent(nametime, this, 10, 5);
            nametime.Size = new Size(280, 190);
            nametime.Font = new Font("Consolas", 10, FontStyle.Bold);
            nametime.ForeColor = ColorTranslator.FromHtml("#12232E");
            nametime.TextAlign = ContentAlignment.MiddleCenter;
            nametime.Text = "Name: " + archive.Name + "\n" + "Date:\n" + archive.Time.ToString("f");
        }
        public void load()
        {
            loadNameTime();
            loadButton();
        }
    }
}
