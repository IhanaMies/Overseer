using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Overseer
{
    public partial class NodeInput : UserControl
    {
        public Intermediate Intermediate = new Intermediate();
        public NodeInput()
        {
            InitializeComponent();
        }
    }
}
