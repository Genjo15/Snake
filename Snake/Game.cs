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
        private Render _Render;
        
        ///////////////////////////
        // Multiplayer variables :

        private System.Threading.Thread _ReceptionThread;      // The reception thread.
        private System.Threading.Thread _SendingThread;        // The sending thread.

        private ListWalls _ListWalls;                          // List of walls.
        private Network _Sending;                              // Network components (for sending data).
        private Network _Reception;                            // Network components (for receiving data).
        private NetworkContainer _SendingContainer;            // Temporary container for sending data.
        private NetworkContainer _ReceptionContainer;          // Temporary container for receiving data.
        
        private Boolean _Multiplayer;                          // Boolean which indicates if the game is multiplayer mode or not.
        private Boolean _InGame;                               // Boolean which indicates if the multiplayer match is running or not.
        private Boolean _WallHasBeenAdded;                     // Boolean which indicates if a wall has spawn or not.
        private Boolean _EndGameHasBeenInvoked;                // Boolean which indicates if the function EndGame has been invoked or not.

        private delegate void processOnMainThread();           // A delegate type
        private delegate void processOnMainThread2(Boolean b); // Another delegate type
        private processOnMainThread _CommandDispatcherDel;     // The command dispatcher delegate.
        private processOnMainThread _PlayGameDel;              // The play game delegate.
        private processOnMainThread2 _EndGameDel;              // The end game delegate.

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public Game()
        {
            InitializeComponent(); // Initialize components of the form.
            InitializeFont();      // Initialize font.

            _Render = new Render(this.gameBoardPictureBox, this.miniGameBoardPictureBox);

            _CommandDispatcherDel = new processOnMainThread(NetworkProcessOnMainThread); //// 
            _PlayGameDel = new processOnMainThread(PlayGame);                            // Initialize delegates.
            _EndGameDel = new processOnMainThread2(EndGame);                             //

            _Nickname = "";       // Initialize the _Nickname to "";
            _Multiplayer = false; // Initialize _Multiplayer to false.
            _InGame = false;      // Initialize _Ingame to false.
   
            LoadMenu(); // Load menu.
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Game Methods

        ////////////////////////////////////
        // Method for initializing the game

        public void InitializeGame()
        {
            _FullSnake = new FullSnake(this.gameBoardPictureBox.Width);                                        // New FullSnake.
            _Fruit = new Fruit(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height);               // New fruit.
            _Insect = new Insect(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, -666, -666); // New insect.
            if (_Multiplayer)
                _ListWalls = new ListWalls(); // New list of Walls (only in multiplayer mode).

            _Score = 0;                 // Score is set to 0.
            _TimerInterval = 70;        // Timer interval tick is set to 140 ms.
            _Direction = 1;             // First direction is initially set to 1 (right).
            _GameOver = false;          // _GameOver is initialized to FALSE.
            _InsectIsPresent = false;   // _InsectIsPresent is initialized to FALSE.
            _InGame = true;             // _InGame is set to TRUE
        
            _Timer = new Timer();                       // New timer.
            _Timer.Interval = _TimerInterval;           // Interval of the timer is set.
            _Timer.Tick += new EventHandler(TimerTick); // New EventHandler. 

            _InsectTimer = new Timer();                             // New timer.
            _InsectTimer.Interval = 1000;                           // Interval of the timer is set to 1s.
            _InsectTimer.Tick += new EventHandler(InsectTimerTick); // New EventHandler. 

             _RenderThread = new System.Threading.Thread(new System.Threading.ThreadStart(Render)); // Initialize the thread for rendering game.
             _RenderThread.Name = "RenderThread";                                                   // Set its name.
             _RenderThread.IsBackground = true;                                                     // Make it background runnable.
        }

        /////////////////////////////
        //  Event for the timer tick

        public void TimerTick(object sender, EventArgs e)
        {
            if (_Multiplayer)                                                                                                                       ////
                _GameOver = _FullSnake.CheckCollision(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _ListWalls.Get_ListWalls()); // 
            else                                                                                                                                    // Check collision (!= function is called depending if multiplayer mode or not).
                _GameOver = _FullSnake.CheckCollision(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height);                             // 

            if (!_GameOver)
            {
                _FullSnake.UpdateSnake(_Direction, this.gameBoardPictureBox.Width); // Update the movement of the snake.

                if (_Fruit.IsReached(_FullSnake.Get_Snake()[0])) // If a fruit is reached...
                {
                    if (_Multiplayer)                                                                                                              //////
                    {                                                                                                                              // Multiplayer mode :
                        _Fruit.MoveFruit(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake, _ListWalls.Get_ListWalls()); //   - Move fruit positions (considering walls)
                        while (!_Reception.Get_Container().Get_Msg().Equals("102"))                                                                //   - Send a message to the opponent (101) 
                        {                                                                                                                          //     ordering him to pop a wall in its game.
                            _SendingContainer.Set_Msg("101");                                                                                      //   - Keep sending that message while an
                            _SendingContainer.Set_HasBeenModified(true);                                                                           //     acknowledgement of receipt has not been received (102).
                        }                                                                                                                          //   - Set bool HasBeenModified to TRUE
                    }                                                                                                                              //     (so that the sending thread send the container).

                    else _Fruit.MoveFruit(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake); // Move fruit positions (without considering walls, because offline mode).

                    _Score = _Score + _Fruit.Get_POINT();                    // Increment the score.
                    _FullSnake.AddSnakePart(this.gameBoardPictureBox.Width); // Add a Snake part.
                }

                if (_Insect.IsReached(_FullSnake.Get_Snake()[0])) // If the insect is reached...
                {
                    _InsectTimerCounter = 0;               // Reset the insect counter.
                    _Insect.MoveInsect();                  // Move the insect (make it unreacheable by the player).
                    _Score = _Score + _Insect.Get_POINT(); // Increment the score.
                    _InsectIsPresent = false;              // Set the boolean to FALSE.
                }

                this.scoreLabel.Text = "Score : " + _Score; // Refresh the score label.
                if(_Multiplayer)                                                                                                                     // If multiplayer mode,
                    this.opponentScoreLabel.Text = _Reception.Get_Container().Get_Nickname() + " score : " + _Reception.Get_Container().Get_Score(); // refresh also the opponent' score label.
            }

            else EndGame(false); // end game if _GameOver is TRUE.
        }

        /////////////////////////////
        // Event for the insect timer

        public void InsectTimerTick(object sender, EventArgs e)
        {
            _InsectTimerCounter = _InsectTimerCounter + 1; // Increment the counter.

            if ((_InsectTimerCounter % 8 == 0) && (_InsectIsPresent == false)) // If insect is not present, and it's time to show it...
            {
                if (_Multiplayer)                                                                                                                        // Multiplayer mode :
                    _Insect.MoveInsect(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake, _Fruit, _ListWalls.Get_ListWalls()); // Move insect considering walls.
                else
                    _Insect.MoveInsect(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake, _Fruit); // Move insect.

                _InsectTimerCounter = 0; // Reset the counter.
                _InsectIsPresent = true; // Set the boolean to true.
            }

            else if ((_InsectTimerCounter % 3 == 0) && (_InsectIsPresent == true)) // If the user takes too much time to pick up the insect, it will disappear.
            {
                _Insect.MoveInsect();     // Move the insect (make it unreacheable by the player).
                _InsectTimerCounter = 0;  // Reset the counter.
                _InsectIsPresent = false; // Set the boolean to false.
            }
        }

        ///////////////////////////////
        // Method for loading the menu

        private void LoadMenu()
        {
            _Menu = new Menu();                               // Instanciate a new menu;   
            this.gameBoardPictureBox.Controls.Add(_Menu);     // Add it to the gameboard.
            _Menu.MainMenu();                                 // Set the configuration for a game start (hide/show labels).

            _Menu.playPictureBox.Click += new EventHandler(playPictureBox_Click);               ///// 
            _Menu.retryPictureBox.Click += new EventHandler(retryPictureBox_Click);             // 
            _Menu.mainMenuPictureBox.Click += new EventHandler(mainMenuPictureBox_Click);       //  
            _Menu.multiplayerPictureBox.Click += new EventHandler(multiplayerPictureBox_Click); // 
            _Menu.highScoresPictureBox.Click += new EventHandler(highScoresPictureBox_Click);   // Define event handlers
            _Menu.backPictureBox.Click += new EventHandler(backPictureBox_Click);               //
            _Menu.createGamePictureBox.Click += new EventHandler(createGamePictureBox_Click);   //
            _Menu.joinGamePictureBox.Click += new EventHandler(joinGamePictureBox_Click);       //
            _Menu.okPictureBox.Click += new EventHandler(okPictureBox_Click);                   //
            _Menu.startGamePictureBox.Click += new EventHandler(startGamePictureBox_Click);     //            
        }

        ////////////////////////
        // Method for play game

        private void PlayGame()
        {
            if (_Multiplayer)                                  ////
            {                                                  // Multiplayer mode :
                this.Width = 1200;                             // Enlarge the form.
                this.CenterToScreen();                         // Center the form.
                this.miniGameBoardPictureBox.Controls.Clear(); // Erase the content of the mini picture Box.
            }                                                  //

            InitializeGame(); // Initialize game.
                    
            this.gameBoardPictureBox.Controls.Clear();     // Erase the content of the picture Box.
            this.scoreLabel.Visible = true;                // Show the score label. 
            _Menu.InGame();                                // Set the configuration for a game end (hide/show labels).

            _Timer.Start();        // Start timer.
            _InsectTimer.Start();  // Start insect timer.  
            _RenderThread.Start(); // Start Render thread.      
        }

        //////////////////////////
        // Method for ending game

        private void EndGame(Boolean victory)
        {
            _Timer.Stop();       // Stop the timer.
            _InsectTimer.Stop(); // Stop insect timer.

            this.gameBoardPictureBox.Controls.Add(_Menu);           // Reattach the menu to the gameboard.
            Invoke(_Menu.Get_GameOverDel(), _Multiplayer, victory); // Set the configuration for a game end (hide/show labels).

            if (_Multiplayer && !victory)                                   ////
            {                                                               // Multiplayer mode :
                while (!_Reception.Get_Container().Get_Msg().Equals("112")) // if current player is the looser :
                {                                                           //   - Send a message to the opponent (111) to tell him that
                    _SendingContainer.Set_Msg("111");                       //     the game is finished.
                    _SendingContainer.Set_HasBeenModified(true);            //   - Keep sending that message while an acknowledgement
                }                                                           //     of receipt has not been received (112).
            }                                                               //   - Set bool HasBeenModified to TRUE.

            if (!_Multiplayer)
            {
                if (submitScore())                                                  //Test if new highscore has been submitted and display text accordingly
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

        ///////////////////////////////////////
        // Method to refresh the display (all)

        private void Render()
        {
            while (_InGame)
            {
                try
                {
                    Invoke(_Render.Get_RenderFruitDel(), _Fruit, gameBoardPictureBox);   // Refresh the display of the fruit.  
                    Invoke(_Render.Get_RenderInsectDel(), _Insect, gameBoardPictureBox); // Refresh the display of the insect.  
                    _Render.RenderSnake(_FullSnake, gameBoardPictureBox);                // Refresh the display of the snake.


                    if (_Multiplayer)                                                                                                     ////
                    {                                                                                                                     // Multiplayer mode :
                        Invoke(_Render.Get_RenderMiniSnakeDel(), _Reception.Get_Container().Get_Snake(), this.miniGameBoardPictureBox);   // - Refresh the opponent's snake on the mini-gameboard.
                        Invoke(_Render.Get_RenderMiniFruitDel(), _Reception.Get_Container().Get_Fruit(), this.miniGameBoardPictureBox);   // - Refresh the opponent's fruit on the mini-gameboard.
                        Invoke(_Render.Get_RenderMiniInsectDel(), _Reception.Get_Container().Get_Insect(), this.miniGameBoardPictureBox); // - Refresh the opponent's insect on the mini-gameboard.
                        _Render.RenderMiniWalls(_Reception.Get_Container().Get_ListWalls());                                              // - Refresh the opponent's walls on the mini-gameboard.
                        _Render.RenderWalls(_ListWalls);                                                                                  // - Refresh the player's wall too.
                    }
                }

                catch (Exception e) { Console.WriteLine(e); }
            }
        }

        //////////////////////////////////////
        // Method to clean the miniPictureBox

        public void CleanMiniPictureBox()
        {
            Graphics myGraphics;  // Graphics for main drawing.
            SolidBrush myBrush;   // Brush for filling shapes.

            myGraphics = this.miniGameBoardPictureBox.CreateGraphics(); // Initialize the graphics. 
            myBrush = new System.Drawing.SolidBrush(Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))))); // Initialize the brush.
            myGraphics.FillRectangle(myBrush, 790, 140, this.miniGameBoardPictureBox.Width, this.miniGameBoardPictureBox.Height); // Draw a rectangle (color of the background) of the size of the mini gameboard.
        }

        /////////////////////////////
        // Method to initialize font

        public void InitializeFont()
        {
            this._Font = new PersonalFont(); // Create new font.
            this.scoreLabel.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));      // Set the font for the score label.
            this.opponentScoreLabel.Font = new System.Drawing.Font(_Font.getPersonalFont(), 16, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Set the font for the opponent score label.
        }

        #endregion

        #region Events Methods

        /////////////////////////////////////////
        // Event for clicking the retryPictureBox

        void retryPictureBox_Click(object sender, EventArgs e)
        {
            PlayGame(); // Play game.
        }

        /////////////////////////////////////////////
        // Event for clicking the mainMenuPictureBox

        void mainMenuPictureBox_Click(object sender, EventArgs e)
        {
            if (_Multiplayer)                             ////
            {                                             // Multiplayer mode:
                Invoke(_Sending.Get_NetworkDelegate());   //   - Close Sending thread by invoking the network delegate.
                Invoke(_Reception.Get_NetworkDelegate()); //   - Close Reception thread by invoking the network delegate.
            }

            if (_RenderThread != null)
            {
                _RenderThread.Abort();  // Terminate the render thread.
            }
            CleanMiniPictureBox();  // Clean the mini gameboard.

            this.scoreLabel.Visible = false; // Hide score label. 
            _Menu.MainMenu();                // Set the configuration for the menu (hide/show labels).
            this.Width = 800;                // Resize the form.
            this.CenterToScreen();           // Center it.
            _InGame = false;                 // Set _InGame to FALSE.   
            _Multiplayer = false;            // Set _Multiplayer to FALSE. 
        }

        /////////////////////////////////////////
        // Event for clicking the exitPictureBox

        private void exitPictureBox_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.Close(); // Close the form.
            }
            catch (Exception exitException) { Console.WriteLine(exitException); }
        }

        //////////////////////////////////////////
        // Event for clicking the playPictureBox

        private void playPictureBox_Click(object sender, EventArgs e)
        {
            _Nickname = _Menu.nicknameTextBox.Text; // Fetch the nickname entered on the textbox.
            PlayGame(); // Play game.
        }

        //////////////////////////////////////////
        // Event for clicking the multiplayerPictureBox

        private void multiplayerPictureBox_Click(object sender, EventArgs e)
        {
            _Nickname = _Menu.nicknameTextBox.Text; // Fetch the nickname entered on the textbox.
            _Menu.Multiplayer(); // Set the configuration for the menu (hide/show labels).                                                             
        }

        ///////////////////////////////////////////////
        // Event for clicking the highScoresPictureBox

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

        //////////////////////////////////////////
        // Event for clicking the backPictureBox

        private void backPictureBox_Click(object sender, EventArgs e)
        {
            _Menu.MainMenu(); // Set the configuration for the menu (hide/show labels).

            if (_Multiplayer && _Sending != null && _Reception != null) ////
            {                                                           // Multiplayer mode:
                Invoke(_Sending.Get_NetworkDelegate());                 //   - Close Sending thread by invoking the network delegate.
                Invoke(_Reception.Get_NetworkDelegate());               //   - Close Reception thread by invoking the network delegate.
            }                                                           //

            _Multiplayer = false; // Set _Multiplayer to FALSE.
        }

        ///////////////////////////////////////////////
        // Event for clicking the createGamePictureBox

        private void createGamePictureBox_Click(object sender, EventArgs e)
        {
            _Menu.Host();            // Set the configuration for the menu (hide/show labels).
            InitializeNetwork(true); // Initialize network components.
        }

        //////////////////////////////////////////////
        // Event for clicking the joinGamePictureBox

        private void joinGamePictureBox_Click(object sender, EventArgs e)
        {
            _Menu.Client1();                                   // Set the configuration for the menu (hide/show labels).
            InitializeNetwork(false);                          // Initialize network components.
        }

        ///////////////////////////////////////
        // Event for clicking the okPictureBox

        private void okPictureBox_Click(object sender, EventArgs e)
        {
            if (!_Sending.Get_IsHost())
            {
                _Sending.Set_HostIpAdress(_Menu.ipTextBox1.Text + "." + _Menu.ipTextBox2.Text + "." + _Menu.ipTextBox3.Text + "." + _Menu.ipTextBox4.Text); // Set the entered ip by the user.
                _SendingContainer.Set_Msg("001");            // Set the message to send : 001 --> ask for connection.
                _SendingContainer.Set_HasBeenModified(true); // Set the bool HasBeenModified to TRUE (so that the message is sent by the sending thread).
            }
            
            _Menu.Client2(); // Set the configuration for the menu (hide/show labels).
        }



        /////////////////////////////////////////////////////
        // Event for pressing the startGamePictureBox button

        private void startGamePictureBox_Click(object sender, EventArgs e)
        {
            if (_Sending.Get_IsHost()) // If user is host...
            {
                _SendingContainer.Set_Msg("100");            // Set the message to send : 001 --> Game Started.
                _SendingContainer.Set_HasBeenModified(true); // Set the bool HasBeenModified to TRUE (so that the message is sent by the sending thread).
            }

            PlayGame(); // Launch game.
        }

        //////////////////////////////////////
        // Event for pressing keyboard touchs 

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (_FullSnake.Get_Snake()[0].Get_Direction() != 2) _Direction = 0; // Snake will move up.
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (_FullSnake.Get_Snake()[0].Get_Direction() != 3) _Direction = 1; // Snake will move right.
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (_FullSnake.Get_Snake()[0].Get_Direction() != 0) _Direction = 2; // Snake will move down.
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (_FullSnake.Get_Snake()[0].Get_Direction() != 1) _Direction = 3; // Snake will move left.
            }
        }

        #endregion

        #region Network

        //////////////////////////////////
        // Multiplayer Command Dispatcher

        private void NetworkProcessOnMainThread()
        {
            _Sending.Set_EndPoint(_Reception.Get_EndPoint()); // Set the endpoint.

            switch(_Reception.Get_Container().Get_Msg()) // Switch 
            {
                case "001": _SendingContainer.Set_Msg("010");                                                  ////
                            _SendingContainer.Set_HasBeenModified(true);                                       // 001 --> Ask for connection
                            Invoke(_Menu.Get_ConnectionEstablishedDel(), _Sending.Get_IsHost(), _Multiplayer); //  - Send a message to the client (010). this is
                            break;                                                                             //    an acknowledgement of receipt.
                                                                                                               //  - Set the menu configuration for connection established (hide/show pictures box)

                case "010": Invoke(_Menu.Get_ConnectionEstablishedDel(), _Sending.Get_IsHost(), _Multiplayer); // 010 --> acknowledgement of receipt received by the client.
                            break;                                                                             // Set the menu configuration for connection established (hide/show pictures box)

                case "100": if (!_InGame)                     ////
                                Invoke(_PlayGameDel);         // 
                                                              // 100 --> Game running.
                            if (_WallHasBeenAdded)            //   - if not in game, launch game.
                                _WallHasBeenAdded = false;    //   - Set _WallHasBeenAdded to FALSE if not the case.
                            _SendingContainer.Set_Msg("100"); //   - Send a message to the opponent (100, still in game)
                            FillContainer();                  //   - Fill Container
                            break;                            //

                case "101": if (!_WallHasBeenAdded)                                                                                                                                                 ////
                            {                                                                                                                                                                       //
                                _ListWalls.Get_ListWalls().Add(new Wall(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake, _Fruit, _Insect, _ListWalls.Get_ListWalls())); // 101 --> when this message is received, a wall must spawn.
                                _WallHasBeenAdded = true;                                                                                                                                           //  - Create a new Wall and add it to the list of walls.
                            }                                                                                                                                                                       //  - Send a message to the opponent (102). this is
                            _SendingContainer.Set_Msg("102");                                                                                                                                       //    an acknowledgement of receipt.
                            FillContainer();                                                                                                                                                        //  - Fill container.
                            break;                                                                                                                                                                  //

                case "102": _SendingContainer.Set_Msg("100"); // 102 --> wall created (opponent side) and aknowledgement of receipt received.
                            FillContainer();                  //  - Send a message to the opponent (100) : Game is continuing.
                            break;                            //  - Fill container.

                case "111": if(!_EndGameHasBeenInvoked)                  ////
                                Invoke(_EndGameDel, true);               // 111 --> when this message is received, the opponent has lost the game.
                            _EndGameHasBeenInvoked = true;               //  - Invoke EndGame method.
                            _SendingContainer.Set_Msg("112");            //  - Send a message to the opponent (112) as an aknowledgement of receipt.
                            _SendingContainer.Set_HasBeenModified(true); //
                            break;                                       //
            }
        }

        //////////////////////////////////////////
        // Function initializing network elements

        private void InitializeNetwork(Boolean isHost)
        {
            _SendingContainer = new NetworkContainer();   // New Network container for sending.
            _ReceptionContainer = new NetworkContainer(); // New Network container for reception.

            _Multiplayer = true;            // Initialize _Multiplayer to FALSE.
            _EndGameHasBeenInvoked = false; // Initialize _EndGameHasBeenInvoked to FALSE.
            _WallHasBeenAdded = false;      // _WallHasBeenAdded to FALSE.


            _Sending = new Network(ref _SendingContainer, isHost, true, _CommandDispatcherDel);      // New network component for sinding (including socket).
            _Reception = new Network(ref _ReceptionContainer, isHost, false, _CommandDispatcherDel); // New network component for reception (including socket).

            InitializeNetworkThreads(); // Initialize sending and reception threads.
        }


        ////////////////////////////////////////////////////////////////
        // Function initializing threads for sending and receiving data

        private void InitializeNetworkThreads()
        {
            _ReceptionThread = new System.Threading.Thread(new System.Threading.ThreadStart(_Reception.ReceiveLoop)); // Initialize the thread.
            _ReceptionThread.Name = "ReceptionThread";                                                                // Set its name.
            _ReceptionThread.IsBackground = true;                                                                     // Make it background runnable.
            _ReceptionThread.Start();                                                                                 // Start the thread.

            _SendingThread = new System.Threading.Thread(new System.Threading.ThreadStart(_Sending.SendLoop)); // Initialize the thread.
            _SendingThread.Name = "SendingThread";                                                             // Set its name.
            _SendingThread.IsBackground = true;                                                                // Make it background runnable.
            _SendingThread.Start();                                                                            // Start the thread.
        }

        /////////////////////////////////////////////////////////
        // Method to fill the sending container (except message)

        private void FillContainer()
        {
            _SendingContainer.Set_Snake(_FullSnake);     // Fill _FullSnake in the sending container by the player fullsnake.
            _SendingContainer.Set_Fruit(_Fruit);         // Fill _Fruit in the sending container by the player fruit.
            _SendingContainer.Set_Insect(_Insect);       // Fill _Insect in the sending container by the player insect.
            _SendingContainer.Set_ListWalls(_ListWalls); // Fill _ListWalls in the sending container by the player listWalls.
            _SendingContainer.Set_Nickname(_Nickname);   // Fill _Nickname in the sending container by the player nickname.
            _SendingContainer.Set_Score(_Score);         // Fill _Score in the sending container by the player score.
            _SendingContainer.Set_HasBeenModified(true); // Set _HasBeenModified to TRUE (so that the sending thread send the container).
        }

        #endregion
    }
}

