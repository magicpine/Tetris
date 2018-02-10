namespace Tetris
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.playBtn = new System.Windows.Forms.Button();
            this.highScoresBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // playBtn
            // 
            this.playBtn.BackColor = System.Drawing.Color.RoyalBlue;
            this.playBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.playBtn.FlatAppearance.BorderSize = 2;
            this.playBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MidnightBlue;
            this.playBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.playBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playBtn.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playBtn.Location = new System.Drawing.Point(12, 431);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(171, 49);
            this.playBtn.TabIndex = 0;
            this.playBtn.Text = "Start Game";
            this.playBtn.UseVisualStyleBackColor = false;
            this.playBtn.Click += new System.EventHandler(this.playBtn_Click);
            // 
            // highScoresBtn
            // 
            this.highScoresBtn.BackColor = System.Drawing.Color.RoyalBlue;
            this.highScoresBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.highScoresBtn.FlatAppearance.BorderSize = 2;
            this.highScoresBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MidnightBlue;
            this.highScoresBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.highScoresBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.highScoresBtn.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.highScoresBtn.Location = new System.Drawing.Point(470, 431);
            this.highScoresBtn.Name = "highScoresBtn";
            this.highScoresBtn.Size = new System.Drawing.Size(171, 49);
            this.highScoresBtn.TabIndex = 1;
            this.highScoresBtn.Text = "High Scores";
            this.highScoresBtn.UseVisualStyleBackColor = false;
            this.highScoresBtn.Click += new System.EventHandler(this.highScoresBtn_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Tetris.Properties.Resources.tetrisMainScreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(653, 554);
            this.Controls.Add(this.highScoresBtn);
            this.Controls.Add(this.playBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris By: Christian Bradford";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Button highScoresBtn;
    }
}