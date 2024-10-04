using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;

namespace DirectInputNamespace {
    class Program {
        static void Main(string[] args) {
            Selector select = new Selector();
            select.ShowDialog();
        }
    }
}
