using hieracyGenerator.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using hieracyGenerator.view;

namespace hieracyGenerator.control
{
    class ControlArchive : Icontrol<Archive>
    {
        private ChainedHashtable<string, Archive> allArchives;
        private string url = Application.StartupPath + "\\archives.txt";
        private int size;

        public ControlArchive(int size)
        {
            this.size = size;
            read();
        }

        public void read()
        {
            allArchives = new ChainedHashtable<string, Archive>(this.size);
            StreamReader stream = new StreamReader(url);
            string line = string.Empty;
            while ((line = stream.ReadLine()) != null)
            {
                string[] words = line.Split('|');
                string[] images = words[2].Split('~');
                string[] date = words[3].Split('.');
                int[] dates = new int[6];
                for (int i = 0; i < 6; i++)
                    dates[i] = int.Parse(date[i]);
                HeadData[] data = new HeadData[images.Length];
                for (int i = 0; i < images.Length; i++)
                {
                    string[] p = images[i].Split('`');
                    HeadData head = new HeadData(p[0], p[1]);
                    data[i] = head;
                }
                DateTime time = new DateTime(dates[0], dates[1], dates[2], dates[3], dates[4], dates[5]);
                Archive a = new Archive(words[0], words[1], data, time);
                allArchives.put(a.Username, a);
            }
            stream.Close();
        }
        public void write()
        {
            StreamWriter stream = new StreamWriter(url);
            string res = string.Empty;
            Lista<Archive> ar = allArchives.allValues();
            Node<Archive> head = ar.First;
            while (head != null)
            {
                res += head.Data.ToString() + "\n";
                head = head.Next;
            }
            stream.Write(res);
            stream.Close();
        }
        public void add(string username, Archive archive)
        {
            allArchives.put(username, archive);
            write();
        }
        public void remove(string username, Archive archive)
        {
            allArchives.remove(username, archive);
            write();
        }
        public void loadHistory(string username, Panel parent)
        {
            parent.Controls.Clear();
            Lista<Archive> history = allArchives.getAllValues(username);
            Node<Archive> head = history.First;
            int x = 330, y = 125, ct = 0;
            while(head != null)
            {
                if(ct == 2)
                {
                    y += 310;
                    x = 330;
                    ct = 0;
                }
                ArchiveCard card = new ArchiveCard(parent, head.Data);
                card.setLocation(x, y);
                card.load();
                ct++;
                x += 310;
                head = head.Next;
            }
        }
    }
}
