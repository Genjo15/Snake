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

        private NetworkContainer _Container;           // The container.
        private Delegate _Delegate;                    // The delegate (which comes from the main)

        private delegate void networkDelegate();       // The delegate (inside the class)
        private networkDelegate _NetworkDelegate;      // The delegate variable.

        private Boolean _Continue;
        private Boolean _IsHost;                       // Boolean which determines if we are the host or not.

        private System.Net.Sockets.UdpClient _Socket;  // The socket (for sending data / receiving data, depends on the context)
        private System.Net.IPEndPoint _EndPoint;       // IP Endpoint.


        //private Boolean _ConnectionEstablished;   // Boolean which determines if the connection is established or not.
        

        #endregion


        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public Network(NetworkContainer container, Boolean isHost, Boolean forSending, Delegate del)
        {
            _Container = container;
            _IsHost = isHost;

            _Continue = true;

            _Delegate = del;
            _NetworkDelegate = new networkDelegate(AbortConnection);

            _Socket = new System.Net.Sockets.UdpClient(); // Initialization of the socket.
            _Socket.EnableBroadcast = false;              // Disable broadcast.
            if (forSending)
            {
                _Socket.Client.Bind(new System.Net.IPEndPoint(0, 5000));
                _EndPoint = new System.Net.IPEndPoint(0, 5000);
            }
            else if (!forSending)
            {
                _Socket.Client.Bind(new System.Net.IPEndPoint(0, 5001));
                _EndPoint = new System.Net.IPEndPoint(0, 5001);
            }  
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        ///////////////////////////////////
        // Method for abort the connection

        private void AbortConnection()
        {
            _Socket.Close();
            _Continue = false;
        }

        //////////////////////////////////////////////
        // Function called when _ReceptionThread starts

        public void ReceiveLoop()
        {
            while (_Continue)
            {
                if (_IsHost)
                {


                }

                _Delegate.DynamicInvoke();

            }
        }

        //////////////////////////////////////////////
        // Function called when _SendingThread starts

        public void SendLoop()
        {
            while (_Continue)
            {


            }
        }
      

        #endregion


        #region Accessors

        public NetworkContainer Get_Container()
        {
            return _Container;
        }

        public Delegate Get_NetworkDelegate()
        {
            return _NetworkDelegate;
        }

        #endregion
    }
}
