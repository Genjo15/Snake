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

        /**************************************************** Constructor ****************************************************/

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

        //////////////////////////////////////////////
        // Method which check if there is a collision

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

        #endregion

        #region RenderMethods

        //////////////////////////////////////////////
        // Method to refresh the display of the snake

        public void RenderSnake(PictureBox gameBoardPictureBox)
        {
            Graphics MyGraphics;  // Graphics for main drawing.
            SolidBrush MyBrush;   // Brush for filling shapes.
            SolidBrush MyBrush2;  // Second brush for erasing streaks.

            MyGraphics = gameBoardPictureBox.CreateGraphics(); // Initialize the 2nd graphics. 
            MyBrush = new System.Drawing.SolidBrush(Color.Black); // Initialize the first brush.
            MyBrush2 = new System.Drawing.SolidBrush(Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))))); // Initialize the 2nd brush.


            // Suppress the streak behind the snake

            if (_Snake[_Snake.Count - 1].Get_Direction() == 0) // If the tail is moving up...
            {
                MyGraphics.FillRectangle(MyBrush2, _Snake[_Snake.Count - 1].Get_X(), _Snake[_Snake.Count - 1].Get_Y() + (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54 - 2), (gameBoardPictureBox.Width / 54 - 2)); // Recolor below the last part of snake.
                if (_Snake[_Snake.Count - 1].Get_Y() == gameBoardPictureBox.Height - (gameBoardPictureBox.Width / 54)) // If the snake goes to the other side of the gameboard the streak to recolor is not behind the snake.
                    MyGraphics.FillRectangle(MyBrush2, _Snake[_Snake.Count - 1].Get_X(), 0, (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54)); // Recolor at the opposite side.
            }

            if (_Snake[_Snake.Count - 1].Get_Direction() == 1) // If the tail is moving right...
            {
                MyGraphics.FillRectangle(MyBrush2, _Snake[_Snake.Count - 1].Get_X() - (gameBoardPictureBox.Width / 54), _Snake[_Snake.Count - 1].Get_Y(), (gameBoardPictureBox.Width / 54 - 2), (gameBoardPictureBox.Width / 54 - 2)); // Recolor at the left of the last part of snake.
                if (_Snake[_Snake.Count - 1].Get_X() == 0) // If the snake goes to the other side of the gameboard the streak to recolor is not behind the snake.
                    MyGraphics.FillRectangle(MyBrush2, gameBoardPictureBox.Width - (gameBoardPictureBox.Width / 54), _Snake[_Snake.Count - 1].Get_Y(), (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54)); // Recolor at the opposite side.
            }

            if (_Snake[_Snake.Count - 1].Get_Direction() == 2) // If the tail is moving down...
            {
                MyGraphics.FillRectangle(MyBrush2, _Snake[_Snake.Count - 1].Get_X(), _Snake[_Snake.Count - 1].Get_Y() - (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54 - 2), (gameBoardPictureBox.Width / 54 - 2)); // Recolor above the last part of snake.
                if (_Snake[_Snake.Count - 1].Get_Y() == 0) // If the snake goes to the other side of the gameboard the streak to recolor is not behind the snake.
                    MyGraphics.FillRectangle(MyBrush2, _Snake[_Snake.Count - 1].Get_X(), gameBoardPictureBox.Height - (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54)); // Recolor at the opposite side.
            }

            if (_Snake[_Snake.Count - 1].Get_Direction() == 3) // If the tail is moving left...
            {
                MyGraphics.FillRectangle(MyBrush2, _Snake[_Snake.Count - 1].Get_X() + (gameBoardPictureBox.Width / 54), _Snake[_Snake.Count - 1].Get_Y(), (gameBoardPictureBox.Width / 54 - 2), (gameBoardPictureBox.Width / 54 - 2)); // Recolor at the right of the last part of snake.
                if (_Snake[_Snake.Count - 1].Get_X() == gameBoardPictureBox.Width - (gameBoardPictureBox.Width / 54)) // If the snake goes to the other side of the gameboard the streak to recolor is not behind the snake.
                    MyGraphics.FillRectangle(MyBrush2, 0, _Snake[_Snake.Count - 1].Get_Y(), (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54)); // Recolor at the opposite side
            }


            // Draw all parts of the snake (at the beginning of the game)

            if ((_Snake.Count == 3)) // If the size of the snake is equal to 3 (at the begin of the game)...
                for (int i = 0; i < _Snake.Count; i++) // ... Look for each part of the snake...
                    MyGraphics.FillEllipse(MyBrush, new Rectangle(_Snake[i].Get_X(), _Snake[i].Get_Y(), (gameBoardPictureBox.Width / 54 - 2) - 1, (gameBoardPictureBox.Width / 54 - 2) - 1)); // ... And draw the part.

            // Draw the head of the snake

            else
                MyGraphics.FillEllipse(MyBrush, new Rectangle(_Snake[0].Get_X(), _Snake[0].Get_Y(), (gameBoardPictureBox.Width / 54 - 2) - 1, (gameBoardPictureBox.Width / 54 - 2) - 1));

        }



        public void RenderMiniSnake(PictureBox miniGameBoardPictureBox)
        {

            Graphics MyGraphics;  // Graphics for main drawing.
            SolidBrush MyBrush;   // Brush for filling shapes.
            SolidBrush MyBrush2;  // Second brush for erasing streaks.

            MyGraphics = miniGameBoardPictureBox.CreateGraphics(); // Initialize the 2nd graphics. 
            MyBrush = new System.Drawing.SolidBrush(Color.Black); // Initialize the first brush.
            MyBrush2 = new System.Drawing.SolidBrush(Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))))); // Initialize the 2nd brush.

            // Suppress the streak behind the snake
            if (_Snake.Count >= 1)
            {
                if (_Snake[_Snake.Count - 1].Get_Direction() == 0) // If the tail is moving up...
                {
                    MyGraphics.FillRectangle(MyBrush2, _Snake[_Snake.Count - 1].Get_X() / 2, (_Snake[_Snake.Count - 1].Get_Y() + (miniGameBoardPictureBox.Width / 27)) / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2); // Recolor below the last part of snake.
                    if (_Snake[_Snake.Count - 1].Get_Y() == miniGameBoardPictureBox.Height * 2 - (miniGameBoardPictureBox.Width / 27)) // If the snake goes to the other side of the gameboard the streak to recolor is not behind the snake.
                        MyGraphics.FillRectangle(MyBrush2, _Snake[_Snake.Count - 1].Get_X() / 2, 0, (miniGameBoardPictureBox.Width / 27 - 2) / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2); // Recolor at the opposite side.
                }

                if (_Snake[_Snake.Count - 1].Get_Direction() == 1) // If the tail is moving right...
                {
                    MyGraphics.FillRectangle(MyBrush2, (_Snake[_Snake.Count - 1].Get_X() - (miniGameBoardPictureBox.Width / 27)) / 2, _Snake[_Snake.Count - 1].Get_Y() / 2, ((miniGameBoardPictureBox.Width / 27 - 2)) / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2); // Recolor at the left of the last part of snake.
                    if (_Snake[_Snake.Count - 1].Get_X() == 0) // If the snake goes to the other side of the gameboard the streak to recolor is not behind the snake.
                        MyGraphics.FillRectangle(MyBrush2, ((miniGameBoardPictureBox.Width) * 2 - (miniGameBoardPictureBox.Width / 27)) / 2, _Snake[_Snake.Count - 1].Get_Y() / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2); // Recolor at the opposite side.
                }

                if (_Snake[_Snake.Count - 1].Get_Direction() == 2) // If the tail is moving down...
                {
                    MyGraphics.FillRectangle(MyBrush2, _Snake[_Snake.Count - 1].Get_X() / 2, (_Snake[_Snake.Count - 1].Get_Y() - (miniGameBoardPictureBox.Width / 27)) / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2); // Recolor above the last part of snake.
                    if (_Snake[_Snake.Count - 1].Get_Y() == 0) // If the snake goes to the other side of the gameboard the streak to recolor is not behind the snake.
                        MyGraphics.FillRectangle(MyBrush2, _Snake[_Snake.Count - 1].Get_X() / 2, (miniGameBoardPictureBox.Height * 2 - (miniGameBoardPictureBox.Width / 27)) / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2); // Recolor at the opposite side.
                }

                if (_Snake[_Snake.Count - 1].Get_Direction() == 3) // If the tail is moving left...
                {
                    MyGraphics.FillRectangle(MyBrush2, (_Snake[_Snake.Count - 1].Get_X() + (miniGameBoardPictureBox.Width / 27)) / 2, _Snake[_Snake.Count - 1].Get_Y() / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2); // Recolor at the right of the last part of snake.
                    if (_Snake[_Snake.Count - 1].Get_X() == miniGameBoardPictureBox.Width * 2 - (miniGameBoardPictureBox.Width / 27)) // If the snake goes to the other side of the gameboard the streak to recolor is not behind the snake.
                        MyGraphics.FillRectangle(MyBrush2, 0, _Snake[_Snake.Count - 1].Get_Y() / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2, (miniGameBoardPictureBox.Width / 27 - 2) / 2); // Recolor at the opposite side
                }


                // Draw all parts of the snake (at the beginning of the game)

                if ((_Snake.Count == 3)) // If the size of the snake is equal to 3 (at the begin of the game)...
                    for (int i = 0; i < _Snake.Count; i++) // ... Look for each part of the snake...
                        MyGraphics.FillEllipse(MyBrush, new Rectangle(_Snake[i].Get_X() / 2, _Snake[i].Get_Y() / 2, ((miniGameBoardPictureBox.Width / 27 - 2) - 1) / 2, ((miniGameBoardPictureBox.Width / 27 - 2) - 1) / 2)); // ... And draw the part.

                // Draw the head of the snake

                else
                    MyGraphics.FillEllipse(MyBrush, new Rectangle(_Snake[0].Get_X() / 2, _Snake[0].Get_Y() / 2, ((miniGameBoardPictureBox.Width / 27 - 2) - 1) / 2, ((miniGameBoardPictureBox.Width / 27 - 2) - 1) / 2));
            }

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
