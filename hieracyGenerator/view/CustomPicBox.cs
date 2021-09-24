using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace hieracyGenerator.model
{
    class CustomPicBox : PictureBox
    {
        private Panel parent;
        private string url;
        public string URL { get => this.url; set => this.url = value; }
        private int k;

        public CustomPicBox(Panel parent, string url, int k)
        {
            this.parent = parent;
            this.url = url;
            this.k = k;
            this.Parent = parent;

            this.Size = new Size(250, 150);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            if(k==1)
                this.Image = Image.FromFile(Application.StartupPath + "\\images\\" + url + ".jpg");
 

            this.MouseDown += new MouseEventHandler(this.pic_MouseDown);
        }
        public void setLocation(int x, int y)
        {
            this.Location = new Point(x, y);
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && this.Image != null)
            {
                if (k == 1)
                    this.DoDragDrop(Application.StartupPath + "\\images\\" + url + ".jpg", DragDropEffects.Move);
                else this.DoDragDrop(this.url, DragDropEffects.Move);
            }
        }

    }
}
