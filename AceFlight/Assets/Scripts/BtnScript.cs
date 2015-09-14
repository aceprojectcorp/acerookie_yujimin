using UnityEngine;
using System.Collections;

// << 버튼 클릭 처리, 현재 씬값 변경, 화면크기 저장  >> 
public class BtnScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () 
	{
		// 스크린 사이즈 저장
		if (GameData.Instance.g_nowScene == GameSceneState.title && GameData.Instance.g_screenHeight == 0f ) 
		{
			GameObject UIRootObj = GameObject.Find ("UI Root");
			GameData.Instance.g_screenHeight = UIRootObj.GetComponent<UIRoot>().manualHeight ; 
			GameData.Instance.g_screenWidth = UIRootObj.GetComponent<UIRoot>().manualWidth ;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//***** Scene move !!! -----
	public void GoGameStartScene()
	{
		GameData.Instance.g_nowGameSpeed = GameData.Instance.g_pixelPerFrame ;
		GameData.Instance.g_playerState = PlayerState.play ;
		GameData.Instance.g_nowScene = GameSceneState.play;
		Application.LoadLevel ("GamePlay");	
	}
	public void GoTitleScene()
	{
		GameData.Instance.g_nowGameSpeed = GameData.Instance.g_pixelPerFrame ;
		GameData.Instance.g_playerState = PlayerState.play ;
		GameData.Instance.g_nowScene = GameSceneState.title;
		Application.LoadLevel ("Title");
	}
	public void GoGameResult()
	{
		if (GameData.Instance.g_scDistFromStage + GameData.Instance.g_scHitMonFromStage > GameData.Instance.g_scBestHighTotal)
			GameData.Instance.g_scBestHighTotal = GameData.Instance.g_scDistFromStage + GameData.Instance.g_scHitMonFromStage;

		GameData.Instance.g_nowGameSpeed = GameData.Instance.g_pixelPerFrame ;
		GameData.Instance.g_playerState = PlayerState.play ;
		GameData.Instance.g_nowScene = GameSceneState.result;
		Application.LoadLevel ("GameResult");
	}
}
