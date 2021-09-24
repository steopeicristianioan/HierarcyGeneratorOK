using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using hieracyGenerator.control;
using hieracyGenerator.useful;
using FontAwesome.Sharp;
using hieracyGenerator.model;

namespace hieracyGenerator.forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private TextBox text;
        private TextBox password;
        private IconButton ok;
        private Label message;

        private void Login_Load(object sender, EventArgs e)
        {
            loadLogin();
        }

        public void loadLogin()
        {
            this.Controls.Clear();
            this.Text = "";
            this.MinimumSize = this.MaximumSize = new Size(400, 400);
            this.BackColor = ColorTranslator.FromHtml("#12232E");
            loadButton();
            loadTextBoxes();
            loadMessage();
            this.CenterToScreen();
        }
        private void loadButton()
        {
            ok = new IconButton();
            Useful.placeInParent(ok, this, 110, 225);
            Useful.setNormalIconBUtton(ref ok);
            Useful.customiseIconButton(ref ok, "#4DA8DA", "", new Size(150, 50));
            ok.Font = new Font("Consolas", 14, FontStyle.Bold);
            ok.Text = "LOGIN";
            ok.IconChar = IconChar.Check;

            ok.MouseHover += new EventHandler(this.button_Hover);
            ok.MouseLeave += new EventHandler(this.button_Leave);
            ok.Click += new EventHandler(this.button_Click);
        }
        private void button_Hover(object sender, EventArgs e)
        {
            IconButton b = (IconButton)sender;
            b.ForeColor = b.IconColor = ColorTranslator.FromHtml("#EEFBFB");
        }
        private void button_Leave(object sender, EventArgs e)
        {
            IconButton b = (IconButton)sender;
            b.ForeColor = b.IconColor = ColorTranslator.FromHtml("#4DA8DA");
        }
        private void button_Click(object sender, EventArgs e)
        {
            ControlUser control = new ControlUser(10);
            ChainedHashtable<string, User> users = control.AllUsers;
            int p = users.findPosition(text.Text, false);
            if(p == -1)
            {
                MessageBox.Show("Invalid username", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            User user = users.get(text.Text);
            if(user.Password != password.Text)
            {
                MessageBox.Show("Invalid password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            openMenu();
        }
        private void loadTextBoxes()
        {
            text = new TextBox();
            password = new TextBox();
            Useful.placeInParent(text, this, 100, 120);
            Useful.placeInParent(password, this, 100, 175);
            text.Font = password.Font = ok.Font;
            text.Width = password.Width = 200;
            text.BorderStyle = password.BorderStyle = BorderStyle.None;
            text.TabStop = password.TabStop = false;
            text.BackColor = password.BackColor = ok.ForeColor;
            text.ForeColor = password.ForeColor = ok.BackColor;
            text.TextAlign = password.TextAlign = HorizontalAlignment.Center;

            text.Text = "Username";
            password.Text = "Password";

            text.Click += new EventHandler(this.textBox_Click);
            password.Click += new EventHandler(this.textBox_Click);
        }
        private void textBox_Click(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Equals(text) && t.Text == "Username")
                t.Text = string.Empty;
            else if(t.Equals(password) && t.Text == "Password")
            {
                t.Text = string.Empty;
                t.PasswordChar = '*';
            }
        }
        private void loadMessage()
        {
            message = new Label();
            Useful.placeInParent(message, this, 25, 10);
            message.Size = new Size(350, 100);
            message.TextAlign = ContentAlignment.MiddleCenter;
            message.Font = ok.Font;
            message.Text = "Fill in your data and start having fun!";
            message.ForeColor = ok.ForeColor;
        }
        private void openMenu()
        {
            this.Controls.Clear();
            this.MaximumSize = new Size(3000, 3000);
            this.MinimumSize = this.Size = new Size(1000, 750);
            this.SetDesktopLocation(450, 175);
            MENU menu = new MENU();
            menu.Username = text.Text;
            menu.LOGIN = this;
            menu.TopLevel = false;
            menu.Parent = this;
            menu.Dock = DockStyle.Fill;
            menu.FormBorderStyle = FormBorderStyle.None;
            menu.Show();
        }
    }
}
