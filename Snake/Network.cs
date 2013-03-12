using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    class Network
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables

        private Boolean _IsServer;              // Boolean which indicates if it's the server.
        private Boolean _ConnectionEstablished; // Boolean which indicates if the connection is established between the server & the client.


        #endregion


        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public Network(Boolean isServer)
        {
            _IsServer = isServer;
            _ConnectionEstablished = false;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

       

        #endregion


        #region Accessors

        /////////////////
        // Get _IsServer

        public Boolean Get_IsServer()
        {
            return _IsServer;
        }

        //////////////////////////////
        // Get _ConnectionEstablished

        public Boolean Get_ConnectionEstablished()
        {
            return _ConnectionEstablished;
        }


        #endregion
    }
}
