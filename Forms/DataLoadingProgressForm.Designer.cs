namespace FactorioApp
{
    partial class DataLoadingProgressForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBarTask = new System.Windows.Forms.ProgressBar();
            this.progressBarTotal = new System.Windows.Forms.ProgressBar();
            this.TaskTextBox = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // progressBarTask
            // 
            this.progressBarTask.Location = new System.Drawing.Point(46, 71);
            this.progressBarTask.Name = "progressBarTask";
            this.progressBarTask.Size = new System.Drawing.Size(379, 23);
            this.progressBarTask.TabIndex = 0;
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(46, 143);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Size = new System.Drawing.Size(379, 23);
            this.progressBarTotal.TabIndex = 1;
            // 
            // TaskTextBox
            // 
            this.TaskTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TaskTextBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TaskTextBox.Location = new System.Drawing.Point(46, 45);
            this.TaskTextBox.Name = "TaskTextBox";
            this.TaskTextBox.ReadOnly = true;
            this.TaskTextBox.Size = new System.Drawing.Size(379, 20);
            this.TaskTextBox.TabIndex = 2;
            this.TaskTextBox.TabStop = false;
            this.TaskTextBox.Text = "Task Progress";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(46, 117);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "Total Progress";
            // 
            // DataLoadingProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 219);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.TaskTextBox);
            this.Controls.Add(this.progressBarTotal);
            this.Controls.Add(this.progressBarTask);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataLoadingProgressForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "DataLoadingProgressForm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.ProgressBar progressBarTask;
        public System.Windows.Forms.ProgressBar progressBarTotal;
        public System.Windows.Forms.TextBox TaskTextBox;
    }
}