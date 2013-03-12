using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public partial class Menu : UserControl
    {
        #region Constructor

        public Menu()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        internal void MainMenu()
        {
            this.playPictureBox.Visible = true;             // Show playPictureBox.
            this.multiplayerPictureBox.Visible = true;      // Show multiplayerPictureBox.
            this.highScoresPictureBox.Visible = true;       // Show highScoresPictureBox;
            this.titlePictureBox.Visible = true;            // Show titlePictureBox.
            this.gameOverPictureBox.Visible = false;        // Hide gameOverPictureBox.
            this.retryPictureBox.Visible = false;           // Hide retryPictureBox.
            this.mainMenuPictureBox.Visible = false;        // Hide mainMenuPictureBox.
            this.multiplayerMenuPictureBox.Visible = false; // Hide multiplayerMenuPictureBox.
            this.createGamePictureBox.Visible = false;      // Hide createGamePictureBox.
            this.joinGamePictureBox.Visible = false;        // Hide joinGamePictureBox.
            this.backPictureBox.Visible = false;            // Hide backPictureBox.
            this.waitClientPictureBox.Visible = false;      // Hide waitClientPictureBox.
            this.connectionHostPictureBox.Visible = false;  // Hide connectionHostPictureBox.
            this.back2PictureBox.Visible = false;           // Hide back2PictureBox.
        }

        internal void InGame()
        {
            this.Visible = false;
        }

        internal void GameOver()
        {
            this.playPictureBox.Visible = false;            // Hide playPictureBox.
            this.multiplayerPictureBox.Visible = false;     // Hide multiplayerPictureBox.
            this.highScoresPictureBox.Visible = false;      // Hide highScoresPictureBox;
            this.titlePictureBox.Visible = false;           // Hide titlePictureBox.
            this.gameOverPictureBox.Visible = true;         // Show gameOverPictureBox.
            this.retryPictureBox.Visible = true;            // Show retryPictureBox.
            this.mainMenuPictureBox.Visible = true;         // Show mainMenuPictureBox.
            this.multiplayerMenuPictureBox.Visible = false; // Hide multiplayerMenuPictureBox.
            this.createGamePictureBox.Visible = false;      // Hide createGamePictureBox.
            this.joinGamePictureBox.Visible = false;        // Hide joinGamePictureBox.
            this.backPictureBox.Visible = false;            // Hide backPictureBox.
            this.waitClientPictureBox.Visible = false;      // Hide waitClientPictureBox.
            this.connectionHostPictureBox.Visible = false;  // Hide connectionHostPictureBox.
            this.back2PictureBox.Visible = false;           // Hide back2PictureBox.
        }

        internal void Multiplayer()
        {
            this.playPictureBox.Visible = false;           // Hide playPictureBox.
            this.multiplayerPictureBox.Visible = false;    // Hide multiplayerPictureBox.
            this.highScoresPictureBox.Visible = false;     // Hide highScoresPictureBox;
            this.titlePictureBox.Visible = false;          // Hide titlePictureBox.
            this.gameOverPictureBox.Visible = false;       // Hide gameOverPictureBox.
            this.retryPictureBox.Visible = false;          // Hide retryPictureBox.
            this.mainMenuPictureBox.Visible = false;       // Hide mainMenuPictureBox.
            this.multiplayerMenuPictureBox.Visible = true; // Show multiplayerMenuPictureBox.
            this.createGamePictureBox.Visible = true;      // Show createGamePictureBox.
            this.joinGamePictureBox.Visible = true;        // Show joinGamePictureBox.
            this.backPictureBox.Visible = true;            // Show backPictureBox.
            this.waitClientPictureBox.Visible = false;     // Hide waitClientPictureBox.
            this.connectionHostPictureBox.Visible = false; // Hide connectionHostPictureBox.
            this.back2PictureBox.Visible = false;          // Hide back2PictureBox.
        }

        internal void Host()
        {
            this.createGamePictureBox.Visible = false;      // Hide createGamePictureBox.
            this.joinGamePictureBox.Visible = false;        // Hide joinGamePictureBox.
            this.backPictureBox.Visible = false;            // Hide backPictureBox.
            this.waitClientPictureBox.Visible = true;       // Show waitClientPictureBox
            this.back2PictureBox.Visible = true;            // Show back2PictureBox.
        }

        internal void Client()
        {
            this.createGamePictureBox.Visible = false;      // Hide createGamePictureBox.
            this.joinGamePictureBox.Visible = false;        // Hide joinGamePictureBox.
            this.backPictureBox.Visible = false;            // Hide backPictureBox.
            this.connectionHostPictureBox.Visible = true  ; // Show connectionHostPictureBox.
            this.back2PictureBox.Visible = true;            // Show back2PictureBox.
        }

        #endregion
    }
}
