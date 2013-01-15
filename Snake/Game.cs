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

        private FullSnake _FullSnake;           // The complete snake (which is a list of snake elements).
        private int _Score;                     // The score.
        private Timer _Timer;                   // The timer.
        private int _Direction;                 // The direction.
        private const int _TIMERINTERVAL = 100; // Timer interval.


        /**************************************************** Constructor ****************************************************/

        public Game()
        {
            InitializeComponent(); // Initialize components of the form (essentially the menu).
            InitializeGame();      // Initialize game.
        }

        /****************************************************** Methods ******************************************************/

        ////////////////////////////////////
        // Method for initializing the game

        public void InitializeGame()
        {
            _FullSnake = new FullSnake(); // New FullSnake.

            _Score = 0;     // Score is set to 0.
            _Direction = 1; // First direction is initially set to 1 (right).

            _Timer = new Timer();                         // New timer.
            _Timer.Interval = _TIMERINTERVAL;             // Interval of the timer is set.
            _Timer.Tick += new EventHandler(_Timer_Tick); // New EventHandler (the tick of the timer).
            
        }


        /////////////////////////////
        //  Event for the timer tick

        void _Timer_Tick(object sender, EventArgs e)
        {
            this.GameBoard = _FullSnake.Render(this.GameBoard, _Direction); // Refresh the panel (GameBoard).
            this.textBox1.Focus(); // Focus on the textbox (this invisible textbox has been created to manage keyDown events).
            this.scoreLabel.Text = "Score : " + _Score; // Show the score.
        }


        //////////////////////////////////////////
        // Method for initializing the interface

        private void InitializeInterface()
        {
            this.titleLabel.Visible = false; // Hide the title label.
            this.playLabel.Visible = false;  // Hide the play label.
            this.scoreLabel.Visible = true;  // Show the score label.
        }


        #region Events

        /////////////////////////////////////
        // Event for clicking the exit label

        private void exitLabel_Click(object sender, EventArgs e)
        {
            Close();
        }


        /////////////////////////////////////
        // Event for clicking the play label

        private void playLabel_Click(object sender, EventArgs e)
        {
            InitializeInterface(); // Initialize interface.
            _Timer.Start();        // Start timer.
        }


        //////////////////////////////////////
        // Event for pressing keyboard touchs 

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                _Direction = 0; // Snake will move up.
            else if (e.KeyCode == Keys.Right)
                _Direction = 1; // Snake will move right.
            else if (e.KeyCode == Keys.Down)
                _Direction = 2; // Snake will move down.
            else if (e.KeyCode == Keys.Left)
                _Direction = 3; // Snake will move left.
        }

        #endregion






    }
}
