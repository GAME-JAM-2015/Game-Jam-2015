using UnityEngine;
using System.Collections;

public class CowboyLevel{

    public int level;
    public int espisoID;
    public int row;
    public int col;
    public int[][] levelMatrix;

    public void InitLevelMatrix()
    {
        levelMatrix = new int[row][];
        for (int i = 0; i < row; i++)
        {
            levelMatrix[i] = new int[col];
        }
    }
}
