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
            Count = 0;            
        }

    }

    class HighScore
    {
        public const int MAX_HighScores = 5;
        public const String HighScoresFilename = "highScores";

        public static int SaveHighScores(HighScoreData data)
        {
            HighScore.sort(data);
            int success = 0;
            try
            {
                using (Stream file = new FileStream(HighScoresFilename, FileMode.Create))
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


        public static HighScoreData LoadHighScores()                //Retrieve the leaderboard or create a sample one if none exist
        {

            HighScoreData? hsLoad = HighScore._LoadHighScores();
            HighScoreData sample;
            if (hsLoad.Equals(null))
            {
                sample = new HighScoreData(HighScore.MAX_HighScores);
                sample.PlayerName[0] = "Test";
                sample.Score[0] = 2;
                sample.PlayerName[1] = "JohnTheCrazy";
                sample.Score[1] = 20;
                sample.PlayerName[2] = "Doe";
                sample.Score[2] = 120;
                sample.Count = 3;
                HighScore.SaveHighScores(sample);
            }
            else
            {
                sample = (HighScoreData) hsLoad;
            }

            return sample;

    }

        private static HighScoreData? _LoadHighScores()                                             //Use nullable HighScoreData object to handle none existence of file
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


        public static int check_ScoreUp(int score)
        {
            int beatten = -1;
            int i = 0;
            bool exit = false;
            HighScoreData data = HighScore.LoadHighScores();            
            while( i< data.Count || !exit)
            {            
                if(data.Score[i] < score)
                {
                        beatten = i;
                        exit = true;
                }
                i++;
            }
            return beatten;
        }

        public static int isPlayerAlreadyIn(String nickname)
        {
            int index=-1;
            HighScoreData data = HighScore.LoadHighScores();
            for (int i = 0; i < data.Count; i++)
            {
                if (data.PlayerName[i].Equals(nickname))
                {
                    index = i;
                }
            }
            return index;
        }


        private static void sort(HighScoreData data)
        {            
            int maxScoreIndex = 0;
            int oldScore;
            string oldNick;
            for (int i = 0; i < data.Count; i++)
            {
                maxScoreIndex = i;
                for (int j = i; j < data.Count; j++)
                {
                    if (data.Score[j] >= data.Score[maxScoreIndex])
                    {
                        maxScoreIndex = j;
                    }
                }
                oldScore = data.Score[i];
                oldNick = data.PlayerName[i];
                data.PlayerName[i] = data.PlayerName[maxScoreIndex];
                data.Score[i] = data.Score[maxScoreIndex];
                data.PlayerName[maxScoreIndex] = oldNick;
                data.Score[maxScoreIndex] = oldScore;
                Console.Out.WriteLine("dump : dataScore["+i+"]="+data.Score[i]);
            }

        }
    }
}
