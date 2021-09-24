using hieracyGenerator.forms;
using hieracyGenerator.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using hieracyGenerator.control;

namespace hieracyGenerator.service
{
    class mainService
    {
        private frmMain main;

        public mainService(frmMain main)
        {
            this.main = main;
        }

        public void addPhoto_Click(object sender, EventArgs e)
        {
            string url = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG Files (*.jpg)|*.jpg| PNG Files (*.png)|*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
                url = dialog.FileName;
            bool ok = hasUrl(url);
            if (url != "" && !ok)
            {
                CustomPicBox pic = new CustomPicBox(main.Aside, url, 0);
                pic.Image = Image.FromFile(url);
                main.LastY = 930;
                pic.setLocation(35, main.LastY);
                main.LastY += 160;
                main.Add.Location = new Point(10, main.LastY);
                main.AllPaths.Add(url);
            }
            else if (ok) MessageBox.Show("Image has already been added", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public bool hasUrl(string url)
        {
            List<string> urls = main.AllPaths;
            foreach (string s in urls)
                if (s.Equals(url))
                    return true;
            return false;
        }
        public bool existsInHierarcy(string url)
        {
            string[] hierarcy = main.Paths;
            foreach (string head in hierarcy)
                if (head == url)
                    return true;
            return false;
        }
        public void checkIfComplete()
        {
            string[] images = main.Paths;
            for(int i = 0; i<main.N; i++)
            {
                if (images[i] == "" || images[i] == null)
                {
                    main.Save.Enabled = false;
                    return;
                }
            }
            main.Save.Enabled = true;
        }
        public void save_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < main.N; i++)
                main.Nodes[i].Name = main.Boxes[i].NAME.Text;
            Archive archive = new Archive(main.Username, main.HNAME.Text, main.Nodes, DateTime.Now);
            ControlArchive control = new ControlArchive(10);
            control.add(archive.Username, archive);
            main.Close();
        }
        public void update_Click(object sender, EventArgs e, Archive archive)
        {
            ControlArchive control = new ControlArchive(10);
            control.remove(archive.Username, archive);
            for (int i = 0; i < main.N; i++)
                main.Nodes[i].Name = main.Boxes[i].NAME.Text;
            Archive a = new Archive(main.Username, main.HNAME.Text, main.Nodes, DateTime.Now);
            control.add(archive.Username, a);
            main.Close();
        }
        private bool allEmpty()
        {
            string[] images = main.Paths;
            for (int i = 0; i < main.N; i++)
                if (images[i] != null && images[i] != "")
                    return false;
            return true;
        }
        public void exit_Click(object sender, EventArgs e)
        {
            if (!allEmpty() && main.Save.Enabled == true)
            {
                DialogResult res = MessageBox.Show("All your progress will be lost\nAre you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                    main.Close();
                else return;
            }
            else main.Close();
        }
    }
}
