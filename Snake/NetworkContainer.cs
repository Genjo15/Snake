﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Snake
{
    [Serializable] 
    class NetworkContainer
    {
        /********************************************* Declaration of variables *********************************************/

        #region Variables
  
        private String _Msg;        // The message (in String).
        //public int test;
        //private FullSnake _Snake; // Temporary snake.

        private Boolean _HasBeenModified;

        #endregion


        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public NetworkContainer()
        {
            _Msg = "000";

            _HasBeenModified = false;
            //test = 0;
        }

        #endregion

        /****************************************************** Methods ******************************************************/

        #region Methods

        ///////////////////////////////////////////////////////////
        // Function to serialize the object into an array of byte

        public Byte[] SerializeContainer()
        {
            BinaryFormatter binaryFormater = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();

            binaryFormater.Serialize(memoryStream, this);

            return memoryStream.ToArray();
        }


        /////////////////////////////////////////////////////////////////////////////
        // Function to deserialize an array of byte into an object of type Container

        public NetworkContainer DeserializeContainer(byte[] arrayOfBytes)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormater = new BinaryFormatter();

            memoryStream.Write(arrayOfBytes, 0, arrayOfBytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            NetworkContainer obj = (NetworkContainer)binaryFormater.Deserialize(memoryStream);

            return obj;
        }


        #endregion


        #region Accessors&Mutators

        ///////////
        // Get _Msg

        public String Get_Msg()
        {
            return _Msg;
        }

        ///////////
        // Set _Msg

        public void Set_Msg(String message)
        {
            _Msg = message;
        }

        ////////////////////////
        // Get _HasBeenModified

        public Boolean Get_HasBeenModified()
        {
            return _HasBeenModified;
        }

        ////////////////////////
        // Set _HasBeenModified

        public void Set_HasBeenModified(Boolean b)
        {
            _HasBeenModified = b;
        }
        

        #endregion
    }
}
