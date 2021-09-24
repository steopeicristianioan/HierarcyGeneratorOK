using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using hieracyGenerator.useful;
using FontAwesome.Sharp;
using hieracyGenerator.model;
using hieracyGenerator.service;

namespace hieracyGenerator.forms
{
    class frmMain : Form
    {
        //backend data
        #region
        private mainService service;
        private int n;
        private string name;
        private Ierarhie<int> tree = new Ierarhie<int>(0);
        private Panel h;
        private List<string> allPaths;
        private string[] paths;
        private int lastY;
        private HeadData[] nodes;
        private TreeBox[] treeBox;
        private int status;
        private Archive archive;

        public mainService Service { get => this.service; }
        public int N { get => this.n; set => this.n = value; }
        public string Username { get => this.name; set => this.name = value; }
        public List<string> AllPaths { get => this.allPaths; set => this.allPaths = value; }
        public string[] Paths { get => this.paths; set => this.paths = value; }
        public int LastY { get => this.lastY; set => this.lastY = value; }
        public HeadData[] Nodes { get => this.nodes; set => this.nodes = value; }
        public TreeBox[] Boxes { get => this.treeBox; set => this.treeBox = value; }
        public int Status { get => this.status; set => this.status = value; }
        public Archive ARCHIVE { get => this.archive; set => this.archive = value; }
        #endregion

        //header-related controls
        #region
        private Panel header;
        private Label messageH;
        private IconButton save;
        private IconButton exit;
        private TextBox hName;

        public Panel Header { get => this.header; set => this.header = value; }
        public IconButton Save { get => this.save; set => this.save = value; }
        public TextBox HNAME { get => this.hName; set => this.hName = value; }
        #endregion

        //aside-related controls
        #region
        private Panel aside;
        public Panel Aside { get => this.aside; set => this.aside = value; }
        private IconPictureBox logo;
        private Label message;
        private IconButton add;
        public IconButton Add { get => this.add; set => this.add = value; }
        private int y = 300;
        #endregion

        //main-related controls
        #region
        private Panel main;
        public Panel Main { get => this.main; set => this.main = value; }
        #endregion

        public frmMain(string name, int n, int status, Archive archive)
        {
            this.n = n;
            this.name = name;
            this.status = status;
            this.archive = archive;

            paths = new string[n];
            nodes = new HeadData[n];
            treeBox = new TreeBox[n];

            this.BackColor = ColorTranslator.FromHtml("#007CC7");
            this.WindowState = FormWindowState.Maximized;

            service = new mainService(this);
            allPaths = new List<string>();

            setHeader();
            setAside();
            setMain();
        }

