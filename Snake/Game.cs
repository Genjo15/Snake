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

        private FullSnake _FullSnake;             // The complete snake (which is a list of snake elements).
        private Fruit _Fruit;                     // A fruit.
        private Insect _Insect;                   // An insect.
        private int _Score;                       // The score.
        private String _Nickname;                 // The nickname.
        private int _Direction;                   // The direction.
        private Timer _Timer;                     // The timer.
        private Timer _InsectTimer;               // The timer for the fruit.
        private int _InsectTimerCounter;          // Counter used for the FruitTImerTick
        private Boolean _InsectIsPresent;         // Boolean which indicates if the insect is present or not.
        private Boolean _GameOver;                // Boolean which detects if the game is over or not.
        private int _TimerInterval;               // Timer interval.
        private Menu _Menu;                       // The menu.
        private PersonalFont _Font;               // The special font.


        ///////////////////////////
        // Multiplayer variables :

        Network _Sending;                                     // Network components (for sending data).
        Network _Reception;                                   // Network components (for receiving data).

        NetworkContainer _SendingContainer;                   // Temporary container for sending data.
        NetworkContainer _ReceptionContainer;                 // Reception container for receiving data.
        System.Threading.Thread _ReceptionThread;             // The reception thread.
        System.Threading.Thread _SendingThread;               // The sending thread.
        private Boolean _Multiplayer;                         // Boolean which determines if the game is multiplayer or not.
        private Boolean _InGame;                              // Boolean which determines if the multiplayer match is running or not.
        private delegate void processOnMainThread();          // The delegate
        private delegate void processOnMainThread2(Boolean b);   // The delegate
        private processOnMainThread _CommandDispatcherDel;    // The command dispatcher delegate.
        private processOnMainThread _PlayGameDel;             // The play game delegate.
        private processOnMainThread2 _EndGameDel;
        //private String _HostIpAdress;                         //

        private ListWalls _ListWalls;                      // List of walls.


        System.Threading.Thread _RenderThread; // The render thread.

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public Game()
        {
            InitializeComponent(); // Initialize components of the form (essentially the menu).
            InitializeFont(); // Initialize font.
            _CommandDispatcherDel = new processOnMainThread(NetworkProcessOnMainThread); // Initialize delegate.
            _PlayGameDel = new processOnMainThread(PlayGame);
            _EndGameDel = new processOnMainThread2(EndGame);
            _Nickname = "";
            //_HostIpAdress = "";
            _Multiplayer = true;
            _InGame = false;
   
            LoadMenu(); // Load menu.
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Game Methods

        ////////////////////////////////////
        // Method for initializing the game

        public void InitializeGame()
        {
            _FullSnake = new FullSnake(this.gameBoardPictureBox.Width); // New FullSnake.
            _Fruit = new Fruit(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height); // New fruit.
            _Insect = new Insect(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, -666, -666); // New insect.
            if (_Multiplayer)
                _ListWalls = new ListWalls();

            _Score = 0;                 // Score is set to 0.
            _TimerInterval = 70;        // Timer interval tick is set to 140 ms.
            _Direction = 1;             // First direction is initially set to 1 (right).
            _GameOver = false;          // _GameOver is initialized to false.
            _InsectIsPresent = false;   // _InsectIsPresent is initialized to false.
        
            _Timer = new Timer();                       // New timer.
            _Timer.Interval = _TimerInterval;           // Interval of the timer is set.
            _Timer.Tick += new EventHandler(TimerTick); // New EventHandler. 

            _InsectTimer = new Timer();                             // New timer.
            _InsectTimer.Interval = 1000;                           // Interval of the timer is set to 1s.
            _InsectTimer.Tick += new EventHandler(InsectTimerTick); // New EventHandler. 

             _RenderThread = new System.Threading.Thread(new System.Threading.ThreadStart(Render)); // Initialize the thread.
             _RenderThread.Name = "RenderThread";                                                             // Set its name.
             _RenderThread.IsBackground = true;                                                                // Make it background runnable.
        }

        /////////////////////////////
        //  Event for the timer tick

        public void TimerTick(object sender, EventArgs e)
        {
            if (_Multiplayer)
                _GameOver = _FullSnake.CheckCollision(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _ListWalls.Get_ListWalls()); // Check collision.
            else
                _GameOver = _FullSnake.CheckCollision(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height); // Check collision.

            if (!_GameOver)
            {
                _FullSnake.UpdateSnake(_Direction, this.gameBoardPictureBox.Width); // Update the movement of the snake.

                if (_Fruit.IsReached(_FullSnake.Get_Snake()[0]))
                {
                    if (_Multiplayer)
                    {
                        _Fruit.MoveFruit(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake, _ListWalls.Get_ListWalls()); // Move fruit positions.
                        while (!_Reception.Get_Container().Get_Msg().Equals("102"))
                        {
                            _SendingContainer.Set_Msg("101");
                            _SendingContainer.Set_HasBeenModified(true);
                        }
                    }
                    else _Fruit.MoveFruit(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake); // Move fruit positions.
                    _Score = _Score + _Fruit.Get_POINT(); // Increment the score.
                    _FullSnake.AddSnakePart(this.gameBoardPictureBox.Width); // Add a Snake part.
                    _Fruit.Set_EraseStreaks(true); 
                }

                if (_Insect.IsReached(_FullSnake.Get_Snake()[0]))
                {
                    _InsectTimerCounter = 0;               // Reset the counter.
                    _Insect.MoveInsect(); // Move the insect (make it unreacheable by the player).
                    _Score = _Score + _Insect.Get_POINT(); // Increment the score.
                    _InsectIsPresent = false;              // Set the boolean to false.
                }

                //this.textBox1.Focus(); // Focus on the textbox (this invisible textbox has been created to manage keyDown events).
                this.scoreLabel.Text = "Score : " + _Score; // Show the score.
                if(_Multiplayer)
                    this.opponentScoreLabel.Text = _Reception.Get_Container().Get_Nickname() + " score : " + _Reception.Get_Container().Get_Score();

            }

            else EndGame(false); // end game if _GameOver is true.
        }

        /////////////////////////////
        // Event for the insect timer

        public void InsectTimerTick(object sender, EventArgs e)
        {
            _InsectTimerCounter = _InsectTimerCounter + 1; // Increment the counter.

            if ((_InsectTimerCounter % 8 == 0) && (_InsectIsPresent == false))
            {
                if (_Multiplayer)
                    _Insect.MoveInsect(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake, _Fruit, _ListWalls.Get_ListWalls()); // Move insect.
                else
                    _Insect.MoveInsect(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake, _Fruit); // Move insect.
                _InsectTimerCounter = 0; // Reset the counter.
                _InsectIsPresent = true; // Set the boolean to true.
            }

            else if ((_InsectTimerCounter % 3 == 0) && (_InsectIsPresent == true)) // If the user takes too much time to pick up the insect, it will disappear.
            {
                _Insect.MoveInsect(); // Move the insect (make it unreacheable by the player).
                _InsectTimerCounter = 0;  // Reset the counter.
                _InsectIsPresent = false; // Set the boolean to false.
            }
        }

        ///////////////////////////////
        // Method for loading the menu

        private void LoadMenu()
        {
            _Menu = new Menu();                                                 // Instanciate a new menu;   
            this.gameBoardPictureBox.Controls.Add(_Menu);                       // Add it to the gameboard.
            _Menu.MainMenu();                                                   // Set the configuration for a game start (hide/show labels).
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
            InitializeGame();                // Initialize game.
            if (_Multiplayer)
                this.Width = 1200;
            this.CenterToScreen();
            this.gameBoardPictureBox.Controls.Clear(); // Erase the content of the picture Box.
            this.scoreLabel.Visible = true;  // Initialize interface: Essentially show the score label. 
            _Timer.Start();                  // Start timer.
            _InsectTimer.Start();            // Start insect timer.
            _Menu.InGame();                  // Set the configuration for a game end (hide/show labels).
            _RenderThread.Start();
            _InGame = true;
        }

        //////////////////////////
        // Method for ending game

        private void EndGame(Boolean victory)
        {
            _Timer.Stop();       // Stop the timer.
            _InsectTimer.Stop(); // Stop insect timer.
            LoadMenu();          // Load the menu.
            Invoke(_Menu.Get_GameOverDel(), _Multiplayer, victory);    // Set the configuration for a game end (hide/show labels).

            if (_Multiplayer)
            {
                while (!_Reception.Get_Container().Get_Msg().Equals("112"))
                {
                    _SendingContainer.Set_Msg("111");
                    _SendingContainer.Set_HasBeenModified(true);
                }
            }

            _RenderThread.Abort();       
        }

        #endregion

        #region Render

        ///////////////////////////////////////
        // Method to refresh the display (all)

        private void Render()
        {
            while (_InGame)
            {
                _Fruit.RenderFruit(this.gameBoardPictureBox); // Refresh the display of the fruit.     
                _Insect.RenderInsect(this.gameBoardPictureBox); // Refresh the display of the insect.
                _FullSnake.RenderSnake(this.gameBoardPictureBox); // Refresh the display of the snake.

                if (_Multiplayer)
                {
                    _Reception.Get_Container().Get_Snake().RenderMiniSnake(this.miniGameBoardPictureBox);
                    _Reception.Get_Container().Get_Fruit().RenderMiniFruit(this.miniGameBoardPictureBox);
                    _Reception.Get_Container().Get_Insect().RenderMiniInsect(this.miniGameBoardPictureBox);
                    _Reception.Get_Container().Get_ListWalls().RenderMiniWalls(this.miniGameBoardPictureBox);
                    _ListWalls.RenderWalls(this.gameBoardPictureBox);


                    //_FullSnake.RenderMiniSnake(this.miniGameBoardPictureBox);
                    //_Fruit.RenderMiniFruit(this.miniGameBoardPictureBox);
                    //_Insect.RenderMiniInsect(this.miniGameBoardPictureBox);

                }
            }
        }

        //////////////////////////////////////
        // Method to clean the miniPictureBox

        public void CleanMiniPictureBox()
        {
            Graphics myGraphics;  // Graphics for main drawing.
            SolidBrush myBrush;   // Brush for filling shapes.

            myGraphics = this.miniGameBoardPictureBox.CreateGraphics(); // Initialize the graphics. 
            myBrush = new System.Drawing.SolidBrush(Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))))); // Initialize the 2nd brush.
            myGraphics.FillRectangle(myBrush, 790, 140, this.miniGameBoardPictureBox.Width, this.miniGameBoardPictureBox.Height);
        }

        /////////////////////////////
        // Method to initialize font

        public void InitializeFont()
        {
            this._Font = new PersonalFont(); // Create new font.
            this.scoreLabel.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Set the font.
            this.opponentScoreLabel.Font = new System.Drawing.Font(_Font.getPersonalFont(), 16, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Set the font.
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
            if (_Multiplayer)
            {
                Invoke(_Sending.Get_NetworkDelegate());
                Invoke(_Reception.Get_NetworkDelegate());
            } 

            this.gameBoardPictureBox.Controls.Clear();    // Clear the panel.
            this.gameBoardPictureBox.Controls.Add(_Menu); // Attach the menu to the gameboard.
            this.scoreLabel.Visible = false;              // Initialize interface: Essentially show the score label. 
            _Menu.MainMenu();                             // Set the configuration for the menu (hide/show labels).
            this.Width = 800;
            this.CenterToScreen();
            _InGame = false;
            _Multiplayer = false;
            CleanMiniPictureBox();
            
        }

        /////////////////////////////////////////
        // Event for clicking the exitPictureBox

        private void exitPictureBox_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.Close();
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
            if (sample.Equals(null))
            {
                sample = new HighScoreData(1);
                sample.PlayerName[0] = "Test";
                sample.Score[0] = 2;
                sample.time[0] = 4;
                sample.PlayerName[1] = "John";
                sample.Score[1] = 20;
                sample.time[1] = 400;
                HighScore.SaveHighScores(sample);
            }

            _Menu.highScoresLabel.Text = "Player".PadRight(20) + "Score".PadRight(20) + "Time";
            for(int i=0;i<sample.Count;i++)
            {
                _Menu.highScoresLabel.Text+="\n"+sample.PlayerName[i].ToString().PadRight(25) + sample.Score[i].ToString().PadRight(21) + sample.time[i].ToString(); 
            }                             
            _Menu.HighScoreShow();


        }

        //////////////////////////////////////////
        // Event for clicking the backPictureBox

        private void backPictureBox_Click(object sender, EventArgs e)
        {
            _Menu.MainMenu(); // Set the configuration for the menu (hide/show labels).

            if (_Multiplayer && _Sending != null && _Reception != null)
            {
                Invoke(_Sending.Get_NetworkDelegate());
                Invoke(_Reception.Get_NetworkDelegate());
            } 

            _Multiplayer = false;
        }

        ///////////////////////////////////////////////
        // Event for clicking the createGamePictureBox

        private void createGamePictureBox_Click(object sender, EventArgs e)
        {
            _Menu.Host(); // Set the configuration for the menu (hide/show labels).
            InitializeNetwork(true);
        }

        //////////////////////////////////////////////
        // Event for clicking the joinGamePictureBox

        private void joinGamePictureBox_Click(object sender, EventArgs e)
        {
            _Menu.Client1(); // Set the configuration for the menu (hide/show labels).
            InitializeNetwork(false);
        }

        ///////////////////////////////////////
        // Event for clicking the okPictureBox

        private void okPictureBox_Click(object sender, EventArgs e)
        {
            if (!_Sending.Get_IsHost())
            {
                _Sending.Set_HostIpAdress(_Menu.ipTextBox1.Text + "." + _Menu.ipTextBox2.Text + "." + _Menu.ipTextBox3.Text + "." + _Menu.ipTextBox4.Text); // Set the entered ip by the user.
                _SendingContainer.Set_Msg("001");
                _SendingContainer.Set_HasBeenModified(true);
            }
            
            _Menu.Client2(); // Set the configuration for the menu (hide/show labels).
        }

        /////////////////////////////////////////////////////
        // Event for pressing the startGamePictureBox button

        private void startGamePictureBox_Click(object sender, EventArgs e)
        {
            if (_Sending.Get_IsHost())
            {
                _SendingContainer.Set_Msg("100");
                _SendingContainer.Set_HasBeenModified(true);
            }

            PlayGame();
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
            _Sending.Set_EndPoint(_Reception.Get_EndPoint());

            //switch (_ReceptionContainer.Get_Msg())
            switch(_Reception.Get_Container().Get_Msg())
            {
                case "001": _SendingContainer.Set_Msg("010");
                            _SendingContainer.Set_HasBeenModified(true);
                            Invoke(_Menu.Get_ProcessOnMenuThread_Del(), _Sending.Get_IsHost());
                            break;

                case "010": Invoke(_Menu.Get_ProcessOnMenuThread_Del(), _Sending.Get_IsHost());
                            break;

                case "100": if (!_InGame)
                                Invoke(_PlayGameDel);

                            _SendingContainer.Set_Msg("100");
                            _SendingContainer.Set_Snake(_FullSnake);
                            _SendingContainer.Set_Fruit(_Fruit);
                            _SendingContainer.Set_Insect(_Insect);
                            _SendingContainer.Set_ListWalls(_ListWalls);
                            _SendingContainer.Set_Nickname(_Nickname);
                            _SendingContainer.Set_Score(_Score);
                            _SendingContainer.Set_HasBeenModified(true);

                            break;

                case "101": _ListWalls.Get_ListWalls().Add(new Wall(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake, _Fruit, _Insect, _ListWalls.Get_ListWalls()));

                            _SendingContainer.Set_Msg("102");
                            _SendingContainer.Set_Snake(_FullSnake);
                            _SendingContainer.Set_Fruit(_Fruit);
                            _SendingContainer.Set_Insect(_Insect);
                            _SendingContainer.Set_ListWalls(_ListWalls);
                            _SendingContainer.Set_Nickname(_Nickname);
                            _SendingContainer.Set_Score(_Score);
                            _SendingContainer.Set_HasBeenModified(true);

                            break;

                case "102": _SendingContainer.Set_Msg("100");
                            _SendingContainer.Set_Snake(_FullSnake);
                            _SendingContainer.Set_Fruit(_Fruit);
                            _SendingContainer.Set_Insect(_Insect);
                            _SendingContainer.Set_ListWalls(_ListWalls);
                            _SendingContainer.Set_Nickname(_Nickname);
                            _SendingContainer.Set_Score(_Score);
                            _SendingContainer.Set_HasBeenModified(true);

                            break;

                case "111": Invoke(_EndGameDel, true);
                            _SendingContainer.Set_Msg("112");
                            _SendingContainer.Set_HasBeenModified(true);
                            break;
            }
        }

        //////////////////////////////////////////
        // Function initializing network elements

        private void InitializeNetwork(Boolean isHost)
        {
            _Multiplayer = true;

            _SendingContainer = new NetworkContainer();
            _ReceptionContainer = new NetworkContainer();

            _Sending = new Network(ref _SendingContainer, isHost, true, _CommandDispatcherDel);
            _Reception = new Network(ref _ReceptionContainer, isHost, false, _CommandDispatcherDel);

            InitializeNetworkThreads();
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

        #endregion
    }
}

