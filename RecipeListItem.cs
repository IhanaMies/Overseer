using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Overseer
{
    public class ItemListItem : Control
    {
        public string itemName = "";
        public string category = "";
        public string subgroup = "";
        
        protected void OnMouseEnter(object sender, EventArgs e)
        {
            Form1 form = (Form1)Form.ActiveForm;
            form.info._name_val.Text = itemName;
            Size s = form.info.Size;
            s.Height -= 30;
            s.Width -= 10;
            form.info.Location = System.Windows.Forms.Cursor.Position - s;
            form.info.BringToFront();
            form.info.Show();
        }
        protected void OnMouseLeave(object sender, EventArgs e)
        {
            Form1 form = (Form1)Form.ActiveForm;
            form.info.Hide();
        }
        public ItemListItem(Image inBitmap, string inItemName, string inCategory, string inSubGroup)
        {
            SplitContainer a1 = new SplitContainer();
            PictureBox a2 = new PictureBox();
            TextBox a3 = new TextBox();
            itemName = inItemName;
            category = inCategory;
            subgroup = inSubGroup;

            a1.Location = new System.Drawing.Point(3, 3);
            a1.Name = "a1";
            a1.Size = new System.Drawing.Size(252, 34);
            a1.SplitterDistance = 37;
            a1.TabIndex = 5;
            a3.MouseEnter += new EventHandler(OnMouseEnter);
            a3.MouseLeave += new EventHandler(OnMouseLeave);

            a2.Image = inBitmap;
            a2.Location = new System.Drawing.Point(0, 0);
            a2.Name = "a2";
            a2.Size = new System.Drawing.Size(32, 32);
            a2.TabIndex = 0;
            a2.TabStop = false;

            a3.BackColor = System.Drawing.Color.Black;
            a3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            a3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            a3.ForeColor = System.Drawing.Color.White;
            a3.Location = new System.Drawing.Point(3, 7);
            a3.Name = "a3";
            a3.Size = new System.Drawing.Size(220, 20);
            a3.TabIndex = 0;
            a3.TabStop = false;
            a3.Text = inItemName;
            a3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            a1.Panel1.Controls.Add(a2);
            a1.Panel2.Controls.Add(a3);

            Form1 form = (Form1)Form1.ActiveForm;
            //form.flowLayoutPanel1.Controls.Add(a1);
        }
    }
}
