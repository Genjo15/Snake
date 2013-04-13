using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Snake;


namespace Snake
{
    public partial class Game : Form
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        ///////////////////////
        // Common variables :

        private FullSnake _FullSnake;                  // The complete snake (which is a list of snake elements).
        private Fruit _Fruit;                          // A fruit.
        private Insect _Insect;                        // An insect.
        private String _Nickname;                      // The nickname.
        private Timer _Timer;                          // The timer.
        private Timer _InsectTimer;                    // The timer for the fruit.
        private Boolean _InsectIsPresent;              // Boolean which indicates if the insect is present or not.
        private Boolean _GameOver;                     // Boolean which detects if the game is over or not.
        private Menu _Menu;                            // The menu (a user control).
        private PersonalFont _Font;                    // The special font.
        private int _Score;                            // The score.
        private int _Direction;                        // The direction of the snake.      
        private int _InsectTimerCounter;               // Counter used for the FruitTImerTick.       
        private int _TimerInterval;                    // Timer interval.

        private System.Threading.Thread _RenderThread; // The render thread.
        private Render _Render;   // Render object (to use render functions).
        private SystemTime _Time; // Time object (to use the time).
        
        ///////////////////////////
        // Multiplayer variables :

        private System.Threading.Thread _ReceptionThread;       // The reception thread.
        private System.Threading.Thread _SendingThread;         // The sending thread.

        private ListWalls _ListWalls;                           // List of walls.
        private Network _Sending;                               // Network components (for sending data).
        private Network _Reception;                             // Network components (for receiving data).
        private NetworkContainer _SendingContainer;             // Temporary container for sending data.
        private NetworkContainer _ReceptionContainer;           // Temporary container for receiving data.
        
        private Boolean _Multiplayer;                           // Boolean which indicates if the game is multiplayer mode or not.
        private Boolean _InGame;                                // Boolean which indicates if the multiplayer match is running or not.
        private Boolean _WallHasBeenAdded;                      // Boolean which indicates if a wall has spawn or not.
        private Boolean _EndGameHasBeenInvoked;                 // Boolean which indicates if the function EndGame has been invoked or not.

        private delegate void processOnMainThread();            // A delegate type
        private delegate void processOnMainThread2(Boolean b);  // Another delegate type
        private processOnMainThread _CommandDispatcherDel;      // The command dispatcher delegate.
        private processOnMainThread _PlayGameDel;               // The play game delegate.
        private processOnMainThread2 _EndGameDel;               // The end game delegate.

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        /*
         * Constructor of the Game:
         *      - Initialize some objects for the game / variables / label.     
         * */

