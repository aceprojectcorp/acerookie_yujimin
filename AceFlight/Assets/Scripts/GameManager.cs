using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState { title, play, result, loading }

public class GameManager : MonoBehaviour {


	public GameState nowGameState = GameState.title;	// 게임 상황 판단 	// 이거 변경되게 만들기 
	
	void Start () 
	{
		Debug.Log ("nowScene : " + GameData.nowScene);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void bgMove( UISprite bg )
	{

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

	//*****-----


}
