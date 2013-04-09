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

        public FullSnake()
        {
            _Snake = new List<SnakePart>(); // Instanciate the list of snake parts.
        }

        public FullSnake(int width)
        {
            _Snake = new List<SnakePart>(); // Instanciate the list of snake parts.
            _Snake.Add(new SnakePart(width));    // Add one part (the first one) of the snake to the list.
            AddSnakePart(width);                 // Then add two others parts (the game start with a snake composed of three parts).
            AddSnakePart(width);                 // 
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        ///////////////////////////////////////
        // Method which update snake movement

        public void UpdateSnake(int direction, int width)
        {
            int i = 0;

            foreach (SnakePart element in _Snake) // Do, for each element of SnakePart :
            {
                // If the element of the snake is the first one, move it by the direction defined by the event KeyDown.
                if (element.Get_IsHead() == true)
                {
                    element.UpdateSnakePart(direction);
                    i++;
                }

                // Else, if the element is not the head of the snake, move it by the last direction of the previous element.
                else if (element.Get_IsHead() == false)
                {
                    element.UpdateSnakePart(_Snake[i - 1].Get_LastDirection());
                    i++;
                }
            }
        }

        /////////////////////////////////////////////
        // Method which add a part to the full snake

        public void AddSnakePart(int width)
        {
            int lastElementIndex = _Snake.Count - 1; // Compute the last element of the snake.

            if (_Snake[lastElementIndex].Get_Direction() == 0) // If the last element moves up...
                _Snake.Add(new SnakePart(_Snake[lastElementIndex].Get_X(), _Snake[lastElementIndex].Get_Y() + (_Snake[lastElementIndex].Get_SIDE() + 2), 0, width)); // ...then add a part just down on it.
            if (_Snake[lastElementIndex].Get_Direction() == 1) // If the last element moves right...
                _Snake.Add(new SnakePart(_Snake[lastElementIndex].Get_X() - (_Snake[lastElementIndex].Get_SIDE() + 2), _Snake[lastElementIndex].Get_Y(), 1, width)); // ...then add a part just left on it.
            if (_Snake[lastElementIndex].Get_Direction() == 2) // If the last element moves down...
                _Snake.Add(new SnakePart(_Snake[lastElementIndex].Get_X(), _Snake[lastElementIndex].Get_Y() - (_Snake[lastElementIndex].Get_SIDE() + 2), 2, width)); // ...then add a part just up on it.
            if (_Snake[lastElementIndex].Get_Direction() == 3) // If the last element moves left...
                _Snake.Add(new SnakePart(_Snake[lastElementIndex].Get_X() + (_Snake[lastElementIndex].Get_SIDE() + 2), _Snake[lastElementIndex].Get_Y(), 3, width)); // ...then add a part just right on it.
        }

        //////////////////////////////////////////////////////////
        // Method which check if there is a collision with itself.

        public Boolean CheckCollision(int width, int height)
        {
            Boolean collision = false; // Initialize the boolean to false.
            int i = 0; // Iterator.

            // Check collision with itself.
            foreach (SnakePart element in _Snake) // Do, for each element of SnakePart :
            {
                if ((_Snake[0].Get_X() == element.Get_X()) && (_Snake[0].Get_Y() == element.Get_Y()) && (i != 0)) // If the snake head is in the same position as the other parts of the snake;  
                    collision = true; //  then set the boolean to true.
                i++; // Increment i.
            }

            return collision; // Return boolean collision.
        }

        /////////////////////////////////////////////////////////
        // Method which check if there is a collision with a wall

        public Boolean CheckCollision(int width, int height, List<Wall> listWalls)
        {
            Boolean collision = false; // Initialize the boolean to false.
            int i = 0; // Iterator.

            // Check collision with itself.
            foreach (SnakePart element in _Snake) // Do, for each element of SnakePart :
            {
                if ((_Snake[0].Get_X() == element.Get_X()) && (_Snake[0].Get_Y() == element.Get_Y()) && (i != 0)) // If the snake head is in the same position as the other parts of the snake;  
                    collision = true; //  then set the boolean to true.
                i++; // Increment i.
            }

            // Check collision with walls.
            foreach(Wall element in listWalls)
                if ((_Snake[0].Get_X() == element.Get_X()) && (_Snake[0].Get_Y() == element.Get_Y())) // If the snake head is in the same position as the other parts of the snake;  
                    collision = true; //  then set the boolean to true.

            return collision; // Return boolean collision.
        }

        #endregion

        #region Accessors

        /////////////
        // Get snake

        public List<SnakePart> Get_Snake()
        {
            return _Snake; // return the snake.
        }

        /////////////////////
        // Get size of snake

        public int Get_SnakeSize()
        {
            return _Snake.Count; // return the size of the snake.
        }

        #endregion
    }
}
