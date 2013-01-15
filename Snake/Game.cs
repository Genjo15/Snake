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
        private FullSnake _FullSnake;
        private int _Score;
        private Timer _Timer;
        private int _Direction;

        
        //This is the game methode
        public Game()
        {
            InitializeComponent();
            InitializeGame();
        }

        public void InitializeGame()
        {
            _FullSnake = new FullSnake();

            _Score = 0;
            _Direction = 1;

            _Timer = new Timer();
            _Timer.Interval = 100;
            _Timer.Tick += new EventHandler(_Timer_Tick);
            
        }

        void _Timer_Tick(object sender, EventArgs e)
        {
            this.GameBoard = _FullSnake.Render(this.GameBoard, _Direction);
            this.textBox1.Focus();
        }

        private void InitializeInterface()
        {
            this.titleLabel.Visible = false;
            this.playLabel.Visible = false;
            this.scoreLabel.Visible = true;
            this.scoreLabel.Text = "Score : " + _Score;
        }

        #region Events
        private void exitLabel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void playLabel_Click(object sender, EventArgs e)
        {
            InitializeInterface();
            _Timer.Start();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                _Direction = 0;
            else if (e.KeyCode == Keys.Right)
                _Direction = 1;
            else if (e.KeyCode == Keys.Down)
                _Direction = 2;
            else if (e.KeyCode == Keys.Left)
                _Direction = 3;

            this.textBox1.Focus();
        }

        #endregion






    }
}
