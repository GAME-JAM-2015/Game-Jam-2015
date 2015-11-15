using UnityEngine;
using System.Collections;

public class UIMenuController : MonoBehaviour {

    public void playEventClick()
    {
        GameController.Instance.screenController.HidePopup(BaseScreenType.BS_MENU);
        GameController.Instance.cowboyLevel = GameController.Instance.mapController.cowboyLevel;
        GameController.Instance.screenController.Show(BaseScreenType.BS_GAME_PLAY);

        AudioManager.Instance.PlayOneShot(BaseAudioType.BA_BUTTON_CLICK);
    }

    public void closeEventClick()
    {
        GameController.Instance.screenController.HidePopup(BaseScreenType.BS_MENU);
        AudioManager.Instance.PlayOneShot(BaseAudioType.BA_BUTTON_CLICK);
    }
}
