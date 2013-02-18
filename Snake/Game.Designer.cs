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
            this.myFont = new Snake.PersonalFont();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.exitLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gameBoard = new Snake.DoubleBufferPanel();
            this.fruitPictureBox = new System.Windows.Forms.PictureBox();
            this.insectPictureBox = new System.Windows.Forms.PictureBox();
            this.gameBoard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fruitPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.insectPictureBox)).BeginInit();
            this.SuspendLayout();

            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape});
            this.shapeContainer1.Size = new System.Drawing.Size(794, 565);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape
            // 
            this.rectangleShape.BorderWidth = 4;
            this.rectangleShape.Location = new System.Drawing.Point(20, 20);
            this.rectangleShape.Name = "rectangleShape";
            this.rectangleShape.Size = new System.Drawing.Size(755, 480);
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font(myFont.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.Location = new System.Drawing.Point(317, 509);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(118, 38);
            this.scoreLabel.TabIndex = 4;
            this.scoreLabel.Text = "Score : ";
            this.scoreLabel.Visible = false;
            // 
            // exitLabel
            // 
            this.exitLabel.AutoSize = true;
            this.exitLabel.Font = new System.Drawing.Font(myFont.getPersonalFont(), 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitLabel.Location = new System.Drawing.Point(710, 516);
            this.exitLabel.Name = "exitLabel";
            this.exitLabel.Size = new System.Drawing.Size(66, 28);
            this.exitLabel.TabIndex = 7;
            this.exitLabel.Text = "EXIT";
            this.exitLabel.Click += new System.EventHandler(this.exitLabel_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 519);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(0, 22);
            this.textBox1.TabIndex = 9;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // gameBoard
            // 
            this.gameBoard.BackColor = System.Drawing.Color.Transparent;
            this.gameBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameBoard.Controls.Add(this.fruitPictureBox);
            this.gameBoard.Controls.Add(this.insectPictureBox);
            this.gameBoard.Location = new System.Drawing.Point(20, 20);
            this.gameBoard.Name = "gameBoard";
            this.gameBoard.Size = new System.Drawing.Size(755, 480);
            this.gameBoard.TabIndex = 8;
            // 
            // fruitPictureBox
            // 
            this.fruitPictureBox.Image = global::Snake.Properties.Resources.Fruit;
            this.fruitPictureBox.Location = new System.Drawing.Point(-666, -666);
            this.fruitPictureBox.Name = "fruitPictureBox";
            this.fruitPictureBox.Size = new System.Drawing.Size(12, 12);
            this.fruitPictureBox.TabIndex = 1;
            this.fruitPictureBox.TabStop = false;
            // 
            // insectPictureBox
            // 
            this.insectPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.insectPictureBox.Image = global::Snake.Properties.Resources.BlackInvader;
            this.insectPictureBox.InitialImage = null;
            this.insectPictureBox.Location = new System.Drawing.Point(484, 240);
            this.insectPictureBox.Name = "insectPictureBox";
            this.insectPictureBox.Size = new System.Drawing.Size(26, 26);
            this.insectPictureBox.TabIndex = 0;
            this.insectPictureBox.TabStop = false;
            this.insectPictureBox.Visible = false;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(794, 565);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.gameBoard);
            this.Controls.Add(this.exitLabel);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Game";
            this.Text = "Snake";
            this.gameBoard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fruitPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.insectPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label exitLabel;
        private System.Windows.Forms.TextBox textBox1;
        private PictureBox insectPictureBox;
        private PictureBox fruitPictureBox;
        private DoubleBufferPanel gameBoard;
        private PersonalFont myFont;
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

}

