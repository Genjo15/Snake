using System.Windows.Forms;

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Snake
{
    partial class Game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.scoreLabel = new System.Windows.Forms.Label();
            this.opponentScoreLabel = new System.Windows.Forms.Label();
            this.miniGameBoardPictureBox = new System.Windows.Forms.PictureBox();
            this.bottomBorderPictureBox = new System.Windows.Forms.PictureBox();
            this.topBorderPictureBox = new System.Windows.Forms.PictureBox();
            this.rightBorderPictureBox = new System.Windows.Forms.PictureBox();
            this.leftBorderPictureBox = new System.Windows.Forms.PictureBox();
            this.exitPictureBox = new System.Windows.Forms.PictureBox();
            this.gameBoardPictureBox = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.miniGameBoardPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomBorderPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topBorderPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightBorderPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftBorderPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameBoardPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Location = new System.Drawing.Point(336, 496);
            this.scoreLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(44, 13);
            this.scoreLabel.TabIndex = 4;
            this.scoreLabel.Text = "Score : ";
            this.scoreLabel.Visible = false;
            // 
            // opponentScoreLabel
            // 
            this.opponentScoreLabel.AutoSize = true;
            this.opponentScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opponentScoreLabel.Location = new System.Drawing.Point(790, 380);
            this.opponentScoreLabel.Name = "opponentScoreLabel";
            this.opponentScoreLabel.Size = new System.Drawing.Size(79, 29);
            this.opponentScoreLabel.TabIndex = 40;
            this.opponentScoreLabel.Text = "label1";
            // 
            // miniGameBoardPictureBox
            // 
            this.miniGameBoardPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.miniGameBoardPictureBox.Location = new System.Drawing.Point(790, 140);
            this.miniGameBoardPictureBox.Name = "miniGameBoardPictureBox";
            this.miniGameBoardPictureBox.Size = new System.Drawing.Size(378, 238);
            this.miniGameBoardPictureBox.TabIndex = 16;
            this.miniGameBoardPictureBox.TabStop = false;
            // 
            // bottomBorderPictureBox
            // 
            this.bottomBorderPictureBox.Image = global::Snake.Properties.Resources.HorizontalBorder;
            this.bottomBorderPictureBox.Location = new System.Drawing.Point(11, 491);
            this.bottomBorderPictureBox.Name = "bottomBorderPictureBox";
            this.bottomBorderPictureBox.Size = new System.Drawing.Size(762, 3);
            this.bottomBorderPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bottomBorderPictureBox.TabIndex = 13;
            this.bottomBorderPictureBox.TabStop = false;
            // 
            // topBorderPictureBox
            // 
            this.topBorderPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("topBorderPictureBox.Image")));
            this.topBorderPictureBox.Location = new System.Drawing.Point(11, 12);
            this.topBorderPictureBox.Name = "topBorderPictureBox";
            this.topBorderPictureBox.Size = new System.Drawing.Size(762, 3);
            this.topBorderPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.topBorderPictureBox.TabIndex = 13;
            this.topBorderPictureBox.TabStop = false;
            // 
            // rightBorderPictureBox
            // 
            this.rightBorderPictureBox.Image = global::Snake.Properties.Resources.VerticalBorder;
            this.rightBorderPictureBox.Location = new System.Drawing.Point(770, 15);
            this.rightBorderPictureBox.Name = "rightBorderPictureBox";
            this.rightBorderPictureBox.Size = new System.Drawing.Size(3, 476);
            this.rightBorderPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.rightBorderPictureBox.TabIndex = 12;
            this.rightBorderPictureBox.TabStop = false;
            // 
            // leftBorderPictureBox
            // 
            this.leftBorderPictureBox.Image = global::Snake.Properties.Resources.VerticalBorder;
            this.leftBorderPictureBox.Location = new System.Drawing.Point(11, 15);
            this.leftBorderPictureBox.Name = "leftBorderPictureBox";
            this.leftBorderPictureBox.Size = new System.Drawing.Size(3, 476);
            this.leftBorderPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.leftBorderPictureBox.TabIndex = 11;
            this.leftBorderPictureBox.TabStop = false;
            // 
            // exitPictureBox
            // 
            this.exitPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("exitPictureBox.Image")));
            this.exitPictureBox.Location = new System.Drawing.Point(714, 495);
            this.exitPictureBox.Name = "exitPictureBox";
            this.exitPictureBox.Size = new System.Drawing.Size(53, 28);
            this.exitPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.exitPictureBox.TabIndex = 10;
            this.exitPictureBox.TabStop = false;
            this.exitPictureBox.Click += new System.EventHandler(this.exitPictureBox_Click_1);
            // 
            // gameBoardPictureBox
            // 
            this.gameBoardPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.gameBoardPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameBoardPictureBox.Location = new System.Drawing.Point(14, 15);
            this.gameBoardPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.gameBoardPictureBox.Name = "gameBoardPictureBox";
            this.gameBoardPictureBox.Size = new System.Drawing.Size(756, 476);
            this.gameBoardPictureBox.TabIndex = 8;
            this.gameBoardPictureBox.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 415);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(784, 531);
            this.Controls.Add(this.opponentScoreLabel);
            this.Controls.Add(this.miniGameBoardPictureBox);
            this.Controls.Add(this.bottomBorderPictureBox);
            this.Controls.Add(this.topBorderPictureBox);
            this.Controls.Add(this.rightBorderPictureBox);
            this.Controls.Add(this.leftBorderPictureBox);
            this.Controls.Add(this.exitPictureBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.gameBoardPictureBox);
            this.Controls.Add(this.scoreLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Game";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "10";
            this.Text = "Snake";
            ((System.ComponentModel.ISupportInitialize)(this.miniGameBoardPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomBorderPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topBorderPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightBorderPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftBorderPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameBoardPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label scoreLabel;
        private PictureBox gameBoardPictureBox;
        private PictureBox exitPictureBox;
        private PictureBox leftBorderPictureBox;
        private PictureBox topBorderPictureBox;
        private PictureBox bottomBorderPictureBox;
        private PictureBox rightBorderPictureBox;
        private PictureBox miniGameBoardPictureBox;
        private Label opponentScoreLabel;
        private TextBox textBox1;
    }

}

