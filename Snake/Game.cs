using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Snake;





namespace Snake
{
    public partial class Game : Form
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

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

        private List<Panel> _SnakeGraphicalParts;
        private PersonalFont _Font;               // The special font.

        // Network

        Network _Sending;                                     // Network components (for sending data).
        Network _Reception;                                   // Network components (for receiving data).

        NetworkContainer _SendingContainer;                   // Temporary container for sending data.
        NetworkContainer _ReceptionContainer;                 // Reception container for receiving data.
        System.Threading.Thread _ReceptionThread;             // The reception thread.
        System.Threading.Thread _SendingThread;               // The sending thread.
        private Boolean _Multiplayer;                         // Boolean which determines if the game is multiplayer or not.
        private Boolean _InGame;                              // Boolean which determines if the multiplayer match is running or not.
        private delegate void processOnMainThread();          // The delegate
        private processOnMainThread _CommandDispatcherDel;    // The command dispatcher delegate.
        private processOnMainThread _PlayGameDel;             // The play game delegate.
        private String _HostIpAdress;                         //

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public Game()
        {
            InitializeComponent(); // Initialize components of the form (essentially the menu).
            InitializeFont(); // Initialize font.
            _CommandDispatcherDel = new processOnMainThread(NetworkProcessOnMainThread); // Initialize delegate.
            _PlayGameDel = new processOnMainThread(PlayGame);
            _Nickname = "";
            _HostIpAdress = "";
            _Multiplayer = false;
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

            _SnakeGraphicalParts = new List<Panel>();

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
        }

        /////////////////////////////
        //  Event for the timer tick

