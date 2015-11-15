using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMapController : MonoBehaviour {

    public List<UIMapChild> mapChilds;
    public int cowboyLevel;
    public void UpdateMap()
    {
        if (GameController.Instance.player != null && mapChilds != null)
        {
            int cowboyLevel = GameController.Instance.player.cowboyLevel;
            for (int i = 0; i < cowboyLevel; ++i)
            {
                UnlockLevel(i);
            }
        }
    }

    public void UnlockLevel(int _cowboyLevel)
    {
        if (mapChilds != null)
        {
            if (mapChilds.Count > _cowboyLevel)
            {
                mapChilds[_cowboyLevel].UnLockLevel();
            }
        }
    }
}