        public Game()
        {
            InitializeComponent(); 
            InitializeFont(); 

            _Time = new SystemTime();
            _Render = new Render(this.gameBoardPictureBox, this.miniGameBoardPictureBox);

            _CommandDispatcherDel = new processOnMainThread(NetworkProcessOnMainThread); 
            _PlayGameDel = new processOnMainThread(PlayGame);                            
            _EndGameDel = new processOnMainThread2(EndGame);                             

            _Nickname = "";                    
            _Multiplayer = false;             
            _InGame = false;                   
            timeLabel.Text = _Time.Get_Time(); 
   
            LoadMenu(); 
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Game Methods

        /*
         * Method for initializing the Game:
         *      - Initialize object for playing.
         *      - Initialize timers (one for the game progress, one for the insect).
         *      - initialize the thread for graphical rendering.
         * */

        public void InitializeGame()
        {
            _FullSnake = new FullSnake(this.gameBoardPictureBox.Width);                                        
            _Fruit = new Fruit(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height);               
            _Insect = new Insect(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, -666, -666); 
            if (_Multiplayer)
                _ListWalls = new ListWalls(); 
            

            _Score = 0;                 
            _TimerInterval = 70;        
            _Direction = 1;             
            _GameOver = false;          
            _InsectIsPresent = false;   
            _InGame = true;             
        
            _Timer = new Timer();                       
            _Timer.Interval = _TimerInterval;           
            _Timer.Tick += new EventHandler(TimerTick);  

            _InsectTimer = new Timer();                             
            _InsectTimer.Interval = 1000;                           
            _InsectTimer.Tick += new EventHandler(InsectTimerTick); 

             _RenderThread = new System.Threading.Thread(new System.Threading.ThreadStart(Render)); 
             _RenderThread.Name = "RenderThread";                                                   
             _RenderThread.IsBackground = true;                                                     
        }

        /*
         * Event for the timer tick. At each tick:
         *      - Check collisions.
         *      - Update snake.
         *      - Check if fruit/insect is reached, and refresh score or list of walls(multiplayer).
         * */

        public void TimerTick(object sender, EventArgs e)
        {
            if (_Multiplayer)                                                                                                                       
                _GameOver = _FullSnake.CheckCollision(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _ListWalls.Get_ListWalls()); 
            else                                                                                                                                    
                _GameOver = _FullSnake.CheckCollision(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height);                              

            if (!_GameOver)
            {
                _FullSnake.UpdateSnake(_Direction, this.gameBoardPictureBox.Width); 

                if (_Fruit.IsReached(_FullSnake.Get_Snake()[0])) 
                {
                    if (_Multiplayer)                                                                                                              ////
                    {                                                                                                                              // Multiplayer mode :
                        _Fruit.MoveFruit(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake, _ListWalls.Get_ListWalls()); //   - Move fruit positions (considering walls)
                        while (!_Reception.Get_Container().Get_Msg().Equals("102"))                                                                //   - Send a message to the opponent (101) 
                        {                                                                                                                          //     ordering him to pop a wall in its game.
                            _SendingContainer.Set_Msg("101");                                                                                      //   - Keep sending that message while an
                            _SendingContainer.Set_HasBeenModified(true);                                                                           //     acknowledgement of receipt has not been received (102).
                        }                                                                                                                          //   - Set bool HasBeenModified to TRUE
                    }                                                                                                                              //     (so that the sending thread send the container).

                    else _Fruit.MoveFruit(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake); 

                    _Score = _Score + _Fruit.Get_POINT();                    
                    _FullSnake.AddSnakePart(this.gameBoardPictureBox.Width); 
                }

                if (_Insect.IsReached(_FullSnake.Get_Snake()[0])) 
                {
                    _InsectTimerCounter = 0;               
                    _Insect.MoveInsect();                  
                    _Score = _Score + _Insect.Get_POINT(); 
                    _InsectIsPresent = false;              
                }

                this.scoreLabel.Text = "Score : " + _Score;                                                                                          
                timeLabel.Text = _Time.Get_Time();                                                                                                   
                if (_Multiplayer)                                                                                                                    
                    this.opponentScoreLabel.Text = _Reception.Get_Container().Get_Nickname() + " score : " + _Reception.Get_Container().Get_Score(); 
            }

            else EndGame(false);
        }

        /*
         * Event for the insect tick. At each tick:
         *      - Manage insect presence (if it must appears/disappears).
         * */

        public void InsectTimerTick(object sender, EventArgs e)
        {
            _InsectTimerCounter = _InsectTimerCounter + 1; 

            if ((_InsectTimerCounter % 8 == 0) && (_InsectIsPresent == false)) 
            {
                if (_Multiplayer)                                                                                                                        
                    _Insect.MoveInsect(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake, _Fruit, _ListWalls.Get_ListWalls()); 
                else
                    _Insect.MoveInsect(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake, _Fruit); 

                _InsectTimerCounter = 0; 
                _InsectIsPresent = true; 
            }

            else if ((_InsectTimerCounter % 3 == 0) && (_InsectIsPresent == true)) 
            {
                _Insect.MoveInsect();     
                _InsectTimerCounter = 0;  
                _InsectIsPresent = false; 
            }
        }

        /*
         * Method for loading the menu:
         *      - Instanciate/attach the menu.
         *      - Define event handlers.
         * */

        private void LoadMenu()
        {
            _Menu = new Menu();                                
            this.gameBoardPictureBox.Controls.Add(_Menu);     
            _Menu.MainMenu();                                 

            _Menu.playPictureBox.Click += new EventHandler(playPictureBox_Click);               
            _Menu.retryPictureBox.Click += new EventHandler(retryPictureBox_Click);             
            _Menu.mainMenuPictureBox.Click += new EventHandler(mainMenuPictureBox_Click);        
            _Menu.multiplayerPictureBox.Click += new EventHandler(multiplayerPictureBox_Click);  
            _Menu.highScoresPictureBox.Click += new EventHandler(highScoresPictureBox_Click);   
            _Menu.backPictureBox.Click += new EventHandler(backPictureBox_Click);               
            _Menu.createGamePictureBox.Click += new EventHandler(createGamePictureBox_Click);   
            _Menu.joinGamePictureBox.Click += new EventHandler(joinGamePictureBox_Click);       
            _Menu.okPictureBox.Click += new EventHandler(okPictureBox_Click);                   
            _Menu.startGamePictureBox.Click += new EventHandler(startGamePictureBox_Click);     
        }

        /*
         * Method for launching the game:
         *      - Prepare interface.
         *      - Start threads.
         * */

        private void PlayGame()
        {
            if (_Multiplayer)                                  
            {                                                  
                this.Width = 1200;                             
                this.CenterToScreen();                         
                this.miniGameBoardPictureBox.Controls.Clear(); 
            }                                                  

            InitializeGame(); 
                    
            this.gameBoardPictureBox.Controls.Clear();     
            this.scoreLabel.Visible = true;                
            _Menu.InGame();                                

            _Timer.Start();        
            _InsectTimer.Start();  
            _RenderThread.Start(); 
        }

        /*
         * Method for ending the game:
         *      - Stop threads.
         *      - Refresh interface.
         *      - Send messages (multiplayer mode).
         *      - Manage highscores
         * */

        private void EndGame(Boolean victory)
        {
            _Timer.Stop();       
            _InsectTimer.Stop(); 

            this.gameBoardPictureBox.Controls.Add(_Menu);           
            Invoke(_Menu.Get_GameOverDel(), _Multiplayer, victory); 

            if (_Multiplayer && !victory)                                   
            {                                                               
                while (!_Reception.Get_Container().Get_Msg().Equals("112")) 
                {                                                           
                    _SendingContainer.Set_Msg("111");                       
                    _SendingContainer.Set_HasBeenModified(true);            
                }                                                           
            }                                                               

            if (!_Multiplayer)
            {
                if (submitScore())                                                  
                {                    
                    _Menu.newHighScoreLabel.Text = "New record : " + _Score;
                }
                else
                {
                    _Menu.newHighScoreLabel.Text = "Score : " + _Score;
                }

            }

            this.gameBoardPictureBox.Controls.Remove(_Render.Get_Fruit());
            this.gameBoardPictureBox.Controls.Remove(_Render.Get_Insect());
        }

        private bool submitScore()
        {
            bool submitted = false;
            HighScoreData data = HighScore.LoadHighScores();
            int index = -1;
            if (data.Count >= HighScore.MAX_HighScores)                           //If leaderboard is full
            {
                if ((index = HighScore.isPlayerAlreadyIn(_Nickname)) >= 0)        //If player is in leaderboard
                {
                    if (data.Score[index] < _Score)                               //And if he has beatten is score
                    {
                        data.Score[index] = _Score;                               //Update and save the new score
                        HighScore.SaveHighScores(data);
                        submitted = true;
                    }
                }   
                else                                                             // if he has beatten someone
                {                    
                    if((index=HighScore.check_ScoreUp(_Score))>=0)               //Replace that someone
                    {
                        data.PlayerName[index] = _Nickname;
                        data.Score[index] = _Score;
                        HighScore.SaveHighScores(data);
                        submitted = true;
                    }

                }

            }
            else                                                                //There is room in the leaderboard
            {
                if ((index = HighScore.isPlayerAlreadyIn(_Nickname)) >= 0)      //If player is in leaderboard
                {
                    if (data.Score[index] < _Score)                             //And if he has beatten is score
                    {
                        data.Score[index] = _Score;                             //Update the new score                          
                        HighScore.SaveHighScores(data);
                        submitted = true;
                    }
                }
                else                                                            //Else create new entry in leaderboard
                {                    
                    data.PlayerName[data.Count] = _Nickname;
                    data.Score[data.Count] = _Score;
                    data.Count++;
                    HighScore.SaveHighScores(data);
                    submitted = true;
                }                                                 
                
            }

            return submitted;
            
        }

        #endregion

        #region Render

        /*
         * Method for refreshing the graphical rendering:
         *      - Refresh Snake/Fruit/Insect.
         *      - Refresh mini Snake/Fruit/Insect/Walls if multiplayer
         * */

        private void Render()
        {
            while (_InGame)
            {
                try
                {
                    Invoke(_Render.Get_RenderFruitDel(), _Fruit, gameBoardPictureBox);  
                    Invoke(_Render.Get_RenderInsectDel(), _Insect, gameBoardPictureBox);
                    _Render.RenderSnake(_FullSnake, gameBoardPictureBox);               


                    if (_Multiplayer)                                                                                                    
                    {                                                                                                                    
                      Invoke(_Render.Get_RenderMiniSnakeDel(), _Reception.Get_Container().Get_Snake(), this.miniGameBoardPictureBox);
                      Invoke(_Render.Get_RenderMiniFruitDel(), _Reception.Get_Container().Get_Fruit(), this.miniGameBoardPictureBox);   
                      Invoke(_Render.Get_RenderMiniInsectDel(), _Reception.Get_Container().Get_Insect(), this.miniGameBoardPictureBox); 
                      _Render.RenderMiniWalls(_Reception.Get_Container().Get_ListWalls());                                              
                      _Render.RenderWalls(_ListWalls);                                                                                                                                                   
                    }
                }

                catch (Exception e) { Console.WriteLine(e); }

                System.Threading.Thread.Sleep(35);
            }
        }

        /*
         * Method for cleaning the miniGameboard
         *      - Initialize graphics
         *      - Draw
         * */

        public void CleanMiniPictureBox()
        {
            Graphics myGraphics;  
            SolidBrush myBrush;   

            myGraphics = this.miniGameBoardPictureBox.CreateGraphics(); 
            myBrush = new System.Drawing.SolidBrush(Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))))); 
            myGraphics.FillRectangle(myBrush, 790, 140, this.miniGameBoardPictureBox.Width, this.miniGameBoardPictureBox.Height); 
        }

        /*
         * Method for initializing font
         *      - Instanciate font.
         *      - Apply font.
         * */

        public void InitializeFont()
        {
            this._Font = new PersonalFont(); 
            this.scoreLabel.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));     
            this.timeLabel.Font = new System.Drawing.Font(_Font.getPersonalFont(), 16, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));       
            this.opponentScoreLabel.Font = new System.Drawing.Font(_Font.getPersonalFont(), 16, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        #endregion

        #region Events Methods

        /*
         * Event for clicking the retryPictureBox
         *      - Launch game.
         * */

        void retryPictureBox_Click(object sender, EventArgs e)
        {
            PlayGame(); 
        }

        /*
         * Event for clicking the mainMenuPictureBox
         *      - End threads.
         *      - Refresh interface.
         * */

        void mainMenuPictureBox_Click(object sender, EventArgs e)
        {
            if (_Multiplayer)                             
            {                                             
                Invoke(_Sending.Get_NetworkDelegate());   
                Invoke(_Reception.Get_NetworkDelegate()); 
            }

            if (_RenderThread != null)
            {
                _RenderThread.Abort(); 
            }
            CleanMiniPictureBox(); 

            this.scoreLabel.Visible = false; 
            _Menu.MainMenu();                
            this.Width = 800;                
            this.CenterToScreen();           
            _InGame = false;                 
            _Multiplayer = false;            
        }

        /*
         * Event for clicking the exitPictureBox
         *      - Close form.
         * */

        private void exitPictureBox_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception exitException) { Console.WriteLine(exitException); }
        }

        /*
         * Event for clicking the playPictureBox
         *      - Update nickname.
         *      - Launch game.
         * */

        private void playPictureBox_Click(object sender, EventArgs e)
        {
            _Nickname = _Menu.nicknameTextBox.Text; 
            PlayGame(); 
        }

        /*
         * Event for clicking the multiplayerPictureBox
         *      - Update nickname.
         *      - Update interface.
         * */

        private void multiplayerPictureBox_Click(object sender, EventArgs e)
        {
            _Nickname = _Menu.nicknameTextBox.Text; 
            _Menu.Multiplayer();                                                             
        }

        /*
         * Event for clicking the highScoresPictureBox
         *      - Show Highscores
         * */

        private void highScoresPictureBox_Click(object sender, EventArgs e)
        {
            HighScoreData sample = HighScore.LoadHighScores();
            
            _Menu.highScoresName.Text = "Players\n";
            _Menu.highScoresScore.Text = "Score\n";

            for(int i=0;i<sample.Count;i++)
            {
                _Menu.highScoresName.Text += "\n" + sample.PlayerName[i].ToString();
                _Menu.highScoresScore.Text += "\n" + sample.Score[i].ToString();               
            }                             
            _Menu.HighScoreShow();
        }

        /*
         * Event for clicking the backPictureBox
         *      - Update interface.
         *      - Close network threads.
         * */

        private void backPictureBox_Click(object sender, EventArgs e)
        {
            _Menu.MainMenu(); 

            if (_Multiplayer && _Sending != null && _Reception != null) 
            {                                                           
                Invoke(_Sending.Get_NetworkDelegate());                 
                Invoke(_Reception.Get_NetworkDelegate());               
            }                                                           

            _Multiplayer = false;
        }

        /*
         * Event for clicking the createGamePictureBox
         *      - Update interface.
         *      - Initialize Network.
         * */

        private void createGamePictureBox_Click(object sender, EventArgs e)
        {
            _Menu.Host();            
            InitializeNetwork(true); 
        }

        /*
         * Event for clicking the joinGamePictureBox
         *      - Update interface.
         *      - Initialize Network.
         * */

        private void joinGamePictureBox_Click(object sender, EventArgs e)
        {
            _Menu.Client1();                                   
            InitializeNetwork(false);                          
        }

        /*
         * Event for clicking the okPictureBox
         *      - Update interface.
         *      - Send message to the server.
         * */

        private void okPictureBox_Click(object sender, EventArgs e)
        {
            if (!_Sending.Get_IsHost())
            {
                _Sending.Set_HostIpAdress(_Menu.ipTextBox1.Text + "." + _Menu.ipTextBox2.Text + "." + _Menu.ipTextBox3.Text + "." + _Menu.ipTextBox4.Text);
                _SendingContainer.Set_Msg("001"); // Set the message to send : 001 --> Ask for connection.           
                _SendingContainer.Set_HasBeenModified(true); 
            }
            
            _Menu.Client2(); 
        }

        /*
         * Event for pressing the startGamePictureBox button
         *      - Send message to the client.
         *      - Launch game.
         * */

        private void startGamePictureBox_Click(object sender, EventArgs e)
        {
            if (_Sending.Get_IsHost()) 
            {
                _SendingContainer.Set_Msg("100"); // Set the message to send : 100 --> Game Started.
                _SendingContainer.Set_HasBeenModified(true); 
            }

            PlayGame(); 
        }

        /*
         * Event for pressing keyboard touchs 
         *      - Move Snake.
         * */

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (_FullSnake.Get_Snake()[0].Get_Direction() != 2) _Direction = 0; 
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (_FullSnake.Get_Snake()[0].Get_Direction() != 3) _Direction = 1; 
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (_FullSnake.Get_Snake()[0].Get_Direction() != 0) _Direction = 2; 
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (_FullSnake.Get_Snake()[0].Get_Direction() != 1) _Direction = 3; 
            }
        }

        #endregion

        #region Network

        /*
         * Multiplayer Command Dispatcher
         *      001 --> Ask for connection
         *      010 --> acknowledgement of receipt received by the client.
         *      100 --> Game running.
         *      101 --> when this message is received, a wall must spawn.
         *      102 --> wall created (opponent side) and aknowledgement of receipt received.
         *      111 --> when this message is received, the opponent has lost the game.
         * */

        private void NetworkProcessOnMainThread()
        {
            _Sending.Set_EndPoint(_Reception.Get_EndPoint()); // Set the endpoint.

            switch(_Reception.Get_Container().Get_Msg()) 
            {
                case "001": _SendingContainer.Set_Msg("010");                                                 
                            _SendingContainer.Set_HasBeenModified(true);                                      
                            Invoke(_Menu.Get_ConnectionEstablishedDel(), _Sending.Get_IsHost(), _Multiplayer);
                            break;                                                                            
                                                                                                              

                case "010": Invoke(_Menu.Get_ConnectionEstablishedDel(), _Sending.Get_IsHost(), _Multiplayer);
                            break;                                                                            

                case "100": if (!_InGame)                    
                                Invoke(_PlayGameDel);        
                                                             
                            if (_WallHasBeenAdded)           
                                _WallHasBeenAdded = false;   
                            _SendingContainer.Set_Msg("100");
                            FillContainer();                 
                            break;                           

                case "101": if (!_WallHasBeenAdded)                                                                                                                                                 
                            {                                                                                                                                                                       
                                _ListWalls.Get_ListWalls().Add(new Wall(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake, _Fruit, _Insect, _ListWalls.Get_ListWalls())); 
                                _WallHasBeenAdded = true;                                                                                                                                           
                            }                                                                                                                                                                       
                            _SendingContainer.Set_Msg("102");                                                                                                                                       
                            FillContainer();                                                                                                                                                        
                            break;                                                                                                                                                                  

                case "102": _SendingContainer.Set_Msg("100"); 
                            FillContainer();                  
                            break;                            

                case "111": if(!_EndGameHasBeenInvoked)                  
                                Invoke(_EndGameDel, true);               
                            _EndGameHasBeenInvoked = true;               
                            _SendingContainer.Set_Msg("112");            
                            _SendingContainer.Set_HasBeenModified(true); 
                            break;                                       
            }
        }

        /*
         * Function initializing network elements
         *      - Initialize network objects/variables.
         *      - Initialize network threads.
         * */

        private void InitializeNetwork(Boolean isHost)
        {
            _SendingContainer = new NetworkContainer();   
            _ReceptionContainer = new NetworkContainer(); 

            _Multiplayer = true;            
            _EndGameHasBeenInvoked = false; 
            _WallHasBeenAdded = false;      

            _Sending = new Network(ref _SendingContainer, isHost, true, _CommandDispatcherDel);      
            _Reception = new Network(ref _ReceptionContainer, isHost, false, _CommandDispatcherDel); 

            InitializeNetworkThreads(); 
        }

        /*
         * Function initializing threads for sending and receiving data
         *      - Initialize and start receiving & sending threads.
         * */

        private void InitializeNetworkThreads()
        {
            _ReceptionThread = new System.Threading.Thread(new System.Threading.ThreadStart(_Reception.ReceiveLoop)); 
            _ReceptionThread.Name = "ReceptionThread";                                                                
            _ReceptionThread.IsBackground = true;                                                                     
            _ReceptionThread.Start();                                                                                 

            _SendingThread = new System.Threading.Thread(new System.Threading.ThreadStart(_Sending.SendLoop)); 
            _SendingThread.Name = "SendingThread";                                                             
            _SendingThread.IsBackground = true;                                                                
            _SendingThread.Start();                                                                            
        }

        /*
         * Method to fill the sending container (except message)
         *      - Fill container to be sent.
         * */

        private void FillContainer()
        {
            _SendingContainer.Set_Snake(_FullSnake);     
            _SendingContainer.Set_Fruit(_Fruit);         
            _SendingContainer.Set_Insect(_Insect);       
            _SendingContainer.Set_ListWalls(_ListWalls); 
            _SendingContainer.Set_Nickname(_Nickname);   
            _SendingContainer.Set_Score(_Score);         
            _SendingContainer.Set_HasBeenModified(true); 
        }

        #endregion
    }
}

