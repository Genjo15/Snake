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
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private PersonalFont _Font;                                       // Font
        private delegate void processOnMenuThread(Boolean b, Boolean b2); // Delegate type.
        private processOnMenuThread _ConnectionEstablishedDel;            // Delegate for ConnectionEstablished method.
        private processOnMenuThread _GameOverDel;                         // Delegate for GameOver method.

        #endregion


        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public Menu()
        {
            InitializeComponent(); // Initialize component.
            InitializeFont();      // Initialize font.
            _ConnectionEstablishedDel = new processOnMenuThread(ConnectionEstablished); // Initialize the  ConnectionEstablished delegate.
            _GameOverDel = new processOnMenuThread(GameOver);                           // Initialize the  GameOver delegate.
        }

        #endregion


        /****************************************************** Methods ******************************************************/

        #region Methods

        internal void InitializeFont()
        {
            _Font = new PersonalFont();
            nicknameTextBox.Font = new System.Drawing.Font(_Font.getPersonalFont(), 16, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));    // Set the font for the nickname textbox.
            ipTextBox1.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));      // Set the font for the ip textbox (part 1).
            ipTextBox2.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));      // Set the font for the ip textbox (part 2).
            ipTextBox3.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));      // Set the font for the ip textbox (part 3).
            ipTextBox4.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));      // Set the font for the ip textbox (part 4).            
            highScoresName.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Set the font.
            highScoresScore.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Set the font.                        
            newHighScoreLabel.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Set the font.                        
        }

        internal void MainMenu()
        {
            this.playPictureBox.Visible = true;                   // Show playPictureBox.
            this.multiplayerPictureBox.Visible = true;            // Show multiplayerPictureBox.
            this.highScoresPictureBox.Visible = true;             // Show highScoresPictureBox;
            this.titlePictureBox.Visible = true;                  // Show titlePictureBox.
            this.gameOverPictureBox.Visible = false;              // Hide gameOverPictureBox.
            this.retryPictureBox.Visible = false;                 // Hide retryPictureBox.
            this.mainMenuPictureBox.Visible = false;              // Hide mainMenuPictureBox.
            this.multiplayerMenuPictureBox.Visible = false;       // Hide multiplayerMenuPictureBox.
            this.createGamePictureBox.Visible = false;            // Hide createGamePictureBox.
            this.joinGamePictureBox.Visible = false;              // Hide joinGamePictureBox.
            this.backPictureBox.Visible = false;                  // Hide backPictureBox.
            this.waitClientPictureBox.Visible = false;            // Hide waitClientPictureBox.
            this.connectionHostPictureBox.Visible = false;        // Hide connectionHostPictureBox.
            this.connectionEstablishedPictureBox.Visible = false; // Hide connectionEstablishedPictureBox.
            this.startGamePictureBox.Visible = false;             // Hide startGamePictureBox.
            this.enterIpPictureBox.Visible = false;               // Hide enterIpPictureBox.
            this.okPictureBox.Visible = false;                    // Hide okPictureBox.
            this.ipTextBox1.Visible = false;                      // Hide ipTextBox1.
            this.ipTextBox2.Visible = false;                      // Hide ipTextBox2.
            this.ipTextBox3.Visible = false;                      // Hide ipTextBox3.
            this.ipTextBox4.Visible = false;                      // Hide ipTextBo41.            
            this.highScoresName.Visible = false;                  // Hide highScoresLabel.             
            this.highScoresScore.Visible = false;                 // Hide highScoresLabel.                         
            this.nicknamePictureBox.Visible = true;               // Show nicknamePictureBox.
            this.nicknameTextBox.Visible = true;                  // Show nicknameTextBox.
            this.playPictureBox.Select();                         // Select the playPictureBox.
            this.winPictureBox.Visible = false;                   // Hide winPictureBox.
            this.loosePictureBox.Visible = false;                 // Hide loosePictureBox.
            this.newHighScoreLabel.Visible = false;               // Hide newHighScoreLabel
        }

        internal void InGame()
        {
            this.Visible = false; // Hide the menu.
        }

        internal void GameOver(Boolean multiplayer, Boolean victory)
        {
            this.Visible = true;
            this.playPictureBox.Visible = false;                  // Hide playPictureBox.
            this.multiplayerPictureBox.Visible = false;           // Hide multiplayerPictureBox.
            this.highScoresPictureBox.Visible = false;            // Hide highScoresPictureBox;
            this.titlePictureBox.Visible = false;                 // Hide titlePictureBox.
            this.mainMenuPictureBox.Visible = true;               // Show mainMenuPictureBox.
            this.multiplayerMenuPictureBox.Visible = false;       // Hide multiplayerMenuPictureBox.
            this.createGamePictureBox.Visible = false;            // Hide createGamePictureBox.
            this.joinGamePictureBox.Visible = false;              // Hide joinGamePictureBox.
            this.backPictureBox.Visible = false;                  // Hide backPictureBox.
            this.waitClientPictureBox.Visible = false;            // Hide waitClientPictureBox.
            this.connectionHostPictureBox.Visible = false;        // Hide connectionHostPictureBox.
            this.connectionEstablishedPictureBox.Visible = false; // Hide connectionEstablishedPictureBox.
            this.startGamePictureBox.Visible = false;             // Hide startGamePictureBox.
            this.enterIpPictureBox.Visible = false;               // Hide enterIpPictureBox.
            this.okPictureBox.Visible = false;                    // Hide okPictureBox.
            this.ipTextBox1.Visible = false;                      // Hide ipTextBox1.
            this.ipTextBox2.Visible = false;                      // Hide ipTextBox2.
            this.ipTextBox3.Visible = false;                      // Hide ipTextBox3.
            this.ipTextBox4.Visible = false;                      // Hide ipTextBox4.            
            this.highScoresName.Visible = false;                  // Hide highScoresLabel.            
            this.highScoresScore.Visible = false;                 // Hide highScoresLabel.                        
            this.nicknamePictureBox.Visible = false;              // Hide nicknamePictureBox.
            this.nicknameTextBox.Visible = false;                 // Hide nicknameTextBox.
            if (!multiplayer)
            {
                this.highScoresPictureBox.Visible = true;            // Hide highScoresPictureBox;
                this.newHighScoreLabel.Visible = true;             // Show newHighScoreLabel
                this.gameOverPictureBox.Visible = true;           // Show gameOverPictureBox.                
                this.retryPictureBox.Visible = true;              // Show retryPictureBox.
                this.winPictureBox.Visible = false;               // Hide winPictureBox.
                this.loosePictureBox.Visible = false;             // Hide loosePictureBox.
            }

            if (multiplayer)                                      ////
            {                                                     // Multiplayer mode :
                if(victory)                                       // if victory
                    this.winPictureBox.Visible = true;            // --> Show winPictureBox.
                else if(!victory)                                 // if defeat
                    this.loosePictureBox.Visible = true;          // --> Show loosePictureBox.
                this.gameOverPictureBox.Visible = false;          // Hide gameOverPictureBox.
                this.retryPictureBox.Visible = false;             // Hide retryPictureBox.
            }

                
        }

        internal void Multiplayer()
        {
            this.playPictureBox.Visible = false;                  // Hide playPictureBox.
            this.multiplayerPictureBox.Visible = false;           // Hide multiplayerPictureBox.
            this.highScoresPictureBox.Visible = false;            // Hide highScoresPictureBox;
            this.titlePictureBox.Visible = false;                 // Hide titlePictureBox.
            this.gameOverPictureBox.Visible = false;              // Hide gameOverPictureBox.
            this.retryPictureBox.Visible = false;                 // Hide retryPictureBox.
            this.mainMenuPictureBox.Visible = false;              // Hide mainMenuPictureBox.
            this.multiplayerMenuPictureBox.Visible = true;        // Show multiplayerMenuPictureBox.
            this.createGamePictureBox.Visible = true;             // Show createGamePictureBox.
            this.joinGamePictureBox.Visible = true;               // Show joinGamePictureBox.
            this.backPictureBox.Visible = true;                   // Show backPictureBox.
            this.waitClientPictureBox.Visible = false;            // Hide waitClientPictureBox.
            this.connectionHostPictureBox.Visible = false;        // Hide connectionHostPictureBox.
            this.connectionEstablishedPictureBox.Visible = false; // Hide connectionEstablishedPictureBox.
            this.startGamePictureBox.Visible = false;             // Hide startGamePictureBox.
            this.enterIpPictureBox.Visible = false;               // Hide enterIpPictureBox.
            this.okPictureBox.Visible = false;                    // Hide okPictureBox.
            this.ipTextBox1.Visible = false;                      // Hide ipTextBox1.
            this.ipTextBox2.Visible = false;                      // Hide ipTextBox2.
            this.ipTextBox3.Visible = false;                      // Hide ipTextBox3.
            this.ipTextBox4.Visible = false;                      // Hide ipTextBox4.            
            this.highScoresName.Visible = false;                  // Hide highScoresLabel.            
            this.highScoresScore.Visible = false;                 // Hide highScoresLabel.            
            this.nicknamePictureBox.Visible = false;              // Hide nicknamePictureBox.
            this.nicknameTextBox.Visible = false;                 // Hide nicknameTextBox.
            this.winPictureBox.Visible = false;                   // Hide winPictureBox.
            this.loosePictureBox.Visible = false;                 // Hide loosePictureBox.
            this.newHighScoreLabel.Visible = false;               // Hide newHighScoreLabel
        }

        internal void Host()
        {
            this.createGamePictureBox.Visible = false;            // Hide createGamePictureBox.
            this.joinGamePictureBox.Visible = false;              // Hide joinGamePictureBox.
            this.waitClientPictureBox.Visible = true;             // Show waitClientPictureBox
            this.connectionEstablishedPictureBox.Visible = false; // Hide connectionEstablishedPictureBox.
            this.startGamePictureBox.Visible = false;             // Hide startGamePictureBox.
            this.enterIpPictureBox.Visible = false;               // Hide enterIpPictureBox.
            this.okPictureBox.Visible = false;                    // Hide okPictureBox.
            this.ipTextBox1.Visible = false;                      // Hide ipTextBox1.
            this.ipTextBox2.Visible = false;                      // Hide ipTextBox2.
            this.ipTextBox3.Visible = false;                      // Hide ipTextBox3.
            this.ipTextBox4.Visible = false;                      // Hide ipTextBox4.
            this.highScoresName.Visible = false;                  // Hide highScoresLabel.            
            this.highScoresScore.Visible = false;                 // Hide highScoresLabel.            
            this.nicknamePictureBox.Visible = false;              // Hide nicknamePictureBox.
            this.nicknameTextBox.Visible = false;                 // Hide nicknameTextBox.
            this.winPictureBox.Visible = false;                   // Hide winPictureBox.
            this.loosePictureBox.Visible = false;                 // Hide loosePictureBox.
            this.newHighScoreLabel.Visible = false;               // Hide newHighScoreLabel
        }

        internal void Client1()
        {
            this.createGamePictureBox.Visible = false;            // Hide createGamePictureBox.
            this.joinGamePictureBox.Visible = false;              // Hide joinGamePictureBox.
            this.connectionHostPictureBox.Visible = false;        // Show connectionHostPictureBox.
            this.connectionEstablishedPictureBox.Visible = false; // Hide connectionEstablishedPictureBox.
            this.startGamePictureBox.Visible = false;             // Hide startGamePictureBox.
            this.enterIpPictureBox.Visible = true;                // Show enterIpPictureBox.
            this.enterIpPictureBox.SendToBack();                  // Send it to background.
            this.okPictureBox.Visible = true;                     // Show okPictureBox.
            this.ipTextBox1.Visible = true;                       // Show ipTextBox1.
            this.ipTextBox2.Visible = true;                       // Show ipTextBox2.
            this.ipTextBox3.Visible = true;                       // Show ipTextBox3.
            this.ipTextBox4.Visible = true;                       // Show ipTextBox4.            
            this.highScoresName.Visible = false;                  // Hide highScoresLabel.            
            this.highScoresScore.Visible = false;                 // Hide highScoresLabel.                        
            this.nicknamePictureBox.Visible = false;              // Hide nicknamePictureBox.
            this.nicknameTextBox.Visible = false;                 // Hide nicknameTextBox.
            this.winPictureBox.Visible = false;                   // Hide winPictureBox.
            this.loosePictureBox.Visible = false;                 // Hide loosePictureBox.
            this.newHighScoreLabel.Visible = false;               // Hide newHighScoreLabel
        }

        internal void Client2()
        {
            this.createGamePictureBox.Visible = false;            // Hide createGamePictureBox.
            this.joinGamePictureBox.Visible = false;              // Hide joinGamePictureBox.
            this.connectionHostPictureBox.Visible = true  ;       // Show connectionHostPictureBox.
            this.connectionEstablishedPictureBox.Visible = false; // Hide connectionEstablishedPictureBox.
            this.startGamePictureBox.Visible = false;             // Hide startGamePictureBox.
            this.enterIpPictureBox.Visible = false;               // Hide enterIpPictureBox.
            this.okPictureBox.Visible = false;                    // Hide okPictureBox.
            this.ipTextBox1.Visible = false;                      // Hide ipTextBox1.
            this.ipTextBox2.Visible = false;                      // Hide ipTextBox2.
            this.ipTextBox3.Visible = false;                      // Hide ipTextBox3.
            this.ipTextBox4.Visible = false;                      // Hide ipTextBox4.            
            this.highScoresName.Visible = false;                  // Hide highScoresLabel.            
            this.highScoresScore.Visible = false;                 // Hide highScoresLabel.                        
            this.nicknamePictureBox.Visible = false;              // Hide nicknamePictureBox.
            this.nicknameTextBox.Visible = false;                 // Hide nicknameTextBox.
            this.winPictureBox.Visible = false;                   // Hide winPictureBox.
            this.loosePictureBox.Visible = false;                 // Hide loosePictureBox.
            this.newHighScoreLabel.Visible = false;               // Hide newHighScoreLabel
        }

        internal void HighScoreShow()
        {
            this.playPictureBox.Visible = false;                  // Hide playPictureBox.
            this.multiplayerPictureBox.Visible = false;           // Hide multiplayerPictureBox.
            this.highScoresPictureBox.Visible = false;            // Hide highScoresPictureBox;
            this.titlePictureBox.Visible = false;                 // Hide titlePictureBox.
            this.gameOverPictureBox.Visible = false;              // Hide gameOverPictureBox.
            this.retryPictureBox.Visible = false;                 // Hide retryPictureBox.
            this.mainMenuPictureBox.Visible = true;               // Show mainMenuPictureBox.
            this.multiplayerMenuPictureBox.Visible = false;       // Hide multiplayerMenuPictureBox.
            this.createGamePictureBox.Visible = false;            // Hide createGamePictureBox.
            this.joinGamePictureBox.Visible = false;              // Hide joinGamePictureBox.
            this.backPictureBox.Visible = false;                  // Hide backPictureBox.
            this.waitClientPictureBox.Visible = false;            // Hide waitClientPictureBox.
            this.connectionHostPictureBox.Visible = false;        // Hide connectionHostPictureBox.
            this.connectionEstablishedPictureBox.Visible = false; // Hide connectionEstablishedPictureBox.
            this.startGamePictureBox.Visible = false;             // Hide startGamePictureBox.
            this.enterIpPictureBox.Visible = false;               // Hide enterIpPictureBox.
            this.okPictureBox.Visible = false;                    // Hide okPictureBox.
            this.ipTextBox1.Visible = false;                      // Hide ipTextBox1.
            this.ipTextBox2.Visible = false;                      // Hide ipTextBox2.
            this.ipTextBox3.Visible = false;                      // Hide ipTextBox3.
            this.ipTextBox4.Visible = false;                      // Hide ipTextBox4.            
            this.highScoresName.Visible = true;                   // Show highScoresLabel.            
            this.highScoresScore.Visible = true;                 // Show highScoresLabel.            
            this.nicknamePictureBox.Visible = false;              // Hide nicknamePictureBox.
            this.nicknameTextBox.Visible = false;                 // Hide nicknameTextBox.
            this.winPictureBox.Visible = false;                   // Hide winPictureBox.
            this.loosePictureBox.Visible = false;                 // Hide loosePictureBox.
            this.newHighScoreLabel.Visible = false;               // Hide newHighScoreLabel
        }

        private void ConnectionEstablished(Boolean isHost, Boolean multiplayer)
        {
 	        this.connectionHostPictureBox.Visible = false;       // Hide connectionHostPictureBox.
            this.waitClientPictureBox.Visible = false;           // Hide waitClientPictureBox
            if (multiplayer)
            {                                                        // If multiplayer mode
                this.connectionEstablishedPictureBox.Visible = true; //     Show connectionEstablishedPictureBox.
                if (isHost)                                          //     if host :
                    this.startGamePictureBox.Visible = true;         //        Show startGamePictureBox (if host).
            }
            this.enterIpPictureBox.Visible = false;              // Hide enterIpPictureBox.
            this.okPictureBox.Visible = false;                   // Hide okPictureBox.
            this.ipTextBox1.Visible = false;                     // Hide ipTextBox1.
            this.ipTextBox2.Visible = false;                     // Hide ipTextBox2.
            this.ipTextBox3.Visible = false;                     // Hide ipTextBox3.
            this.ipTextBox4.Visible = false;                     // Hide ipTextBox4.
            this.nicknamePictureBox.Visible = false;             // Hide nicknamePictureBox.
            this.nicknameTextBox.Visible = false;                // Hide nicknameTextBox.
            this.winPictureBox.Visible = false;                  // Hide winPictureBox.
            this.loosePictureBox.Visible = false;                // Hide loosePictureBox.
        }

        #endregion

        #region Accessors

        ////////////////////////////////
        // Get ConnectionEstablishedDel

        public Delegate Get_ConnectionEstablishedDel()
        {
            return _ConnectionEstablishedDel;
        }

        ////////////////////
        // Get_GameOverDel

        public Delegate Get_GameOverDel()
        {
            return _GameOverDel;
        }

        #endregion

    }
}
