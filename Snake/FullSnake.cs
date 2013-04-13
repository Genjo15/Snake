using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Snake
{
    [Serializable] 
    public class FullSnake
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private List<SnakePart> _Snake; // List of snake parts.
        
        #endregion

        /**************************************************** Constructors ****************************************************/

        #region Constructor

        /*
         * Constructor by default of FullSnake
         *      - Initialize list of snake.
         * */

        public FullSnake()
        {
            _Snake = new List<SnakePart>(); 
        }

        /*
         * Constructor of FullSnake
         *      - Initialize list of snake.
         *      - Add 3 parts.
         * */

        public FullSnake(int width)
        {
            _Snake = new List<SnakePart>(); 
            _Snake.Add(new SnakePart(width));    
            AddSnakePart(width);                 
            AddSnakePart(width);                 
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /*
         * Method which update snake movement
         *      - Access and update each part.
         * */

        public void UpdateSnake(int direction, int width)
        {
            int i = 0;

            foreach (SnakePart element in _Snake) 
            {
                if (element.Get_IsHead() == true)
                {
                    element.UpdateSnakePart(direction);
                    i++;
                }

                else if (element.Get_IsHead() == false)
                {
                    element.UpdateSnakePart(_Snake[i - 1].Get_LastDirection());
                    i++;
                }
            }
        }

        /*
         * Method which add a part to the full snake
         *      - Add snake part depending the movment of the last part of the snake.
         * */

        public void AddSnakePart(int width)
        {
            int lastElementIndex = _Snake.Count - 1; 

            if (_Snake[lastElementIndex].Get_Direction() == 0) 
                _Snake.Add(new SnakePart(_Snake[lastElementIndex].Get_X(), _Snake[lastElementIndex].Get_Y() + (_Snake[lastElementIndex].Get_SIDE() + 2), 0, width)); 
            if (_Snake[lastElementIndex].Get_Direction() == 1) 
                _Snake.Add(new SnakePart(_Snake[lastElementIndex].Get_X() - (_Snake[lastElementIndex].Get_SIDE() + 2), _Snake[lastElementIndex].Get_Y(), 1, width)); 
            if (_Snake[lastElementIndex].Get_Direction() == 2) 
                _Snake.Add(new SnakePart(_Snake[lastElementIndex].Get_X(), _Snake[lastElementIndex].Get_Y() - (_Snake[lastElementIndex].Get_SIDE() + 2), 2, width)); 
            if (_Snake[lastElementIndex].Get_Direction() == 3) 
                _Snake.Add(new SnakePart(_Snake[lastElementIndex].Get_X() + (_Snake[lastElementIndex].Get_SIDE() + 2), _Snake[lastElementIndex].Get_Y(), 3, width)); 
        }

        /*
         * Method which check if there is a collision with itself.
         *      - Check collision depending of the head and snake parts.
         * */

        public Boolean CheckCollision(int width, int height)
        {
            Boolean collision = false; 
            int i = 0; 

            foreach (SnakePart element in _Snake) 
            {
                if ((_Snake[0].Get_X() == element.Get_X()) && (_Snake[0].Get_Y() == element.Get_Y()) && (i != 0))  
                    collision = true; 
                i++; 
            }

            return collision; 
        }

        /*
         * Method which check if there is a collision with a wall.
         *      - Check collision depending of the head and walls.
         * */

        public Boolean CheckCollision(int width, int height, List<Wall> listWalls)
        {
            Boolean collision = false; 
            int i = 0; 

            foreach (SnakePart element in _Snake) 
            {
                if ((_Snake[0].Get_X() == element.Get_X()) && (_Snake[0].Get_Y() == element.Get_Y()) && (i != 0)) 
                    collision = true; 
                i++; 
            }

            foreach(Wall element in listWalls)
                if ((_Snake[0].Get_X() == element.Get_X()) && (_Snake[0].Get_Y() == element.Get_Y()))  
                    collision = true; 

            return collision; 
        }

        #endregion

        #region Accessors

        /////////////
        // Get snake

        public List<SnakePart> Get_Snake()
        {
            return _Snake; 
        }

        /////////////////////
        // Get size of snake

        public int Get_SnakeSize()
        {
            return _Snake.Count; 
        }

        #endregion
    }
}
