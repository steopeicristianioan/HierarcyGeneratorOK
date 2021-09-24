using hieracyGenerator.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace hieracyGenerator.control
{
    class ControlUser : Icontrol<User>
    {
        private ChainedHashtable<string, User> allUsers;
        private string url = Application.StartupPath + "\\users.txt";
        private int size;

        public ChainedHashtable<string, User> AllUsers { get => this.allUsers; }
        public ControlUser(int size)
        {
            this.size = size;
            read();
        }

        public void read()
        {
            allUsers = new ChainedHashtable<string, User>(this.size);
            StreamReader stream = new StreamReader(url);
            string line = string.Empty;
            while((line = stream.ReadLine()) != null)
            {
                string[] words = line.Split('|');
                User user = new User(words[0], words[1]);
                allUsers.put(user.Username, user);
            }
            stream.Close();
        }

        public void write()
        {
            StreamWriter stream = new StreamWriter(url);
            string res = string.Empty;
            Lista<User> users = allUsers.allValues();
            Node<User> head = users.First;
            while(head != null)
            {
                res += head.Data.ToString() + "\n";
                head = head.Next;
            }
            stream.Write(res);
            stream.Close();
        } 
    }
}
