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

        public Wall(int width, int height, FullSnake snake, Fruit fruit, Insect insect, List<Wall> listWall)
        {
            int tmpX, tmpY;

            _Side = width / 54 - 2; // Initialize dynamically the side of the fruit.
            _RandomNumber = new Random(); // Initialize the generator of random.
            tmpX = Generate_X(width);     // First generate a X.
            tmpY = Generate_Y(height);    // First generate a Y.

            while(!CheckPositions(tmpX,tmpY,snake,fruit,insect,listWall)) // Check positions regarding snake, fruit, insect and list of walls.
            {
                tmpX = Generate_X(width);     //  generate a X.
                tmpY = Generate_Y(height);    //  generate a Y.
            }

            _X = tmpX; // Finally assign X & Y.
            _Y = tmpY; //
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        ///////////////
        // Generate _X

        private int Generate_X(int width)
        {
            int generatedNumber;
            generatedNumber = (_Side + 2) * (_RandomNumber.Next(width - _Side) / (_Side + 2));  // Set _X thanks to a generated number.
            return generatedNumber;
        }

        ///////////////
        // Generate _Y

        private int Generate_Y(int height)
        {
            int generatedNumber;
            generatedNumber = (_Side + 2) * (_RandomNumber.Next(height - _Side) / (_Side + 2)); // Set _Y thanks to another generated number.
            return generatedNumber;
        }

        ///////////////////////////////////////////////////////////////////////////////
        // Check if temporary X & Y are on the snake, the fruit, the insect, or a wall

        private Boolean CheckPositions(int x, int y, FullSnake fullSnake, Fruit fruit, Insect insect, List<Wall> listWalls)
        {
            Boolean ok = true; // Boolean

            for (int i = 0; i < fullSnake.Get_SnakeSize(); i++) // Check each snake part.
            {
                if ((x == fullSnake.Get_Snake()[i].Get_X()) && (y == fullSnake.Get_Snake()[i].Get_Y())) // If the fruit is in the same position of one of the snake parts...
                {
                    ok = false; // Set the boolean to false.
                    //Console.WriteLine("Wall appeared on the snake");
                }
            }

            if (x == fruit.Get_X() && y == fruit.Get_Y())
            {
                ok = false; // Set the boolean to false.
                //Console.WriteLine("Wall appeared on the fruit");
            }

            if (((insect.Get_X() == x) && (insect.Get_Y() == y)) || ((insect.Get_X() == (x + (_Side / 2 + 1))) && (insect.Get_Y() == y)) || ((insect.Get_X() == x) && (insect.Get_Y() == (y + (_Side / 2) + 1))) || ((insect.Get_X() == (x + (_Side / 2 + 1))) && (insect.Get_Y() == (y + (_Side / 2) + 1)))) // If the insect is in the same position than the wall...
            {
                ok = false; // Set the boolean to false.
                //Console.WriteLine("Wall appeared on the insect");
            }

            if(listWalls != null) 
                for (int z = 0; z < listWalls.Count(); z++)
                {
                    if ((x == listWalls[z].Get_X()) && (z == listWalls[z].Get_Y())) // If the fruit is in the same position of one of the snake parts...
                    {
                        ok = false; // Set the boolean to false.
                        //Console.WriteLine("Wall appeared on a wall");
                    }
                }

            return ok; // Return boolean.
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
