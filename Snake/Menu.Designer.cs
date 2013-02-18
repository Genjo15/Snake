using System.Windows.Forms;

namespace Snake
{
    partial class Menu
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
            this.myFont = new Snake.PersonalFont();
            this.menuPanel = new System.Windows.Forms.Panel();
            this.mainMenuLabel = new System.Windows.Forms.Label();
            this.retryLabel = new System.Windows.Forms.Label();
            this.gameOverLabel = new System.Windows.Forms.Label();
            this.playLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.menuPanel.SuspendLayout();
            this.SuspendLayout();

            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.menuPanel.Controls.Add(this.mainMenuLabel);
            this.menuPanel.Controls.Add(this.retryLabel);
            this.menuPanel.Controls.Add(this.gameOverLabel);
            this.menuPanel.Controls.Add(this.playLabel);
            this.menuPanel.Controls.Add(this.titleLabel);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(755, 480);
            this.menuPanel.TabIndex = 0;
            // 
            // mainMenuLabel
            // 
            this.mainMenuLabel.AutoSize = true;
            this.mainMenuLabel.Font = new System.Drawing.Font(myFont.getPersonalFont(), 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainMenuLabel.Location = new System.Drawing.Point(257, 272);
            this.mainMenuLabel.Name = "mainMenuLabel";
            this.mainMenuLabel.Size = new System.Drawing.Size(211, 44);
            this.mainMenuLabel.TabIndex = 10;
            this.mainMenuLabel.Text = "Main Menu";
            // 
            // retryLabel
            // 
            this.retryLabel.AutoSize = true;
            this.retryLabel.Font = new System.Drawing.Font(myFont.getPersonalFont(), 25F);
            this.retryLabel.Location = new System.Drawing.Point(305, 207);
            this.retryLabel.Name = "retryLabel";
            this.retryLabel.Size = new System.Drawing.Size(111, 47);
            this.retryLabel.TabIndex = 9;
            this.retryLabel.Text = "Retry";
            // 
            // gameOverLabel
            // 
            this.gameOverLabel.AutoSize = true;
            this.gameOverLabel.Font = new System.Drawing.Font(myFont.getPersonalFont(), 52F);
            this.gameOverLabel.Location = new System.Drawing.Point(142, 97);
            this.gameOverLabel.Name = "gameOverLabel";
            this.gameOverLabel.Size = new System.Drawing.Size(451, 97);
            this.gameOverLabel.TabIndex = 8;
            this.gameOverLabel.Text = "Game Over";
            // 
            // playLabel
            // 
            this.playLabel.AutoSize = true;
            this.playLabel.Font = new System.Drawing.Font(myFont.getPersonalFont(), 28F);
            this.playLabel.Location = new System.Drawing.Point(235, 205);
            this.playLabel.Name = "playLabel";
            this.playLabel.Size = new System.Drawing.Size(267, 53);
            this.playLabel.TabIndex = 7;
            this.playLabel.Text = "Single Player";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font(myFont.getPersonalFont(), 63F);
            this.titleLabel.Location = new System.Drawing.Point(115, 69);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(515, 117);
            this.titleLabel.TabIndex = 6;
            this.titleLabel.Text = "- SNAKE -";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuPanel);
            this.Name = "Menu";
            this.Size = new System.Drawing.Size(755, 480);
            this.menuPanel.ResumeLayout(false);
            this.menuPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.Label titleLabel;
        internal System.Windows.Forms.Label playLabel;
        private System.Windows.Forms.Label gameOverLabel;
        internal System.Windows.Forms.Label retryLabel;
        internal System.Windows.Forms.Label mainMenuLabel;
        private PersonalFont myFont;
    }
}
