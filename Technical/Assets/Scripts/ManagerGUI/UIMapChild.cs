using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMapChild : MonoBehaviour {

    public GameObject imageLock;
    public bool isLock;
    public int cowboyLevel;
    public CanvasGroup canvasGroup;


    public void UnLockLevel()
    {
        if (isLock)
        {
            isLock = false;
            imageLock.SetActive(false);
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void LevelEventClick()
    {
        Debug.Log("Dang mo menu");
        GameController.Instance.screenController.ShowPopup(BaseScreenType.BS_MENU);
        GameController.Instance.mapController.cowboyLevel = cowboyLevel;
        //GameController.Instance.cowboyLevel = cowboyLevel;
    }

}
