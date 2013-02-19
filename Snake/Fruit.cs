using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public class Fruit
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private int _X;               // Position in X.
        private int _Y;               // Position in Y.
        private const int _POINT = 5; // The points earned when item reached.
        private const int _SIDE = 12; // Size of the panel side.
        private Random randomNumber;

        #endregion

        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public Fruit(int width, int height)
        {
            randomNumber = new Random();   // Initialize the generator of random.
            _X = (_SIDE + 2) * (randomNumber.Next(width - _SIDE) / (_SIDE + 2));  // Set _X thanks to a generated number.
            _Y = (_SIDE + 2) * (randomNumber.Next(height - _SIDE) / (_SIDE + 2)); // Set _Y thanks to another generated number.
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /////////////////////////////////////////////////////////////
        // Method which determines if the snake has reached the item

        public Boolean IsReached(SnakePart snake)
        {
            Boolean isReached = false; // A boolean which is initialized to false.

            if ((snake.Get_X() == _X) && (snake.Get_Y() == _Y)) // Check if the snake has reached the fruit.
                isReached = true; // If yes, set the boolean to true.
    
            return isReached; // Return Boolean.
        }

        //////////////
        // Move fruit

        public void MoveFruit(int width, int height, FullSnake fullSnake)
        {
            _X = (_SIDE + 2) * (randomNumber.Next(width - _SIDE) / (_SIDE + 2));  // Set _X thanks to a generated number.
            _Y = (_SIDE + 2) * (randomNumber.Next(height - _SIDE) / (_SIDE + 2)); // Set _Y thanks to another generated number.

            for (int i = 0; i < fullSnake.Get_SnakeSize(); i++) // Check each snake part.
            {
                if ((_X == fullSnake.Get_Snake()[i].Get_X()) && (_Y == fullSnake.Get_Snake()[i].Get_Y())) // If the fruit is in the same position of one of the snake parts...
                    MoveFruit(width, height, fullSnake); // Move the fruit again.
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

        #endregion
    }
}
