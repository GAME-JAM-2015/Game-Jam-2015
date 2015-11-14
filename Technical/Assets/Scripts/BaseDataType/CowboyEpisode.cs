using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CowboyEpisode 
{
    public Dictionary<BaseObjectType, int> numberOfEnemy;
    public int cowboyLevel;
    public int cowboyEpisode;
    public int enemyNormal;
    public int enemyTank;
    public int enemyFlash;
    public int enemyFly;
    public int enemyAntiDamgePhysical;
    public int enemyBuffHealth;
    //
    public float enemyNormalPercent;
    public float enemyTankPercent;
    public float enemyFlashPercent;
    public float enemyFlyPercent;
    public float enemyAntiDamgePhysicalPercent;
    public float enemyBuffHealthPercent;
    //
    public void CalEnemyAppearPercent()
    {
        numberOfEnemy = new Dictionary<BaseObjectType, int>();
        numberOfEnemy.Add(BaseObjectType.OBE_ENEMY_NORMAL, enemyNormal);
        numberOfEnemy.Add(BaseObjectType.OBE_ENEMY_TANKER, enemyTank);
        numberOfEnemy.Add(BaseObjectType.OBE_ENEMY_FLASH, enemyFlash);
        numberOfEnemy.Add(BaseObjectType.OBE_ENEMY_FLY, enemyFly);
        numberOfEnemy.Add(BaseObjectType.OBE_ENEMY_ANTIDAMAGE, enemyAntiDamgePhysical);
        numberOfEnemy.Add(BaseObjectType.OBE_ENEMY_BUFFHEALTH, enemyBuffHealth);
        enemyNormalPercent = BaseUtilExtentions.Instance.RoundToFloat((float)enemyNormal / (enemyNormal + enemyTank + enemyFlash + enemyFly + enemyAntiDamgePhysical + enemyBuffHealth), 1);
        enemyTankPercent = BaseUtilExtentions.Instance.RoundToFloat((float)enemyTank / (enemyNormal + enemyTank + enemyFlash + enemyFly + enemyAntiDamgePhysical + enemyBuffHealth), 1);
        enemyFlashPercent = BaseUtilExtentions.Instance.RoundToFloat((float)enemyFlash / (enemyNormal + enemyTank + enemyFlash + enemyFly + enemyAntiDamgePhysical + enemyBuffHealth), 1);
        enemyFlyPercent = BaseUtilExtentions.Instance.RoundToFloat((float)enemyFly / (enemyNormal + enemyTank + enemyFlash + enemyFly + enemyAntiDamgePhysical + enemyBuffHealth), 1);
        enemyAntiDamgePhysicalPercent = BaseUtilExtentions.Instance.RoundToFloat((float)enemyAntiDamgePhysical / (enemyNormal + enemyTank + enemyFlash + enemyFly + enemyAntiDamgePhysical + enemyBuffHealth), 1);
        enemyBuffHealthPercent = BaseUtilExtentions.Instance.RoundToFloat((float)enemyBuffHealth / (enemyNormal + enemyTank + enemyFlash + enemyFly + enemyAntiDamgePhysical + enemyBuffHealth), 1);
    }
}
