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
            this.startGamePictureBox = new System.Windows.Forms.PictureBox();
            this.connectionEstablishedPictureBox = new System.Windows.Forms.PictureBox();
            this.connectionHostPictureBox = new System.Windows.Forms.PictureBox();
            this.waitClientPictureBox = new System.Windows.Forms.PictureBox();
            this.backPictureBox = new System.Windows.Forms.PictureBox();
            this.joinGamePictureBox = new System.Windows.Forms.PictureBox();
            this.createGamePictureBox = new System.Windows.Forms.PictureBox();
            this.multiplayerMenuPictureBox = new System.Windows.Forms.PictureBox();
            this.multiplayerPictureBox = new System.Windows.Forms.PictureBox();
            this.highScoresPictureBox = new System.Windows.Forms.PictureBox();
            this.mainMenuPictureBox = new System.Windows.Forms.PictureBox();
            this.retryPictureBox = new System.Windows.Forms.PictureBox();
            this.gameOverPictureBox = new System.Windows.Forms.PictureBox();
            this.playPictureBox = new System.Windows.Forms.PictureBox();
            this.titlePictureBox = new System.Windows.Forms.PictureBox();
            this.menuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startGamePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectionEstablishedPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectionHostPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waitClientPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.joinGamePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.createGamePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.multiplayerMenuPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.multiplayerPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.highScoresPictureBox)).BeginInit();
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
            this.menuPanel.Controls.Add(this.startGamePictureBox);
            this.menuPanel.Controls.Add(this.connectionEstablishedPictureBox);
            this.menuPanel.Controls.Add(this.connectionHostPictureBox);
            this.menuPanel.Controls.Add(this.waitClientPictureBox);
            this.menuPanel.Controls.Add(this.backPictureBox);
            this.menuPanel.Controls.Add(this.joinGamePictureBox);
            this.menuPanel.Controls.Add(this.createGamePictureBox);
            this.menuPanel.Controls.Add(this.multiplayerMenuPictureBox);
            this.menuPanel.Controls.Add(this.multiplayerPictureBox);
            this.menuPanel.Controls.Add(this.highScoresPictureBox);
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
            // startGamePictureBox
            // 
            this.startGamePictureBox.Image = global::Snake.Properties.Resources.StartGame;
            this.startGamePictureBox.Location = new System.Drawing.Point(320, 252);
            this.startGamePictureBox.Name = "startGamePictureBox";
            this.startGamePictureBox.Size = new System.Drawing.Size(164, 43);
            this.startGamePictureBox.TabIndex = 27;
            this.startGamePictureBox.TabStop = false;
            // 
            // connectionEstablishedPictureBox
            // 
            this.connectionEstablishedPictureBox.Image = global::Snake.Properties.Resources.ConnectionEstablished;
            this.connectionEstablishedPictureBox.Location = new System.Drawing.Point(234, 211);
            this.connectionEstablishedPictureBox.Name = "connectionEstablishedPictureBox";
            this.connectionEstablishedPictureBox.Size = new System.Drawing.Size(324, 43);
            this.connectionEstablishedPictureBox.TabIndex = 26;
            this.connectionEstablishedPictureBox.TabStop = false;
            // 
            // connectionHostPictureBox
            // 
            this.connectionHostPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("connectionHostPictureBox.Image")));
            this.connectionHostPictureBox.Location = new System.Drawing.Point(232, 241);
            this.connectionHostPictureBox.Name = "connectionHostPictureBox";
            this.connectionHostPictureBox.Size = new System.Drawing.Size(337, 40);
            this.connectionHostPictureBox.TabIndex = 25;
            this.connectionHostPictureBox.TabStop = false;
            // 
            // waitClientPictureBox
            // 
            this.waitClientPictureBox.Image = global::Snake.Properties.Resources.WaitClient;
            this.waitClientPictureBox.Location = new System.Drawing.Point(261, 245);
            this.waitClientPictureBox.Name = "waitClientPictureBox";
            this.waitClientPictureBox.Size = new System.Drawing.Size(274, 33);
            this.waitClientPictureBox.TabIndex = 23;
            this.waitClientPictureBox.TabStop = false;
            // 
            // backPictureBox
            // 
            this.backPictureBox.Image = global::Snake.Properties.Resources.Back;
            this.backPictureBox.Location = new System.Drawing.Point(365, 322);
            this.backPictureBox.Name = "backPictureBox";
            this.backPictureBox.Size = new System.Drawing.Size(66, 33);
            this.backPictureBox.TabIndex = 22;
            this.backPictureBox.TabStop = false;
            // 
            // joinGamePictureBox
            // 
            this.joinGamePictureBox.Image = global::Snake.Properties.Resources.JoinGame;
            this.joinGamePictureBox.Location = new System.Drawing.Point(332, 260);
            this.joinGamePictureBox.Name = "joinGamePictureBox";
            this.joinGamePictureBox.Size = new System.Drawing.Size(135, 38);
            this.joinGamePictureBox.TabIndex = 21;
            this.joinGamePictureBox.TabStop = false;
            // 
            // createGamePictureBox
            // 
            this.createGamePictureBox.Image = global::Snake.Properties.Resources.CreateGame;
            this.createGamePictureBox.Location = new System.Drawing.Point(313, 223);
            this.createGamePictureBox.Name = "createGamePictureBox";
            this.createGamePictureBox.Size = new System.Drawing.Size(168, 38);
            this.createGamePictureBox.TabIndex = 20;
            this.createGamePictureBox.TabStop = false;
            // 
            // multiplayerMenuPictureBox
            // 
            this.multiplayerMenuPictureBox.Image = global::Snake.Properties.Resources.MultiplayerMenu;
            this.multiplayerMenuPictureBox.Location = new System.Drawing.Point(283, 143);
            this.multiplayerMenuPictureBox.Name = "multiplayerMenuPictureBox";
            this.multiplayerMenuPictureBox.Size = new System.Drawing.Size(243, 65);
            this.multiplayerMenuPictureBox.TabIndex = 19;
            this.multiplayerMenuPictureBox.TabStop = false;
            // 
            // multiplayerPictureBox
            // 
            this.multiplayerPictureBox.Image = global::Snake.Properties.Resources.Multiplayer;
            this.multiplayerPictureBox.Location = new System.Drawing.Point(315, 265);
            this.multiplayerPictureBox.Name = "multiplayerPictureBox";
            this.multiplayerPictureBox.Size = new System.Drawing.Size(151, 38);
            this.multiplayerPictureBox.TabIndex = 18;
            this.multiplayerPictureBox.TabStop = false;
            // 
            // highScoresPictureBox
            // 
            this.highScoresPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("highScoresPictureBox.Image")));
            this.highScoresPictureBox.Location = new System.Drawing.Point(312, 305);
            this.highScoresPictureBox.Name = "highScoresPictureBox";
            this.highScoresPictureBox.Size = new System.Drawing.Size(159, 35);
            this.highScoresPictureBox.TabIndex = 17;
            this.highScoresPictureBox.TabStop = false;
            // 
            // mainMenuPictureBox
            // 
            this.mainMenuPictureBox.Image = global::Snake.Properties.Resources.MainMenu;
            this.mainMenuPictureBox.Location = new System.Drawing.Point(292, 260);
            this.mainMenuPictureBox.Name = "mainMenuPictureBox";
            this.mainMenuPictureBox.Size = new System.Drawing.Size(189, 54);
            this.mainMenuPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainMenuPictureBox.TabIndex = 15;
            this.mainMenuPictureBox.TabStop = false;
            // 
            // retryPictureBox
            // 
            this.retryPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("retryPictureBox.Image")));
            this.retryPictureBox.Location = new System.Drawing.Point(347, 207);
            this.retryPictureBox.Name = "retryPictureBox";
            this.retryPictureBox.Size = new System.Drawing.Size(86, 54);
            this.retryPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.retryPictureBox.TabIndex = 14;
            this.retryPictureBox.TabStop = false;
            // 
            // gameOverPictureBox
            // 
            this.gameOverPictureBox.Image = global::Snake.Properties.Resources.GameOver;
            this.gameOverPictureBox.Location = new System.Drawing.Point(215, 102);
            this.gameOverPictureBox.Name = "gameOverPictureBox";
            this.gameOverPictureBox.Size = new System.Drawing.Size(343, 99);
            this.gameOverPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.gameOverPictureBox.TabIndex = 13;
            this.gameOverPictureBox.TabStop = false;
            // 
            // playPictureBox
            // 
            this.playPictureBox.Image = global::Snake.Properties.Resources.Play;
            this.playPictureBox.Location = new System.Drawing.Point(360, 217);
            this.playPictureBox.Name = "playPictureBox";
            this.playPictureBox.Size = new System.Drawing.Size(59, 42);
            this.playPictureBox.TabIndex = 12;
            this.playPictureBox.TabStop = false;
            // 
            // titlePictureBox
            // 
            this.titlePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("titlePictureBox.Image")));
            this.titlePictureBox.Location = new System.Drawing.Point(157, 84);
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
            ((System.ComponentModel.ISupportInitialize)(this.startGamePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectionEstablishedPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectionHostPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waitClientPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.joinGamePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.createGamePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.multiplayerMenuPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.multiplayerPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.highScoresPictureBox)).EndInit();
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
        internal PictureBox highScoresPictureBox;
        internal PictureBox multiplayerPictureBox;
        private PictureBox multiplayerMenuPictureBox;
        internal PictureBox createGamePictureBox;
        internal PictureBox backPictureBox;
        internal PictureBox waitClientPictureBox;
        internal PictureBox connectionHostPictureBox;
        internal PictureBox joinGamePictureBox;
        internal PictureBox connectionEstablishedPictureBox;
        internal PictureBox startGamePictureBox;
    }
}
