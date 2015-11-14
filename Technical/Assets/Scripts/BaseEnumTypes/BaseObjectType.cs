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
    // particaL
    OBP_EXPLOSION_C_E_B = 10,
    OBP_EXPLOSION_GUN_SUPPORT = 11,
    OBP_ENEMY_DIE = 12,
    OBP_WALL_FALL = 13,
    OBP_GROUND_WOOD_HIT =14,
    OBP_BOM_ITEM_EXPLOSION,
    //UI
    OU_COIN = 20,
    OU_NUMBER = 21,
    OB_BOSS_TANKER= 30
}