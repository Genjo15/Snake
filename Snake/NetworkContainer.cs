using System;
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
  
        private String _Msg;        // The message.
        private FullSnake _Snake;   // Temporary snake.
        private Fruit _Fruit;       // Temporary fruit.
        private Insect _Insect;     // Temporary insect.

        private Boolean _HasBeenModified;

        #endregion


        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public NetworkContainer()
        {
            _Msg = "000";
            _Snake = new FullSnake();
            _Fruit = new Fruit();
            _Insect = new Insect();

            _HasBeenModified = false;
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

        //////////////
        // Get _Snake

        public FullSnake Get_Snake()
        {
            return _Snake;
        }

        //////////////
        // Set _Snake

        public void Set_Snake(FullSnake snake)
        {
            _Snake = snake;
        }

        //////////////
        // Get _Fruit

        public Fruit Get_Fruit()
        {
            return _Fruit;
        }

        //////////////
        // Set _Fruit

        public void Set_Fruit(Fruit fruit)
        {
            _Fruit = fruit;
        }

        ///////////////
        // Get _Insect

        public Insect Get_Insect()
        {
            return _Insect;
        }

        //////////////
        // Set _Insect

        public void Set_Insect(Insect insect)
        {
            _Insect = insect;
        }

        #endregion
    }
}
