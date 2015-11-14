using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemyController : MonoSingleton<SpawnEnemyController> {

    //private float timeWaitSpawnEnemy; //Thoi gian sinh enemy trong mot dot
    //public float timeWaitSpawnEnemyNormal; //Thoi gian spawn enemy giau cacs dot
    public Transform spawnPositionLeft;
    public Transform spawnPositionRight;
    
    //Alpha test
    public GameObject enemyPrefab;
    const int ENEMY_GROUP_MAX = 5;
    public int groupEnemyCurrentIndex; //Dot thu bao nhieu
    //private bool isSpawn; //
    //
    public int row; //So hang enemy
    public int col; //So cot enemy
    private Vector3 positionLeft;
    private Vector3 positionRight;
    private float spaceX; //Khoang cach chieu x
    private float spaceY; //Khoang cach chieu y
    //
    public List<BaseEnemyObject> enemyInScreen;

    public Transform tranformBossAppear;// vi tri xuat hien boss
    public bool isCreateBoss = true;

    void Start()
    {
        //isSpawn = true;
        enemyInScreen = new List<BaseEnemyObject>();
        CalIndexBegin();
        SpawnGroupEnemyWithMap();
    }

    void Update()
    {   
        if (groupEnemyCurrentIndex < ENEMY_GROUP_MAX && enemyInScreen.Count <= 0)
        {
            //Invoke("SpawnGroupEnemy", timeWaitSpawnEnemyNormal);
            SpawnGroupEnemyWithMap();
            //isSpawn = true;
        }else if(isCreateBoss && enemyInScreen.Count <=0)
        {
            GameObject obj_Boss = ManagerObject.Instance.SpawnEnemy(BaseObjectType.OB_BOSS_TANKER, tranformBossAppear.position);
            SpawnGroupEnemyWithMap();
            isCreateBoss = false;
        }
    }

    public void SpawnEnemy()
    {
        float posX = Random.Range(spawnPositionLeft.position.x, spawnPositionRight.position.x);
        float posY = Random.Range(spawnPositionLeft.position.y, spawnPositionRight.position.y);
        Vector3 newPos = new Vector3(posX, posY, 0);
        //GameObject enemy = PoolCustomize.Instance.GetBaseObject(enemyPrefab, newPos, "Enemy");
        GameObject enemy = ManagerObject.Instance.SpawnEnemy(BaseObjectType.OBE_ENEMY_TANKER, newPos);
        BaseEnemyObject baseEnemy = enemy.GetComponent<BaseEnemyObject>();
        baseEnemy.ResetValueOfAvariable();
    }

    public void SpawnGroupEnemyWithMap()
    {
        CowboyLevel cowboyLevel = BaseLoadData.cowboyLevel.Find(x=>x.espisoID == groupEnemyCurrentIndex);
        if(cowboyLevel != null)
        {
            GameObject enemy;
            row = cowboyLevel.row;
            col = cowboyLevel.col;
            CalIndexBegin();
            for (int i = cowboyLevel.row - 1; i >= 0; i--)
            {
                for (int j = 0; j < cowboyLevel.col; j++)
                {
                    if (cowboyLevel.levelMatrix[i][j] != 0)
                    {
                        Vector3 newPos = new Vector3(positionLeft.x + spaceX/2 + j * spaceX, positionLeft.y - i * spaceY,0);
                        enemy = ManagerObject.Instance.SpawnEnemy(BaseObjectType.OBE_ENEMY_TANKER, newPos);
                        //PoolCustomize.Instance.GetBaseObject(enemyPrefab, newPos, "Enemy");
                        BaseEnemyObject baseEnemy = enemy.GetComponent<BaseEnemyObject>();
                        baseEnemy.ResetValueOfAvariable();
                        baseEnemy.healthBegin += i * 50;
                        enemyInScreen.Add(baseEnemy);
                    }
                }
            }
        }
        //isSpawn = false;
        groupEnemyCurrentIndex += 1;
    }

    public void SpawnGroupEnemy()
    {
        float numberOfEnemy = Random.Range(3, 6);
        List<Vector3> posOfEnemy = new List<Vector3>();
        GameObject enemy;
        for (int i = 0; i < numberOfEnemy; i++)
        {
            Vector3 newPos = RandomPosition(ref posOfEnemy, row, col);
            enemy = ManagerObject.Instance.SpawnEnemy(BaseObjectType.OBE_ENEMY_TANKER, newPos); 
            //PoolCustomize.Instance.GetBaseObject(enemyPrefab, newPos, "Enemy");
            BaseEnemyObject baseEnemy = enemy.GetComponent<BaseEnemyObject>();
            baseEnemy.ResetValueOfAvariable();
            baseEnemy.healthBegin += i * 50;
            enemyInScreen.Add(baseEnemy);
        }
        //isSpawn = false;
        groupEnemyCurrentIndex += 1;
    }

    public Vector3 RandomPosition(ref List<Vector3> positionArr, int row, int column)
    {
        Vector3 newPos;
        if (positionArr != null)
        {
            float posX;
            float posY;
            do
            {
                posX = Random.Range(- col / 2, col / 2) * spaceX;
                posY = Random.Range(1, row) * spaceY;
                posY = posY != 0 ? posY : positionLeft.y;
                newPos = new Vector3(BaseUtilExtentions.Instance.RoundToFloat(posX, 2),
                                     BaseUtilExtentions.Instance.RoundToFloat(posY, 2), 0);
            } while (positionArr.Contains(newPos));
            positionArr.Add(newPos);
            return newPos;
        }
        return Vector3.zero;
    }

    public bool ContainsInList(ref List<Vector3> positionArr, ref Vector3 enemyPosition)
    {
        foreach (var item in positionArr)
        {
            if (item.x == enemyPosition.x && item.y == enemyPosition.y)
            {
                return true;
            }
        }
        return false;
    }

    public void CalIndexBegin()
    {
        positionLeft = spawnPositionLeft.position;
        positionRight = spawnPositionRight.position;
        spaceX = Mathf.Abs(positionLeft.x - positionRight.x) / col;
        spaceY = Mathf.Abs(-positionRight.y + positionLeft.y) / row;
    }
}
