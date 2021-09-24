using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using hieracyGenerator.forms;
using hieracyGenerator.useful;

namespace hieracyGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private TextBox number;
        private IconButton ok;
        private string username;
        
        public string Username { get => this.username; set => this.username = value; }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MinimumSize = Size;
            this.BackColor = ColorTranslator.FromHtml("#12232E");
            loadTextBox();
            loadButton();
        }

        private void loadTextBox()
        {
            number = new TextBox();
            Useful.placeInParent(number, this, 325, 100);
            number.Font = new Font("Consolas", 13, FontStyle.Bold);
            number.TextAlign = HorizontalAlignment.Center;
            number.Width = 200;
            number.Text = "Nr. of departments";
            number.Anchor = AnchorStyles.None;

            number.Click += new EventHandler(this.number_Click);
        }
        private void loadButton()
        {
            ok = new IconButton();
            Useful.placeInParent(ok, this, 375, 150);
            Useful.customiseIconButton(ref ok, "#4DA8DA", "#203647", new Size(100, 60));
            ok.Font = new Font("Consolas", 13, FontStyle.Bold);
            ok.IconChar = IconChar.CheckCircle;
            ok.Text = "OK";
            Useful.setNormalIconBUtton(ref ok);
            ok.Anchor = AnchorStyles.None;

            ok.MouseHover += new EventHandler(this.ok_Hover);
            ok.MouseLeave += new EventHandler(this.ok_Leave);
            ok.Click += new EventHandler(this.ok_Click);
        }
        private void ok_Hover(object sender, EventArgs e)
        {
            IconButton button = (IconButton)sender;
            button.ForeColor = button.IconColor = ColorTranslator.FromHtml("#EEFBFB");
        }
        private void ok_Leave(object sender, EventArgs e)
        {
            IconButton button = (IconButton)sender;
            button.ForeColor = button.IconColor = ColorTranslator.FromHtml("#4DA8DA");
        }
        private void number_Click(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;
            if(text.Equals(number) && text.Text == "Nr. of departments")
            {
                text.Text = string.Empty;
            }
        }
        private void ok_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(number.Text, out int r) || r < 1)
                return;
            else
            {
                openForm(out frmMain main);
            }
        }
        private void openForm(out frmMain form)
        {
            form = new frmMain(this.username, int.Parse(number.Text), 0, null);
            form.Size = this.Size;
            form.Main.AutoScroll = true;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Show();

            number.Text = "Nr. of departments";
        }
    }
}
