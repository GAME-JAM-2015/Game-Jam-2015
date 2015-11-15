using UnityEngine;
using System.Collections;

public class UITileGrid : MonoBehaviour {

    public Vector2 index;
    public bool isUsed;
    public Vector2 timeRandom;
    private bool isSpawnEnemy;
    public bool acceptSpawnEnemy;
    //
    public void Init()
    {
        CancelInvoke("SpawnEnemy");
        isSpawnEnemy = false;
        Invoke("SpawnEnemy", Random.Range(timeRandom.x, timeRandom.y));
    }

    void Update()
    {
        if (!acceptSpawnEnemy || InitEnemyController.Instance.isFinishedStage)
            return;
        if (isSpawnEnemy)
        {
            isSpawnEnemy = false;
            InitEnemyController.Instance.SpawnGroupEnemy(transform.position);
            Invoke("SpawnEnemy", Random.Range(timeRandom.x, timeRandom.y));
        }
    }

    void SpawnEnemy()
    {
        isSpawnEnemy = true;
    }
}
