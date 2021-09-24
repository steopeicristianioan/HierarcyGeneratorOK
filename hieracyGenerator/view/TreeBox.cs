using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using FontAwesome.Sharp;
using hieracyGenerator.forms;

namespace hieracyGenerator.model
{
    class TreeBox : Panel
    {
        private frmMain frm;
        private int id;
        private string url;
        private Panel parent;
        private IconPictureBox box;
        private TextBox name;
        private IconButton remove;

        public int ID { get => this.id; set => this.id = value; }
        public string URL { get => this.url; set => this.url = value; }
        public IconPictureBox Box { get => this.box; set => this.box = value; }
        public TextBox NAME { get => this.name; set => this.name = value; }

        public TreeBox (Panel parent, int id, frmMain frm)
        {
            this.id = id;
            this.parent = parent;
            this.frm = frm;
            this.url = "";

            this.Parent = this.parent;
            this.Size = new Size(180, 200);
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(this.dragEnter);
            this.DragDrop += new DragEventHandler(this.dragDrop);

        }
        public void setLocation(int x, int y)
        {
            this.Location = new Point(x, y);
        }

        public void load()
        {
            loadTextBox();
            loadPicBox();
            loadRemove();
            
        }
        private void loadTextBox()
        {
            name = new TextBox();
            name.Width = 100;
            name.Font = new Font("Consolas", 13, FontStyle.Bold);
            name.BackColor = ColorTranslator.FromHtml("#EEFBFB");
            name.TextAlign = HorizontalAlignment.Center;
            name.BorderStyle = BorderStyle.None;
            name.ForeColor = ColorTranslator.FromHtml("#203647");
            name.Text = "NAME";
            name.Click += new EventHandler(this.textBox_Click);
            if(url == "")
                useful.Useful.placeInParent(name, this, 37, 170);
            else useful.Useful.placeInParent(name, this, 25, 170);
            name.TabStop = false;

            name.TextChanged += new EventHandler(this.textbox_TextChanged);
        }
        private void loadPicBox()
        {
            box = new IconPictureBox();
            box.Parent = this;
            box.Size = new Size(150, 150);
            box.IconChar = IconChar.Portrait;
            box.IconColor = ColorTranslator.FromHtml("#4DA8DA");
            useful.Useful.placeInParent(box, this, 15, 10);

        }
        private void loadRemove()
        {
            remove = new IconButton();
            useful.Useful.placeInParent(remove, this, 130, 170);
            useful.Useful.setNormalIconBUtton(ref remove);
            remove.ImageAlign = ContentAlignment.MiddleCenter;
            remove.Size = new Size(35, 25);
            remove.IconSize = 30;
            remove.IconColor = ColorTranslator.FromHtml("#007CC7");
            remove.IconChar = IconChar.Trash;
            remove.Click += new EventHandler(this.remove_Click);
            if(url=="")
                remove.Visible = false;
            

            ToolTip tip = new ToolTip();
            tip.SetToolTip(remove, "Remove picture");
        }

        private void textBox_Click(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;
            if (text.Text.Equals("NAME"))
                text.Text = string.Empty;
        }
        private void textbox_TextChanged(object sender, EventArgs e)
        {
            TextBox box = (TextBox)name;
            if (frm.Nodes[this.id].Name != box.Text && frm.Status == 1)
                frm.Save.Enabled = true;
            else if (frm.Status == 1) frm.Save.Enabled = false;
        }
        private void dragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Move;
            else e.Effect = DragDropEffects.None;
        }
        private void dragDrop(object sender, DragEventArgs e)
        {
            url = (string)e.Data.GetData(DataFormats.Text);
            if (!frm.Service.existsInHierarcy(url))
            {
                this.box.IconChar = IconChar.None;
                PictureBox picture = (PictureBox)box;
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                picture.Image = Image.FromFile(url);
                frm.Paths[this.id] = url;
                useful.Useful.placeInParent(name, this, 25, 170);
                remove.Visible = true;
                frm.Service.checkIfComplete();
                frm.Nodes[this.id] = new HeadData(url, "");
            }
            else
            {
                MessageBox.Show("The image is being used for other person", "Be careful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void remove_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)box;
            pic.Image = null;
            box.IconChar = IconChar.Portrait;
            frm.Paths[this.id] = "";
            remove.Visible = false;
            useful.Useful.placeInParent(name, this, 37, 170);
            frm.Service.checkIfComplete();
            frm.Nodes[this.id].URL = "";
        }
    }
}
