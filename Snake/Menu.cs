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
            this.playPictureBox.Visible = true;      // Show playPictureBox.
            this.titlePictureBox.Visible = true;     // Show titlePictureBox.
            this.gameOverPictureBox.Visible = false; // Hide gameOverPictureBox.
            this.retryPictureBox.Visible = false;    // Hide retryPictureBox.
            this.mainMenuPictureBox.Visible = false; // Hide mainMenuPictureBox.
        }

        internal void InGame()
        {
            this.playPictureBox.Visible = false;     // Hide playPictureBox.
            this.titlePictureBox.Visible = false;    // Hide titlePictureBox.
            this.gameOverPictureBox.Visible = false; // Hide gameOverPictureBox.
            this.retryPictureBox.Visible = false;    // Hide retryPictureBox.
            this.mainMenuPictureBox.Visible = false; // Hide mainMenuPictureBox.
            this.Visible = false;
        }

        internal void GameOver()
        {
            this.playPictureBox.Visible = false;    // Hide playPictureBox.
            this.titlePictureBox.Visible = false;   // Hide titlePictureBox.
            this.gameOverPictureBox.Visible = true; // Show gameOverPictureBox.
            this.retryPictureBox.Visible = true;    // Show retryPictureBox.
            this.mainMenuPictureBox.Visible = true; // Show mainMenuPictureBox.
        }

        #endregion
    }
}
