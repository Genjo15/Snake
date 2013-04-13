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

        #region Accessors&Mutators

        /////////////////////////
        // Get The list of walls

        public List<Wall> Get_ListWalls()
        {
            return _ListWalls;
        }

        ////////////////////////////
        // Get the size of the list

        public int Get_ListWallsSize()
        {
            return _ListWalls.Count;
        }

        #endregion
    }
   
}
