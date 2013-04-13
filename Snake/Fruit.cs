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
        private const int _POINT = 5; // The points earned when item reached.
        private int _Side;            // Size of the panel side.
        private Random _RandomNumber; // Random number.

        #endregion

        /**************************************************** Constructors ****************************************************/

        #region Constructors

        /*
         * Constructor of the fruit
         *      - Initialize variables.
         * */

        public Fruit(int width, int height)
        {
            _Side = width / 54 - 2; 
            _RandomNumber = new Random();   
            _X = (_Side + 2) * (_RandomNumber.Next(width - _Side) / (_Side + 2));  
            _Y = (_Side + 2) * (_RandomNumber.Next(height - _Side) / (_Side + 2)); 
        }

        /*
         * Another Constructor of the fruit
         *      - Initialize variables.
         * */

        public Fruit()
        {
            _X = -666;
            _Y = -666;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /*
         * Method which determines if the snake has reached the item
         * */

        public Boolean IsReached(SnakePart snake)
        {
            Boolean isReached = false; 

            if ((snake.Get_X() == _X) && (snake.Get_Y() == _Y)) 
                isReached = true;
    
            return isReached; 
        }

        /*
         * Move fruit
         *      - Move Position of the fruit.
         *      - Check if new positions are on the snake, regenerate if needed.
         * */

        public void MoveFruit(int width, int height, FullSnake fullSnake)
        {
            int tmpX; 
            int tmpY; 

            tmpX = Generate_X(width);  
            tmpY = Generate_Y(height); 

            while (!CheckPositions(tmpX, tmpY, fullSnake)) 
            {
                tmpX = Generate_X(width);  
                tmpY = Generate_Y(height); 
            }

            _X = tmpX; 
            _Y = tmpY; 
        }

        /*
         * Move fruit (multiplayer)
         *      - Move Position of the fruit.
         *      - Check if new positions are on the snake/walls, regenerate if needed.
         * */

        public void MoveFruit(int width, int height, FullSnake fullSnake, List<Wall> listWalls)
        {
            int tmpX; 
            int tmpY; 

            tmpX = Generate_X(width); 
            tmpY = Generate_Y(height);

            while (!CheckPositions(tmpX, tmpY, fullSnake, listWalls)) 
            {
                tmpX = Generate_X(width);  
                tmpY = Generate_Y(height); 
            }

            _X = tmpX; 
            _Y = tmpY; 
        }

        /*
         * Generate _X
         * */

        private int Generate_X(int width)
        {
            int generatedNumber;
            generatedNumber = (_Side + 2) * (_RandomNumber.Next(width - _Side) / (_Side + 2));  
            return generatedNumber; 
        }

        /*
         * Generate _Y
         * */

        private int Generate_Y(int height)
        {
            int generatedNumber;
            generatedNumber = (_Side + 2) * (_RandomNumber.Next(height - _Side) / (_Side + 2)); 
            return generatedNumber;
        }

        /*
         * Check if temporary X & Y are on the snake
         * */

        private Boolean CheckPositions(int x, int y, FullSnake fullSnake)
        {
            Boolean ok = true;

            for (int i = 0; i < fullSnake.Get_SnakeSize(); i++) 
            {
                if ((x == fullSnake.Get_Snake()[i].Get_X()) && (y == fullSnake.Get_Snake()[i].Get_Y())) 
                {
                    ok = false; 
                    //Console.WriteLine("Fruit appeared on the snake");
                }
            }

            return ok; 
        }

        /*
         * Check if temporary X & Y are on the snake or a wall (multiplayers)
         * */

        private Boolean CheckPositions(int x, int y, FullSnake fullSnake, List<Wall> listWalls)
        {
            Boolean ok = true; 

            for (int i = 0; i < fullSnake.Get_SnakeSize(); i++)
            {
                if ((x == fullSnake.Get_Snake()[i].Get_X()) && (y == fullSnake.Get_Snake()[i].Get_Y())) 
                {
                    ok = false; 
                    //Console.WriteLine("Fruit appeared on the snake");
                }
            }

            foreach (Wall element in listWalls)
            {
                if (element.Get_X() == x && element.Get_Y() == y)
                {
                    ok = false;
                    //Console.WriteLine("Fruit appeared on a wall");
                }
            }

            return ok; 
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
