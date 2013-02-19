using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Insect
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private int _X;                // Position in X.
        private int _Y;                // Position in Y.
        private const int _POINT = 25; // The points earned when item reached.
        private const int _SIDE = 26;  // Size of the panel side.
        private Random randomNumber;

        #endregion

        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public Insect(int width, int height)
        {
            randomNumber = new Random();   // Initialize the generator of random.
            _X = (_SIDE + 2) * (randomNumber.Next(width - _SIDE) / (_SIDE + 2));  // Set _X thanks to a generated number.
            _Y = (_SIDE + 2) * (randomNumber.Next(height - _SIDE) / (_SIDE + 2)); // Set _Y thanks to another generated number.
        }

        public Insect(int width, int height, int x, int y)
        {
            randomNumber = new Random();   // Initialize the generator of random.
            _X = x;
            _Y = y;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        ///////////////////////////////////////////////////////////////
        // Method which determines if the snake has reached the insect

        public Boolean IsReached(SnakePart snake)
        {
            Boolean isReached = false;

            if (((snake.Get_X() == _X) && (snake.Get_Y() == _Y)) || ((snake.Get_X() == (_X + (_SIDE / 2 + 1))) && (snake.Get_Y() == _Y)) || ((snake.Get_X() == _X) && (snake.Get_Y() == (_Y + (_SIDE / 2) + 1))) || ((snake.Get_X() == (_X + (_SIDE / 2 + 1))) && (snake.Get_Y() == (_Y + (_SIDE / 2) + 1))))
                isReached = true;
    
            return isReached;
        }

        ///////////////
        // Move insect

        public void MoveInsect(int width, int height, FullSnake fullSnake)
        {           
            _X = (_SIDE + 2) * (randomNumber.Next(width - _SIDE) / (_SIDE + 2));  // Set _X thanks to a generated number.
            _Y = (_SIDE + 2) * (randomNumber.Next(height - _SIDE) / (_SIDE + 2)); // Set _Y thanks to another generated number.

            for (int i = 0; i < fullSnake.Get_SnakeSize(); i++)
            {
                if ((_X == fullSnake.Get_Snake()[i].Get_X()) && (_Y == fullSnake.Get_Snake()[i].Get_Y())) // If the insect is in the same position of one of the snake parts...
                    MoveInsect(width, height, fullSnake); // Move the insect again.
            }
        }

        #endregion

        #region Accessors

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
