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
            this.nodeInputOutput1 = new Overseer.NodeInputOutput();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nodeInputOutput1
            // 
            this.nodeInputOutput1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.nodeInputOutput1.Location = new System.Drawing.Point(3, 3);
            this.nodeInputOutput1.Name = "nodeInputOutput1";
            this.nodeInputOutput1.Size = new System.Drawing.Size(89, 98);
            this.nodeInputOutput1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.nodeInputOutput1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(446, 367);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // ProductionNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ProductionNode";
            this.Size = new System.Drawing.Size(452, 373);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private NodeInputOutput nodeInputOutput1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
