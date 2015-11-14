using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InitEnemyController : MonoSingleton<InitEnemyController> {

    const int ENEMY_GROUP_MAX = 5;
    public int groupEnemyCurrentIndex; //Dot thu bao nhieu
    public List<BaseEnemyObject> enemyInScreen;
    public Transform tranformBossAppear;// vi tri xuat hien boss
    public bool isCreateBoss = true;
    public List<UITileGrid>gridTiles;
    public GameObject gridTilePrefab;
    public int gridCol;
    public int gridRow;
    //
    public bool isFinished;
    public Dictionary<BaseObjectType, int> dicEnemyOfEpisode;
    void Start()
    {
        gridTiles = new List<UITileGrid>();
        dicEnemyOfEpisode = new Dictionary<BaseObjectType, int>();
        CreateGrid();

    }

    void Update()
    {
        if (groupEnemyCurrentIndex < ENEMY_GROUP_MAX && enemyInScreen.Count <= 0)
        {
            //Invoke("SpawnGroupEnemy", timeWaitSpawnEnemyNormal);
            //SpawnGroupEnemy();
            //isSpawn = true;
            
        }
        else if (isCreateBoss && enemyInScreen.Count <= 0)
        {
            isFinished = true;
            GameObject obj_Boss = ManagerObject.Instance.SpawnEnemy(BaseObjectType.OB_BOSS_TANKER, tranformBossAppear.position);
            //SpawnGroupEnemy();
            isCreateBoss = false;
        }
    }

    void CreateGrid()
    {
        UITileGrid uiTileGrid;
        CowboyRandomConfig cowboyRandomConfig = null;
        int cowboyLevel = 1;
        int cowbowEpisode = 2;
        if (GameController.Instance.player != null)
        {
            cowboyLevel = GameController.Instance.player.cowboyLevel;
            cowbowEpisode = GameController.Instance.player.cowboyEpisode;
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Chua co du lieu nguoi dung");
#endif
        }
        cowboyRandomConfig = BaseLoadData.cowboyRandomConfig.Find(x => x.cowboyLevel == cowboyLevel &&
                                                                x.cowboyEpisode == cowbowEpisode);
        CowboyTimeConfig cowboyTimeConfig = null;
        for (int i = 0; i < gridRow; i++)
        {
            for (int j = 0; j < gridCol; j++)
            {
                GameObject gridTile = (GameObject)Instantiate(gridTilePrefab);
                gridTile.transform.SetParent(transform, false);
                uiTileGrid = gridTile.GetComponent<UITileGrid>();
                uiTileGrid.index = new Vector2(i, j);
                uiTileGrid.isUsed = false;
                if (i <= 0 && cowboyRandomConfig != null)
                {
                    uiTileGrid.acceptSpawnEnemy = true;
                    switch (j)
                    {
                        case 0:
                            cowboyTimeConfig = BaseLoadData.cowboyTimeConfig.Find(x => x.timeID == cowboyRandomConfig.cowboyLaneOne);
                            break;
                        case 1:
                            cowboyTimeConfig = BaseLoadData.cowboyTimeConfig.Find(x => x.timeID == cowboyRandomConfig.cowboyLaneTwo);
                            break;
                        case 2:
                            cowboyTimeConfig = BaseLoadData.cowboyTimeConfig.Find(x => x.timeID == cowboyRandomConfig.cowboyLaneThree);
                            break;
                        case 3:
                            cowboyTimeConfig = BaseLoadData.cowboyTimeConfig.Find(x => x.timeID == cowboyRandomConfig.cowboyLaneFour);
                            break;
                        case 4:
                            cowboyTimeConfig = BaseLoadData.cowboyTimeConfig.Find(x => x.timeID == cowboyRandomConfig.cowboyLaneFive);
                            break;
                        case 5:
                            cowboyTimeConfig = BaseLoadData.cowboyTimeConfig.Find(x => x.timeID == cowboyRandomConfig.cowboyLaneSix);
                            break;
                        case 6:
                            cowboyTimeConfig = BaseLoadData.cowboyTimeConfig.Find(x => x.timeID == cowboyRandomConfig.cowboyLaneSeven);
                            break;
                        default:
                            break;
                    }
                    if (cowboyTimeConfig != null)
                    {
                        uiTileGrid.timeRandom = new Vector2(cowboyTimeConfig.timeMin, cowboyTimeConfig.timeMax);
                        uiTileGrid.Init();
                    }
                    else
                    {
                        uiTileGrid.acceptSpawnEnemy = false;
#if UNITY_EDITOR
                        Debug.Log("Bo oi! Thang nay chua duoc config hay la no ko co config");
#endif
                    }
                    
                }
                gridTiles.Add(uiTileGrid);

            }
        }
    }

    void Reset()
    {
        foreach (var gridTile in gridTiles)
        {
            gridTile.isUsed = false;
        }
    }

    public void SpawnGroupEnemy(Vector3 _enemyPos)
    {
        int cowboyLevel = 1;
        int cowboyEpiso = 2;
        if (GameController.Instance.player != null)
        {
            cowboyLevel = GameController.Instance.player.cowboyLevel;
            cowboyEpiso = GameController.Instance.player.cowboyEpisode;
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Chua co du lieu nguoi dung");
#endif
        }
        CowboyEpisode cowboyEpisode = BaseLoadData.cowboyEpisode.Find(x=>x.cowboyLevel == cowboyLevel
                                                                            && x.cowboyEpisode == cowboyEpiso);
        BaseObjectType baseObjectType;
        float random =(float) Random.Range(0, 100) / 100;
        float rand1 = 0;
        float rand2 = cowboyEpisode.enemyNormalPercent;
        float rand3 = cowboyEpisode.enemyNormalPercent + cowboyEpisode.enemyTankPercent;
        float rand4 = cowboyEpisode.enemyNormalPercent + cowboyEpisode.enemyTankPercent + cowboyEpisode.enemyFlashPercent;
        float rand5 = cowboyEpisode.enemyNormalPercent + cowboyEpisode.enemyTankPercent + cowboyEpisode.enemyFlashPercent + cowboyEpisode.enemyFlyPercent;
        float rand6 = cowboyEpisode.enemyNormalPercent + cowboyEpisode.enemyTankPercent + cowboyEpisode.enemyFlashPercent + cowboyEpisode.enemyFlyPercent + cowboyEpisode.enemyAntiDamgePhysicalPercent;
        float rand7 = cowboyEpisode.enemyNormalPercent + cowboyEpisode.enemyTankPercent + cowboyEpisode.enemyFlashPercent + cowboyEpisode.enemyFlyPercent + cowboyEpisode.enemyAntiDamgePhysicalPercent + cowboyEpisode.enemyBuffHealthPercent;
        Debug.Log("Rand 1: " + rand1);
        Debug.Log("Rand 2: " + rand2);
        Debug.Log("Rand 3: " + rand3);
        Debug.Log("Rand 4: " + rand4);
        Debug.Log("Rand 5: " + rand5);
        Debug.Log("Rand 6: " + rand6);
        Debug.Log("Rand 7: "+ rand7);
        if (random >= rand1 && random <= rand2)
        {
            baseObjectType = BaseObjectType.OBE_ENEMY_NORMAL;
        }
        else if (random > rand2 &&
            random <= rand3)
        {
            baseObjectType = BaseObjectType.OBE_ENEMY_TANKER;
        }
        else if (random > rand3 &&
            random <= rand4)
        {
            baseObjectType = BaseObjectType.OBE_ENEMY_FLASH;
        }
        else if (random > rand4 &&
            random <= rand5)
        {
            baseObjectType = BaseObjectType.OBE_ENEMY_FLY;
        }
        else if (random > rand5 &&
            random <= rand6)
        {
            baseObjectType = BaseObjectType.OBE_ENEMY_ANTIDAMAGE;
        }
        else if (random > rand6 &&
            random <= rand7)
        {
            baseObjectType = BaseObjectType.OBE_ENEMY_BUFFHEALTH;
        }
        else
        {
            Debug.Log("Random loi roi");
            baseObjectType = BaseObjectType.OBE_ENEMY_OTHER;
        }
        if (dicEnemyOfEpisode.ContainsKey(baseObjectType))
        {
            if (dicEnemyOfEpisode[baseObjectType] < cowboyEpisode.numberOfEnemy[baseObjectType])
            {
                //Spawn thang nay
                dicEnemyOfEpisode[baseObjectType] += 1;
                Debug.Log(baseObjectType + ": " + dicEnemyOfEpisode[baseObjectType]);
                CreateEnemy(_enemyPos, baseObjectType);
            } 
            else
            {
                //Chuyen sang thang khac
                baseObjectType = GetEnemyTypeNext(cowboyEpisode, baseObjectType);
                if (baseObjectType != BaseObjectType.OBE_ENEMY_OTHER)
                {
                    CreateEnemy(_enemyPos, baseObjectType);
                    if (dicEnemyOfEpisode.ContainsKey(baseObjectType))
                    {
                        dicEnemyOfEpisode[baseObjectType] += 1;
                    }
                    else
                    {
                        dicEnemyOfEpisode.Add(baseObjectType, 1);
                    }
                }
                else
                {
                    isFinished = true;
                }
            }
        }
        else
        {
            //baseObjectType = GetEnemyTypeNext(cowboyEpisode, baseObjectType);
            if (baseObjectType != BaseObjectType.OBE_ENEMY_OTHER)
            {
                if (cowboyEpisode.numberOfEnemy[baseObjectType] > 0)
                {
                    dicEnemyOfEpisode.Add(baseObjectType, 1);
                    CreateEnemy(_enemyPos, baseObjectType);
                }
                else
                {
                    baseObjectType = GetEnemyTypeNext(cowboyEpisode, baseObjectType);
                    if (baseObjectType != BaseObjectType.OBE_ENEMY_OTHER)
                    {
                        CreateEnemy(_enemyPos, baseObjectType);
                        if (dicEnemyOfEpisode.ContainsKey(baseObjectType))
                        {
                            dicEnemyOfEpisode[baseObjectType] += 1;
                        }
                        else
                        {
                            dicEnemyOfEpisode.Add(baseObjectType, 1);
                        }
                    }
                    else
                    {
                        isFinished = true;
                    }
                }
            }
        }
       
    }

    public void CreateEnemy(Vector3 _enemyPos, BaseObjectType _baseObjectType)
    {
        GameObject enemy;
        enemy = ManagerObject.Instance.SpawnEnemy(_baseObjectType, _enemyPos);
        if (enemy != null)
        {
            //PoolCustomize.Instance.GetBaseObject(enemyPrefab, newPos, "Enemy");
            BaseEnemyObject baseEnemy = enemy.GetComponent<BaseEnemyObject>();
            baseEnemy.ResetValueOfAvariable();
            baseEnemy.healthBegin += 50;
            enemyInScreen.Add(baseEnemy);
            groupEnemyCurrentIndex += 1;
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Chua co thang nay ba oi! Xin loi");
#endif
            
        }
    }

    public BaseObjectType GetEnemyTypeNext(CowboyEpisode _cowboyEpisode, BaseObjectType _baseObjectType)
    {
        //Neu a du so luong thi moi goi ham nay
        foreach (var enemy in _cowboyEpisode.numberOfEnemy)
        {

            if (enemy.Key != _baseObjectType && enemy.Value > 0)
            {
                if (dicEnemyOfEpisode.ContainsKey(enemy.Key))
                {
                    if (dicEnemyOfEpisode[enemy.Key] < enemy.Value)
                        return enemy.Key;
                }
                else
                    return enemy.Key;
            }
        }
        return BaseObjectType.OBE_ENEMY_OTHER;
    }
}

public class EnemySpawnTimeConfig
{

}