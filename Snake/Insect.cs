using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    class Insect
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private int _X;                // Position in X.
        private int _Y;                // Position in Y.
        private int _LastX;            // Last position in X.
        private int _LastY;            // Last position in Y.
        private const int _POINT = 25; // The points earned when item reached.
        private int _SIDE;             // Size of the insect.
        private Random _RandomNumber;  // Random number.
        private Boolean _Moved;        // Boolean which informs if the insect has been moved.

        #endregion

        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public Insect(int width, int height)
        {
            _Moved = false;
            _SIDE = width / 27 - 2; // Initialize dynamically the side of the insect.
            _RandomNumber = new Random();   // Initialize the generator of random.
            _X = (_SIDE + 2) * (_RandomNumber.Next(width - _SIDE) / (_SIDE + 2));  // Set _X thanks to a generated number.
            _Y = (_SIDE + 2) * (_RandomNumber.Next(height - _SIDE) / (_SIDE + 2)); // Set _Y thanks to another generated number.
        }

        public Insect(int width, int height, int x, int y)
        {
            _Moved = false;
            _RandomNumber = new Random();   // Initialize the generator of random.
            _X = x;
            _Y = y;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        ///////////////////////////////////////////////////////////////
        // Method which determines if the snake has reached the insect

        public Boolean IsReached(SnakePart snakePart)
        {
            Boolean isReached = false;

            if (((snakePart.Get_X() == _X) && (snakePart.Get_Y() == _Y)) || ((snakePart.Get_X() == (_X + (_SIDE / 2 + 1))) && (snakePart.Get_Y() == _Y)) || ((snakePart.Get_X() == _X) && (snakePart.Get_Y() == (_Y + (_SIDE / 2) + 1))) || ((snakePart.Get_X() == (_X + (_SIDE / 2 + 1))) && (snakePart.Get_Y() == (_Y + (_SIDE / 2) + 1))))
                isReached = true;
    
            return isReached;
        }

        ///////////////
        // Move insect

        public void MoveInsect(int width, int height, FullSnake snake, Fruit fruit)
        {
            _LastX = _X; // Save _X in _LastX.
            _LastY = _Y; // Save _Y in _LastY.

            _SIDE = width / 27 - 2; // Initialize dynamically the side of the insect.
            _X = (_SIDE + 2) * (_RandomNumber.Next(width - _SIDE) / (_SIDE + 2));  // Set _X thanks to a generated number.
            _Y = (_SIDE + 2) * (_RandomNumber.Next(height - _SIDE) / (_SIDE + 2)); // Set _Y thanks to another generated number.
            _LastX = (_SIDE + 2) * (_RandomNumber.Next(width - _SIDE) / (_SIDE + 2));  // Set _LastX thanks to a generated number.
            _LastY = (_SIDE + 2) * (_RandomNumber.Next(height - _SIDE) / (_SIDE + 2)); // Set _LastY thanks to another generated number.

            for (int i = 0; i < snake.Get_SnakeSize(); i++)
            {
                if (((snake.Get_Snake()[i].Get_X() == _X) && (snake.Get_Snake()[i].Get_Y() == _Y)) || ((snake.Get_Snake()[i].Get_X() == (_X + (_SIDE / 2 + 1))) && (snake.Get_Snake()[i].Get_Y() == _Y)) || ((snake.Get_Snake()[i].Get_X() == _X) && (snake.Get_Snake()[i].Get_Y() == (_Y + (_SIDE / 2) + 1))) || ((snake.Get_Snake()[i].Get_X() == (_X + (_SIDE / 2 + 1))) && (snake.Get_Snake()[i].Get_Y() == (_Y + (_SIDE / 2) + 1)))) // If the insect is in the same position of one of the snake parts...
                {
                    MoveInsect(width, height, snake, fruit); // Move the insect again.
                    Console.WriteLine("Insect is on the snake");
                }
            }

            if (((fruit.Get_X() == _X) && (fruit.Get_Y() == _Y)) || ((fruit.Get_X() == (_X + (_SIDE / 2 + 1))) && (fruit.Get_Y() == _Y)) || ((fruit.Get_X() == _X) && (fruit.Get_Y() == (_Y + (_SIDE / 2) + 1))) || ((fruit.Get_X() == (_X + (_SIDE / 2 + 1))) && (fruit.Get_Y() == (_Y + (_SIDE / 2) + 1)))) // If the insect is in the same position of the fruit...
            {
                MoveInsect(width, height, snake, fruit); // Move the insect again.
                Console.WriteLine("Insect moved becoz of the fruit");
            }
                

            _Moved = true;
        }

        ////////////////////////////////////
        // Move insect (make it unreachable)

        public void MoveInsect()
        {
            _LastX = _X; // Save _X in _LastX.
            _LastY = _Y; // Save _Y in _LastY.
            _X = -666; // Make the item unreachable for the user by changing its X & Y.
            _Y = -666;

            _Moved = true;
        }

        #endregion

        #region RenderMethods

        ///////////////////////////////////////////////
        // Method to refresh the display of the insect 

        public void RenderInsect(PictureBox gameBoardPictureBox)
        {
            Graphics myGraphics;  // Graphics for main drawing.
            SolidBrush myBrush;   // Brush for filling shapes.
            SolidBrush myBrush2;  // Second brush for erasing streaks.
            Image _InsectPicture = Snake.Properties.Resources.Insect; // The picture of the insect.

            myGraphics = gameBoardPictureBox.CreateGraphics(); // Initialize the 2nd graphics. 
            myBrush = new System.Drawing.SolidBrush(Color.Black); // Initialize the first brush.
            myBrush2 = new System.Drawing.SolidBrush(Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))))); // Initialize the 2nd brush.

            if (_Moved)
            {
                myGraphics.FillRectangle(myBrush2, new Rectangle(_LastX, _LastY, (gameBoardPictureBox.Width / 27 - 2), (gameBoardPictureBox.Width / 27 - 2))); // Erase the streak.
                _Moved = false;
            }

            myGraphics.DrawImage(_InsectPicture, new Rectangle(_X, _Y, (gameBoardPictureBox.Width / 27 - 2), (gameBoardPictureBox.Width / 27 - 2))); // Draw insect.
        }

        ////////////////////////////////////////////////////
        // Method to refresh the display of the mini insect 

        public void RenderMiniInsect(PictureBox gameBoardPictureBox)
        {
            Graphics myGraphics;  // Graphics for main drawing.
            SolidBrush myBrush;   // Brush for filling shapes.
            SolidBrush myBrush2;  // Second brush for erasing streaks.
            Image _InsectPicture = Snake.Properties.Resources.Insect; // The picture of the insect.

            myGraphics = gameBoardPictureBox.CreateGraphics(); // Initialize the 2nd graphics. 
            myBrush = new System.Drawing.SolidBrush(Color.Black); // Initialize the first brush.
            myBrush2 = new System.Drawing.SolidBrush(Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))))); // Initialize the 2nd brush.

            //if (_InsectMoved)
            //{
                myGraphics.FillRectangle(myBrush2, new Rectangle(_LastX/2, _LastY/2, (gameBoardPictureBox.Width / 27 - 1), (gameBoardPictureBox.Width / 27 - 1))); // Erase the streak.
                //_InsectMoved = false;
            //}
            myGraphics.DrawImage(_InsectPicture, new Rectangle(_X/2, _Y/2, (gameBoardPictureBox.Width / 27 - 1), (gameBoardPictureBox.Width / 27 - 1))); // Draw insect.
        }

        #endregion

        #region Accessors&Mutators

        //////////
        // Get _X

        public int Get_X()
        {
            return _X;
        }

        //////////
        // Get _Y

        public int Get_Y()
        {
            return _Y;
        }

        //////////////
        // Get _POINT

        public int Get_POINT()
        {
            return _POINT;
        }

        /////////////
        // Get _SIDE

        public int Get_SIDE()
        {
            return _SIDE;
        }

        //////////
        // Set _X

        public void Set_X(int x)
        {
            _X = x;
        }

        //////////
        // Set _Y

        public void Set_Y(int y)
        {
            _Y = y;
        }

        #endregion
    }
}
