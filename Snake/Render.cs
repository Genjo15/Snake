using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Snake
{
    class Render
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        Graphics _MyGraphics;     // Graphics for main drawing.
        Graphics _MyMiniGraphics; // Graphics for mini map drawing.
        SolidBrush _MyBrush;      // Brush for filling shapes.
        SolidBrush _MyBrush2;     // Second brush for erasing streaks.
        Pen _Pen;                 // Pen.

        private List<PictureBox> _MiniSnakeGraphicalParts;
        PictureBox _Fruit;      // The picturebox of the fruit.
        PictureBox _MiniFruit;  // The picturebox of the mini fruit.
        PictureBox _Insect;     // The picturebox of the insect.
        PictureBox _MiniInsect; // The picturebox of the mini insect.

        private delegate void processOnRender(FullSnake f, PictureBox p); // Delegate type for snake methods.
        private delegate void processOnRender2(Fruit f, PictureBox p);    // Delegate type for fruit methods.
        private delegate void processOnRender3(Insect i, PictureBox p);   // Delegate type for insect methods.
        private processOnRender _RenderMiniSnakeDel;            // Delegate for RenderMiniSnake method.
        private processOnRender2 _RenderFruitDel;               // Delegate for RenderFruit method.
        private processOnRender2 _RenderMiniFruitDel;           // Delegate for RenderMiniFruit method.
        private processOnRender3 _RenderInsectDel;              // Delegate for RenderInsect method.
        private processOnRender3 _RenderMiniInsectDel;          // Delegate for RenderMiniInsect method.
       

        #endregion

        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public Render(PictureBox gameBoardPictureBox, PictureBox miniGameBoardPictureBox)
        {
            _RenderMiniSnakeDel = new processOnRender(RenderMiniSnake);    ////
            _RenderFruitDel = new processOnRender2(RenderFruit);           //
            _RenderMiniFruitDel = new processOnRender2(RenderMiniFruit);   // Initialiaze delegates.
            _RenderInsectDel = new processOnRender3(RenderInsect);         //
            _RenderMiniInsectDel = new processOnRender3(RenderMiniInsect); //

            _MyGraphics = gameBoardPictureBox.CreateGraphics();         // Initialize the graphics object for the gameboard.
            _MyMiniGraphics = miniGameBoardPictureBox.CreateGraphics(); // Initialize the graphics object for the mini gameboard.
            _MyBrush = new System.Drawing.SolidBrush(Color.Black);      // Initialize the first brush.
            _MyBrush2 = new System.Drawing.SolidBrush(Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))))); // Initialize the 2nd brush.
            _Pen = new Pen(Color.Black, 2); // Initialize pen.

            _MiniSnakeGraphicalParts = new List<PictureBox>(); // New list of picturebox (for mini snake).

            _Fruit = new PictureBox();                       // New PictureBox.
            _Fruit.Size = new System.Drawing.Size(12,12);    // Definition of the picturebox size.
            _Fruit.Image = Snake.Properties.Resources.Fruit; // Initialize the fruit image.

            _MiniFruit = new PictureBox();                           // New PictureBox.
            _MiniFruit.Size = new System.Drawing.Size(6, 6);         // Definition of the picturebox size. 
            _MiniFruit.Image = Snake.Properties.Resources.MiniFruit; // Initialize the mini fruit image.

            _Insect = new PictureBox();                        // New PictureBox.
            _Insect.Size = new System.Drawing.Size(26,26);     // Definition of the picturebox size.
            _Insect.Image = Snake.Properties.Resources.Insect; // Initialize the insect image.

            _MiniInsect =  new PictureBox();                           // New PictureBox.
            _MiniInsect.Size = new System.Drawing.Size(13,13);         // Definition of the picturebox size.
            _MiniInsect.Image = Snake.Properties.Resources.MiniInsect; // Initialize the mini insect image.                     
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        ////////////////////////
        // Display of the snake

        public void RenderSnake(FullSnake snake, PictureBox gameBoardPictureBox)
        {
            try
            {
                // Suppress the streak behind the snake

                if (snake.Get_SnakeSize() >= 1)
                {
                    if (snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_Direction() == 0) // If the tail is moving up...
                    {
                        _MyGraphics.FillRectangle(_MyBrush2, snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_X(), snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_Y() + (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54)); // Recolor below the last part of snake.
                        if (snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_Y() == gameBoardPictureBox.Height - (gameBoardPictureBox.Width / 54)) // If the snake goes to the other side of the gameboard the streak to recolor is not behind the snake.
                            _MyGraphics.FillRectangle(_MyBrush2, snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_X(), 0, (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54)); // Recolor at the opposite side.
                    }

                    if (snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_Direction() == 1) // If the tail is moving right...
                    {
                        _MyGraphics.FillRectangle(_MyBrush2, snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_X() - (gameBoardPictureBox.Width / 54), snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_Y(), (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54)); // Recolor at the left of the last part of snake.
                        if (snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_X() == 0) // If the snake goes to the other side of the gameboard the streak to recolor is not behind the snake.
                            _MyGraphics.FillRectangle(_MyBrush2, gameBoardPictureBox.Width - (gameBoardPictureBox.Width / 54), snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_Y(), (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54)); // Recolor at the opposite side.
                    }

                    if (snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_Direction() == 2) // If the tail is moving down...
                    {
                        _MyGraphics.FillRectangle(_MyBrush2, snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_X(), snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_Y() - (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54)); // Recolor above the last part of snake.
                        if (snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_Y() == 0) // If the snake goes to the other side of the gameboard the streak to recolor is not behind the snake.
                            _MyGraphics.FillRectangle(_MyBrush2, snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_X(), gameBoardPictureBox.Height - (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54)); // Recolor at the opposite side.
                    }

                    if (snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_Direction() == 3) // If the tail is moving left...
                    {
                        _MyGraphics.FillRectangle(_MyBrush2, snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_X() + (gameBoardPictureBox.Width / 54), snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_Y(), (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54)); // Recolor at the right of the last part of snake.
                        if (snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_X() == gameBoardPictureBox.Width - (gameBoardPictureBox.Width / 54)) // If the snake goes to the other side of the gameboard the streak to recolor is not behind the snake.
                            _MyGraphics.FillRectangle(_MyBrush2, 0, snake.Get_Snake()[snake.Get_SnakeSize() - 1].Get_Y(), (gameBoardPictureBox.Width / 54), (gameBoardPictureBox.Width / 54)); // Recolor at the opposite side
                    }
                }


                // Draw all parts of the snake (at the beginning of the game)

                if ((snake.Get_SnakeSize() == 3)) // If the size of the snake is equal to 3 (at the begin of the game)...
                    for (int i = 0; i < snake.Get_SnakeSize(); i++) // ... Look for each part of the snake...
                        _MyGraphics.FillEllipse(_MyBrush, new Rectangle(snake.Get_Snake()[i].Get_X(), snake.Get_Snake()[i].Get_Y(), (gameBoardPictureBox.Width / 54 - 2) - 1, (gameBoardPictureBox.Width / 54 - 2) - 1)); // ... And draw the part.

                // Draw the head of the snake

                else
                    _MyGraphics.FillEllipse(_MyBrush, new Rectangle(snake.Get_Snake()[0].Get_X(), snake.Get_Snake()[0].Get_Y(), (gameBoardPictureBox.Width / 54 - 2) - 1, (gameBoardPictureBox.Width / 54 - 2) - 1));
            }

            catch (Exception e) { Console.WriteLine(e); }
        }

        /////////////////////////////
        // Display of the mini snake

        public void RenderMiniSnake(FullSnake snake, PictureBox miniGameBoardPictureBox)
        {
            for (int i = 0; i < snake.Get_SnakeSize(); i++)
            {
                if (_MiniSnakeGraphicalParts.Count < snake.Get_SnakeSize()) // If there is not enough picturebox in the pool ...
                {
                    _MiniSnakeGraphicalParts.Add(new PictureBox());                   // ...Add it one.
                    _MiniSnakeGraphicalParts[_MiniSnakeGraphicalParts.Count - 1].Size = new System.Drawing.Size(snake.Get_Snake()[i].Get_SIDE() / 2, snake.Get_Snake()[i].Get_SIDE() / 2); // Definition of the picturebox size.
                    _MiniSnakeGraphicalParts[_MiniSnakeGraphicalParts.Count - 1].BackColor = System.Drawing.Color.Black; // Definition of the picturebox color.
                    miniGameBoardPictureBox.Controls.Add(_MiniSnakeGraphicalParts[_MiniSnakeGraphicalParts.Count - 1]);  // Attach the control to the gameboard.
                }

                if(_MiniSnakeGraphicalParts[i].Parent != miniGameBoardPictureBox)      // If the control is not attached to the gameboard...
                    miniGameBoardPictureBox.Controls.Add(_MiniSnakeGraphicalParts[i]); // Attach the control to the gameboard.
                _MiniSnakeGraphicalParts[i].Location = new System.Drawing.Point(snake.Get_Snake()[i].Get_X()/2, snake.Get_Snake()[i].Get_Y()/2);  // Definition of the panel location.       
            }
        }

        ////////////////////////
        // Display of the fruit 

        public void RenderFruit(Fruit fruit, PictureBox gameBoardPictureBox)
        {
            if (_Fruit.Parent != gameBoardPictureBox)
                gameBoardPictureBox.Controls.Add(_Fruit);
            
            _Fruit.Location = new System.Drawing.Point(fruit.Get_X(), fruit.Get_Y());
        }

        /////////////////////////////
        // Display of the mini fruit 

        public void RenderMiniFruit(Fruit fruit, PictureBox miniGameBoardPictureBox)
        {
            if (_MiniFruit.Parent != miniGameBoardPictureBox)
                miniGameBoardPictureBox.Controls.Add(_MiniFruit);

            _MiniFruit.Location = new System.Drawing.Point(fruit.Get_X()/2, fruit.Get_Y()/2);
        }

        ////////////////////////
        // Display of the insect 

        public void RenderInsect(Insect insect, PictureBox gameBoardPictureBox)
        {
            if (_Insect.Parent != gameBoardPictureBox)
                gameBoardPictureBox.Controls.Add(_Insect);

            _Insect.Location = new System.Drawing.Point(insect.Get_X(), insect.Get_Y());
        }

        //////////////////////////////
        // Display of the mini insect 

        public void RenderMiniInsect(Insect insect, PictureBox miniGameBoardPictureBox)
        {
            if (_MiniInsect.Parent != miniGameBoardPictureBox)
                miniGameBoardPictureBox.Controls.Add(_MiniInsect);

            _MiniInsect.Location = new System.Drawing.Point(insect.Get_X()/2, insect.Get_Y()/2);
        }

        ////////////////
        // Render Walls

        public void RenderWalls(ListWalls listWalls)
        {
            if(listWalls.Get_ListWallsSize() >= 1)
                /*for (int i = 0; i < listWalls.Get_ListWallsSize(); i++)
                {
                    _MyGraphics.DrawRectangle(_Pen, element.Get_X(), element.Get_Y(), element.Get_Side(), element.Get_Side()); // Draw a rectangle for each element of the list of walls.
                }*/
                _MyGraphics.DrawRectangle(_Pen, listWalls.Get_ListWalls()[listWalls.Get_ListWallsSize() - 1].Get_X(), listWalls.Get_ListWalls()[listWalls.Get_ListWallsSize() - 1].Get_Y(), listWalls.Get_ListWalls()[listWalls.Get_ListWallsSize() - 1].Get_Side(), listWalls.Get_ListWalls()[listWalls.Get_ListWallsSize() - 1].Get_Side());
            //}

            //catch (Exception e) { Console.WriteLine(e); }
        }

        /////////////////////
        // Render mini Walls

        public void RenderMiniWalls(ListWalls listWalls)
        {
            if (listWalls.Get_ListWallsSize() >= 1)
            //try
            //{
                _MyMiniGraphics.DrawRectangle(_Pen, listWalls.Get_ListWalls()[listWalls.Get_ListWallsSize() - 1].Get_X()/2, listWalls.Get_ListWalls()[listWalls.Get_ListWallsSize() - 1].Get_Y()/2, listWalls.Get_ListWalls()[listWalls.Get_ListWallsSize() - 1].Get_Side()/2, listWalls.Get_ListWalls()[listWalls.Get_ListWallsSize() - 1].Get_Side()/2);
            //}

   //         catch (Exception e) { Console.WriteLine(e); }
        }

        #endregion

        #region Accessors

        //////////////
        // Get _Fruit

        public PictureBox Get_Fruit()
        {
            return _Fruit;
        }

        ///////////////
        // Get _Insect

        public PictureBox Get_Insect()
        {
            return _Insect;
        }

        //////////////////////////
        // Get RenderMiniSnakeDel

        public Delegate Get_RenderMiniSnakeDel()
        {
            return _RenderMiniSnakeDel;
        }

        ///////////////////
        // Get RenderFruit

        public Delegate Get_RenderFruitDel()
        {
            return _RenderFruitDel;
        }

        ///////////////////////
        // Get RenderMiniFruit

        public Delegate Get_RenderMiniFruitDel()
        {
            return _RenderMiniFruitDel;
        }

        ///////////////////
        // Get RenderInsect

        public Delegate Get_RenderInsectDel()
        {
            return _RenderInsectDel;
        }

        ////////////////////////
        // Get RenderMiniInsect

        public Delegate Get_RenderMiniInsectDel()
        {
            return _RenderMiniInsectDel;
        }

        #endregion
    }
}
