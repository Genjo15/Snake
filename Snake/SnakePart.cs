using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public class SnakePart
    {
        /********************************************* Declaration of variables *********************************************/

        private int _X;             // Position in X.
        private int _Y;             // Position in Y.
        private int _Direction;     // Direction.
        private int _LastDirection; // Last direction.
        private Panel _GraphicPart; // His panel.


        /**************************************************** Constructor ****************************************************/

        public SnakePart()
        {
            _X = 250;                         // Position in Y set to 250.
            _Y = 250;                         // Position in Y set to 250.
            _Direction = 1;                   // Direction is set to 1 (right).
            _LastDirection = 1;               // Last direction is set to 1 (right).
            _GraphicPart = InitGraphicPart(); // Initialization of the panel.
        }


        /****************************************************** Methods ******************************************************/

        //////////////////////////////////////////
        // Initialization of the snake part panel

        private Panel InitGraphicPart()
        {
            Panel panel = new Panel();                         // Instanciation of a new panel.
            panel.Location = new System.Drawing.Point(_X, _Y); // Definition of the panel location.
            panel.Size = new System.Drawing.Size(8, 8);        // Definition of the panel size.
            panel.BackColor = System.Drawing.Color.Black;      // Definition of the panel color.

            return panel; // Return panel.
        }


        ///////////////////////////
        // Update snake part panel

        private Panel Update()
        {  
            _GraphicPart.Location = new System.Drawing.Point(_X, _Y); // Define the location.
            return _GraphicPart; // Return panel.
        }


        ///////////
        // Move up

        public Panel MoveUp()
        {
            if (_Y - 10 < 0) // Exception (border up) 
                _Y = 0;
            else
                _Y = _Y - 10; // Move 10 px up.

            _Direction = 0; // Set the direction to 0 (up).

            if (_Direction != _LastDirection)
                _LastDirection = _Direction; // Update the last direction.

            return Update(); // Update the panel and return it.
        }


        //////////////
        // Move right

        public Panel MoveRight()
        {
            if (_X + 10 > 1000)
                _X = 1000;
            else
                _X = _X + 10;

            _Direction = 1;

            if (_Direction != _LastDirection)
                _LastDirection = _Direction;
            return Update();
        }


        /////////////
        // Move down

        public Panel MoveDown()
        {
            if (_Y + 10 > 1000)
                _Y = 1000;
            else
                _Y = _Y + 10;

            _Direction = 2;

            if (_Direction != _LastDirection)
                _LastDirection = _Direction;
            return Update();
        }


        /////////////
        // Move left

        public Panel MoveLeft()
        {
            if (_X - 10 < 0)
                _X = 0;
            else
                _X = _X - 10;

            _Direction = 3;

            if (_Direction != _LastDirection)
                _LastDirection = _Direction;
            return Update();
        }


        ////////////////
        // Get the part

        public Panel GetPart(int direction)
        {         
            //  si l'utilisateur n'appuie sur aucune touche, la direction est la même que la précedente.
            if (direction == -1)
                _Direction = _LastDirection;
            else
                _Direction = direction;



            if (_Direction == 0)
                return MoveUp();
            else if (_Direction == 1)
                return MoveRight();
            else if (_Direction == 2)
                return MoveDown();
            else return MoveLeft();  
        }


    }
}
