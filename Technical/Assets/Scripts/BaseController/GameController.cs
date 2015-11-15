using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoSingleton<GameController> {

    public ControlGun playerGun;
    public PlayerController player;
    public UIMapController mapController;
    public List<Sprite> listSpriteNumber;
    public UIScore scoreView;
    //
    public int cowboyLevel; //Day la level hien tai cua game
    public int cowboyEpisode; //Day la mot state trong level
	// Update is called once per frame
    public bool isUnlockLevel;
    //
    public UIScreenController screenController;

    void GameInit()
    {
        //scoreView.UpdateScoreView(player.score); //Chay khi load man choi
        isUnlockLevel = false;
        //Load len du lieu nguoi dung
        //player.InitUserData(); 
        //Chuyen sang scene word map
        screenController.Show(BaseScreenType.BS_WORLD_MAP);
        mapController.UpdateMap();
    }

    void GameUpdate()
    {

    }

    void GameLose()
    {

    }

    void GameWin()
    {

    }

    void Awake()
    {
        BaseLoadData.LoadData();
    }

	void Start () {
        //AudioManager.Instance.Play(BaseAudioType.BA_WORKD_AUDIO, true);
        //scoreView.UpdateScoreView(player.score);
        GameInit();
	}

    void Update()
    {

    }

    public void UpdatePlayerScore(float _score)
    {
        player.score += _score;
        scoreView.UpdateScoreView(_score);
    }
}
