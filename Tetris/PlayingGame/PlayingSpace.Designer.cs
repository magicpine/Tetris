namespace Tetris
{
    partial class PlayingSpace
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayingSpace));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ScoresTimer = new System.Windows.Forms.Timer(this.components);
            this.scoreLbl = new System.Windows.Forms.Label();
            this.heldBlockPb = new System.Windows.Forms.PictureBox();
            this.previewThreePb = new System.Windows.Forms.PictureBox();
            this.previewTwoPb = new System.Windows.Forms.PictureBox();
            this.previewOnePb = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.levelLbl = new System.Windows.Forms.Label();
            this.lineLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.heldBlockPb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewThreePb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewTwoPb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewOnePb)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(208)))), ((int)(((byte)(119)))));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Held Block";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(208)))), ((int)(((byte)(119)))));
            this.label2.Location = new System.Drawing.Point(432, 354);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 33);
            this.label2.TabIndex = 5;
            this.label2.Text = "Score";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(208)))), ((int)(((byte)(119)))));
            this.label5.Location = new System.Drawing.Point(430, 414);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 33);
            this.label5.TabIndex = 7;
            this.label5.Text = "Level";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(208)))), ((int)(((byte)(119)))));
            this.label4.Location = new System.Drawing.Point(430, 469);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(216, 33);
            this.label4.TabIndex = 9;
            this.label4.Text = "Lines Cleared";
            // 
            // ScoresTimer
            // 
            this.ScoresTimer.Enabled = true;
            this.ScoresTimer.Interval = 10;
            this.ScoresTimer.Tick += new System.EventHandler(this.ScoresTimer_Tick);
            // 
            // scoreLbl
            // 
            this.scoreLbl.AutoSize = true;
            this.scoreLbl.BackColor = System.Drawing.Color.Transparent;
            this.scoreLbl.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLbl.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.scoreLbl.Location = new System.Drawing.Point(448, 388);
            this.scoreLbl.Name = "scoreLbl";
            this.scoreLbl.Size = new System.Drawing.Size(67, 23);
            this.scoreLbl.TabIndex = 10;
            this.scoreLbl.Text = "Score";
            // 
            // heldBlockPb
            // 
            this.heldBlockPb.BackColor = System.Drawing.Color.Transparent;
            this.heldBlockPb.Location = new System.Drawing.Point(32, 51);
            this.heldBlockPb.Name = "heldBlockPb";
            this.heldBlockPb.Size = new System.Drawing.Size(101, 76);
            this.heldBlockPb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.heldBlockPb.TabIndex = 3;
            this.heldBlockPb.TabStop = false;
            // 
            // previewThreePb
            // 
            this.previewThreePb.BackColor = System.Drawing.Color.Transparent;
            this.previewThreePb.Location = new System.Drawing.Point(451, 215);
            this.previewThreePb.Name = "previewThreePb";
            this.previewThreePb.Size = new System.Drawing.Size(101, 76);
            this.previewThreePb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewThreePb.TabIndex = 2;
            this.previewThreePb.TabStop = false;
            // 
            // previewTwoPb
            // 
            this.previewTwoPb.BackColor = System.Drawing.Color.Transparent;
            this.previewTwoPb.Location = new System.Drawing.Point(451, 133);
            this.previewTwoPb.Name = "previewTwoPb";
            this.previewTwoPb.Size = new System.Drawing.Size(101, 76);
            this.previewTwoPb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewTwoPb.TabIndex = 1;
            this.previewTwoPb.TabStop = false;
            // 
            // previewOnePb
            // 
            this.previewOnePb.BackColor = System.Drawing.Color.Transparent;
            this.previewOnePb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewOnePb.Location = new System.Drawing.Point(451, 51);
            this.previewOnePb.Name = "previewOnePb";
            this.previewOnePb.Size = new System.Drawing.Size(101, 76);
            this.previewOnePb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewOnePb.TabIndex = 0;
            this.previewOnePb.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(208)))), ((int)(((byte)(119)))));
            this.label3.Location = new System.Drawing.Point(448, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 26);
            this.label3.TabIndex = 13;
            this.label3.Text = "Next";
            // 
            // levelLbl
            // 
            this.levelLbl.AutoSize = true;
            this.levelLbl.BackColor = System.Drawing.Color.Transparent;
            this.levelLbl.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelLbl.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.levelLbl.Location = new System.Drawing.Point(448, 447);
            this.levelLbl.Name = "levelLbl";
            this.levelLbl.Size = new System.Drawing.Size(67, 23);
            this.levelLbl.TabIndex = 14;
            this.levelLbl.Text = "Score";
            // 
            // lineLbl
            // 
            this.lineLbl.AutoSize = true;
            this.lineLbl.BackColor = System.Drawing.Color.Transparent;
            this.lineLbl.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineLbl.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lineLbl.Location = new System.Drawing.Point(447, 511);
            this.lineLbl.Name = "lineLbl";
            this.lineLbl.Size = new System.Drawing.Size(67, 23);
            this.lineLbl.TabIndex = 15;
            this.lineLbl.Text = "Score";
            // 
            // PlayingSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(680, 585);
            this.Controls.Add(this.lineLbl);
            this.Controls.Add(this.levelLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.scoreLbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.heldBlockPb);
            this.Controls.Add(this.previewThreePb);
            this.Controls.Add(this.previewTwoPb);
            this.Controls.Add(this.previewOnePb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlayingSpace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tetris By: Christian Bradford";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayingSpace_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPressAsync);
            ((System.ComponentModel.ISupportInitialize)(this.heldBlockPb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewThreePb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewTwoPb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewOnePb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox previewOnePb;
        private System.Windows.Forms.PictureBox previewTwoPb;
        private System.Windows.Forms.PictureBox previewThreePb;
        private System.Windows.Forms.PictureBox heldBlockPb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer ScoresTimer;
        private System.Windows.Forms.Label scoreLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label levelLbl;
        private System.Windows.Forms.Label lineLbl;
    }
}

