using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    [Serializable] 
    public class Fruit
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private int _X;               // Position in X.
        private int _Y;               // Position in Y.
        private int _LastX;           // Last position in X.
        private int _LastY;           // Last position in Y.
        private const int _POINT = 5; // The points earned when item reached.
        private int _Side;            // Size of the panel side.
        private Random _RandomNumber; // Random number.

        #endregion

        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public Fruit(int width, int height)
        {
            _Side = width / 54 - 2; // Initialize dynamically the side of the fruit.
            _RandomNumber = new Random();   // Initialize the generator of random.
            _X = (_Side + 2) * (_RandomNumber.Next(width - _Side) / (_Side + 2));  // Set _X thanks to a generated number.
            _Y = (_Side + 2) * (_RandomNumber.Next(height - _Side) / (_Side + 2)); // Set _Y thanks to another generated number.
            _LastX = (_Side + 2) * (_RandomNumber.Next(width - _Side) / (_Side + 2));  // Set _LastX thanks to a generated number.
            _LastY = (_Side + 2) * (_RandomNumber.Next(height - _Side) / (_Side + 2)); // Set _LastY thanks to another generated number.
        }

        public Fruit()
        {
            _X = -666;
            _Y = -666;
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
            int tmpX; // Temporary X.
            int tmpY; // Temporary Y. 

            _LastX = _X; // Save _X in _LastX.
            _LastY = _Y; // Save _Y in _LastY.

            tmpX = Generate_X(width);  // Generate a temporary X.
            tmpY = Generate_Y(height); // Generate a temporary Y.

            while (!CheckPositions(tmpX, tmpY, fullSnake)) // Check positions regarding the snake.
            {
                tmpX = Generate_X(width);  // Still generate if not OK.
                tmpY = Generate_Y(height); //
            }

            _X = tmpX; // Finally Set _X & _Y.
            _Y = tmpY; //
        }

        ////////////////////////////
        // Move fruit (multiplayer)

        public void MoveFruit(int width, int height, FullSnake fullSnake, List<Wall> listWalls)
        {
            int tmpX; // Temporary X.
            int tmpY; // Temporary Y. 

            _LastX = _X; // Save _X in _LastX.
            _LastY = _Y; // Save _Y in _LastY.

            tmpX = Generate_X(width);  // Generate a temporary X.
            tmpY = Generate_Y(height); // Generate a temporary Y.

            while (!CheckPositions(tmpX, tmpY, fullSnake, listWalls)) // Check positions regarding the snake and walls.
            {
                tmpX = Generate_X(width);  // Still generate if not OK.
                tmpY = Generate_Y(height); //
            }

            _X = tmpX; // Finally Set _X & _Y.
            _Y = tmpY; //
        }

        ///////////////
        // Generate _X

        private int Generate_X(int width)
        {
            int generatedNumber; // Generated number
            generatedNumber = (_Side + 2) * (_RandomNumber.Next(width - _Side) / (_Side + 2));  // Set _X thanks to a generated number.
            return generatedNumber; // Return generated number.
        }

        ///////////////
        // Generate _Y

        private int Generate_Y(int height)
        {
            int generatedNumber; // Generated number
            generatedNumber = (_Side + 2) * (_RandomNumber.Next(height - _Side) / (_Side + 2)); // Set _Y thanks to another generated number.
            return generatedNumber; // Return generated number.
        }

        /////////////////////////////////////////////
        // Check if temporary X & Y are on the snake

        private Boolean CheckPositions(int x, int y, FullSnake fullSnake)
        {
            Boolean ok = true; // Boolean.

            for (int i = 0; i < fullSnake.Get_SnakeSize(); i++) // Check each snake part.
            {
                if ((x == fullSnake.Get_Snake()[i].Get_X()) && (y == fullSnake.Get_Snake()[i].Get_Y())) // If the fruit is in the same position of one of the snake parts...
                {
                    ok = false; // Set the boolean ok to FALSE.
                    //Console.WriteLine("Fruit appeared on the snake");
                }
            }

            return ok; // Return the boolean.
        }

        //////////////////////////////////////////////////////////////////////
        // Check if temporary X & Y are on the snake or a wall (multiplayers)

        private Boolean CheckPositions(int x, int y, FullSnake fullSnake, List<Wall> listWalls)
        {
            Boolean ok = true; //Boolean.

            for (int i = 0; i < fullSnake.Get_SnakeSize(); i++) // Check each snake part.
            {
                if ((x == fullSnake.Get_Snake()[i].Get_X()) && (y == fullSnake.Get_Snake()[i].Get_Y())) // If the fruit is in the same position of one of the snake parts...
                {
                    ok = false; // Set the boolean ok to FALSE.
                    //Console.WriteLine("Fruit appeared on the snake");
                }
            }

            foreach (Wall element in listWalls)
            {
                if (element.Get_X() == x && element.Get_Y() == y)
                {
                    ok = false; // Set the boolean ok to FALSE/
                    //Console.WriteLine("Fruit appeared on a wall");
                }
            }

            return ok; // Return the boolean.
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
            return _Side;
        }

        #endregion
    }
}
