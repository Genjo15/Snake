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
            this.playLabel.Visible = true;      // Show playLabel.
            this.titleLabel.Visible = true;     // Show titleLabel.
            this.gameOverLabel.Visible = false; // Hide gameOverLabel.
            this.retryLabel.Visible = false;    // Hide retryLabel.
            this.mainMenuLabel.Visible = false; // Hide mainMenuLabel.
        }

        internal void InGame()
        {
            this.playLabel.Visible = false;     // Hide playLabel.
            this.titleLabel.Visible = false;    // Hide titleLabel.
            this.gameOverLabel.Visible = false; // Hide gameOverLabel.
            this.retryLabel.Visible = false;    // Hide retryLabel.
            this.mainMenuLabel.Visible = false; // Hide mainMenuLabel.
            this.Visible = false;
        }

        internal void GameOver()
        {
            this.playLabel.Visible = false;    // Hide playLabel.
            this.titleLabel.Visible = false;   // Hide titleLabel.
            this.gameOverLabel.Visible = true; // Show gameOverLabel.
            this.retryLabel.Visible = true;    // Show retryLabel.
            this.mainMenuLabel.Visible = true; // Show mainMenuLabel.
        }

        #endregion
    }
}
