﻿namespace Program
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
            this.btnRandom = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Label();
            this.btnDraw = new System.Windows.Forms.Button();
            this.time = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MultiThread
            // 
            this.MultiThread.AutoSize = true;
            this.MultiThread.Location = new System.Drawing.Point(395, 13);
            this.MultiThread.Name = "MultiThread";
            this.MultiThread.Size = new System.Drawing.Size(82, 17);
            this.MultiThread.TabIndex = 0;
            this.MultiThread.Text = "MultiThread";
            this.MultiThread.UseVisualStyleBackColor = true;
            this.MultiThread.CheckedChanged += new System.EventHandler(this.MultiThread_CheckedChanged);
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(548, 10);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(59, 23);
            this.btnRandom.TabIndex = 1;
            this.btnRandom.Text = "Random";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.RandomPoints_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(613, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(59, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.Clear);
            // 
            // timer
            // 
            this.timer.Location = new System.Drawing.Point(332, 13);
            this.timer.Name = "timer";
            this.timer.Size = new System.Drawing.Size(57, 17);
            this.timer.TabIndex = 3;
            this.timer.Text = "\r\n";
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(483, 10);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(59, 23);
            this.btnDraw.TabIndex = 4;
            this.btnDraw.Text = "Draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.Draw);
            // 
            // time
            // 
            this.time.Location = new System.Drawing.Point(300, 13);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(36, 17);
            this.time.TabIndex = 5;
            this.time.Text = "Time:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.time);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.timer);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRandom);
            this.Controls.Add(this.MultiThread);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.CheckBox MultiThread;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label timer;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Label time;

        #endregion
    }
}
