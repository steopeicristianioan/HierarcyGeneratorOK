using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hieracyGenerator.model
{
    class HeadData
    {
        private string url;
        private string name;

        public string URL { get => this.url; set => this.url = value; }
        public string Name { get => this.name; set => this.name = value; }

        public HeadData(string url, string name)
        {
            this.url = url;
            this.name = name;
        }

        public override string ToString()
        {
            return url + "`" + name;
        }
    }
}
