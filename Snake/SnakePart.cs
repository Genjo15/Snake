using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace Snake
{
    public class SnakePart
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private Boolean _IsHead;      // Head or not.
        private int _X;               // Position in X.
        private int _Y;               // Position in Y.
        private int _Direction;       // Direction.
        private int _LastDirection;   // Last direction.
        private const int _SIDE = 12; // Size of the panel side.

        #endregion

        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public SnakePart()
        {
            _IsHead = true;
            _X = (_SIDE + 2) * 15;            // Position in X set to 210 (must be a multiple of _SIDE + 2 to be in synchronisation with items).
            _Y = (_SIDE + 2) * 15;            // Position in Y set to 210 (must be a multiple of _SIDE + 2 to be in synchronisation with items).
            _Direction = 1;                   // Direction is set to 1 (right).
            _LastDirection = 1;               // Last direction is set to 1 (right).
            
        }

        public SnakePart(int x, int y, int direction)
        {
            _IsHead = false;
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
            _Y = _Y - (_SIDE + 2); // Move 14 px up.
            _Direction = 0; // Set the direction to 0 (up).
        }


        //////////////
        // Move right

        public void MoveRight()
        {
            _X = _X + (_SIDE + 2);
            _Direction = 1;
        }


        /////////////
        // Move down

        public void MoveDown()
        {
            _Y = _Y + (_SIDE + 2);
            _Direction = 2;
        }


        /////////////
        // Move left

        public void MoveLeft()
        {
            _X = _X - (_SIDE + 2);
            _Direction = 3;
        }


        ////////////////
        // Get the part

        public void GetPart(int direction)
        {         
            //  If the user does nothing, direction is the same as the previous one.
            if (direction == -1)
                _Direction = _LastDirection;

            else if(direction == _Direction + 2) // Force the direction to be the same as the previous one if the user wants to move back
            {                                    // Case Up to Down and Right to Left.
                _Direction = _LastDirection;
            }

            else if (direction == _Direction - 2) // Force the direction to be the same as the previous one if the user wants to move back
            {                                     // Case Down to Up and Left to Right.
                _Direction = _LastDirection;
            }

            else // Update direction and last direction.
            {
                _LastDirection = _Direction;
                _Direction = direction;
            }

            if (_Direction == 0)
                MoveUp();
            else if (_Direction == 1)
                MoveRight();
            else if (_Direction == 2)
                MoveDown();
            else MoveLeft();  
        }


        ///////////////////
        // Check Collision

        public Boolean CheckCollision(int width, int height)
        {
            Boolean collision = false;

            if ((_Y < 0) || (_X + _SIDE > width) || (_Y + 10 > height) || (_X < 0))
                collision = true;

            return collision;
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
