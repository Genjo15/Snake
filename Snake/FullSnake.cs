using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace Snake
{
    public class FullSnake
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private List<SnakePart> _Snake; // List of snake parts.

        #endregion


        /**************************************************** Constructor ****************************************************/

        #region Constructor

        public FullSnake()
        {
            _Snake = new List<SnakePart>(); // Instanciate the list of snake parts.
            _Snake.Add(new SnakePart());    // Add one part (the first one) of the snake to the list.
            AddSnakePart();
            AddSnakePart();
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        ///////////////////////////////////////
        // Method which update snake movement

        public void UpdateSnakeMovement(int direction)
        {
            int i = 0;

            foreach (SnakePart element in _Snake) // Do, for each element of SnakePart :
            {
                // If the element of the snake is the first one, move it by the direction defined by the event KeyDown.
                if (element.Get_IsHead() == true)
                {
                    element.GetPart(direction);
                    i++;
                }

                // Else, if the element is not the head of the snake, move it by the last direction of the previous element.
                else if (element.Get_IsHead() == false)
                {
                    element.GetPart(_Snake[i - 1].Get_LastDirection());
                    i++;
                }
            }
        }

        /////////////////////////////////////////////
        // Method which add a part to the full snake

        public void AddSnakePart()
        {
            int lastElementIndex = _Snake.Count - 1;

            if (_Snake[lastElementIndex].Get_Direction() == 0)
                _Snake.Add(new SnakePart(_Snake[lastElementIndex].Get_X(), _Snake[lastElementIndex].Get_Y() + (_Snake[lastElementIndex].Get_SIDE() + 2), 0));
            if(_Snake[lastElementIndex].Get_Direction() == 1)
                _Snake.Add(new SnakePart(_Snake[lastElementIndex].Get_X() - (_Snake[lastElementIndex].Get_SIDE() + 2), _Snake[lastElementIndex].Get_Y(), 1));
            if (_Snake[lastElementIndex].Get_Direction() == 2)
                _Snake.Add(new SnakePart(_Snake[lastElementIndex].Get_X(), _Snake[lastElementIndex].Get_Y() - (_Snake[lastElementIndex].Get_SIDE() + 2), 2));
            if (_Snake[lastElementIndex].Get_Direction() == 3)
                _Snake.Add(new SnakePart(_Snake[lastElementIndex].Get_X() + (_Snake[lastElementIndex].Get_SIDE() + 2), _Snake[lastElementIndex].Get_Y(), 3));
        }

        //////////////////////////////////////////////
        // Method which check if there is a collision

        public Boolean CheckCollision(int width, int height)
        {
            Boolean collision = false; // Initialize the boolean to false.
            int i = 0; // Iterator.

            collision = _Snake[0].CheckCollision(width, height); // Check collision with borders.

            // Check collision with itself.
            foreach (SnakePart element in _Snake) // Do, for each element of SnakePart :
            {
                if ((_Snake[0].Get_X() == element.Get_X()) && (_Snake[0].Get_Y() == element.Get_Y()) && (i != 0))  
                    collision = true;
                i++;
            }

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
