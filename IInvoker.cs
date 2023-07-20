using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleInv
{
    public interface IInvoker
    {
        void Invoke(int choice);

    }
}
