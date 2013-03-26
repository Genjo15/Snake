using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Snake
{
    [Serializable]
    public struct HighScoreData
    {
        public string[] PlayerName;
        public int[] Score;
        public int[] time;

        public int Count;

        public HighScoreData(int count)
        {
            PlayerName = new string[count];
            Score = new int[count];
            time = new int[count];

            Count = count;
        }
    }

    class HighScore
    {
        public const String HighScoresFilename = "highScores";

        public static int SaveHighScores(HighScoreData data)
        {
            int success = 0;
            using (Stream file = new FileStream(HighScoresFilename, FileMode.OpenOrCreate))
                try
                {
                    // Convert the object to XML data and put it in the stream
                    XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
                    serializer.Serialize(file, data);
                    success = 1;
                }
                finally
                {
                    file.Close();
                }

            return success;
        }


        public static HighScoreData LoadHighScores()
        {
            HighScoreData data= new HighScoreData();
            // Open the file
            using (Stream file = new FileStream(HighScoresFilename, FileMode.Open))
                try
                {
                    // Read the data from the file
                    XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
                    data = (HighScoreData)serializer.Deserialize(file);
                }catch(Exception e)
                {
                }
                finally
                {
                    // Close the file
                    file.Close();
                }

            return data;
        }
    }
}
