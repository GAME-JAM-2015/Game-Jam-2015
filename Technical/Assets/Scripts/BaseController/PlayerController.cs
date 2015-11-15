using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public BaseGunType gunType;
    public float score;
    //public Transform scorePosition;
    public GunController gunController;
    public int cowboyLevel = 1;
    public int cowboyEpisode = 2;

    void Start()
    {
        
    }

    public void InitUserData()
    {
        gunType = BaseGunType.BG_SHORT;
        ChangeGun(gunType);
    }

    public void ChangeGun(BaseGunType _gunType)
    {
        gunType = _gunType;
        gunController.SetGun(_gunType);
    }

    public void GunShoot(Vector3 _mousePosition)
    {
        gunController.ShootSpawn(_mousePosition);
    }

    public bool IsAutoGun()
    {
        return gunController.IsAutoGun();
    }

    public void GunStop()
    {
        gunController.GunStop();
    }


}
