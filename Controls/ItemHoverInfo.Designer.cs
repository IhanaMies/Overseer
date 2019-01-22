namespace Overseer
{
    partial class ItemHoverInfo
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._name = new System.Windows.Forms.TextBox();
            this._name_val = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.InList = new System.Windows.Forms.FlowLayoutPanel();
            this.OutList = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.OutList, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._name_val, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._name, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.InList, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(0, 80);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(337, 80);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _name
            // 
            this._name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._name.BackColor = System.Drawing.SystemColors.Control;
            this._name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._name.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._name.Location = new System.Drawing.Point(3, 3);
            this._name.Name = "_name";
            this._name.Size = new System.Drawing.Size(100, 20);
            this._name.TabIndex = 0;
            this._name.TabStop = false;
            this._name.Text = "Name:";
            // 
            // _name_val
            // 
            this._name_val.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._name_val.BackColor = System.Drawing.SystemColors.Control;
            this._name_val.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._name_val.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._name_val.Location = new System.Drawing.Point(109, 3);
            this._name_val.Name = "_name_val";
            this._name_val.Size = new System.Drawing.Size(225, 20);
            this._name_val.TabIndex = 1;
            this._name_val.TabStop = false;
            this._name_val.Text = "xxx";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(3, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(94, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "In:";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(3, 55);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(94, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "Out:";
            // 
            // InList
            // 
            this.InList.AutoSize = true;
            this.InList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.InList.Location = new System.Drawing.Point(109, 29);
            this.InList.Name = "InList";
            this.InList.Size = new System.Drawing.Size(0, 0);
            this.InList.TabIndex = 4;
            // 
            // OutList
            // 
            this.OutList.AutoSize = true;
            this.OutList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OutList.Location = new System.Drawing.Point(109, 55);
            this.OutList.Name = "OutList";
            this.OutList.Size = new System.Drawing.Size(0, 0);
            this.OutList.TabIndex = 5;
            // 
            // ItemHoverInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ItemHoverInfo";
            this.Size = new System.Drawing.Size(340, 83);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.TextBox _name_val;
        public System.Windows.Forms.TextBox _name;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.FlowLayoutPanel InList;
        public System.Windows.Forms.FlowLayoutPanel OutList;
    }
}
