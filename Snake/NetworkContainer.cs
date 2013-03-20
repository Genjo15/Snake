using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    class NetworkContainer
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private String _Msg;        // The message (in String).

        //private FullSnake _Snake; // Temporary snake.

        #endregion


        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public NetworkContainer()
        {
            _Msg = "000";
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods


        #endregion


        #region Accessors&Mutators

        public String Get_Msg()
        {
            return _Msg;
        }

        public void Set_Msg(String message)
        {
            _Msg = message;
        }

        

        #endregion
    }
}
