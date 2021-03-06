﻿using UnityEngine;
using System.Collections;

public class EnemyWizard : BaseEnemyObject {
    public Transform hitPosition;

    public override void KillPlayer()
    {
        throw new System.NotImplementedException();
    }
    public override void InitObject()
    {
        StartCoroutine(WaitingAttack(4));
        base.InitObject();
    }
    float timer = 0;
    IEnumerator WaitingAttack(float timeWait)
    {
        while (timer < 100)
        {
            yield return new WaitForSeconds(timeWait);
            attacking = true;
            timeWait = Random.Range(timeWait,timeWait+2);
            timer++;
        }
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

    public void AdditionHP()
    {
        if(attacking)
        {
            attacking = false;
        }
        if(InitEnemyController.Instance.enemyInScreen != null)
        {
            foreach (BaseEnemyObject baseEnemy in InitEnemyController.Instance.enemyInScreen)
            {
                baseEnemy.healthPoint += 100;
                ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_ENEMY_ADDITION_HP,baseEnemy.transform.position);
#if UNITY_EDITOR
                Debug.Log("but HP");
#endif
            }
        }
    }

    public void GiveDamge()
    {
        if (groundTarget != null)
        {
            if (groundTarget.healthPoint > 0)
            {
                groundTarget.ReceiveDamge(this.damge);

                if (groundTarget.healthPoint <= 0)
                {
                    groundTarget.DestroyObject();
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
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            this.attacking = true;
            BaseWallObject baseWallObject = other.gameObject.GetComponent<BaseWallObject>();
            groundTarget = baseWallObject;
        }
        // dan ban lan
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
