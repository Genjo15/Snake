using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

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
        private IPEndPoint _EndPoint;                  // IP Endpoint.

        private String _HostIpAdress;                  // Host Ip Adress.
    
        #endregion


        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public Network(ref NetworkContainer container, Boolean isHost, Boolean forSending, Delegate del)
        {
            _Container = container; // Set _Container.
            _IsHost = isHost;       // Set _IsHost.
            _HostIpAdress = "";     // Initialize _HostIpAdress to "".
            _Continue = true;       // Initialize _Continue to TRUE.

            _Delegate = del; // Set _Delegate.
            _NetworkDelegate = new networkDelegate(AbortConnection); // Initilialize delegate.

            _Socket = new System.Net.Sockets.UdpClient(); // Initialization of the socket.
            _Socket.EnableBroadcast = false;              // Disable broadcast.

            if (forSending)                                              ////
            {                                                            //
                _Socket.Client.Bind(new System.Net.IPEndPoint(0, 5000)); // Bind Sending socket to port 5001.
                _EndPoint = new System.Net.IPEndPoint(0, 5000);          //
            }                                                            //
            else if (!forSending)                                        //
            {                                                            //
                _Socket.Client.Bind(new System.Net.IPEndPoint(0, 5001)); // Bind Reception socket to port 5001.
                _EndPoint = new System.Net.IPEndPoint(0, 5001);          //
            }                                                            //
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        ///////////////////////////////////
        // Method for abort the connection

        private void AbortConnection()
        {
            _Continue = false;                // Set _Continue to FALSE.
            System.Threading.Thread.Sleep(5); // Wait 5 ms.

            try
            {
                _Socket.Close(); // Close the socket.
            }
            catch (Exception e) { Console.WriteLine(e); }
        }

        //////////////////////////////////////////////
        // Function called when _ReceptionThread starts

        public void ReceiveLoop()
        {
            while (_Continue) // While _Continue is TRUE...
            {
                if (_IsHost) // If Host...
                {
                    try
                    {
                        _Buffer = _Socket.Receive(ref _EndPoint); // Receive data.
                    }
                    catch (Exception e) { Console.WriteLine(e); }

                    _Container = _Container.DeserializeContainer(_Buffer); // Deserialize data.
                    Console.WriteLine("Server has received : " + _Container.Get_Msg() + " from : " + _EndPoint.Address.ToString().Split(':')[0]);
                }

                if (!_IsHost) // If Client...
                {
                    try
                    {
                        _Buffer = _Socket.Receive(ref _EndPoint); // Receive data.
                    }
                    catch (Exception e) { Console.WriteLine(e); }

                    _Container = _Container.DeserializeContainer(_Buffer); // Deserialize data.
                    Console.WriteLine("Client has received : " + _Container.Get_Msg() + " from : " + _EndPoint.Address.ToString().Split(':')[0]);
                }

                _Delegate.DynamicInvoke(); // Invoke delegate from main (command dispatcher).
            }
        }

        //////////////////////////////////////////////
        // Function called when _SendingThread starts

        public void SendLoop()
        {
            while (_Continue) // While _Continue is TRUE...
            {
                if (!_IsHost && _Container.Get_HasBeenModified()) // If Client...
                {
                    _Buffer = _Container.SerializeContainer();                  // Serialize data.
                    _Socket.Send(_Buffer, _Buffer.Length, _HostIpAdress, 5001); // Send data to host.
                    _Container.Set_HasBeenModified(false);                      // Set _HasBeenModified to FALSE.
                    Console.WriteLine("Client has sent : " + _Container.Get_Msg() + " to " + _HostIpAdress);
                }
                
                if (_IsHost && _Container.Get_HasBeenModified()) // If Host...
                {
                    _Buffer = _Container.SerializeContainer();                                               // Serialize data.
                    _Socket.Send(_Buffer, _Buffer.Length, _EndPoint.Address.ToString().Split(':')[0], 5001); // Send data to client.
                    _Container.Set_HasBeenModified(false);                                                   // Set _HasBeenModified to FALSE.
                    Console.WriteLine("Server has sent : " + _Container.Get_Msg() + " to " + _EndPoint.Address.ToString().Split(':')[0]);
                }

                System.Threading.Thread.Sleep(30); // Sleep for 35ms.
            }
        }
      
        #endregion

        #region Accessors&Mutators

        //////////////////
        // Get _Container

        public NetworkContainer Get_Container()
        {
            return _Container;
        }

        ////////////////////////
        // Get _NetworkDelegate

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

        /////////////////
        // Get _EndPoint

        public IPEndPoint Get_EndPoint()
        {
            return _EndPoint;
        }

        /////////////////
        // Set _EndPoint

        public void Set_EndPoint(IPEndPoint ep)
        {
            _EndPoint = ep;
        }

        #endregion
    }

}
