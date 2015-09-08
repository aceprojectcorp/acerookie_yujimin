using UnityEngine;
using System.Collections;

public class BtnScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () 
	{
		// 스크린 사이즈 저장
		if (GameData.Instance.nowScene == GameSceneState.title && GameData.Instance.screenHeight == 0f) 
		{
			GameObject UIRootObj = GameObject.Find ("UI Root");
			GameData.Instance.screenHeight = UIRootObj.GetComponent<UIRoot>().manualHeight ; 
			GameData.Instance.screenWidth = UIRootObj.GetComponent<UIRoot>().manualWidth ;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//***** Scene move !!! -----
	public void GoGameStartScene()
	{
		GameData.Instance.nowScene = GameSceneState.play;
		Application.LoadLevel ("GamePlay");	
	}
	public void GoTitleScene()
	{
		GameData.Instance.nowScene = GameSceneState.title;
		Application.LoadLevel ("Title");
	}
	public void GoGameResult()
	{
		GameData.Instance.nowScene = GameSceneState.result;
		Application.LoadLevel ("GameResult");
	}
}
