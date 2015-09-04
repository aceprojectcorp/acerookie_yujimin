using UnityEngine;
using System.Collections;

public class BtnScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//***** Scene move !!! -----
	public void GoGameStartScene()
	{
		GameData.nowScene = GameSceneState.play;
		Application.LoadLevel ("GamePlay");
		
	}
	public void GoTitleScene()
	{
		GameData.nowScene = GameSceneState.title;
		Application.LoadLevel ("Title");
	}
	public void GoGameResult()
	{
		GameData.nowScene = GameSceneState.result;
		Application.LoadLevel ("GameResult");
	}
}
