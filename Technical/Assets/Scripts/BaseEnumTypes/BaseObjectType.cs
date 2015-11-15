using UnityEngine;
using System.Collections;

public enum BaseObjectType
{
    OB_GUN = 0,
    OB_ENEMY = 1,
    OB_WALL = 2,
    OB_BULLET = 3,
    OB_ITEM = 4,
    OB_OTHER = 5,
    // enemy 10
    OBE_ENEMY_NORMAL = 1001,
    OBE_ENEMY_TANKER = 1002,
    OBE_ENEMY_FLASH = 1003,
    OBE_ENEMY_FLY = 1004,
    OBE_ENEMY_ANTIDAMAGE = 1005,
    OBE_ENEMY_BUFFHEALTH = 1006,
    OBE_ENEMY_OTHER = 1007,
    //
    OBB_BOM_ITEM = 8,
    // particaL 15
    OBP_EXPLOSION_C_E_B = 1500,
    OBP_EXPLOSION_GUN_SUPPORT = 1501,
    OBP_ENEMY_DIE = 1502,
    OBP_WALL_FALL = 1503,
    OBP_GROUND_WOOD_HIT =1504,
    OBP_BOM_ITEM_EXPLOSION =  1505,
    OBP_ENEMY_HYPNOSIS = 1506,
    OBP_ENEMY_STUN = 1507,
    //UI
    OU_COIN = 20,
    OU_NUMBER = 21,
    OB_BOSS_TANKER= 30
}