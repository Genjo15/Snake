using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    [Serializable] 
    public class Wall
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private int _X;                // Position in X.
        private int _Y;                // Position in Y.
        private int _Side;             // Side.
        private Random _RandomNumber;  // Random number.

        #endregion

        /**************************************************** Constructors ****************************************************/

        #region Constructors

        /*
         * Constructor of Wall
         *      - Initialize variables
         * */
        
        public Wall(int width, int height, FullSnake snake, Fruit fruit, Insect insect, List<Wall> listWall)
        {
            int tmpX, tmpY;

            _Side = width / 54 - 2; 
            _RandomNumber = new Random(); 
            tmpX = Generate_X(width);     
            tmpY = Generate_Y(height);    

            while(!CheckPositions(tmpX,tmpY,snake,fruit,insect,listWall)) 
            {
                tmpX = Generate_X(width);    
                tmpY = Generate_Y(height);   
            }

            _X = tmpX; 
            _Y = tmpY; 
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

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
         * Check if temporary X & Y are on the snake, the fruit, the insect, or a wall
         * */

        private Boolean CheckPositions(int x, int y, FullSnake fullSnake, Fruit fruit, Insect insect, List<Wall> listWalls)
        {
            Boolean ok = true; 

            for (int i = 0; i < fullSnake.Get_SnakeSize(); i++) 
            {
                if ((x == fullSnake.Get_Snake()[i].Get_X()) && (y == fullSnake.Get_Snake()[i].Get_Y())) 
                {
                    ok = false; 
                    //Console.WriteLine("Wall appeared on the snake");
                }
            }

            if (x == fruit.Get_X() && y == fruit.Get_Y())
            {
                ok = false; 
                //Console.WriteLine("Wall appeared on the fruit");
            }

            if (((insect.Get_X() == x) && (insect.Get_Y() == y)) || ((insect.Get_X() == (x + (_Side / 2 + 1))) && (insect.Get_Y() == y)) || ((insect.Get_X() == x) && (insect.Get_Y() == (y + (_Side / 2) + 1))) || ((insect.Get_X() == (x + (_Side / 2 + 1))) && (insect.Get_Y() == (y + (_Side / 2) + 1)))) 
            {
                ok = false;
                //Console.WriteLine("Wall appeared on the insect");
            }

            if(listWalls != null) 
                for (int z = 0; z < listWalls.Count(); z++)
                {
                    if ((x == listWalls[z].Get_X()) && (z == listWalls[z].Get_Y())) 
                    {
                        ok = false; 
                        //Console.WriteLine("Wall appeared on a wall");
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

        /////////////
        // Get _Side

        public int Get_Side()
        {
            return _Side;
        }

        ////////////////
        // Get The wall

        public Wall Get_Wall()
        {
            return this;
        }


        #endregion
    }
}