        public void TimerTick(object sender, EventArgs e)
        {
            _GameOver = _FullSnake.CheckCollision(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height); // Check collision.

            if (!_GameOver)
            {
                _FullSnake.UpdateSnake(_Direction, this.gameBoardPictureBox.Width); // Update the movement of the snake.

                if (_Fruit.IsReached(_FullSnake.Get_Snake()[0]))
                {
                    _Fruit.MoveFruit(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake); // Move fruit positions.
                    _Score = _Score + _Fruit.Get_POINT(); // Increment the score.
                    _FullSnake.AddSnakePart(this.gameBoardPictureBox.Width); // Add a Snake part.
                    _Fruit.Set_IsReached(true);
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

                Render(); // Refresh the display.
            }

            else EndGame(); // end game if _GameOver is true.
        }

        /////////////////////////////
        // Event for the insect timer

        public void InsectTimerTick(object sender, EventArgs e)
        {
            _InsectTimerCounter = _InsectTimerCounter + 1; // Increment the counter.

            if ((_InsectTimerCounter % 8 == 0) && (_InsectIsPresent == false))
            {
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
            {
                this.Width = 1200;
            }
            this.CenterToScreen();
            this.gameBoardPictureBox.Controls.Clear(); // Erase the content of the picture Box.
            this.scoreLabel.Visible = true;  // Initialize interface: Essentially show the score label. 
            _Timer.Start();                  // Start timer.
            _InsectTimer.Start();            // Start insect timer.
            _Menu.InGame();                  // Set the configuration for a game end (hide/show labels).
        }

        //////////////////////////
        // Method for ending game

        private void EndGame()
        {
            _Timer.Stop();       // Stop the timer.
            _InsectTimer.Stop(); // Stop insect timer.
            LoadMenu();          // Load the menu.
            _Menu.GameOver();    // Set the configuration for a game end (hide/show labels).

            _InGame = false;
        }

        #endregion

        #region Render

        ///////////////////////////////////////
        // Method to refresh the display (all)

        private void Render()
        {
            _Fruit.RenderFruit(this.gameBoardPictureBox); // Refresh the display of the fruit.     
            _Insect.RenderInsect(this.gameBoardPictureBox); // Refresh the display of the insect.
            _FullSnake.RenderSnake(this.gameBoardPictureBox); // Refresh the display of the snake.

            //_FullSnake.RenderMiniSnake(this.miniGameBoardPictureBox);

            // SECTION IN CONSTRUCTION!!!
            if (_Multiplayer && _InGame)
            {
                //_Insect.RenderMiniInsect(this.miniGameBoardPictureBox); // for test.
                //RenderMiniSnake();
                    //_ReceptionContainer.Get_Snake().RenderMiniSnake(this.miniGameBoardPictureBox); // for test.
                    _Reception.Get_Container().Get_Snake().RenderMiniSnake(this.miniGameBoardPictureBox); // for test.
            }
        }

        //////////////////////////
        // Display the mini snake

        public void RenderMiniSnake()
        {
            for (int i = 0; i < _Reception.Get_Container().Get_Snake().Get_SnakeSize(); i++)
            {
                if (_SnakeGraphicalParts.Count < _Reception.Get_Container().Get_Snake().Get_SnakeSize()) // If there is not enough panel in the pool ...
                    _SnakeGraphicalParts.Add(new Panel());                   // ...Add it one.

                _SnakeGraphicalParts[i].Location = new System.Drawing.Point(_Reception.Get_Container().Get_Snake().Get_Snake()[i].Get_X() / 2, _Reception.Get_Container().Get_Snake().Get_Snake()[i].Get_Y() / 2);  // Definition of the panel location.
                _SnakeGraphicalParts[i].Size = new System.Drawing.Size(_Reception.Get_Container().Get_Snake().Get_Snake()[i].Get_SIDE() / 2, _Reception.Get_Container().Get_Snake().Get_Snake()[i].Get_SIDE() / 2); // Definition of the panel size.
                _SnakeGraphicalParts[i].BackColor = System.Drawing.Color.Black; // Definition of the panel color.

                this.miniGameBoardPictureBox.Controls.Add(_SnakeGraphicalParts[i]); // Attach the panel to the gameboard.
            }
        }

        /////////////////////////////
        // Method to initialize font

        public void InitializeFont()
        {
            this._Font = new PersonalFont(); // Create new font.
            this.scoreLabel.Font = new System.Drawing.Font(_Font.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Set the font.
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
            this.gameBoardPictureBox.Controls.Clear();    // Clear the panel.
            this.gameBoardPictureBox.Controls.Add(_Menu); // Attach the menu to the gameboard.
            this.scoreLabel.Visible = false;              // Initialize interface: Essentially show the score label. 
            _Menu.MainMenu();                             // Set the configuration for the menu (hide/show labels).
            this.Width = 800;
            this.CenterToScreen();
            this.miniGameBoardPictureBox.Controls.Clear();
            
        }

        /////////////////////////////////////////
        // Event for clicking the exitPictureBox

        private void exitPictureBox_Click_1(object sender, EventArgs e)
        {
            Close(); // Exit program
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

            if (_Multiplayer)
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
            _Multiplayer = true;

            _SendingContainer = new NetworkContainer();
            _ReceptionContainer = new NetworkContainer();
            _Sending = new Network(ref _SendingContainer, true, true, _CommandDispatcherDel);
            _Reception = new Network(ref _ReceptionContainer, true, false, _CommandDispatcherDel);

            InitializeNetworkThreads();
        }

        //////////////////////////////////////////////
        // Event for clicking the joinGamePictureBox

        private void joinGamePictureBox_Click(object sender, EventArgs e)
        {
            _Menu.Client1(); // Set the configuration for the menu (hide/show labels).
            _Multiplayer = true;

            _SendingContainer = new NetworkContainer();
            _ReceptionContainer = new NetworkContainer();
            _Sending = new Network(ref _SendingContainer, false, true, _CommandDispatcherDel);
            _Reception = new Network(ref _ReceptionContainer, false, false, _CommandDispatcherDel);

            InitializeNetworkThreads();
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
                _InGame = true;
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
                            {
                                Invoke(_PlayGameDel);
                                _InGame = true;
                            }

                            _SendingContainer.Set_Msg("100");
                            _SendingContainer.Set_Snake(_FullSnake);
                            _SendingContainer.Set_HasBeenModified(true);

                            break;
            }
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

