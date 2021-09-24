using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hieracyGenerator.control
{
    interface Icontrol<T>
    {
        void read();
        void write();

    }
}
