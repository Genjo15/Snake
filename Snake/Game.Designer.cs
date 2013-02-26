using System.Windows.Forms;

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.topBorderPictureBox = new System.Windows.Forms.PictureBox();
            this.leftBorderPictureBox = new System.Windows.Forms.PictureBox();
            this.exitPictureBox = new System.Windows.Forms.PictureBox();
            this.gameBoard = new Snake.DoubleBufferPanel();
            this.fruitPictureBox = new System.Windows.Forms.PictureBox();
            this.insectPictureBox = new System.Windows.Forms.PictureBox();
            this.bottomBorderPictureBox = new System.Windows.Forms.PictureBox();
            this.rightBorderPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.topBorderPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftBorderPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitPictureBox)).BeginInit();
            this.gameBoard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fruitPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.insectPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomBorderPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightBorderPictureBox)).BeginInit();
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 415);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
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
            // gameBoard
            // 
            this.gameBoard.BackColor = System.Drawing.Color.Transparent;
            this.gameBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameBoard.Controls.Add(this.fruitPictureBox);
            this.gameBoard.Controls.Add(this.insectPictureBox);
            this.gameBoard.Location = new System.Drawing.Point(14, 15);
            this.gameBoard.Margin = new System.Windows.Forms.Padding(2);
            this.gameBoard.Name = "gameBoard";
            this.gameBoard.Size = new System.Drawing.Size(756, 476);
            this.gameBoard.TabIndex = 8;
            // 
            // fruitPictureBox
            // 
            this.fruitPictureBox.Image = global::Snake.Properties.Resources.Fruit;
            this.fruitPictureBox.Location = new System.Drawing.Point(-533, -533);
            this.fruitPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.fruitPictureBox.Name = "fruitPictureBox";
            this.fruitPictureBox.Size = new System.Drawing.Size(12, 12);
            this.fruitPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.fruitPictureBox.TabIndex = 1;
            this.fruitPictureBox.TabStop = false;
            // 
            // insectPictureBox
            // 
            this.insectPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.insectPictureBox.Image = global::Snake.Properties.Resources.Insect;
            this.insectPictureBox.InitialImage = null;
            this.insectPictureBox.Location = new System.Drawing.Point(387, 192);
            this.insectPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.insectPictureBox.Name = "insectPictureBox";
            this.insectPictureBox.Size = new System.Drawing.Size(26, 26);
            this.insectPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.insectPictureBox.TabIndex = 0;
            this.insectPictureBox.TabStop = false;
            this.insectPictureBox.Visible = false;
            // 
            // bottomBorderPictureBox
            // 
            this.bottomBorderPictureBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bottomBorderPictureBox.Image = global::Snake.Properties.Resources.HorizontalBorder;
            this.bottomBorderPictureBox.Location = new System.Drawing.Point(11, 491);
            this.bottomBorderPictureBox.Name = "bottomBorderPictureBox";
            this.bottomBorderPictureBox.Size = new System.Drawing.Size(762, 3);
            this.bottomBorderPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bottomBorderPictureBox.TabIndex = 14;
            this.bottomBorderPictureBox.TabStop = false;
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
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(784, 531);
            this.Controls.Add(this.bottomBorderPictureBox);
            this.Controls.Add(this.topBorderPictureBox);
            this.Controls.Add(this.rightBorderPictureBox);
            this.Controls.Add(this.leftBorderPictureBox);
            this.Controls.Add(this.exitPictureBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.gameBoard);
            this.Controls.Add(this.scoreLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Game";
            this.Text = "Snake";
            ((System.ComponentModel.ISupportInitialize)(this.topBorderPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftBorderPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitPictureBox)).EndInit();
            this.gameBoard.ResumeLayout(false);
            this.gameBoard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fruitPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.insectPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomBorderPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightBorderPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.TextBox textBox1;
        private PictureBox insectPictureBox;
        private PictureBox fruitPictureBox;
        private DoubleBufferPanel gameBoard;
        private PictureBox exitPictureBox;
        private PictureBox leftBorderPictureBox;
        private PictureBox topBorderPictureBox;
        private PictureBox bottomBorderPictureBox;
        private PictureBox rightBorderPictureBox;
    }

    public class DoubleBufferPanel : System.Windows.Forms.Panel
    {
        public DoubleBufferPanel()
        {
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.DoubleBuffer |
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint,
            true);

            this.UpdateStyles();
        }
    }

    public class RoundedPanel : System.Windows.Forms.Panel
    {
        protected override void OnResize(EventArgs e)
        {
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                this.Region = new Region(path);
            }
            base.OnResize(e);
        }
    }

}

