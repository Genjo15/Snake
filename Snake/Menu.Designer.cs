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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.menuPanel = new System.Windows.Forms.Panel();
            this.mainMenuPictureBox = new System.Windows.Forms.PictureBox();
            this.retryPictureBox = new System.Windows.Forms.PictureBox();
            this.gameOverPictureBox = new System.Windows.Forms.PictureBox();
            this.playPictureBox = new System.Windows.Forms.PictureBox();
            this.titlePictureBox = new System.Windows.Forms.PictureBox();
            this.menuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenuPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameOverPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.titlePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.menuPanel.Controls.Add(this.mainMenuPictureBox);
            this.menuPanel.Controls.Add(this.retryPictureBox);
            this.menuPanel.Controls.Add(this.gameOverPictureBox);
            this.menuPanel.Controls.Add(this.playPictureBox);
            this.menuPanel.Controls.Add(this.titlePictureBox);
            this.menuPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Margin = new System.Windows.Forms.Padding(2);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(756, 476);
            this.menuPanel.TabIndex = 0;
            // 
            // mainMenuPictureBox
            // 
            this.mainMenuPictureBox.Image = global::Snake.Properties.Resources.MainMenu;
            this.mainMenuPictureBox.Location = new System.Drawing.Point(272, 240);
            this.mainMenuPictureBox.Name = "mainMenuPictureBox";
            this.mainMenuPictureBox.Size = new System.Drawing.Size(189, 54);
            this.mainMenuPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainMenuPictureBox.TabIndex = 15;
            this.mainMenuPictureBox.TabStop = false;
            // 
            // retryPictureBox
            // 
            this.retryPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("retryPictureBox.Image")));
            this.retryPictureBox.Location = new System.Drawing.Point(327, 187);
            this.retryPictureBox.Name = "retryPictureBox";
            this.retryPictureBox.Size = new System.Drawing.Size(86, 54);
            this.retryPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.retryPictureBox.TabIndex = 14;
            this.retryPictureBox.TabStop = false;
            // 
            // gameOverPictureBox
            // 
            this.gameOverPictureBox.Image = global::Snake.Properties.Resources.GameOver;
            this.gameOverPictureBox.Location = new System.Drawing.Point(195, 82);
            this.gameOverPictureBox.Name = "gameOverPictureBox";
            this.gameOverPictureBox.Size = new System.Drawing.Size(343, 99);
            this.gameOverPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.gameOverPictureBox.TabIndex = 13;
            this.gameOverPictureBox.TabStop = false;
            // 
            // playPictureBox
            // 
            this.playPictureBox.Image = global::Snake.Properties.Resources.Play;
            this.playPictureBox.Location = new System.Drawing.Point(318, 181);
            this.playPictureBox.Name = "playPictureBox";
            this.playPictureBox.Size = new System.Drawing.Size(89, 72);
            this.playPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playPictureBox.TabIndex = 12;
            this.playPictureBox.TabStop = false;
            // 
            // titlePictureBox
            // 
            this.titlePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("titlePictureBox.Image")));
            this.titlePictureBox.Location = new System.Drawing.Point(137, 64);
            this.titlePictureBox.Name = "titlePictureBox";
            this.titlePictureBox.Size = new System.Drawing.Size(459, 125);
            this.titlePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.titlePictureBox.TabIndex = 11;
            this.titlePictureBox.TabStop = false;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.menuPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Menu";
            this.Size = new System.Drawing.Size(758, 478);
            this.menuPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainMenuPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameOverPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.titlePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menuPanel;
        internal PictureBox titlePictureBox;
        internal PictureBox playPictureBox;
        private PictureBox gameOverPictureBox;
        internal PictureBox retryPictureBox;
        internal PictureBox mainMenuPictureBox;
    }
}
