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
    public partial class ProductionNode : UserControl
    {
        public List<NodeInput> inputs = new List<NodeInput>();
        public List<NodeOutput> outputs = new List<NodeOutput>();
        public ProductionNode()
        {
            InitializeComponent();
            Paint += ProductionNode_Paint;
        }

        private void ProductionNode_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("ProductionNode paint");
        }
        public void AddInputNode(Intermediate inIntermediate)
        {
            NodeInput input = new NodeInput
            {
                Intermediate = inIntermediate
            };
        }
        public void AddOutputNode(Intermediate inIntermediate)
        {
            NodeOutput output = new NodeOutput
            {
                Intermediate = inIntermediate
            };
        }
    }
}
