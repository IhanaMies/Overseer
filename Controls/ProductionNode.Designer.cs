namespace Overseer
{
    partial class ProductionNode
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
            this.BuildingIcon = new System.Windows.Forms.PictureBox();
            this.iLayout = new System.Windows.Forms.TableLayoutPanel();
            this.InputsPanel = new System.Windows.Forms.Panel();
            this.OutPutsPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.BuildingIcon)).BeginInit();
            this.iLayout.SuspendLayout();
            this.InputsPanel.SuspendLayout();
            this.OutPutsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BuildingIcon
            // 
            this.BuildingIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BuildingIcon.Location = new System.Drawing.Point(75, 103);
            this.BuildingIcon.Name = "BuildingIcon";
            this.BuildingIcon.Size = new System.Drawing.Size(64, 62);
            this.BuildingIcon.TabIndex = 3;
            this.BuildingIcon.TabStop = false;
            // 
            // Layout
            // 
            this.iLayout.ColumnCount = 1;
            this.iLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.iLayout.Controls.Add(this.InputsPanel, 0, 2);
            this.iLayout.Controls.Add(this.BuildingIcon, 0, 1);
            this.iLayout.Controls.Add(this.OutPutsPanel, 0, 0);
            this.iLayout.Location = new System.Drawing.Point(0, 0);
            this.iLayout.Margin = new System.Windows.Forms.Padding(0);
            this.iLayout.Name = "Layout";
            this.iLayout.RowCount = 3;
            this.iLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.iLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.iLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.iLayout.Size = new System.Drawing.Size(214, 268);
            this.iLayout.TabIndex = 4;
            // 
            // InputsPanel
            // 
            this.InputsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputsPanel.Location = new System.Drawing.Point(0, 168);
            this.InputsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.InputsPanel.Name = "InputsPanel";
            this.InputsPanel.Size = new System.Drawing.Size(214, 100);
            this.InputsPanel.TabIndex = 5;
            // 
            // OutPutsPanel
            // 
            this.OutPutsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutPutsPanel.Location = new System.Drawing.Point(0, 0);
            this.OutPutsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.OutPutsPanel.Name = "OutPutsPanel";
            this.OutPutsPanel.Size = new System.Drawing.Size(214, 100);
            this.OutPutsPanel.TabIndex = 4;
            // 
            // ProductionNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.iLayout);
            this.Name = "ProductionNode";
            this.Size = new System.Drawing.Size(214, 268);
            ((System.ComponentModel.ISupportInitialize)(this.BuildingIcon)).EndInit();
            this.iLayout.ResumeLayout(false);
            this.InputsPanel.ResumeLayout(false);
            this.InputsPanel.PerformLayout();
            this.OutPutsPanel.ResumeLayout(false);
            this.OutPutsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.TableLayoutPanel iLayout;
        private System.Windows.Forms.Panel InputsPanel;
        private System.Windows.Forms.Panel OutPutsPanel;
        public System.Windows.Forms.PictureBox BuildingIcon;
    }
}
