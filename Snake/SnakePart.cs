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

        /*
         * Constructor of SnakePart
         *      - Initialize variables.
         * */

        public SnakePart(int width)
        {
            _SIDE = width / 54 - 2;           
            _IsHead = true;                   
            _X = (_SIDE + 2) * 15;            
            _Y = (_SIDE + 2) * 15;            
            _Direction = 1;                   
            _LastDirection = 1;               
            
        }

        /*
         * Another Constructor of SnakePart
         *      - Initialize variables.
         * */

        public SnakePart(int x, int y, int direction, int width)
        {
            _SIDE = width / 54 - 2;           
            _IsHead = false;                  
            _X = x;                           
            _Y = y;                           
            _Direction = direction;           
            _LastDirection = direction;       
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        /*
         * Move Up
         *      - Update snakePart position (edge reaching included)      
         */

        public void MoveUp()
        {
            _Y = _Y - (_SIDE + 2);       
            if (_Y == (0 - _SIDE - 2))   
                _Y = (33 * (_SIDE + 2)); 
            _Direction = 0;              
        }

        /*
         * Move Right
         *      - Update snakePart position (edge reaching included)      
         */

        public void MoveRight()
        {
            _X = _X + (_SIDE + 2);    
            if (_X == (54*(_SIDE+2))) 
                _X = 0;               
            _Direction = 1;           
        }

        /*
         * Move Down
         *      - Update snakePart position (edge reaching included)      
         */

        public void MoveDown()
        {
            _Y = _Y + (_SIDE + 2);         
            if (_Y == (34 * (_SIDE + 2)))  
                _Y = 0;                    
            _Direction = 2;                
        }

        /*
         * Move Left
         *      - Update snakePart position (edge reaching included)      
         */

        public void MoveLeft()
        {
            _X = _X - (_SIDE + 2);      
            if (_X == 0 - _SIDE - 2)    
                _X = 53 * (_SIDE + 2);  
            _Direction = 3;             
        }

        /*
         * Update part
         *      - Update snakePart position (main function calling the above ones).     
         */

        public void UpdateSnakePart(int direction)
        {         
            if (direction == -1)
                _Direction = _LastDirection;

            else 
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
