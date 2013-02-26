using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private int _TimerInterval;               // Timer interval.
        private Menu _Menu;                       // The menu.
        private List<RoundedPanel> _SnakeGraphicalParts; // List of panel which contains Snake graphical parts.
        private PersonalFont _MyFont;              // The special font.

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
            _FullSnake = new FullSnake(this.gameBoard.Width); // New FullSnake.
            _Fruit = new Fruit(this.gameBoard.Width, this.gameBoard.Height); // New fruit.
            _Insect = new Insect(this.gameBoard.Width, this.gameBoard.Height,-666, -666); // New insect.

            _SnakeGraphicalParts = new List<RoundedPanel>();

            _Score = 0;               // Score is set to 0.
            _TimerInterval = 140;     // Timer interval tick is set to 140 ms.
            _Direction = 1;           // First direction is initially set to 1 (right).
            _GameOver = false;        // _GameOver is initialized to false.
            _RefreshItem = false;     //_RefreshItem is initialized to false.
            _InsectIsPresent = false; // _InsectIsPresent is initialized to false.

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
            _GameOver = _FullSnake.CheckCollision(this.gameBoard.Width, this.gameBoard.Height); // Check collision.

            if (!_GameOver)
            {
                _FullSnake.UpdateSnake(_Direction, this.gameBoard.Width); // Update the movement of the snake.

                _RefreshItem = false; // Set the boolean for the refresh of the item to false.

                if (_Fruit.IsReached(_FullSnake.Get_Snake()[0]))
                {
                    _Fruit.MoveFruit(this.gameBoard.Width, this.gameBoard.Height, _FullSnake); // Move fruit positions.
                    _Score = _Score + _Fruit.Get_POINT(); // Increment the score.
                    if (_Timer.Interval <= 150 && _Timer.Interval >= 110)   //////
                        _Timer.Interval -= 5;                               // Increase the difficulty by decreasing the timer interval.
                    else if (_Timer.Interval < 110 && _Timer.Interval > 70) //
                        _Timer.Interval -= 10;                              //
                    _FullSnake.AddSnakePart(this.gameBoard.Width); // Add a Snake part.
                    _RefreshItem = true; // Set the boolean for the refresh of the item to true.
                }

                if (_Insect.IsReached(_FullSnake.Get_Snake()[0]))
                {
                    _InsectTimerCounter = 0;               // Reset the counter.
                    _Insect.Set_X(-666);                   // Make the item unreachable for the user by changing its X & Y.
                    _Insect.Set_Y(-666);                   //        ,,
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
        // Event for the fruit timer

        public void InsectTimerTick(object sender, EventArgs e)
        {
            _InsectTimerCounter = _InsectTimerCounter + 1; // Increment the counter.

            if ((_InsectTimerCounter % 8 == 0) && (_InsectIsPresent == false))
            {
                _Insect.MoveInsect(this.gameBoard.Width, this.gameBoard.Height, _FullSnake); // Move insect.
                _InsectTimerCounter = 0; // Reset the counter.
                _InsectIsPresent = true; // Set the boolean to true.
            }

            else if ((_InsectTimerCounter % 3 == 0) && (_InsectIsPresent == true)) // If the user takes too much time to pick up the insect, it will disappear.
            {
                _Insect.Set_X(-666); // Make the item unreachable for the user by changing its X & Y.
                _Insect.Set_Y(-666);
                _InsectTimerCounter = 0;  // Reset the counter.
                _InsectIsPresent = false; // Set the boolean to false.
            }
        }

        ///////////////////////////////
        // Method for loading the menu

        private void LoadMenu()
        {
            _Menu = new Menu();                                                 // Instanciate a new menu;
            _Menu.Location = new Point(20, 20);                                 // Define the location of the menu.
            this.gameBoard.Controls.Add(_Menu);                                 // Add it to the gameboard.
            _Menu.MainMenu();                                                   // Set the configuration for a game start (hide/show labels).
            _Menu.playPictureBox.Click += new EventHandler(playPictureBox_Click);         ///// 
            _Menu.retryPictureBox.Click += new EventHandler(retryPictureBox_Click);       // Define event handlers
            _Menu.mainMenuPictureBox.Click += new EventHandler(mainMenuPictureBox_Click); // Define event handlers
        }

        ////////////////////////
        // Method for play game

        private void PlayGame()
        {
            InitializeGame();                // Initialize game.
            this.gameBoard.Controls.Clear(); // Erase the content of the panel.
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
            RenderSnake(); // Refresh the display of the snake
            if ((_FullSnake.Get_Snake().Count == 3) || (_RefreshItem)) // Refresh the display of the fruit (only if it has been reached or at the begin of the game).
                RenderFruit();
            RenderInsect(); // Refresh the display of the insect
        }

        ///////////////////////////////////////////////
        // Method to refresh the display of the fruit 

        public void RenderFruit()
        {
            this.fruitPictureBox.Location = new System.Drawing.Point(_Fruit.Get_X(), _Fruit.Get_Y()); // Change the location of the picture box.
            this.gameBoard.Controls.Add(this.fruitPictureBox); // Attach the picturebox to the gameboard.
        }

        ///////////////////////////////////////////////
        // Method to refresh the display of the insect 

        public void RenderInsect()
        {
            this.insectPictureBox.Location = new System.Drawing.Point(_Insect.Get_X(), _Insect.Get_Y()); // Change the location of the picture box.
            this.gameBoard.Controls.Add(this.insectPictureBox); // Attach the picturebox to the gameboard.
        }

        //////////////////////////////////////////////
        // Method to refresh the display of the snake

        public void RenderSnake()
        {
            for (int i = 0; i < _FullSnake.Get_SnakeSize(); i++)
            {
                if (_SnakeGraphicalParts.Count < _FullSnake.Get_SnakeSize()) // If there is not enough panel in the pool ...
                    //_SnakeGraphicalParts.Add(new Panel());                   // ...Add it one.
                    _SnakeGraphicalParts.Add(new RoundedPanel());                   // ...Add it one.

                _SnakeGraphicalParts[i].Location = new System.Drawing.Point(_FullSnake.Get_Snake()[i].Get_X(), _FullSnake.Get_Snake()[i].Get_Y());  // Definition of the panel location.
                _SnakeGraphicalParts[i].Size = new System.Drawing.Size(_FullSnake.Get_Snake()[i].Get_SIDE(), _FullSnake.Get_Snake()[i].Get_SIDE()); // Definition of the panel size.
                _SnakeGraphicalParts[i].BackColor = System.Drawing.Color.Black; // Definition of the panel color.

                this.gameBoard.Controls.Add(_SnakeGraphicalParts[i]); // Attach the panel to the gameboard.
            }
        }

        /////////////////////////////
        // Method to initialize font

        public void InitializeFont()
        {
            this._MyFont = new PersonalFont(); // Create new font.
            this.scoreLabel.Font = new System.Drawing.Font(_MyFont.getPersonalFont(), 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.gameBoard.Controls.Clear();    // Clear the panel.
            this.gameBoard.Controls.Add(_Menu); // Attach the menu to the gameboard.
            this.scoreLabel.Visible = false;    // Initialize interface: Essentially show the score label. 
            _Menu.MainMenu();                   // Set the configuration for the menu (hide/show labels).
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
            this.insectPictureBox.Visible = true; // Show the insect.
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
