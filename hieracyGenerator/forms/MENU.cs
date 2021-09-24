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
using hieracyGenerator.useful;
using hieracyGenerator.control;

namespace hieracyGenerator.forms
{
    public partial class MENU : Form
    {
        public MENU()
        {
            InitializeComponent();
        }

        private Login login;
        private string username;
        private Panel main;
        private Panel aside;
        private Panel header;

        public Panel Main { get => this.main; set => this.main = value; }
        public Panel Aside { get => this.aside; set => this.aside = value; }
        public Panel Header { get => this.header; set => this.header = value; }
        public string Username { get => this.username; set => this.username = value; }
        public Login LOGIN { get => this.login; set => this.login = value; }

        private Label message;

        private IconPictureBox logo;
        private IconButton create;
        private IconButton profile;
        private IconButton logout;

        private PictureBox wait;
        private Label mainMessage;

        private void MENU_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size = new Size(1000, 750);
           

            loadHeader();
            loadAside();
            loadMain();
        }

        //header-related methods
        #region
        private void loadHeader()
        {
            header = new Panel();
            Useful.placeInParent(Header, this, 200, 0);
            header.Size = new Size(550, 100);
            header.Dock = DockStyle.Top;
            header.BackColor = ColorTranslator.FromHtml("#12232E");

            loadMessage();
        }
        private void loadMessage()
        {
            message = new Label();
            Useful.placeInParent(message, header, 250, 5);
            message.Size = new Size(500, 90);
            message.Font = new Font("Consolas", 14, FontStyle.Bold);
            message.ForeColor = ColorTranslator.FromHtml("#4DA8DA");
            message.TextAlign = ContentAlignment.MiddleCenter;
            message.Text = "Welcome to your profile, " + this.username + "!";
            message.Anchor = AnchorStyles.None;
        }
        #endregion

        //aside-related methods
        #region
        private void loadAside()
        {
            aside = new Panel();
            Useful.placeInParent(aside, this, 0, 0);
            aside.Dock = DockStyle.Left;
            aside.Width = 290;
            aside.BackColor = header.BackColor;

            loadLogo();
            loadCreate();
            loadProfile();
            loadLogOut();
            create.Anchor = profile.Anchor = logout.Anchor = AnchorStyles.None;
        }
        private void loadLogo()
        {
            logo = new IconPictureBox();
            Useful.placeInParent(logo, aside, 25, 10);
            logo.IconColor = ColorTranslator.FromHtml("#4DA8DA");
            logo.IconChar = IconChar.Sitemap;
            logo.Size = new Size(240, 200);
        }
        private void loadCreate()
        {
            create = new IconButton();
            Useful.placeInParent(create, aside, 5, 275);
            Useful.customiseIconButton(ref create, "#4DA8DA", "", new Size(280, 75));
            Useful.setNormalIconBUtton(ref create);
            create.IconChar = IconChar.PlusSquare;
            create.Text = "New hierarcy";
            create.Font = new Font("Cosolas", 12, FontStyle.Bold);

            create.MouseHover += new EventHandler(this.iconButton_Hover);
            create.MouseLeave += new EventHandler(this.iconButton_Leave);
            create.Click += new EventHandler(this.create_Click);
        }
        private void loadProfile()
        {
            profile = new IconButton();
            Useful.placeInParent(profile, aside, 5, 355);
            Useful.customiseIconButton(ref profile, "#4DA8DA", "", create.Size);
            Useful.setNormalIconBUtton(ref profile);
            profile.IconChar = IconChar.FileArchive;
            profile.Text = "My archieves";
            profile.Font = create.Font;

            profile.MouseHover += new EventHandler(this.iconButton_Hover);
            profile.MouseLeave += new EventHandler(this.iconButton_Leave);
            profile.Click += new EventHandler(this.profile_Click);
        }
        private void loadLogOut()
        {
            logout = new IconButton();
            Useful.placeInParent(logout, aside, 5, 435);
            Useful.customiseIconButton(ref logout, "#4DA8DA", "", create.Size);
            Useful.setNormalIconBUtton(ref logout);
            logout.IconChar = IconChar.SignOutAlt;
            logout.Text = "Logout";
            logout.Font = create.Font;

            logout.MouseHover += new EventHandler(this.iconButton_Hover);
            logout.MouseLeave += new EventHandler(this.iconButton_Leave);
            logout.Click += new EventHandler(this.logout_Click);
        }
        private void iconButton_Hover(object sender, EventArgs e)
        {
            IconButton button = (IconButton)sender;
            button.ForeColor = button.IconColor = ColorTranslator.FromHtml("#EEFBFB");
        }
        private void iconButton_Leave(object sender, EventArgs e)
        {
            IconButton button = (IconButton)sender;
            button.ForeColor = button.IconColor = ColorTranslator.FromHtml("#4DA8DA");
        }
        private void create_Click(object sender, EventArgs e)
        {
            main.Controls.Clear();
            message.Text = "Welcome to your profile, " + this.username + "!";
            Form1 form = new Form1();
            form.TopLevel = false;
            form.Username = "cristi";
            Useful.placeInParent(form, main, 300, 100);
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Show();
        }
        private void profile_Click(object sender, EventArgs e)
        {
            main.Controls.Clear();
            message.Text = "These are your archieves!";
            ControlArchive control = new ControlArchive(10);
            control.loadHistory(this.username, main);
        }
        private void logout_Click(object sender, EventArgs e)
        {
            this.login.loadLogin();
            //this.Close();
        }
        #endregion

        //main-related methods
        #region
        private void loadMain()
        {
            main = new Panel();
            main.AutoScroll = true;
            Useful.placeInParent(main, this, 290, 100);
            main.Size = new Size(this.Width - 290, this.Height - 100);
            main.BackColor = header.BackColor;
           
            main.Dock = DockStyle.Fill;

            loadWait();
            loadMainMessage();
        }
        private void loadWait()
        {
            wait = new PictureBox();
            Useful.placeInParent(wait, main, 500, 150);
            wait.Size = new Size(400, 300);
            wait.Image = Image.FromFile(Application.StartupPath + "\\images\\hourglass1.gif");
            wait.Anchor = AnchorStyles.None;
        }
        private void loadMainMessage()
        {
            mainMessage = new Label();
            mainMessage.Size = new Size(400, 150);
            Useful.placeInParent(mainMessage, main, 500, 455);
            mainMessage.Font = message.Font;
            mainMessage.ForeColor = message.ForeColor;
            mainMessage.TextAlign = ContentAlignment.MiddleCenter;
            mainMessage.Text = "Start doing things and have fun!\nWhat are you waiting for?";
            mainMessage.Anchor = AnchorStyles.None;
        }
        #endregion

    }
}
