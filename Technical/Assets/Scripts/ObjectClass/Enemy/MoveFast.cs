using UnityEngine;
using System.Collections;

public class MoveFast : BaseEnemyObject {
    public override void InitObject()
    {
        base.InitObject();
    }
    public override void Attack()
    {
        throw new System.NotImplementedException();
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
                        positionBegin.y -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vy * Time.deltaTime;
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
    public override void Run()
    {
        throw new System.NotImplementedException();
    }
    public override void UpdateObject()
    {
        base.UpdateObject();
    }
    public override void Idle()
    {
        throw new System.NotImplementedException();
    }
    public override void KillPlayer()
    {
        throw new System.NotImplementedException();
    }
    public override void Die()
    {
        throw new System.NotImplementedException();
    }
    public override void ReceiveDamge(float damge)
    {
        throw new System.NotImplementedException();
    }
}
