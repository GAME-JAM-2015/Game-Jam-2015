using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseLoadData {

    public static List<CowboyLevel> cowboyLevel;
    public static List<CowboyEpisode> cowboyEpisode;
    public static List<CowboyRandomConfig> cowboyRandomConfig;
    public static List<CowboyTimeConfig> cowboyTimeConfig;

    public static void LoadData()
    {
        cowboyLevel = new List<CowboyLevel>();
        cowboyEpisode = new List<CowboyEpisode>();
        cowboyRandomConfig = new List<CowboyRandomConfig>();
        cowboyTimeConfig = new List<CowboyTimeConfig>();
        LoadCowboyLevelFromFile(cowboyLevel, "DataType/CowboyLevel");
        LoadCowboyLevelFromFile(cowboyEpisode, "DataType/CowboyEpisode");
        LoadCowboyLevelFromFile(cowboyRandomConfig, "DataType/CowboyRandomConfig");
        LoadCowboyLevelFromFile(cowboyTimeConfig, "DataType/CowboyTimeConfig");
    }

    public static void LoadCowboyLevelFromFile(List<CowboyLevel> listname, string assetPath)
    {
        if (listname != null)
        {
            listname.Clear();
        }
        TextAsset textAsset = (TextAsset)Resources.Load(assetPath, typeof(TextAsset));

        if (textAsset)
        {
            CowboyLevel newObject = new CowboyLevel();
            bool isNewRow = true;
            string[] temp = textAsset.text.Split('\n');
            string[] context;
            int row = 0;
            for (int i = 1; i < temp.Length; i++)
            {
                context = temp[i].Split('\t');
                if (context[0] == "#End")
                {
                    listname.Add(newObject);
                    newObject = new CowboyLevel();
                    isNewRow = true;
                    row = 0;
                }
                else if (isNewRow)
                {
                    isNewRow = false;
                    newObject.level = int.Parse(context[0]);
                    newObject.espisoID = int.Parse(context[1]);
                    newObject.row = int.Parse(context[2]);
                    newObject.col = int.Parse(context[3]);
                    newObject.InitLevelMatrix();
                }
                else
                {
                    for (int j = 0; j < context.Length; j++)
                    {
                        newObject.levelMatrix[row][j] = int.Parse(context[j]);
                    }
                    ++row;
                }
            }
        }
    }

    public static void LoadCowboyLevelFromFile(List<CowboyEpisode> listname, string assetPath)
    {
        if (listname != null)
        {
            listname.Clear();
        }
        TextAsset textAsset = (TextAsset)Resources.Load(assetPath, typeof(TextAsset));

        if (textAsset)
        {

            string[] temp = textAsset.text.Split('\n');

            for (int i = 1; i < temp.Length; i++)
            {

                string[] context = temp[i].Split('\t');
                CowboyEpisode newObject = new CowboyEpisode();
                newObject.cowboyLevel = int.Parse(context[0]);
                newObject.cowboyEpisode = int.Parse(context[1]);
                newObject.enemyNormal = int.Parse(context[2]);
                newObject.enemyTank = int.Parse(context[3]);
                newObject.enemyFlash = int.Parse(context[4]);
                newObject.enemyFly = int.Parse(context[5]);
                newObject.enemyAntiDamgePhysical = int.Parse(context[6]);
                newObject.enemyBuffHealth = int.Parse(context[7]);
                newObject.CalEnemyAppearPercent();
                listname.Add(newObject);
            }
        }
    }

    public static void LoadCowboyLevelFromFile(List<CowboyRandomConfig> listname, string assetPath)
    {
        if (listname != null)
        {
            listname.Clear();
        }
        TextAsset textAsset = (TextAsset)Resources.Load(assetPath, typeof(TextAsset));

        if (textAsset)
        {

            string[] temp = textAsset.text.Split('\n');

            for (int i = 1; i < temp.Length; i++)
            {

                string[] context = temp[i].Split('\t');
                CowboyRandomConfig newObject = new CowboyRandomConfig();
                newObject.cowboyLevel = int.Parse(context[0]);
                newObject.cowboyEpisode = int.Parse(context[1]);
                newObject.cowboyLaneOne = int.Parse(context[2]);
                newObject.cowboyLaneTwo = int.Parse(context[3]);
                newObject.cowboyLaneThree = int.Parse(context[4]);
                newObject.cowboyLaneFour = int.Parse(context[5]);
                newObject.cowboyLaneFive = int.Parse(context[6]);
                newObject.cowboyLaneSix = int.Parse(context[7]);
                newObject.cowboyLaneSeven = int.Parse(context[8]);
                listname.Add(newObject);
            }
        }
    }

    public static void LoadCowboyLevelFromFile(List<CowboyTimeConfig> listname, string assetPath)
    {
        if (listname != null)
        {
            listname.Clear();
        }
        TextAsset textAsset = (TextAsset)Resources.Load(assetPath, typeof(TextAsset));

        if (textAsset)
        {

            string[] temp = textAsset.text.Split('\n');

            for (int i = 1; i < temp.Length; i++)
            {
                string[] context = temp[i].Split('\t');
                CowboyTimeConfig newObject = new CowboyTimeConfig();
                newObject.timeID = int.Parse(context[0]);
                newObject.timeMin = float.Parse(context[1]);
                newObject.timeMax = float.Parse(context[2]);
                listname.Add(newObject);
            }
        }
    }
}
