using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState { title, play, result, loading }

public class GameManager : MonoBehaviour {

//	public static GameManager gamePlayManager ; 
	public GameState nowGameState = GameState.title;	// 게임 상황 판단 	// 이거 변경되게 만들기 

	float bgSpeed = 0f;									// 배경 이동 속도 
	Vector3 bgPos = new Vector3( 0, 0, 0 );				// 배경 최초 위치  
	Vector3 posZero = new Vector3( 0, 0, 0 );
	Vector3 posTest = new Vector3( 0, 0, 0 );


	public UISprite background1;						// 배경. 이동테스트 
	public UISprite background2;

//	public UILabel totalScore;
//	public UILabel hitScore;
//	public UILabel goldScore ;

	void Awake()
	{
		GameData.Instance.gamePlayManager = this;
	}

	void OnDestory()
	{
		GameData.Instance.gamePlayManager = null;
	}

	void Start () 
	{
//		Debug.Log (nowGameState);
		background1.gameObject.SetActive (true);
		background2.gameObject.SetActive (true);
		posTest.y += background2.localSize.y;
//		Vector3 posTmp = Background2.transform.localPosition;//transform.position;
//		posTmp.y += 960f;//Background2.localSize.y;
//		Background2.transform.position = posTmp ;
//		Background2.transform.localPosition = posTest;
//		Background2.transform.position = posTest;

		background2.transform.localPosition = posZero;
		background2.transform.localPosition = posTest;

//		Debug.Log ( posTest );
//		Debug.Log ( Background1.transform.position );
//		Debug.Log ( Background1.transform.localPosition );
//		Debug.Log ( Background1.localSize.y );
	}
	
	// Update is called once per frame
	void Update () 
	{

		if ( nowGameState == GameState.title ) 
		{
			bgMove( background1 );
			bgMove( background2 );

//			staticGameData.distFromPreStage++;
//			totalScore.text = (staticGameData.distFromPreStage + staticGameData.hitScoreFromPreStage).ToString();
//			hitScore.text = staticGameData.hitScoreFromPreStage.ToString();
//			goldScore.text = staticGameData.getGoldFromPreStage.ToString();

//			Debug.Log ( "Background1.transform.localScale : " 	+ Background1.transform.localScale );
//			Debug.Log ( "Background1.transform.lossyScale : " 	+ Background1.transform.lossyScale );
//			Debug.Log ( "Background1.transform.position" 		+ Background1.transform.position );
//			Debug.Log ( "Background1.transform.localPosition : " + Background1.transform.localPosition );
//			Debug.Log ( "!!!!!!!!!!!!" );
		}
		else if( nowGameState == GameState.play )
		{
//			totalScore.text = staticGameData.getGoldFromPreStage.ToString();
//			hitScore.text = staticGameData.getGoldFromPreStage.ToString();
//			goldScore.text = staticGameData.getGoldFromPreStage.ToString();

		}
	}

	void bgMove( UISprite bg )
	{
//		Debug.Log ( "bg.pixelSize " + bg.pixelSize );
//		Debug.Log ( "bg.transform.localScale : " + bg.transform.localScale );
//		Debug.Log ( "bg.transform.lossyScale : " + bg.transform.lossyScale );
//		Debug.Log ( "bg.localSize" + bg.localSize );
//		Debug.Log ( "bg.transform.position" + bg.transform.position );
//		Debug.Log ( "bg.transform.localPosition : " + bg.transform.localPosition );
//		Debug.Log ( "!!!!!!!!!!!!" );
		bgSpeed = 0.01f;

		if ( -(bg.localSize.y) >= bg.transform.localPosition.y ) 
		{ 	
			bgPos.y = bg.localSize.y - (bgSpeed*1000f);
			bg.transform.localPosition = bgPos ;
		} 
		else 
		{
			bgPos = bg.transform.position;
			bgPos.y -= bgSpeed;
			bg.transform.position = bgPos;
		}



	}





	//***** Scene move !!! -----
	public void GoGameStartScene()
	{
		nowGameState = GameState.play;
		Application.LoadLevel ("GamePlay");

	}
	public void GoTitleScene()
	{
		nowGameState = GameState.title;
		Application.LoadLevel ("Title");
	}
	public void GoGameResult()
	{
		nowGameState = GameState.result;
		Application.LoadLevel ("GameResult");
	}

	//*****-----


}
