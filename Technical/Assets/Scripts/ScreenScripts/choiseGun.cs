using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class choiseGun : MonoBehaviour {
    public Image foreground;
    public Image foreground_Gun;
    public RectTransform rectTranform;
	// Use this for initialization
	void Start () {
        //StartCoroutine();
	}
    public void Move()
    {
        
    }
	public void OnClickBtnGun()
    {
        foreground.gameObject.SetActive(false);
        foreground_Gun.gameObject.SetActive(true);
    }
	// Update is called once per frame
	void Update () {
        
	}
}
