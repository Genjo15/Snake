using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    [Serializable] 
    class ListWalls
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private List<Wall> _ListWalls;

        #endregion

        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public ListWalls()
        {
            _ListWalls = new List<Wall>();
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        

        #endregion

        #region RenderMethods

        ////////////////
        // Render Walls

        public void RenderWalls(PictureBox gameBoardPictureBox)
        {
            Graphics myGraphics;  // Graphics for main drawing.
            Pen pen; 

            try
            {
                myGraphics = gameBoardPictureBox.CreateGraphics(); // Initialize the graphics. 
                pen = new Pen(Color.Black, 2);
                
                foreach(Wall element in _ListWalls)
                    myGraphics.DrawRectangle(pen, element.Get_X(), element.Get_Y(), element.Get_Side(), element.Get_Side());
      
            }

            catch (Exception e) { Console.WriteLine(e); }
        }

        /////////////////////
        // render miniWalls

        public void RenderMiniWalls(PictureBox gameBoardPictureBox)
        {
            Graphics myGraphics;  // Graphics for main drawing.
            Pen pen;

            try
            {
                myGraphics = gameBoardPictureBox.CreateGraphics(); // Initialize the graphics. 
                pen = new Pen(Color.Black, 1);

                foreach (Wall element in _ListWalls)
                    myGraphics.DrawRectangle(pen, element.Get_X()/2, element.Get_Y()/2, element.Get_Side()/2, element.Get_Side()/2);

            }

            catch (Exception e) { Console.WriteLine(e); }
        }

        #endregion

        #region Accessors&Mutators

        /////////////////////////
        // Get The list of walls

        public List<Wall> Get_ListWalls()
        {
            return _ListWalls;
        }


        #endregion
    }
   
}
