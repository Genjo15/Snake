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

        public int Count;        

        public HighScoreData(int count)
        {
            PlayerName = new string[count];
            Score = new int[count];           
            Count = count;            
        }

    }

    class HighScore
    {
        public const int MAX_HighScores = 5;
        public const String HighScoresFilename = "highScores";

        public static int SaveHighScores(HighScoreData data)
        {
            int success = 0;
            try
            {
                using (Stream file = new FileStream(HighScoresFilename, FileMode.OpenOrCreate))
                {
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
                }
            }
            catch (Exception e)
            {
                success = -1;                
            }
                

            return success;
        }


        public static HighScoreData LoadHighScores()
        {

            HighScoreData? hsLoad = HighScore._LoadHighScores();
            HighScoreData sample;
            if (hsLoad.Equals(null))
            {
                sample = new HighScoreData(3);
                sample.PlayerName[0] = "Test";
                sample.Score[0] = 2;
                sample.PlayerName[1] = "JohnTheCrazy";
                sample.Score[1] = 20;
                sample.PlayerName[2] = "Doe";
                sample.Score[2] = 120;
                HighScore.SaveHighScores(sample);
            }
            else
            {
                sample = (HighScoreData) hsLoad;
            }

            return sample;

    }

        private static HighScoreData? _LoadHighScores()
        {
            HighScoreData? data= null;
            // Open the file
            try
            {
                using (Stream file = new FileStream(HighScoresFilename, FileMode.OpenOrCreate))
                {
                    try
                    {
                        // Read the data from the file
                        XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
                        data = (HighScoreData)serializer.Deserialize(file);
                    }
                    finally
                    {
                        // Close the file
                        file.Close();
                    }
                }
            }catch(Exception e)
            {
            }

            return data;
        }


        public static bool check_ScoreUp(int score)
        {
            bool beatten = false;
            HighScoreData data = HighScore.LoadHighScores();            
            for(int i = 0;i < data.Count; i++)
            {
                if(data.Score[i] < score)
                {
                        beatten = true;
                }
            }
            return beatten;
        }
    }
}
