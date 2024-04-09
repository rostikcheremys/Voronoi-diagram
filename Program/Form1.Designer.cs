namespace Program
{
    sealed partial class Form1
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
            this.MultiThread = new System.Windows.Forms.CheckBox();
            this.btnRandomPoints = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MultiThread
            // 
            this.MultiThread.AutoSize = true;
            this.MultiThread.Location = new System.Drawing.Point(12, 16);
            this.MultiThread.Name = "MultiThread";
            this.MultiThread.Size = new System.Drawing.Size(85, 17);
            this.MultiThread.TabIndex = 0;
            this.MultiThread.Text = "Multi-Thread";
            this.MultiThread.UseVisualStyleBackColor = true;
            this.MultiThread.CheckedChanged += new System.EventHandler(this.SingleThread_CheckedChanged);
            // 
            // btnRandomPoints
            // 
            this.btnRandomPoints.Location = new System.Drawing.Point(557, 12);
            this.btnRandomPoints.Name = "btnRandomPoints";
            this.btnRandomPoints.Size = new System.Drawing.Size(115, 23);
            this.btnRandomPoints.TabIndex = 1;
            this.btnRandomPoints.Text = "Random Points";
            this.btnRandomPoints.UseVisualStyleBackColor = true;
            this.btnRandomPoints.Click += new System.EventHandler(this.btnRandomPoints_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.btnRandomPoints);
            this.Controls.Add(this.MultiThread);
            this.Name = "Form1";
            this.Text = "Voronoi Diagram";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.CheckBox MultiThread;
        private System.Windows.Forms.Button btnRandomPoints;

        #endregion
    }
}
