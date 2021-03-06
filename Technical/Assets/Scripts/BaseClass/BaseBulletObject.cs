﻿using UnityEngine;
using System.Collections;

public abstract class BaseBulletObject : BaseMoveObject
{

    protected float damge; // Damge cua dan
    protected BaseBulletType bulletType; //Thong so chi loai dan
    protected BaseObjectType objectUseType; //Thong so chi loai dan nay do thang nao su dung
    protected float angleShoot; //Goc ban cua vien dan

    protected bool isStun; //Dan nay co stun
    protected bool isCritDamge; // Dan nay co crit damge hay ko
    protected float critDamge; //Damge tang them bao nhieu %

    //Range neu co
    public RangeOfBullet rangeOfBullet; //Range cua dan

    public bool IsStun
    {
        get { return isStun; }
        set { isStun = value; }
    }


    public bool IsCritDamge
    {
        get { return isCritDamge; }
        set { isCritDamge = value; }
    }


    public float CritDamge
    {
        get { return critDamge; }
        set { critDamge = value; }
    }

    public float AngelShoot
    {
        get { return angleShoot; }
        set { angleShoot = value; }
    }

    public float Damge
    {
        get { return damge; }
        set { damge = value; }
    }

    public BaseBulletType BulletType
    {
        get { return bulletType; }
        set { bulletType = value; }
    }

    public BaseObjectType ObjectUseType
    {
        get { return objectUseType; }
        set { objectUseType = value; }
    }

    public override void InitObject()
    {
        base.InitObject();
        gameObjectType = BaseObjectType.OB_GUN;
        positionBegin = transform.position;
        //direction = BaseDirectionType.UP;

        //isStun = true;      // test enemy stun
    }

    public override void Move()
    {
        vx += accelerationNormalX * Time.deltaTime;
        vy += accelerationNormalY * Time.deltaTime;

        switch (direction)
        {
            case BaseDirectionType.UP:
                positionBegin.y += vy * Time.deltaTime;
                break;
            case BaseDirectionType.LEFT:
                positionBegin.x -= vx * Time.deltaTime;
                break;
            case BaseDirectionType.RIGHT:
                positionBegin.x += vx * Time.deltaTime;
                break;
            case BaseDirectionType.DOWN:
                positionBegin.y -= vy * Time.deltaTime;
                break;
            case BaseDirectionType.LEFT_UP:
                positionBegin.x -= vx * Time.deltaTime;
                positionBegin.y += vy * Time.deltaTime;
                break;
            case BaseDirectionType.RIGHT_UP:
                positionBegin.x += vx * Time.deltaTime;
                positionBegin.y += vy * Time.deltaTime;
                break;
            case BaseDirectionType.LEFT_DOWN:
                positionBegin.x -= vx * Time.deltaTime;
                positionBegin.y -= vy * Time.deltaTime;
                break;
            case BaseDirectionType.RIGHT_DOWN:
                positionBegin.x += vx * Time.deltaTime;
                positionBegin.y -= vy * Time.deltaTime;
                break;
            case BaseDirectionType.NONE:
                break;
            default:
                break;
        }

        transform.position = positionBegin;
    }

    public override void UpdateObject()
    {
        base.UpdateObject();
        this.Move();
        DestroyObject();
        KillEnemies();
    }

    public abstract void KillEnemies();

    public override void DestroyObject()
    {
        //Dua bullet vao lai trong Pool
        if (positionBegin.y <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y ||
            positionBegin.y >= Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0.0f)).y ||
            positionBegin.x <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x ||
            positionBegin.x >= Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0.0f)).x
            )
        {
            PoolCustomize.Instance.HideBaseObject(gameObject, "Bullet");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "GroundBox")
        {
            PoolCustomize.Instance.HideBaseObject(gameObject, "Bullet");
        }
        else if (other.tag == "Enemy")
        {
           
            // spawn partical 
            ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_EXPLOSION_C_E_B, transform.position);
            // bullet crit damge
            BaseEnemyObject baseEnemy = other.gameObject.GetComponent<BaseEnemyObject>();
            if (bulletType != BaseBulletType.BL_FLY && baseEnemy.isFling == true)
            {
                return;
            }
            // bullet
            switch(bulletType)
            {
                case BaseBulletType.BL_SLOW:
                    baseEnemy.effectRenderer.AddStatModifier(BaseStatModifierType.BSM_SLOW, 3f, 0.5f);
                    //ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_ENEMY_HYPNOSIS, other.transform.position);
                    baseEnemy.SetColor(new Color(0, 0.5f, 0));
                    break;
                case BaseBulletType.BL_HYPNOSIS:
                    baseEnemy.effectRenderer.AddStatModifier(BaseStatModifierType.BSM_HYPNOSIS, 1.5f, 0);
                    ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_ENEMY_HYPNOSIS, transform.position);
                    baseEnemy.direction = BaseDirectionType.UP;
                    break;
                case BaseBulletType.BL_ARMOR:
                    if(bulletType == BaseBulletType.BL_ARMOR  && baseEnemy.armor !=0)
                    {
                        baseEnemy.effectRenderer.AddStatModifier(BaseStatModifierType.BSM_ARMOR, 2.0f, 10);
                        baseEnemy.armor = 0;
                        baseEnemy.ReceiveDamge(Damge);
                    }
                    break;
                case BaseBulletType.BL_FLY:
                    if (baseEnemy.isFling)
                    {
                        baseEnemy.isFling = false;
                        baseEnemy.objUmbrella.SetActive(false);
                        baseEnemy.objShowHP.SetActive(true);
                    }
                    break;
                case BaseBulletType.BL_STUN:
                    isStun = true;
                    break;
                default:
                    //baseEnemy.ReceiveDamge(damge);
                    break;
            }

            if (baseEnemy.healthPoint > 0)
            {
                if (IsCritDamge)
                {
                    baseEnemy.ReceiveDamge(damge + (int)(damge * CritDamge));
                }
                else 
                {
                    if (baseEnemy.isFling == false && bulletType != BaseBulletType.BL_ARMOR && bulletType != BaseBulletType.BL_HYPNOSIS && bulletType != BaseBulletType.BL_STUN && bulletType != BaseBulletType.BL_SLOW)
                    {
                        baseEnemy.ReceiveDamge(damge);
                    }
                }
                PoolCustomize.Instance.HideBaseObject(gameObject, "Bullet");
            }
            if (isStun)
            {
                baseEnemy.effectRenderer.AddStatModifier(BaseStatModifierType.BSM_STUN, 2f, 0.0f);
                ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_ENEMY_STUN, other.transform.position);    // partical stun
                baseEnemy.SetColor(new Color(1.0f, 0.5f, 0));
#if UNITY_EDITOR
#endif
            }
            else
            {
                baseEnemy.SetColor(new Color(0.8f, 0.2f, 0.2f, 1f));
                baseEnemy.ResetColor(0.5f);
            }
        }
        else if (other.tag == "Boss")
        {
            // instance partical
            ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_EXPLOSION_C_E_B, transform.position);
            //tru mau
            BaseBossObject baseBoss = other.gameObject.GetComponent<BaseBossObject>();
            if (baseBoss.healthPoint > 0)
            {
                baseBoss.ReceiveDamge(this.damge);
                PoolCustomize.Instance.HideBaseObject(gameObject, "Bullet");
            }

        }
    }
}