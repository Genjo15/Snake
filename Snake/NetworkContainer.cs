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
  
        private String _Msg;          // The message.
        private FullSnake _Snake;     // Temporary snake.
        private Fruit _Fruit;         // Temporary fruit.
        private Insect _Insect;       // Temporary insect.
        private ListWalls _ListWalls; // Temporary list of walls.
        private String _Nickname;     // Temporary nickname.
        private int _Score;           // Temporary score.

        private Boolean _HasBeenModified; // Boolean which indicates if the container has been modified or not.

        #endregion


        /**************************************************** Constructors ****************************************************/

        #region Constructors

        public NetworkContainer()
        {
            _Msg = "000";                 // Initialize _Msg.
            _Snake = new FullSnake();     // Initialize _Snake.
            _Fruit = new Fruit();         // Initialize _Fruit.
            _Insect = new Insect();       // Initialize _Insect.
            _ListWalls = new ListWalls(); // Initialize _ListWalls.
            _Nickname = "";               // Initialize _Nickname.
            _Score = 0;                   // Initialize _Score.

            _HasBeenModified = false; // Initlialize _HasBeenModified to FALSE.
        }

        #endregion


        /****************************************************** Methods ******************************************************/

        #region Methods

        ///////////////////////////////////////////////////////////
        // Function to serialize the object into an array of byte

        public Byte[] SerializeContainer()
        {
            BinaryFormatter binaryFormater = new BinaryFormatter(); // Binary formatter.
            MemoryStream memoryStream = new MemoryStream();         // Memory stream.

            binaryFormater.Serialize(memoryStream, this); // Serialize data.

            return memoryStream.ToArray(); // Return memory stream to array of bytes.
        }


        /////////////////////////////////////////////////////////////////////////////
        // Function to deserialize an array of byte into an object of type Container

        public NetworkContainer DeserializeContainer(byte[] arrayOfBytes)
        {
            MemoryStream memoryStream = new MemoryStream();         // Memory stream.
            BinaryFormatter binaryFormater = new BinaryFormatter(); // Binary formatter.
            NetworkContainer obj = new NetworkContainer();          // Network Container.

            try
            {
                memoryStream.Write(arrayOfBytes, 0, arrayOfBytes.Length); // Get the array of bytes .
                memoryStream.Seek(0, SeekOrigin.Begin);
                obj = (NetworkContainer)binaryFormater.Deserialize(memoryStream); // Deserialize container.
                
            }
            catch (Exception e) { Console.WriteLine(e); }

            return obj; // Return object.   
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

        /////////////////
        // Get _ListWalls

        public ListWalls Get_ListWalls()
        {
            return _ListWalls;
        }

        /////////////////
        // Set _ListWalls

        public void Set_ListWalls(ListWalls listWalls)
        {
            _ListWalls = listWalls;
        }

        /////////////////
        // Get _Nickname

        public String Get_Nickname()
        {
            return _Nickname;
        }

        /////////////////
        // Set _Nickname

        public void Set_Nickname(String nm)
        {
            _Nickname = nm;
        }

        //////////////
        // Get _Score

        public int Get_Score()
        {
            return _Score;
        }

        //////////////
        // Set _Score

        public void Set_Score(int s)
        {
            _Score = s;
        }

        #endregion
    }
}
