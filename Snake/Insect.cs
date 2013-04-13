using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    [Serializable] 
    public class Insect
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private int _X;                            // Position in X.
        private int _Y;                            // Position in Y.
        private const int _POINT = 25;             // The points earned when item reached.
        private int _Side;                         // Size of the insect.
        private Random _RandomNumber;              // Random number.

        #endregion

        /**************************************************** Constructors ****************************************************/

        #region Constructors

        /*
         * Constructor for the Insect
         *      - Initialize variables
         * */

        public Insect(int width, int height)
        {
            _Side = width / 27 - 2; 
            _RandomNumber = new Random(); 
            _X = (_Side + 2) * (_RandomNumber.Next(width - _Side) / (_Side + 2));  
            _Y = (_Side + 2) * (_RandomNumber.Next(height - _Side) / (_Side + 2)); 
        }

        /*
         * Another Constructor for the Insect
         *      - Initialize variables
         * */

        public Insect(int width, int height, int x, int y)
        {
            _Side = width / 27 - 2; 
            _RandomNumber = new Random();
            _X = x; 
            _Y = y; 
        }

        /*
         * Another Constructor for the Insect
         *      - Initialize variables
         * */

        public Insect()
        {
            _X = -666;
            _Y = -666;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /*
         * Method which determines if the snake has reached the insect
         */

        public Boolean IsReached(SnakePart snakePart)
        {
            Boolean isReached = false; 

            if (((snakePart.Get_X() == _X) && (snakePart.Get_Y() == _Y)) || ((snakePart.Get_X() == (_X + (_Side / 2 + 1))) && (snakePart.Get_Y() == _Y)) || ((snakePart.Get_X() == _X) && (snakePart.Get_Y() == (_Y + (_Side / 2) + 1))) || ((snakePart.Get_X() == (_X + (_Side / 2 + 1))) && (snakePart.Get_Y() == (_Y + (_Side / 2) + 1))))
                isReached = true; 
    
            return isReached;
        }

        /*
         * Move insect
         *      - Move positions of the insect
         *      - Check if new positions are on the snake/fruit, regenerate if needed.
         * */

        public void MoveInsect(int width, int height, FullSnake snake, Fruit fruit)
        {
            int tmpX; 
            int tmpY; 

            tmpX = Generate_X(width);  
            tmpY = Generate_Y(height); 

            while (!CheckPositions(tmpX, tmpY, snake, fruit)) 
            {
                tmpX = Generate_X(width);  
                tmpY = Generate_Y(height); 
            }

            _X = tmpX; 
            _Y = tmpY; 
        }

        /*
         * Move insect (multiplayers)
         *      - Move positions of the insect
         *      - Check if new positions are on the snake/fruit/walls, regenerate if needed.
         * */

        public void MoveInsect(int width, int height, FullSnake snake, Fruit fruit, List<Wall> listWalls)
        {
            int tmpX; 
            int tmpY; 

            tmpX = Generate_X(width);  
            tmpY = Generate_Y(height); 

            while (!CheckPositions(tmpX, tmpY, snake, fruit, listWalls)) 
            {
                tmpX = Generate_X(width); 
                tmpY = Generate_Y(height);
            }

            _X = tmpX; 
            _Y = tmpY; 
        }

        /*
         * Move insect (make it unreachable)
         * */

        public void MoveInsect()
        {
            _X = -666; 
            _Y = -666;
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
         * Check if temporary X & Y are on the snake or the fruit
         * */

        private Boolean CheckPositions(int x, int y, FullSnake snake, Fruit fruit)
        {
            Boolean ok = true;

            for (int i = 0; i < snake.Get_SnakeSize(); i++)
            {
                if (((snake.Get_Snake()[i].Get_X() == x) && (snake.Get_Snake()[i].Get_Y() == y)) || ((snake.Get_Snake()[i].Get_X() == (x + (_Side / 2 + 1))) && (snake.Get_Snake()[i].Get_Y() == y)) || ((snake.Get_Snake()[i].Get_X() == x) && (snake.Get_Snake()[i].Get_Y() == (y + (_Side / 2) + 1))) || ((snake.Get_Snake()[i].Get_X() == (x + (_Side / 2 + 1))) && (snake.Get_Snake()[i].Get_Y() == (y + (_Side / 2) + 1))))
                {
                    ok = false; 
                    //Console.WriteLine("Insect is on the snake");
                }
            }

            if (((fruit.Get_X() == x) && (fruit.Get_Y() == y)) || ((fruit.Get_X() == (x + (_Side / 2 + 1))) && (fruit.Get_Y() == y)) || ((fruit.Get_X() == x) && (fruit.Get_Y() == (y + (_Side / 2) + 1))) || ((fruit.Get_X() == (x + (_Side / 2 + 1))) && (fruit.Get_Y() == (y + (_Side / 2) + 1)))) 
            {
                ok = false; 
                //Console.WriteLine("Insect is on the fruit");
            }

            return ok;
        }

        /*
         * Check if temporary X & Y are on the snake, the fruit, or a wall (multiplayers)
         * */

        private Boolean CheckPositions(int x, int y, FullSnake snake, Fruit fruit, List<Wall> listWalls)
        {
            Boolean ok = true; 

            for (int i = 0; i < snake.Get_SnakeSize(); i++)
            {
                if (((snake.Get_Snake()[i].Get_X() == x) && (snake.Get_Snake()[i].Get_Y() == y)) || ((snake.Get_Snake()[i].Get_X() == (x + (_Side / 2 + 1))) && (snake.Get_Snake()[i].Get_Y() == y)) || ((snake.Get_Snake()[i].Get_X() == x) && (snake.Get_Snake()[i].Get_Y() == (y + (_Side / 2) + 1))) || ((snake.Get_Snake()[i].Get_X() == (x + (_Side / 2 + 1))) && (snake.Get_Snake()[i].Get_Y() == (y + (_Side / 2) + 1)))) 
                {
                    ok = false; 
                    //Console.WriteLine("Insect is on the snake");
                }
            }

            if (((fruit.Get_X() == x) && (fruit.Get_Y() == y)) || ((fruit.Get_X() == (x + (_Side / 2 + 1))) && (fruit.Get_Y() == y)) || ((fruit.Get_X() == x) && (fruit.Get_Y() == (y + (_Side / 2) + 1))) || ((fruit.Get_X() == (x + (_Side / 2 + 1))) && (fruit.Get_Y() == (y + (_Side / 2) + 1))))
            {
                ok = false; 
                //Console.WriteLine("Insect is on the fruit");
            }

            foreach (Wall element in listWalls)
            {
                if (((element.Get_X() == x) && (element.Get_Y() == y)) || ((element.Get_X() == (x + (_Side / 2 + 1))) && (element.Get_Y() == y)) || ((element.Get_X() == x) && (element.Get_Y() == (y + (_Side / 2) + 1))) || ((element.Get_X() == (x + (_Side / 2 + 1))) && (element.Get_Y() == (y + (_Side / 2) + 1)))) 
                {
                    ok = false;
                    //Console.WriteLine("Insect is on a wall");
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
