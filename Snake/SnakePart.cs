using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public class SnakePart
    {
        private int _X;
        private int _Y;
        private int _Direction;
        private int _LastDirection;
        private Panel _GraphicPart;


        public SnakePart()
        {
            _X = 50;
            _Y = 50;
            _Direction = 1;
            _LastDirection = 1;
            _GraphicPart = InitGraphicPart();
        }

        private Panel InitGraphicPart()
        {
            Panel panel = new Panel();
            panel.Location = new System.Drawing.Point(_X, _Y);
            panel.Size = new System.Drawing.Size(8, 8);
            panel.BackColor = System.Drawing.Color.Black;

            return panel;
        }

        private Panel Update()
        {  
            _GraphicPart.Location = new System.Drawing.Point(_X, _Y);
            return _GraphicPart;
        }

        public Panel MoveUp()
        {
            if (_Y - 10 < 0)
                _Y = 0;
            else
                _Y = _Y - 10;

            _Direction = 0;

            if (_Direction != _LastDirection)
                _LastDirection = _Direction;
            return Update();
        }

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
