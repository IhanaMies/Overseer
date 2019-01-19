namespace Overseer
{
    partial class NodeInputOutput
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
            this.SuppliedText = new System.Windows.Forms.TextBox();
            this.DemandText = new System.Windows.Forms.TextBox();
            this.ReagentPicture = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReagentPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.SuppliedText, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.DemandText, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ReagentPicture, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(89, 98);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // SuppliedText
            // 
            this.SuppliedText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SuppliedText.BackColor = System.Drawing.SystemColors.ControlDark;
            this.SuppliedText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SuppliedText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SuppliedText.Location = new System.Drawing.Point(13, 71);
            this.SuppliedText.Name = "SuppliedText";
            this.SuppliedText.Size = new System.Drawing.Size(62, 20);
            this.SuppliedText.TabIndex = 2;
            this.SuppliedText.Text = "12 / s";
            this.SuppliedText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DemandText
            // 
            this.DemandText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DemandText.BackColor = System.Drawing.SystemColors.ControlDark;
            this.DemandText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DemandText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DemandText.Location = new System.Drawing.Point(13, 6);
            this.DemandText.Name = "DemandText";
            this.DemandText.Size = new System.Drawing.Size(62, 20);
            this.DemandText.TabIndex = 0;
            this.DemandText.Text = "12 / s";
            this.DemandText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ReagentPicture
            // 
            this.ReagentPicture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ReagentPicture.Location = new System.Drawing.Point(28, 35);
            this.ReagentPicture.Name = "ReagentPicture";
            this.ReagentPicture.Size = new System.Drawing.Size(32, 26);
            this.ReagentPicture.TabIndex = 1;
            this.ReagentPicture.TabStop = false;
            // 
            // NodeInputOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "NodeInputOutput";
            this.Size = new System.Drawing.Size(89, 98);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReagentPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox SuppliedText;
        private System.Windows.Forms.TextBox DemandText;
        private System.Windows.Forms.PictureBox ReagentPicture;
    }
}
