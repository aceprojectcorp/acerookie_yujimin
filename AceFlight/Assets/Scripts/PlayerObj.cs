﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 플레이어 객체에 관련된 스크립트 - 이동, 초기화 
public class PlayerObj : MonoBehaviour {

	Vector3 playerPos 		= new Vector3(0,0,0);	// 플레이어 초기 위치 셋팅 
	Vector3 clickPrePos 	= new Vector3(0,0,0);	// 바로 이전프레임의 클릭 위치 
	Vector3 clickNowPos 	= new Vector3(0,0,0);	// 현재 클릭 위치  
	Vector3 changePlayerPos = new Vector3(0,0,0);	// 플레이어 위치의 최종 이동값 저장 

	bool deadAlphaEffectSw  = false; 

	void Awake()
	{
		GameData.Instance.nowScene = GameSceneState.play;		// test
	}
	void Start () 
	{

		// 플레이어 이미지 랜덤으로 변경 
		int randIdx = Random.Range(0, GameData.Instance.playerSprNames.GetLength(0) );
		ChangePlayerSpr( "Body", 	randIdx, 0 );
		ChangePlayerSpr( "WingL", 	randIdx, 1 ); 
		ChangePlayerSpr( "WingR",	randIdx, 1 );

		// 플레이어 초기 세로 위치를 하단 1/4에 위치 시키기.  ----- // 0, -240 위치에 플레이어 캐릭터 등장 
		playerPos = gameObject.transform.localPosition; 
		playerPos.y -= ( GameData.Instance.screenHeight / 4f); 			// 플레이어 캐릭터 위치가 0,0 에서 시작하기 때문에 화면 가로크기 1/4만큼 빼줌 
		gameObject.transform.localPosition = playerPos; //-----//
		//		Debug.Log ( gameObject.transform.localPosition);	// localPosition은 -240으로 표현됨 
		//		Debug.Log( gameObject.transform.position); 			// position은 -0.5 로 표현됨 
	}

	void Update ()  
	{
		if (GameData.Instance.playerState == PlayerState.play) 
		{
			// ---- 캐릭터 이동 ----------------------------------------------------//
			// 가로 55~850 위치의 터치 받음. 마우스 입력 위치는 좌측 하단부터 0,0 시작. 전부 양수 
			if (Input.mousePosition.y < GameData.Instance.screenHeight &&
				Input.mousePosition.y > 55f) {
				if (Input.GetMouseButtonDown (0) == true) {
					clickPrePos = clickNowPos = Input.mousePosition;
				}
			// 바로 이전 프레임 위치값과 현재 위치값의 차이를 이용해서 캐릭터 x좌표 이동 
			else if (Input.GetMouseButton (0) == true) {	
					clickPrePos = clickNowPos;
					clickNowPos = Input.mousePosition;
				
					changePlayerPos = gameObject.transform.localPosition;
					changePlayerPos.x += clickNowPos.x - Mathf.Abs (clickPrePos.x);
				
					// 변경될 x위치 값이 -300 ~ 300 사이일 경우에만 위치 변경 
					// 300은 전체 화면 가로크기 640/2를 한 후 캐릭터 크기의 반만큼 빼서 구했음. 
					if (changePlayerPos.x <= GameData.Instance.screenWidth / 2f - 20 &&
						changePlayerPos.x >= -(GameData.Instance.screenWidth / 2f - 20)) {	
						gameObject.transform.localPosition = changePlayerPos;
					}
				}
			}//---------------------------------------------------------//
		}
		else
		{
			if( deadAlphaEffectSw == true )
			{
				// secPlayerDeadEffect == 1 
				transform.FindChild("Body").GetComponent<UISprite>().alpha -= 1 * (Time.deltaTime/2); ;
				transform.FindChild("WingL").GetComponent<UISprite>().alpha -= 1 * (Time.deltaTime/2); ;
				transform.FindChild("WingR").GetComponent<UISprite>().alpha -= 1 * (Time.deltaTime/2); ;
//				gameObject.GetComponent<UIPanel>().alpha -= 1 * (Time.deltaTime/2); ;
			}
		}

	}

	// 플레이어 이미지 변경 
	void ChangePlayerSpr (string sprName, int indexI, int indexJ)
	{
		transform.FindChild (sprName).GetComponent<UISprite> ().spriteName
			= GameData.Instance.playerSprNames [indexI, indexJ];		
		transform.FindChild (sprName).GetComponent<UISprite> ().MakePixelPerfect ();
		transform.FindChild (sprName).transform.localScale = new Vector3 (1f, 1f, 0.5f);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Monster") 
		{
			Destroy( transform.GetComponent<BoxCollider>() ); 
			GameData.Instance.playerState = PlayerState.dead ;
			transform.FindChild("Missiles").parent = GameObject.Find("UI Root").transform;
			Destroy( transform.FindChild("WingL").GetComponent<TweenRotation>() );
			Destroy( transform.FindChild("WingR").GetComponent<TweenRotation>() );

			PlayerActiveOff();
			Invoke("CancelInvokeAndNextEffectOn", 3f);
		}
	}

	void PlayerActiveOn()
	{
		gameObject.SetActive ( true );
		Invoke ( "PlayerActiveOff", 0.2f );
	}

	void PlayerActiveOff()
	{
		gameObject.SetActive ( false );
		Invoke ( "PlayerActiveOn", 0.2f );
	}

	void CancelInvokeAndNextEffectOn()
	{
		CancelInvoke();
		gameObject.SetActive ( true );
		deadAlphaEffectSw = true; 
	}
}
