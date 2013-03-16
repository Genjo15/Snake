using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;



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
        private int _Direction;                   // The direction.
        private Timer _Timer;                     // The timer.
        private Timer _InsectTimer;               // The timer for the fruit.
        private int _InsectTimerCounter;          // Counter used for the FruitTImerTick
        private Boolean _InsectIsPresent;         // Boolean which indicates if the insect is present or not.
        private Boolean _RefreshItem;             // Boolean for refreshing the item or not.
        private Boolean _GameOver;                // Boolean which detects if the game is over or not.
        private Boolean _Multiplayer;             // Boolean which determines if the game is multiplayer or not.
        private int _TimerInterval;               // Timer interval.
        private Menu _Menu;                       // The menu.

        private PersonalFont _MyFont;             // The special font.

        #endregion

        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public Game()
        {
            InitializeComponent(); // Initialize components of the form (essentially the menu).
            InitializeFont(); // Initialize font.
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
            _Insect = new Insect(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height,-666, -666); // New insect.
            
            _Score = 0;                 // Score is set to 0.
            _TimerInterval = 140;       // Timer interval tick is set to 140 ms.
            _Direction = 1;             // First direction is initially set to 1 (right).
            _GameOver = false;          // _GameOver is initialized to false.
            _RefreshItem = false;       // _RefreshItem is initialized to false.
            _InsectIsPresent = false;   // _InsectIsPresent is initialized to false.
            _Multiplayer = false;       // _Multiplayer is initialized to false. BECAUSE NOT IMPLEMENTED YET !!!!!!!!!!!!

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

                _RefreshItem = false; // Set the boolean for the refresh of the item to false.

                if (_Fruit.IsReached(_FullSnake.Get_Snake()[0]))
                {
                    _Fruit.MoveFruit(this.gameBoardPictureBox.Width, this.gameBoardPictureBox.Height, _FullSnake); // Move fruit positions.
                    _Score = _Score + _Fruit.Get_POINT(); // Increment the score.
                    if (_Timer.Interval <= 150 && _Timer.Interval >= 110)   //////
                        _Timer.Interval -= 5;                               // Increase the difficulty by decreasing the timer interval.
                    else if (_Timer.Interval < 110 && _Timer.Interval > 70) //
                        _Timer.Interval -= 10;                              //
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

                this.textBox1.Focus(); // Focus on the textbox (this invisible textbox has been created to manage keyDown events).
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
            _Menu.back2PictureBox.Click += new EventHandler(back2PictureBox_Click);             //
        }

        ////////////////////////
        // Method for play game

        private void PlayGame()
        {
            InitializeGame();                // Initialize game.
            if (_Multiplayer)
                this.Width = 1200; 
            this.gameBoardPictureBox.Controls.Clear(); // Erase the content of the panel.
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

            // SECTION IN CONSTRUCTION!!!
            /*if (_Multiplayer)
            {
                _Insect.RenderMiniInsect(this.miniGameBoardPictureBox); // for test.
                _FullSnake.RenderMiniSnake(this.miniGameBoardPictureBox); // for test.
            }*/
        }

        /////////////////////////////
        // Method to initialize font

        public void InitializeFont()
        {
            this._MyFont = new PersonalFont(); // Create new font.
            this.scoreLabel.Font = new System.Drawing.Font(_MyFont.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Set the font.
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
            _Menu.MainMenu();                         // Set the configuration for the menu (hide/show labels).
            this.Width = 800; // for test.
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
            PlayGame(); // Play game.
        }

        //////////////////////////////////////////
        // Event for clicking the multiplayerPictureBox

        private void multiplayerPictureBox_Click(object sender, EventArgs e)
        {
            _Menu.Multiplayer(); // Set the configuration for the menu (hide/show labels).
        }

        ///////////////////////////////////////////////
        // Event for clicking the highScoresPictureBox

        private void highScoresPictureBox_Click(object sender, EventArgs e)
        {
            // LOUIS A TOI D'JOUER (YUUUUGIYOOOOOOOO)!!!!!!!
        }

        //////////////////////////////////////////
        // Event for clicking the backPictureBox

        private void backPictureBox_Click(object sender, EventArgs e)
        {
            _Menu.MainMenu(); // Set the configuration for the menu (hide/show labels).
        }

        ///////////////////////////////////////////////
        // Event for clicking the createGamePictureBox

        private void createGamePictureBox_Click(object sender, EventArgs e)
        {
            _Menu.Host(); // Set the configuration for the menu (hide/show labels).
        }

        //////////////////////////////////////////////
        // Event for clicking the joinGamePictureBox

        private void joinGamePictureBox_Click(object sender, EventArgs e)
        {
            _Menu.Client(); // Set the configuration for the menu (hide/show labels).
        }

        //////////////////////////////////////////
        // Event for clicking the back2PictureBox

        private void back2PictureBox_Click(object sender, EventArgs e)
        {
            _Menu.Multiplayer(); // Set the configuration for the menu (hide/show labels).
        }

        //////////////////////////////////////
        // Event for pressing keyboard touchs 

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if(_FullSnake.Get_Snake()[0].Get_Direction() != 2)_Direction = 0; // Snake will move up.
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
    }
}
