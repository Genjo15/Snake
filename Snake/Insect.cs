using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    [Serializable] 
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
        private Boolean _SinglePlayerEraseStreak;        // Boolean which informs if the insect has been moved.
        private Boolean _TwoPlayersEraseStreak;

        #endregion

        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public Insect(int width, int height)
        {
            Set_EraseStreaks(false);
            _SIDE = width / 27 - 2; // Initialize dynamically the side of the insect.
            _RandomNumber = new Random();   // Initialize the generator of random.
            _X = (_SIDE + 2) * (_RandomNumber.Next(width - _SIDE) / (_SIDE + 2));  // Set _X thanks to a generated number.
            _Y = (_SIDE + 2) * (_RandomNumber.Next(height - _SIDE) / (_SIDE + 2)); // Set _Y thanks to another generated number.
        }

        public Insect(int width, int height, int x, int y)
        {
            Set_EraseStreaks(false);
            _SIDE = width / 27 - 2; // Initialize dynamically the side of the insect.
            _RandomNumber = new Random();   // Initialize the generator of random.
            _X = x;
            _Y = y;
        }

        public Insect()
        {

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
            int tmpX; // Temporary X.
            int tmpY; // Temporary Y. 

            _LastX = _X; // Save _X in _LastX.
            _LastY = _Y; // Save _Y in _LastY.

            tmpX = Generate_X(width);
            tmpY = Generate_Y(height);

            while (!CheckPositions(tmpX, tmpY, snake, fruit))
            {
                tmpX = Generate_X(width);
                tmpY = Generate_Y(height);
            }

            _X = tmpX;
            _Y = tmpY;

            Set_EraseStreaks(true);
        }

        ////////////////////////////////////
        // Move insect (make it unreachable)

        public void MoveInsect()
        {
            _LastX = _X; // Save _X in _LastX.
            _LastY = _Y; // Save _Y in _LastY.
            _X = -666; // Make the item unreachable for the user by changing its X & Y.
            _Y = -666;

            Set_EraseStreaks(true);
        }

        ///////////////
        // Generate _X

        private int Generate_X(int width)
        {
            int generatedNumber;
            generatedNumber = (_SIDE + 2) * (_RandomNumber.Next(width - _SIDE) / (_SIDE + 2));  // Set _X thanks to a generated number.
            return generatedNumber;
        }

        ///////////////
        // Generate _Y

        private int Generate_Y(int height)
        {
            int generatedNumber;
            generatedNumber = (_SIDE + 2) * (_RandomNumber.Next(height - _SIDE) / (_SIDE + 2)); // Set _Y thanks to another generated number.
            return generatedNumber;
        }

        /////////////////////////////////////////////
        // Check if temporary X & Y are on the snake

        private Boolean CheckPositions(int x, int y, FullSnake snake, Fruit fruit)
        {
            Boolean ok = true;

            for (int i = 0; i < snake.Get_SnakeSize(); i++)
            {
                if (((snake.Get_Snake()[i].Get_X() == x) && (snake.Get_Snake()[i].Get_Y() == y)) || ((snake.Get_Snake()[i].Get_X() == (x + (_SIDE / 2 + 1))) && (snake.Get_Snake()[i].Get_Y() == y)) || ((snake.Get_Snake()[i].Get_X() == x) && (snake.Get_Snake()[i].Get_Y() == (y + (_SIDE / 2) + 1))) || ((snake.Get_Snake()[i].Get_X() == (x + (_SIDE / 2 + 1))) && (snake.Get_Snake()[i].Get_Y() == (y + (_SIDE / 2) + 1)))) // If the insect is in the same position of one of the snake parts...
                {
                    ok = false;
                    Console.WriteLine("Insect is on the snake");
                }
            }

            if (((fruit.Get_X() == x) && (fruit.Get_Y() == y)) || ((fruit.Get_X() == (x + (_SIDE / 2 + 1))) && (fruit.Get_Y() == y)) || ((fruit.Get_X() == x) && (fruit.Get_Y() == (y + (_SIDE / 2) + 1))) || ((fruit.Get_X() == (x + (_SIDE / 2 + 1))) && (fruit.Get_Y() == (y + (_SIDE / 2) + 1)))) // If the insect is in the same position of the fruit...
            {
                ok = false;
                Console.WriteLine("Insect is on the fruit");
            }

            return ok;
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

            if (_SinglePlayerEraseStreak)
            {
                myGraphics.FillRectangle(myBrush2, new Rectangle(_LastX, _LastY, (gameBoardPictureBox.Width / 27 - 2), (gameBoardPictureBox.Width / 27 - 2))); // Erase the streak.
                _SinglePlayerEraseStreak = false;
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

            if (_TwoPlayersEraseStreak)
            {
                myGraphics.FillRectangle(myBrush2, new Rectangle(_LastX/2, _LastY/2, (gameBoardPictureBox.Width / 27 - 1), (gameBoardPictureBox.Width / 27 - 1))); // Erase the streak.
                _TwoPlayersEraseStreak = false;
            }

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

        ///////////////////////////
        // Set EraseStreaks (both)

        public void Set_EraseStreaks(Boolean b)
        {
            _SinglePlayerEraseStreak = b;
            _TwoPlayersEraseStreak = b;
        }

        #endregion
    }
}
