using UnityEngine;
using System.Collections;

public class EnemyBigHp :BaseEnemyObject{
    public Transform hitPosition;

    public override void InitObject()
    {
        base.InitObject();
    }
    public override void KillPlayer()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        base.Move();
    }

    public override void ReceiveDamge(float damge)
    {
        if (armor != 0)
        {
            damge = (int)(damge - (armor * healthPoint) / 100);
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
        if (groundTarget != null)
        {
            if (groundTarget.healthPoint > 0)
            {
                groundTarget.ReceiveDamge(this.damge);

                if (groundTarget.healthPoint <= 0)
                {
                    groundTarget.DestroyObject();
                    //Destroy(wallTarget.gameObject);
                    groundTarget = null;
                    attacking = false;
                    stateMachine.ChangeState(BaseStateType.ES_RUN);
                }
            }
        }
        else
        {
            groundTarget = null;
            attacking = false;
            stateMachine.ChangeState(BaseStateType.ES_RUN);
        }
    }

    public override void DestroyObject()
    {
        base.DestroyObject();
        ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_ENEMY_DIE, transform.position);
        PoolCustomize.Instance.HideBaseObject(gameObject, "Enemy");
#if UNITY_EDITOR

#endif
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            //stateMachine.ChangeState(BaseStateType.ES_ATTACK);
            this.attacking = true;
            BaseWallObject baseWallObject = other.gameObject.GetComponent<BaseWallObject>();
            groundTarget = baseWallObject;
            //baseWallObject.ReceiveDamge(this.damge);
        }
        if (other.tag == "Diffuse")
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
        if (other.tag == "LimiteMap")
        {
            Debug.Log("game over !");
            GameController.Instance.screenController.ShowOnly(BaseScreenType.BS_GAME_OVER);
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
            groundTarget = null;
        }
    }
}
