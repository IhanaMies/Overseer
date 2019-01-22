namespace Overseer
{
    partial class NodeInput
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
            this.iLayout = new System.Windows.Forms.TableLayoutPanel();
            this.SuppliedText = new System.Windows.Forms.TextBox();
            this.ReagentPicture = new System.Windows.Forms.PictureBox();
            this.iLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReagentPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // iLayout
            // 
            this.iLayout.AutoSize = true;
            this.iLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iLayout.BackColor = System.Drawing.SystemColors.ControlDark;
            this.iLayout.ColumnCount = 1;
            this.iLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.iLayout.Controls.Add(this.SuppliedText, 0, 1);
            this.iLayout.Controls.Add(this.ReagentPicture, 0, 0);
            this.iLayout.Location = new System.Drawing.Point(0, 0);
            this.iLayout.Margin = new System.Windows.Forms.Padding(0);
            this.iLayout.Name = "iLayout";
            this.iLayout.RowCount = 2;
            this.iLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.iLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.iLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.iLayout.Size = new System.Drawing.Size(68, 60);
            this.iLayout.TabIndex = 0;
            // 
            // SuppliedText
            // 
            this.SuppliedText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SuppliedText.BackColor = System.Drawing.SystemColors.ControlDark;
            this.SuppliedText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SuppliedText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SuppliedText.Location = new System.Drawing.Point(3, 39);
            this.SuppliedText.Name = "SuppliedText";
            this.SuppliedText.Size = new System.Drawing.Size(62, 20);
            this.SuppliedText.TabIndex = 2;
            this.SuppliedText.Text = "12 / s";
            this.SuppliedText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ReagentPicture
            // 
            this.ReagentPicture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ReagentPicture.Location = new System.Drawing.Point(19, 3);
            this.ReagentPicture.Name = "ReagentPicture";
            this.ReagentPicture.Size = new System.Drawing.Size(30, 30);
            this.ReagentPicture.TabIndex = 1;
            this.ReagentPicture.TabStop = false;
            // 
            // NodeInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.iLayout);
            this.Name = "NodeInput";
            this.Size = new System.Drawing.Size(68, 60);
            this.iLayout.ResumeLayout(false);
            this.iLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReagentPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel iLayout;
        private System.Windows.Forms.TextBox SuppliedText;
        private System.Windows.Forms.PictureBox ReagentPicture;
    }
}
