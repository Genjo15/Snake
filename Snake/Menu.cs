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

        /*
         * Construtor of the menu
         *      - Initialize Components/variables.
         * */

        public Menu()
        {
            InitializeComponent(); 
            InitializeFont();      
            _ConnectionEstablishedDel = new processOnMenuThread(ConnectionEstablished); 
            _GameOverDel = new processOnMenuThread(GameOver);                           
        }

        #endregion


        /****************************************************** Methods ******************************************************/

        #region Methods

        /*
         * Method for initialiazing fonts
         *      - Instanciate font.
         *      - Apply font.
         * */

        internal void InitializeFont()
        {
            _Font = new PersonalFont();
            nicknameTextBox.Font = new System.Drawing.Font(_Font.getPersonalFont(), 16, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));    
            ipTextBox1.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));      
            ipTextBox2.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));      
            ipTextBox3.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));      
            ipTextBox4.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));               
            highScoresName.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); 
            highScoresScore.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); 
            newHighScoreLabel.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        internal void MainMenu()
        {
            this.playPictureBox.Visible = true;                   
            this.multiplayerPictureBox.Visible = true;            
            this.highScoresPictureBox.Visible = true;             
            this.titlePictureBox.Visible = true;                  
            this.gameOverPictureBox.Visible = false;              
            this.retryPictureBox.Visible = false;                 
            this.mainMenuPictureBox.Visible = false;              
            this.multiplayerMenuPictureBox.Visible = false;       
            this.createGamePictureBox.Visible = false;            
            this.joinGamePictureBox.Visible = false;              
            this.backPictureBox.Visible = false;                  
            this.waitClientPictureBox.Visible = false;            
            this.connectionHostPictureBox.Visible = false;        
            this.connectionEstablishedPictureBox.Visible = false; 
            this.startGamePictureBox.Visible = false;             
            this.enterIpPictureBox.Visible = false;               
            this.okPictureBox.Visible = false;                    
            this.ipTextBox1.Visible = false;                      
            this.ipTextBox2.Visible = false;                      
            this.ipTextBox3.Visible = false;                      
            this.ipTextBox4.Visible = false;                      
            this.highScoresName.Visible = false;                  
            this.highScoresScore.Visible = false;                 
            this.nicknamePictureBox.Visible = true;               
            this.nicknameTextBox.Visible = true;                  
            this.playPictureBox.Select();                         
            this.winPictureBox.Visible = false;                   
            this.loosePictureBox.Visible = false;                 
            this.newHighScoreLabel.Visible = false;               
        }

        internal void InGame()
        {
            this.Visible = false; 
        }

        internal void GameOver(Boolean multiplayer, Boolean victory)
        {
            this.Visible = true;
            this.playPictureBox.Visible = false;                  
            this.multiplayerPictureBox.Visible = false;           
            this.highScoresPictureBox.Visible = false;            
            this.titlePictureBox.Visible = false;                 
            this.mainMenuPictureBox.Visible = true;               
            this.multiplayerMenuPictureBox.Visible = false;       
            this.createGamePictureBox.Visible = false;            
            this.joinGamePictureBox.Visible = false;              
            this.backPictureBox.Visible = false;                  
            this.waitClientPictureBox.Visible = false;            
            this.connectionHostPictureBox.Visible = false;        
            this.connectionEstablishedPictureBox.Visible = false; 
            this.startGamePictureBox.Visible = false;             
            this.enterIpPictureBox.Visible = false;               
            this.okPictureBox.Visible = false;                    
            this.ipTextBox1.Visible = false;                      
            this.ipTextBox2.Visible = false;                      
            this.ipTextBox3.Visible = false;                      
            this.ipTextBox4.Visible = false;                      
            this.highScoresName.Visible = false;                  
            this.highScoresScore.Visible = false;                     
            this.nicknamePictureBox.Visible = false;              
            this.nicknameTextBox.Visible = false;                 
            if (!multiplayer)
            {
                this.highScoresPictureBox.Visible = true;        
                this.newHighScoreLabel.Visible = true;           
                this.gameOverPictureBox.Visible = true;          
                this.retryPictureBox.Visible = true;             
                this.winPictureBox.Visible = false;              
                this.loosePictureBox.Visible = false;            
            }

            if (multiplayer)                                     
            {                                                    
                if(victory)                                      
                    this.winPictureBox.Visible = true;           
                else if(!victory)                                
                    this.loosePictureBox.Visible = true;         
                this.gameOverPictureBox.Visible = false;         
                this.retryPictureBox.Visible = false;            
            }              
        }

        internal void Multiplayer()
        {
            this.playPictureBox.Visible = false;                  
            this.multiplayerPictureBox.Visible = false;           
            this.highScoresPictureBox.Visible = false;            
            this.titlePictureBox.Visible = false;                 
            this.gameOverPictureBox.Visible = false;              
            this.retryPictureBox.Visible = false;                 
            this.mainMenuPictureBox.Visible = false;              
            this.multiplayerMenuPictureBox.Visible = true;        
            this.createGamePictureBox.Visible = true;             
            this.joinGamePictureBox.Visible = true;               
            this.backPictureBox.Visible = true;                   
            this.waitClientPictureBox.Visible = false;            
            this.connectionHostPictureBox.Visible = false;        
            this.connectionEstablishedPictureBox.Visible = false; 
            this.startGamePictureBox.Visible = false;             
            this.enterIpPictureBox.Visible = false;               
            this.okPictureBox.Visible = false;                    
            this.ipTextBox1.Visible = false;                      
            this.ipTextBox2.Visible = false;                      
            this.ipTextBox3.Visible = false;                      
            this.ipTextBox4.Visible = false;                      
            this.highScoresName.Visible = false;                  
            this.highScoresScore.Visible = false;                 
            this.nicknamePictureBox.Visible = false;              
            this.nicknameTextBox.Visible = false;                 
            this.winPictureBox.Visible = false;                   
            this.loosePictureBox.Visible = false;                 
            this.newHighScoreLabel.Visible = false;               
        }

        internal void Host()
        {
            this.createGamePictureBox.Visible = false;            
            this.joinGamePictureBox.Visible = false;              
            this.waitClientPictureBox.Visible = true;             
            this.connectionEstablishedPictureBox.Visible = false; 
            this.startGamePictureBox.Visible = false;             
            this.enterIpPictureBox.Visible = false;               
            this.okPictureBox.Visible = false;                    
            this.ipTextBox1.Visible = false;                      
            this.ipTextBox2.Visible = false;                      
            this.ipTextBox3.Visible = false;                      
            this.ipTextBox4.Visible = false;                      
            this.highScoresName.Visible = false;                  
            this.highScoresScore.Visible = false;                 
            this.nicknamePictureBox.Visible = false;              
            this.nicknameTextBox.Visible = false;                 
            this.winPictureBox.Visible = false;                   
            this.loosePictureBox.Visible = false;                 
            this.newHighScoreLabel.Visible = false;               
        }

        internal void Client1()
        {
            this.createGamePictureBox.Visible = false;            
            this.joinGamePictureBox.Visible = false;              
            this.connectionHostPictureBox.Visible = false;        
            this.connectionEstablishedPictureBox.Visible = false; 
            this.startGamePictureBox.Visible = false;             
            this.enterIpPictureBox.Visible = true;                
            this.enterIpPictureBox.SendToBack();                  
            this.okPictureBox.Visible = true;                     
            this.ipTextBox1.Visible = true;                       
            this.ipTextBox2.Visible = true;                       
            this.ipTextBox3.Visible = true;                       
            this.ipTextBox4.Visible = true;                       
            this.highScoresName.Visible = false;                  
            this.highScoresScore.Visible = false;                      
            this.nicknamePictureBox.Visible = false;              
            this.nicknameTextBox.Visible = false;                 
            this.winPictureBox.Visible = false;                   
            this.loosePictureBox.Visible = false;                 
            this.newHighScoreLabel.Visible = false;               
        }

        internal void Client2()
        {
            this.createGamePictureBox.Visible = false;            
            this.joinGamePictureBox.Visible = false;              
            this.connectionHostPictureBox.Visible = true  ;       
            this.connectionEstablishedPictureBox.Visible = false; 
            this.startGamePictureBox.Visible = false;             
            this.enterIpPictureBox.Visible = false;               
            this.okPictureBox.Visible = false;                    
            this.ipTextBox1.Visible = false;                      
            this.ipTextBox2.Visible = false;                      
            this.ipTextBox3.Visible = false;                      
            this.ipTextBox4.Visible = false;                      
            this.highScoresName.Visible = false;                  
            this.highScoresScore.Visible = false;                    
            this.nicknamePictureBox.Visible = false;              
            this.nicknameTextBox.Visible = false;                 
            this.winPictureBox.Visible = false;                   
            this.loosePictureBox.Visible = false;                 
            this.newHighScoreLabel.Visible = false;               
        }

        internal void HighScoreShow()
        {
            this.playPictureBox.Visible = false;                  
            this.multiplayerPictureBox.Visible = false;           
            this.highScoresPictureBox.Visible = false;            
            this.titlePictureBox.Visible = false;                 
            this.gameOverPictureBox.Visible = false;              
            this.retryPictureBox.Visible = false;                 
            this.mainMenuPictureBox.Visible = true;               
            this.multiplayerMenuPictureBox.Visible = false;       
            this.createGamePictureBox.Visible = false;            
            this.joinGamePictureBox.Visible = false;              
            this.backPictureBox.Visible = false;                  
            this.waitClientPictureBox.Visible = false;            
            this.connectionHostPictureBox.Visible = false;        
            this.connectionEstablishedPictureBox.Visible = false; 
            this.startGamePictureBox.Visible = false;             
            this.enterIpPictureBox.Visible = false;               
            this.okPictureBox.Visible = false;                    
            this.ipTextBox1.Visible = false;                      
            this.ipTextBox2.Visible = false;                      
            this.ipTextBox3.Visible = false;                      
            this.ipTextBox4.Visible = false;                      
            this.highScoresName.Visible = true;                   
            this.highScoresScore.Visible = true;                 
            this.nicknamePictureBox.Visible = false;              
            this.nicknameTextBox.Visible = false;                 
            this.winPictureBox.Visible = false;                   
            this.loosePictureBox.Visible = false;                 
            this.newHighScoreLabel.Visible = false;               
        }

        private void ConnectionEstablished(Boolean isHost, Boolean multiplayer)
        {
 	        this.connectionHostPictureBox.Visible = false;      
            this.waitClientPictureBox.Visible = false;          
            if (multiplayer)
            {                                                       
                this.connectionEstablishedPictureBox.Visible = true;
                if (isHost)                                         
                    this.startGamePictureBox.Visible = true;        
            }
            this.enterIpPictureBox.Visible = false;              
            this.okPictureBox.Visible = false;                   
            this.ipTextBox1.Visible = false;                     
            this.ipTextBox2.Visible = false;                     
            this.ipTextBox3.Visible = false;                     
            this.ipTextBox4.Visible = false;                     
            this.nicknamePictureBox.Visible = false;             
            this.nicknameTextBox.Visible = false;                
            this.winPictureBox.Visible = false;                  
            this.loosePictureBox.Visible = false;                
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
