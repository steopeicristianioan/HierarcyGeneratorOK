using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hieracyGenerator.model
{
    class Archive : IComparable<Archive>
    {
        private string username;
        private HeadData[] images;
        private DateTime time;
        private string name;

        public string Username { get => this.username; set => this.username = value; }
        public HeadData[] Images { get => this.images; }
        public DateTime Time { get => this.time; set => this.time = value; }
        public string Name { get => this.name; set => this.name = value; }
        public int Size { get => this.images.Length; }

        public Archive(string username, string name, HeadData[] images, DateTime time)
        {
            this.username = username;
            this.name = name;
            this.images = images;
            this.time = time;
        }

        public override string ToString()
        {
            string img = string.Empty;
            for (int i = 0; i < images.Length; i++)
                img += images[i].ToString() + "~";
            img = img.Remove(img.Length - 1, 1);
            string date = time.Year + "." + time.Month + "." + time.Day + "." + time.Hour + "." + time.Minute + "." + time.Second;
            return username + "|" + name + "|" + img + "|" + date;
        }
        public override bool Equals(object obj)
        {
            Archive other = (Archive)obj;
            return username.Equals(other.username) && time.Equals(other.time);
        }
        public int CompareTo(Archive archive)
        {
            return 1;
        }
    }
}
