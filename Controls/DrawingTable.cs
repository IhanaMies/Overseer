using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Drawing.Drawing2D;

namespace Overseer
{
    public partial class DrawingTable : UserControl
    {
        private float prevZoomScale = 1.0f;
        private float zoomScale = 1.0f;
        public float zoomScaleDelta = 0.1f;
        public Point mousePrevLoc = new Point();

        public List<ProductionNode> productionNodes = new List<ProductionNode>();

        public DrawingTable()
        {
            InitializeComponent();
            DragDrop += DrawingTable_DragDrop;
            DragEnter += DrawingTable_DragEnter;
            MouseMove += DrawingTable_OnMouseMove;
            MouseWheel += DrawingTable_OnMouseWheel;
            Paint += DrawingTable_Paint;
        }

        private void DrawingTable_Paint(object sender, PaintEventArgs e)
        {
            if(prevZoomScale != zoomScale)
            {
                e.Graphics.ScaleTransform(zoomScale, zoomScale);
            }
        }

        private void DrawingTable_OnMouseWheel(object sender, MouseEventArgs e)
        {
            prevZoomScale = zoomScale;
            int x = e.Delta / 120;
            zoomScale += zoomScaleDelta * x;
            if(zoomScale < 0.1f)
            {
                zoomScale = 0.1f;
            }
            else if(zoomScale > 3)
            {
                zoomScale = 3.0f;
            }
            //float f = c.Font.SizeInPoints;
            //c.Font = new Font("Calibri", f * y);
            Invalidate();
        }

        private void DrawingTable_OnMouseMove(object sender, MouseEventArgs e)
        {
            if(MouseButtons.HasFlag(MouseButtons.Left))
            {
                int xDelta = Cursor.Position.X - mousePrevLoc.X;
                int yDelta = Cursor.Position.Y - mousePrevLoc.Y;

                foreach (Control control in this.Controls)
                {
                    control.Location = new Point(control.Location.X + xDelta, control.Location.Y + yDelta);
                }
            }
            mousePrevLoc = Cursor.Position;
        }
        private void DrawingTable_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("DrawingTable DragEnter event");
            if(e.Data.GetDataPresent(typeof(Item)))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void DrawingTable_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("DrawingTable DragDrop event");
            Item item = (Item)e.Data.GetData(typeof(Item));
            ProductionNode newNode = new ProductionNode();
            //foreach(Intermediate ins in item.)
            this.Controls.Add(newNode);
            newNode.Location = Functions.Sub(Cursor.Position, this.Location);
            Data.Icons.TryGetValue(item.name, out Image xd);
            newNode.BuildingIcon.Image = xd;
            newNode.Show();
            productionNodes.Add(newNode);
        }
    }
}