        //header-related methods
        #region
        private void loadWelcome()
        {
            messageH = new Label();
            messageH.Parent = header;
            messageH.Size = new Size(500, 80);
            messageH.Location = new Point(30, 5);
            messageH.ForeColor = ColorTranslator.FromHtml("#4DA8DA");
            messageH.Text = "Customise your own hierarcy!";
            messageH.Font = new Font("Consolas", 15, FontStyle.Bold);
            messageH.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void loadSave()
        {
            save = new IconButton();
            save.Enabled = false;
            useful.Useful.placeInParent(save, header, 1200, 1);
            useful.Useful.setNormalIconBUtton(ref save);
            useful.Useful.customiseIconButton(ref save, "#4DA8DA", "", new Size(250, 98));
            save.IconChar = IconChar.FileUpload;
            save.Font = new Font("Consolas", 10, FontStyle.Bold);
            save.Text = "Upload hierarcy";
            if (status == 1)
                save.Text = "Update hierarcy";
            if (status == 0)
                save.Click += new EventHandler(service.save_Click);
            else save.Click += delegate (object sender2, EventArgs e2) { service.update_Click(sender2, e2, this.archive); };
        }
        private void loadExit()
        {
            exit = new IconButton();
            Useful.placeInParent(exit, header, 1475, 1);
            Useful.setNormalIconBUtton(ref exit);
            Useful.customiseIconButton(ref exit, "#4DA8DA", "", new Size(100, 98));
            exit.IconChar = IconChar.TimesCircle;

            ToolTip tip = new ToolTip();
            tip.SetToolTip(exit, "Exit");

            exit.Click += new EventHandler(this.service.exit_Click);
        }
        private void loadText()
        {
            hName = new TextBox();
            Useful.placeInParent(hName, header, 625, 35);
            hName.Font = new Font("Consolas", 20, FontStyle.Bold);
            hName.BackColor = ColorTranslator.FromHtml("#EEFBFB");
            hName.ForeColor = ColorTranslator.FromHtml("#12232E");
            hName.Width = 350;
            hName.Text = "HIERARCY NAME";
            hName.BorderStyle = BorderStyle.None;
            hName.TabStop = false;
            hName.TextAlign = HorizontalAlignment.Center;

            hName.Click += new EventHandler(this.text_Click);
            hName.TextChanged += new EventHandler(this.text_TextChanged);
        }
        private void text_Click(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;
            if (text.Text == "HIERARCY NAME")
                text.Text = "";
        }
        private void setHeader()
        {
            this.header = new Panel();
            Useful.placeInParent(header, this, 0, 0);
            header.Size = new Size(this.Width, 100);
            header.Dock = DockStyle.Top;
            header.BackColor = ColorTranslator.FromHtml("#12232E");

            loadWelcome();
            loadText();
            loadSave();
            loadExit();
        }
        private void text_TextChanged(object sender, EventArgs e)
        {
            if (status == 0)
                return;
            TextBox box = (TextBox)sender;
            if (box.Text != archive.Name)
                save.Enabled = true;
            else save.Enabled = false;
        }
        #endregion

        //aside-relted methods
        #region
        private void loadLogo()
        {
            logo = new IconPictureBox();
            Useful.placeInParent(logo, aside, 90, 10);
            logo.IconColor = ColorTranslator.FromHtml("#4DA8DA");
            logo.IconChar = IconChar.Sitemap;
            logo.Size = new Size(150, 150); 
        }
        private void loadMessage()
        {
            message = new Label();
            Useful.placeInParent(message, aside, 25, 175);
            message.Size = new Size(290, 100);
            message.Text = "Choose listed images\nOr upload your own images";
            message.Font = new Font("Cosolas", 10, FontStyle.Bold);
            message.TextAlign = ContentAlignment.MiddleCenter;
            message.ForeColor = ColorTranslator.FromHtml("#4DA8DA");
        }
        private void loadImages()
        {
            int x = 35;
            for(int i = 1; i<=10; i++)
            {
                CustomPicBox box = new CustomPicBox(aside, ("p" + i.ToString()), 1);
                box.setLocation(x,y);
                y += 160;
                allPaths.Add("p" + i.ToString());
            }
            lastY = y;
        }
        private void loadAdd()
        {
            add = new IconButton();
            add.Size = new Size(300, 150);
            Useful.placeInParent(add, aside, 10, y);
            Useful.setNormalIconBUtton(ref add);
            add.IconChar = IconChar.PlusCircle;
            add.IconColor = add.ForeColor = ColorTranslator.FromHtml("#4DA8DA");
            add.ImageAlign = add.TextAlign = ContentAlignment.MiddleCenter;
            add.Text = "Add photo";
            add.Font = new Font("Consolas", 15, FontStyle.Bold);
            add.IconSize = 100;

            add.Click += new EventHandler(service.addPhoto_Click);
        }
        private void setAside()
        {
            aside = new Panel();
            Useful.placeInParent(aside, this, 0, 0);
            aside.AutoScroll = true;
            aside.Size = new Size(350, this.Height);
            aside.Dock = DockStyle.Left;
            aside.BackColor = header.BackColor;
            aside.BorderStyle = BorderStyle.FixedSingle;
            aside.AllowDrop = true;
            aside.DragEnter += new DragEventHandler(this.panel_dragEnter);
            aside.DragDrop += new DragEventHandler(this.panel_dragDrop);
            

            loadLogo();
            loadMessage();
            loadImages();
            loadAdd();
        }
        #endregion

        //main-related methods
        #region
        private void loadTree()
        {
            Coada<int> coada = new Coada<int>(0);
            int ct = 0, value = 1;
            while (!coada.isEmpty() && value < n)
            {
                if (ct < 2)
                {
                    coada.push(value);
                    tree.add(coada.top(), value);
                    ct++;
                    value++;
                }
                else if (ct == 2)
                {
                    ct = 0;
                    coada.pop();
                }
            }
            tree.print();
        }
        private void showTree(TreeNode<int> root, int y, ref int x, bool show)
        {
            if (root == null)
                return;
            y += 10;
            showTree(root.Left, y, ref x, show);
            x += 100;
            if (show)
            {
                int newY = 0;
                for (int i = 10; i < y; i++)
                    newY += 21;
                TreeBox box = new TreeBox(h, root.Data, this);
                if (status == 1)
                    box.URL = archive.Images[root.Data].URL;
                box.setLocation(x, newY);
                box.load();
                treeBox[root.Data] = box;
            }
            showTree(root.Right, y, ref x, show);
        }
        private void setH()
        {
            h = new Panel();
            h.Parent = this.main;
            h.DragEnter += new DragEventHandler(this.panel_dragEnter);
            //h.AutoScroll = true;
            //h.BorderStyle = BorderStyle.FixedSingle;
        }
        private void panel_dragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Move;
            else e.Effect = DragDropEffects.None;
        }
        private void panel_dragDrop(object sender, DragEventArgs e)
        {
            //MessageBox.Show("DROP");
        }
        private void setMain()
        {
            
            main = new Panel();
            Useful.placeInParent(main, this, 350, 100);
            main.AutoScroll = true;
            Rectangle rectangle = Screen.PrimaryScreen.WorkingArea;
            main.Size = new Size(rectangle.Width - 350, rectangle.Height);
            //main.Dock = DockStyle.Fill;
            main.BackColor = ColorTranslator.FromHtml("#203647");
            main.AllowDrop = true;
            main.DragEnter += new DragEventHandler(this.panel_dragEnter);
            
            loadTree();
            int x = -100;
            showTree(tree.Root, 0, ref x, false);
            setH();
            h.Size = new Size(x + 180, main.Height - 30);
            x = -100;
            showTree(tree.Root, 0, ref x, true);
            h.Location = new Point((main.Width - h.Width) / 2, 0);
        }
        public void loadArchieve(Archive archive)
        {
            hName.Text = archive.Name;
            HeadData[] heads = archive.Images;
            nodes = heads;
            for(int i = 0; i<archive.Size; i++)
            {
                paths[i] = heads[i].URL;
                treeBox[i].Box.IconChar = IconChar.None;
                PictureBox picture = (PictureBox)treeBox[i].Box;
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                picture.Image = Image.FromFile(heads[i].URL);
                treeBox[i].NAME.Text = heads[i].Name;
            }
        }
        #endregion

        public void printPaths()
        {
            string k = string.Empty;
            for (int i = 0; i < n; i++)
                k += paths[i] + "\n";
            MessageBox.Show(k);
        }
        public void printAllPaths()
        {
            string k = string.Empty;
            foreach (string path in allPaths)
                k += path + "\n";
            MessageBox.Show(k);
        }
    }
}
