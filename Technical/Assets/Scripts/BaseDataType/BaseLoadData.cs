using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseLoadData {

    public static List<CowboyLevel> cowboyLevel;

    public static void LoadData()
    {
        cowboyLevel = new List<CowboyLevel>();
        LoadCowboyLevelFromFile(cowboyLevel, "DataType/CowboyLevel");
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
}
