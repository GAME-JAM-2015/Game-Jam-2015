using UnityEngine;
using System.Collections;

public class TankerEnemy : BaseEnemyObject {

    // enemy fly
    public Transform hitPosition;
    

    //public int armor = 10;

    public override void InitObject()
    {
        
        base.InitObject();
    }
    public override void KillPlayer()
    {
        throw new System.NotImplementedException();
    }
    public override void UpdateObject()
    {
        if (effectRenderer.Contains(BaseStatModifierType.BSM_ARMOR))
        {
            if (effectRenderer.RemoveStatModifier(BaseStatModifierType.BSM_ARMOR))
            {
                armor = 10;
               // Debug.Log(armor);
            }
        }
        if(effectRenderer.Contains(BaseStatModifierType.BSM_HYPNOSIS))
        {
            if(effectRenderer.RemoveStatModifier(BaseStatModifierType.BSM_HYPNOSIS))
            {
                direction = BaseDirectionType.DOWN;
            }
        }
        base.UpdateObject();
    }
    public override void Move()
    {
        vx += accelerationNormalX * Time.deltaTime;
        vy += accelerationNormalY * Time.deltaTime;
        //
        if (effectRenderer.Contains(BaseStatModifierType.BSM_SLOW))
        {
            if (!effectRenderer.RemoveStatModifier(BaseStatModifierType.BSM_SLOW))
            {
                switch (direction)
                {
                    case BaseDirectionType.UP:
                        positionBegin.y += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.LEFT:
                        positionBegin.x -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vx * Time.deltaTime;
                        break;
                    case BaseDirectionType.RIGHT:
                        positionBegin.x += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vx * Time.deltaTime;
                        break;
                    case BaseDirectionType.DOWN:
                        //Debug.Log(effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW));
                        positionBegin.y -= (effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vy) * Time.deltaTime;
                        break;
                    case BaseDirectionType.LEFT_UP:
                        positionBegin.x -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vx * Time.deltaTime;
                        positionBegin.y += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.RIGHT_UP:
                        positionBegin.x += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vx * Time.deltaTime;
                        positionBegin.y += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.LEFT_DOWN:
                        positionBegin.x -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vx * Time.deltaTime;
                        positionBegin.y -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.RIGHT_DOWN:
                        positionBegin.x += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vx * Time.deltaTime;
                        
                        positionBegin.y -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.NONE:
                        break;
                    default:
                        break;
                }
                //vx *= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW);
                //vy *= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW);
                //Remove khi het thoi gian
            }
            else
            {
                this.SetColor(new Color(255, 255, 255));
            }
            //heffectRenderer.RemoveStatModifier(BaseStatModifierType.BSM_SLOW);
        }
        else if (effectRenderer.Contains(BaseStatModifierType.BSM_STUN))
        {
            if (!effectRenderer.RemoveStatModifier(BaseStatModifierType.BSM_STUN))
            {
                switch (direction)
                {
                    case BaseDirectionType.UP:
                        positionBegin.y += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_STUN) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.LEFT:
                        positionBegin.x -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_STUN) * vx * Time.deltaTime;
                        break;
                    case BaseDirectionType.RIGHT:
                        positionBegin.x += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_STUN) * vx * Time.deltaTime;
                        break;
                    case BaseDirectionType.DOWN:
                        positionBegin.y -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_STUN) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.LEFT_UP:
                        positionBegin.x -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_STUN) * vx * Time.deltaTime;
                        positionBegin.y += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_STUN) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.RIGHT_UP:
                        positionBegin.x += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_STUN) * vx * Time.deltaTime;
                        positionBegin.y += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_STUN) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.LEFT_DOWN:
                        positionBegin.x -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_STUN) * vx * Time.deltaTime;
                        positionBegin.y -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_STUN) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.RIGHT_DOWN:
                        positionBegin.x += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_STUN) * vx * Time.deltaTime;
                        positionBegin.y -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_STUN) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.NONE:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Debug.Log("enemy bi huy stun!");
                this.SetColor(new Color(255, 255, 255));
            }
        }
        else
        {
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
        }
        transform.position = positionBegin;
    }

    public override void ReceiveDamge(float damge)
    {
        if (armor != 0)
        {
            damge = (int)(damge - (armor*healthPoint)/100);
        }
        this.healthPoint -= damge;
        this.hpView.UpdateHealthPoint(healthPoint / healthBegin);
        try
        {
            //
            GameObject numberObj = ManagerObject.Instance.SpawnObject(BaseObjectType.OU_NUMBER, hitPosition.position, "Item");
            Number number = numberObj.GetComponent<Number>();
            number.Reset();
            number.SetValue(damge.ToString());
        }
        catch
        {

        }
    }

    public void AllowAttack()
    {
        isIdle = false;
        if (isAlive)
        {
            stateMachine.ChangeState(BaseStateType.ES_ATTACK);
        }
    }

    public void AllowIdle()
    {
        isAttack = false;
        if (isAlive)
        {
            stateMachine.ChangeState(BaseStateType.ES_IDLE);
        }
    }

    public override void Attack()
    {
        if (healthPoint <= 0)
        {
            stateMachine.ChangeState(BaseStateType.ES_DIE);
        }
        else
        {
            if (!isAttack)
            {
                isAttack = true;
                Invoke("AllowIdle", timeAttack);
            }
        }
    }

    public override void Idle()
    {
        if (healthPoint <= 0)
        {
            stateMachine.ChangeState(BaseStateType.ES_DIE);
        }
        else
        {
            if (attacking)
            {
                if (!isIdle)
                {
                    isIdle = true;
                    Invoke("AllowAttack", timeWaitAttack);
                }
            }
            else
            {
                stateMachine.ChangeState(BaseStateType.ES_RUN);
            }
        }
    }

    public override void Die()
    {
        if (isAlive)
        {
            isAlive = false;
            Invoke("DestroyObject", 0.2f);
        }
    }

    // RUN
    public override void Run()
    {
        if (healthPoint <= 0)
        {
            stateMachine.ChangeState(BaseStateType.ES_DIE);
        }
        else
        {
            if (attacking)
            {
                stateMachine.ChangeState(BaseStateType.ES_IDLE);
            }
            else
                Move();
        }
    }

    public void GiveDamge()
    {
        Debug.Log("Cho damge");
        if (wallTarget != null)
        {
            if (wallTarget.healthPoint > 0)
            {
                wallTarget.ReceiveDamge(this.damge);

                if (wallTarget.healthPoint <= 0)
                {
                    wallTarget.DestroyObject();
                    //Destroy(wallTarget.gameObject);
                    wallTarget = null;
                    attacking = false;
                    stateMachine.ChangeState(BaseStateType.ES_RUN);
                }
            }
        }
        else
        {
            wallTarget = null;
            attacking = false;
            stateMachine.ChangeState(BaseStateType.ES_RUN);
        }
    }

    public override void DestroyObject()
    {
        base.DestroyObject();
        ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_ENEMY_DIE, transform.position);
        PoolCustomize.Instance.HideBaseObject(gameObject, "Enemy");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            //stateMachine.ChangeState(BaseStateType.ES_ATTACK);
            this.attacking = true;
            BaseWallObject baseWallObject = other.gameObject.GetComponent<BaseWallObject>();
            wallTarget = baseWallObject;
            //baseWallObject.ReceiveDamge(this.damge);
        }
        if(other.gameObject.tag == "Bom")
        {
            PoolCustomize.Instance.HideBaseObject(other.gameObject, "Item", 0.3f);
            float timeRandom = Random.Range(0.2f, 0.8f);
            Invoke("InstantialeParticalExplosion", timeRandom);
            Item1 itemBom = other.gameObject.GetComponent<Item1>();
            ReceiveDamge(itemBom.damge);
            //Debug.Log(timeRandom);
        }
        //if(other.gameObject.tag == "Bullet")
        //{
        //    BaseBulletObject baseBullet = other.gameObject.GetComponent<BaseBulletObject>();
        //    switch(baseBullet.BulletType)
        //    {
        //        case BaseBulletType.BL_HYPNOSIS:
        //            ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_ENEMY_HYPNOSIS, transform.position);
        //            direction = BaseDirectionType.UP;
        //            Invoke("AllowHypnosis", timeAllowhypnosis);
        //            break;
        //        case BaseBulletType.BL_FLY:
        //            objUmbrella.SetActive(false);
        //            objShowHP.SetActive(true);
        //            break;
        //        case BaseBulletType.BL_ARMOR:
        //            //Debug.Log(armor);
        //            if(gameObjectType == BaseObjectType.OBE_ENEMY_ARMOR && armor !=0)
        //            {
        //                effectRenderer.AddStatModifier(BaseStatModifierType.BSM_ARMOR, 2.0f, 10);
        //                armor = 0;
        //                //Debug.Log(armor);
        //                ReceiveDamge(baseBullet.Damge);
        //            }
        //            break;
        //        default:

        //            break;
        //    }
        //}
        if(other.tag == "Diffuse")
        {
            PoolCustomize.Instance.HideBaseObject(other.gameObject, "Item", 0.3f); 
            float timeRandom = Random.Range(0.2f, 0.8f);
            Invoke("InstantialeParticalExplosion", timeRandom); // Instantiale partical
            BaseBulletObject basebullet = other.transform.parent.GetComponent<BaseBulletObject>();
            if (basebullet != null)
            {
                ReceiveDamge(basebullet.Damge);
            }
        }
    }
    public void AllowHypnosis()
    {
        direction = BaseDirectionType.DOWN;
    }
    public void InstantialeParticalExplosion()
    {
        //AudioSource.PlayClipAtPoint(AudioManager.Instance.GetSoundByType(BaseAudioType.BA_ENEMY_BOM_ITEM), transform.position);           // viet note
        ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_BOM_ITEM_EXPLOSION, transform.position);
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            stateMachine.ChangeState(BaseStateType.ES_RUN);
            this.attacking = false;
            wallTarget = null;
            //baseWallObject.ReceiveDamge(this.damge);
        }
    }
}
