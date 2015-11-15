using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIScreenController : MonoBehaviour {

    public List<ScreenConfig> screenContains;
    private Dictionary<BaseScreenType, GameObject> dicScreenContains;

    public GameObject screenCurrent;

    void Start()
    {
        dicScreenContains = new Dictionary<BaseScreenType, GameObject>();
        ConvertToDictionary();
    }

    void ConvertToDictionary()
    {
        foreach (var screenConfig in screenContains)
        {
            dicScreenContains.Add(screenConfig.baseScreenType, screenConfig.screen);
        }
    }

    public void Show(BaseScreenType _baseScreenType)
    {
        if (screenCurrent != null)
        {
            screenCurrent.SetActive(false);
        }
        screenCurrent = GetScreenByType(_baseScreenType);
        if(screenCurrent != null)
            screenCurrent.SetActive(true);
    }

    public void ShowOnly(BaseScreenType _baseScreenType)
    {
        GameObject screen =  GetScreenByType(_baseScreenType);
        if (screen != null)
            screen.SetActive(true);
    }

    public void ShowPopup(BaseScreenType _baseScreenType)
    {
        GameObject screen = GetScreenByType(_baseScreenType);
        if (screen != null)
        {
            screen.transform.localScale = Vector3.zero;
            screen.SetActive(true);
            iTween.ScaleTo(screen, iTween.Hash(iT.ScaleTo.time, 0.6f,
                                               iT.ScaleTo.scale, Vector3.one,
                                               iT.ScaleTo.easetype, iTween.EaseType.easeInSine));
        }
    }

    public void Hide(BaseScreenType _baseScreenType)
    {
        screenCurrent = GetScreenByType(_baseScreenType);
        if (screenCurrent != null)
            screenCurrent.SetActive(true);
    }

    public GameObject screenPopup;
    public void HidePopup(BaseScreenType _baseScreenType)
    {
        GameObject screen = GetScreenByType(_baseScreenType);
        screenPopup = screen;
        if (screen != null)
        {
            iTween.ScaleTo(screen, iTween.Hash(iT.ScaleTo.time, 0.3f,
                                               iT.ScaleTo.scale, Vector3.zero,
                                               iT.ScaleTo.easetype, iTween.EaseType.easeInSine,
                                               iT.ScaleTo.oncomplete, "HidePopup"));
        }
    }

    void HidePopup()
    {
        if (screenPopup != null)
        {
            screenPopup.SetActive(false);
            screenPopup = null;
        }
    }

    public GameObject GetScreenByType(BaseScreenType _baseScreenType)
    {
        if (dicScreenContains.ContainsKey(_baseScreenType))
            return dicScreenContains[_baseScreenType];
#if UNIT_EDITOR
        Debug.log("Khong co man hinh nay ba oi");
#endif
        return null;
    }
}

[System.Serializable]
public class ScreenConfig
{
    public BaseScreenType baseScreenType;
    public GameObject screen;
}


public enum BaseScreenType
{
    BS_WORLD_MAP= 0,
    BS_MENU = 1,
    BS_GAME_PLAY = 2
}