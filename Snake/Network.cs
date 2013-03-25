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

        private NetworkContainer _Container;           // The container for senfing/receiving data.
        private Byte[] _Buffer;                        // The buffer which will contain serialized data (before sending) or incoming data (to deserialize).

        private Delegate _Delegate;                    // The delegate (which comes from the main)
        private delegate void networkDelegate();       // The delegate (inside the class)
        private networkDelegate _NetworkDelegate;      // The delegate variable.

        private Boolean _Continue;
        private Boolean _IsHost;                       // Boolean which determines if we are the host or not.

        private System.Net.Sockets.UdpClient _Socket;  // The socket (for sending data / receiving data, depends on the context)
        private System.Net.IPEndPoint _EndPoint;       // IP Endpoint.

        private String _HostIpAdress;                  // Host Ip Adress.

        //private Boolean _ConnectionEstablished;   // Boolean which determines if the connection is established or not.
        

        #endregion


        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public Network(ref NetworkContainer container, Boolean isHost, Boolean forSending, Delegate del)
        {
            _Container = container;
            _IsHost = isHost;

            _HostIpAdress = "";
            _Continue = true;

            //_ConnectionEstablished = false;

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
            //_Container.test++;
            //Console.WriteLine("here the receive loop : " + _Container.test);
            while (_Continue)
            {
                if (_IsHost)
                {
                    _Buffer = _Socket.Receive(ref _EndPoint);
                    _Container = _Container.DeserializeContainer(_Buffer);
                    Console.WriteLine("Server has received : " + _Container.Get_Msg() + " from : " + _EndPoint.Address.ToString().Split(':')[0]);
                }

                if (!_IsHost)
                {
                    _Buffer = _Socket.Receive(ref _EndPoint);
                    _Container = _Container.DeserializeContainer(_Buffer);
                    Console.WriteLine("Client has received : " + _Container.Get_Msg() + " from : " + _EndPoint.Address.ToString().Split(':')[0]);
                }

                System.Threading.Thread.Sleep(1000);

                _Delegate.DynamicInvoke();

            }
        }

        //////////////////////////////////////////////
        // Function called when _SendingThread starts

        public void SendLoop()
        {
            //_Container.test++;
            //Console.WriteLine("here the send loop : " + _Container.test);
            while (_Continue)
            {
                if (!_IsHost && _Container.Get_HasBeenModified())
                {
                    _Buffer = _Container.SerializeContainer();
                    _Socket.Send(_Buffer, _Buffer.Length, _HostIpAdress, 5001);
                    _Container.Set_HasBeenModified(false);
                }
                
                /*if (!_IsHost && !_ConnectionEstablished)
                {
                    _Container.Set_Msg("001");
                    _Buffer = _Container.SerializeContainer();
                    _Socket.Send(_Buffer, _Buffer.Length, _HostIpAdress, 5001);
                }*/

                /*if (_IsHost && !_ConnectionEstablished && _Container.Get_Msg().Equals("001"))
                {
                    _Container.Set_Msg("010");
                    _Buffer = _Container.SerializeContainer();
                    Console.WriteLine("server sender : " + _EndPoint.Address.ToString().Split(':')[0]);
                    _Socket.Send(_Buffer, _Buffer.Length, _EndPoint.Address.ToString().Split(':')[0], 5001);
                    _ConnectionEstablished = true;
                }*/


                //Console.WriteLine("container is : " + _Container.Get_Msg());
                System.Threading.Thread.Sleep(1000); // Sleep for 1s.

            }
        }
      

        #endregion


        #region Accessors&Mutators

        public NetworkContainer Get_Container()
        {
            return _Container;
        }

        public Delegate Get_NetworkDelegate()
        {
            return _NetworkDelegate;
        }

        ////////////////
        // Get _IsHost

        public Boolean Get_IsHost()
        {
            return _IsHost;
        }

        /////////////////////
        // Set _HostIpAdress

        public void Set_HostIpAdress (String str)
        {
            _HostIpAdress = str;
        }

        //////////////////////////////
        // Set _ConnectionEstablished

        public void Set_ConnectionEstablished(Boolean b)
        {
            //_ConnectionEstablished = b;
        }

        #endregion
    }
}
