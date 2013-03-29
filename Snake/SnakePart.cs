using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace Snake
{
    [Serializable] 
    public class SnakePart
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private Boolean _IsHead;      // Head or not.
        private int _X;               // Position in X.
        private int _Y;               // Position in Y.
        private int _Direction;       // Direction.
        private int _LastDirection;   // Last direction.
        private int _SIDE;            // Size of the pictureBox side.

        #endregion

        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public SnakePart(int width)
        {
            _SIDE = width / 54 - 2;           // Determinate dynamically the side.
            _IsHead = true;                   // Initialize _IsHead to TRUE.
            _X = (_SIDE + 2) * 15;            // Position in X set to 210 (must be a multiple of _SIDE + 2 to be in synchronisation with items).
            _Y = (_SIDE + 2) * 15;            // Position in Y set to 210 (must be a multiple of _SIDE + 2 to be in synchronisation with items).
            _Direction = 1;                   // Direction is set to 1 (right).
            _LastDirection = 1;               // Last direction is set to 1 (right).
            
        }

        public SnakePart(int x, int y, int direction, int width)
        {
            _SIDE = width / 54 - 2;           // Determinate dynamically the side.
            _IsHead = false;                  // Initialize _IsHead to FALSE.
            _X = x;                           // Position in X set to 250.
            _Y = y;                           // Position in Y set to 250.
            _Direction = direction;           // Direction is set to 1 (right).
            _LastDirection = direction;       // Last direction is set to 1 (right).
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        ///////////
        // Move up

        public void MoveUp()
        {
            _Y = _Y - (_SIDE + 2);       // Move 14 px up.
            if (_Y == (0 - _SIDE - 2))   // if the snake part reaches the top edge...
                _Y = (33 * (_SIDE + 2)); // it appears at the opposite of the map.
            _Direction = 0;              // Set the direction to 0 (up).
        }

        //////////////
        // Move right

        public void MoveRight()
        {
            _X = _X + (_SIDE + 2);    // Move 14 px right.
            if (_X == (54*(_SIDE+2))) // if the snake part reaches the right edge...
                _X = 0;               // it appears at the opposite of the map.
            _Direction = 1;           // Set the direction to 1 (right).
        }

        /////////////
        // Move down

        public void MoveDown()
        {
            _Y = _Y + (_SIDE + 2);         // Move 14 px down.
            if (_Y == (34 * (_SIDE + 2)))  // if the snake part reaches the lower edge...
                _Y = 0;                    // it appears at the opposite of the map.
            _Direction = 2;                // Set the direction to 2 (down).
        }

        /////////////
        // Move left

        public void MoveLeft()
        {
            _X = _X - (_SIDE + 2);      // Move 14 px left.
            if (_X == 0 - _SIDE - 2)    // if the snake part reaches the left edge...
                _X = 53 * (_SIDE + 2);  // it appears at the opposite of the map.
            _Direction = 3;             // Set the direction to 2 (left).
        }

        ////////////////
        // Get the part

        public void UpdateSnakePart(int direction)
        {         
            //  If the user does nothing, direction is the same as the previous one.
            if (direction == -1)
                _Direction = _LastDirection;

            else // Update direction and last direction.
            {
                _LastDirection = _Direction;
                _Direction = direction;
            }

            if (_Direction == 0)
                MoveUp(); // Move up.
            else if (_Direction == 1)
                MoveRight(); // Move right.
            else if (_Direction == 2)
                MoveDown(); // Move down.
            else MoveLeft(); // Move left. 
        }

        #endregion

        # region Accessors

        ///////////////////////////
        // Get the boolean _IsHead

        public Boolean Get_IsHead()
        {
            return _IsHead;
        }

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

        //////////////////////
        // Get _Direction

        public int Get_Direction()
        {
            return _Direction;
        }

        //////////////////////
        // Get _LastDirection

        public int Get_LastDirection()
        {
            return _LastDirection;
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
