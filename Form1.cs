using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Data;
// using System.Drawing;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Drawing;

using System.IO;

namespace Overseer
{
    public partial class Form1 : Form
    {
        static int EdgeMargin = 10;
        public ItemHoverInfo info = new ItemHoverInfo();
        public Form1()
        {
            InitializeComponent();
            info.Hide();
            Controls.Add(info);
        }

        private void OnResize(object sender, EventArgs e)
        {
            dataGridView1.Location = new Point(EdgeMargin, 125);
            dataGridView1.Size = new Size(275, Size.Height - dataGridView1.Location.Y - EdgeMargin - 40);
            progressBar1.Location = new Point(dataGridView1.Location.X + dataGridView1.Size.Width + EdgeMargin, Size.Height - progressBar1.Size.Height - EdgeMargin - 40);
            progressBar1.Size = new Size(Size.Width - 325, progressBar1.Size.Height);
            drawingTable1.Location = new Point(dataGridView1.Location.X + dataGridView1.Size.Width + EdgeMargin, dataGridView1.Location.Y);
            drawingTable1.Size = new Size(Size.Width - 325, Size.Height - dataGridView1.Location.Y - EdgeMargin * 2 - 40 - progressBar1.Size.Height);
        }

        private void TestClick(object sender, EventArgs e)
        {
            PopulateItemList();
        }

        private class DataGridViewItemCell : DataGridViewCell
        {
            public Item item = new Item();
        }

        private class DataGridViewItemColumn : DataGridViewColumn
        {
            public DataGridViewItemColumn()
            {
                CellTemplate = new DataGridViewItemCell();
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                ReadOnly = true;
                Resizable = DataGridViewTriState.False;
                Width = 300;
            }
        }

        public void PopulateItemList()
        {
            DataGridViewImageColumn col1 = new DataGridViewImageColumn();
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            DataGridViewItemColumn col3 = new DataGridViewItemColumn();

            col1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            col1.ReadOnly = true;
            col1.Resizable = DataGridViewTriState.False;

            col2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            col2.ReadOnly = true;
            col2.Resizable = DataGridViewTriState.False;

            col2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            col2.ReadOnly = true;
            col2.Resizable = DataGridViewTriState.False;

            dataGridView1.Columns.Add(col1);
            dataGridView1.Columns[0].Width = 35;

            dataGridView1.Columns.Add(col2);
            dataGridView1.Columns[1].Width = 250;

            dataGridView1.Columns.Add(col3);
            dataGridView1.Columns[2].Width = 0;

            var en = Data.Items.GetEnumerator();
            for (int i = 0; i < Data.Items.Count; ++i)                                
            {
                en.MoveNext();                
                Item item = en.Current.Value;
                Data.Icons.TryGetValue(item.name, out Image xd);
                string localised_name = item.localised_name;
                if (localised_name == null)
                {
                    Data.Locale[0].TryGetValue(item.name, out localised_name);
                    if(localised_name == null)
                    {
                        Console.WriteLine("{0} missing localized name", item.name);
                    }
                }
                int index = dataGridView1.Rows.Add(xd, localised_name, item);
                dataGridView1.Rows[index].Cells[2].ValueType = typeof(Item);
                dataGridView1.Rows[index].Cells[2].Value = item;
            }
            dataGridView1.CellMouseClick += new DataGridViewCellMouseEventHandler(OnCellMouseClicked);
            dataGridView1.CellMouseEnter += new DataGridViewCellEventHandler(OnCellMouseEnter);
            dataGridView1.CellMouseLeave += new DataGridViewCellEventHandler(OnCellMouseLeave);
        }
        private void OnCellMouseClicked(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }
        private void OnCellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            Item item = dataGridView1.Rows[e.RowIndex].Cells[2].Value as Item;
            info._name_val.Text = item.name;
            info.Location = new Point(300,140);
            info.Location.Offset(Cursor.Position.X, Cursor.Position.Y);

            if(item.MadeByRecipes.Count > 0)
            {
                DataGridView inGrid = new DataGridView();
                SetDataGridViewSettings(ref inGrid);
                foreach (Recipe inRec in item.MadeByRecipes)
                {
                    string str = inRec.name;
                    string localised_name = inRec.localised_name;
                    if (localised_name == null)
                    {
                        foreach(var dic in Data.Locale)
                        {
                            dic.TryGetValue(inRec.name, out localised_name);
                            if (localised_name != null)
                                break;
                        }
                    }
                    Data.Icons.TryGetValue(str, out Image img);
                    inGrid.Rows.Add(img, localised_name);
                }
                info.InList.Controls.Add(inGrid);
            }

            if (item.UsedInRecipes.Count > 0)
            {
                DataGridView outGrid = new DataGridView();
                SetDataGridViewSettings(ref outGrid);
                foreach (Recipe outRec in item.UsedInRecipes)
                {
                    string str = outRec.name;
                    string localised_name = outRec.localised_name;
                    if (localised_name == null)
                    {
                        foreach (var dic in Data.Locale)
                        {
                            dic.TryGetValue(outRec.name, out localised_name);
                            if (localised_name != null)
                                break;
                        }
                    }
                    Data.Icons.TryGetValue(str, out Image img);
                    outGrid.Rows.Add(img, localised_name);
                }
                info.OutList.Controls.Add(outGrid);
            }
            info.BringToFront();
            info.Show();
        }
        private void OnCellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            info.Hide();
            info.InList.Controls.Clear();
            info.OutList.Controls.Clear();
        }

        private void SetDataGridViewSettings(ref DataGridView d)
        {
            d.AutoSize = true;
            d.Height = 35;
            d.Width = 220;
            d.MaximumSize = new Size(220, 600);
            d.ColumnHeadersVisible = false;
            d.RowHeadersVisible = false;
            d.AllowUserToAddRows = false;
            DataGridViewImageColumn col1 = new DataGridViewImageColumn();
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            d.Columns.Add(col1);
            d.Columns[0].Width = 35;
            d.Columns.Add(col2);
            d.Columns[1].Width = 165;
            d.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            d.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            d.AllowDrop = false;
            d.BackgroundColor = Color.White;
            d.TabStop = false;
            d.ReadOnly = true;
            d.BorderStyle = BorderStyle.Fixed3D;
            d.CellBorderStyle = DataGridViewCellBorderStyle.None;
            d.DefaultCellStyle.SelectionForeColor = d.ForeColor;
            d.DefaultCellStyle.SelectionBackColor = d.BackgroundColor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModHandling.GetModList();
        }
    }
}