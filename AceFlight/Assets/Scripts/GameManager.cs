using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// [ GameManager 하는일 ] 
//  - 이동거리 계산 
//  - gamedata의 scDistFromStagePre에 빠른 참조를 위해 세로 인덱스 값인 idxCM를 이동거리에 따라 변경, 
//  - 이동 거리에 따라 게임속도 변경 ( gamedata의 scDistFromStagePre 참조 ) 
//  - gamePlayScene에 필요한 값들 초기화 
// 	- 초당 프레임수 초기화 
//	- 플레이어 죽음시 게임 스피드 정지 및 해당 변수들 초기화 및 처리 
public class GameManager : MonoBehaviour {
	
	float totalPixelMoved   = 0 ;		// 총 픽셀 이동거리(px) 
	int scDistFromStagePre  = 0 ; 		// 이전 이동 거리(m)
	int idxPreVal 		 	= 0 ; 		// 이전 인덱스값
	bool isPlayerDeadState	= false;	

	void Awake()
	{
		Application.targetFrameRate = GameData.Instance.g_framePerSec; //60;	// 초당 프레임수 고정 
	}

	void Start () 
	{
		GameData.Instance.g_scGoldFromStage = 0;
		GameData.Instance.g_scHitMonFromStage = 0;
		GameData.Instance.g_scDistFromStage = 0 ;
		GameData.Instance.g_idxMsiPower = 0; 
		GameData.Instance.g_idxCM = 0;  
		idxPreVal = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{ 
		if ( GameData.Instance.g_playerState == PlayerState.play ) 
		{
			// 이동 거리 계산 
			scDistFromStagePre = GameData.Instance.g_scDistFromStage;
			totalPixelMoved += ( GameData.Instance.g_nowGameSpeed * GameData.Instance.g_framePerSec ) * Time.deltaTime;  
			GameData.Instance.g_scDistFromStage = (int)( totalPixelMoved / GameData.Instance.g_pixelPerMeter );

			// 1m씩 증가할 때마다 거리 점수, 이동 거리에 해당하는 idxCM값 변경 여부 결정, 게임속도 변경 ----------------------------//
			if ( GameData.Instance.g_scDistFromStage != scDistFromStagePre ) 
			{
				scDistFromStagePre = GameData.Instance.g_scDistFromStage;
				// 이동 거리 구간에 따라 infoForChangeMeter를 참조할 세로 인덱스번호인 idxCM 구하고, idxCM값 변경 시 게임 속도 변경 
				if ( GameData.Instance.g_idxCM != GameData.Instance.g_infoForChangeMeter.GetLength (0) - 1 ) 
				{
					for ( int i=0; i < GameData.Instance.g_infoForChangeMeter.GetLength(0) ; i++ ) 
					{				
						if ( GameData.Instance.g_scDistFromStage < GameData.Instance.g_infoForChangeMeter [i][0] ) 
						{
							GameData.Instance.g_idxCM = i;
							break;
						}
						// 거리가 1000미터 이상일 경우, ( 테이블에서 마지막 줄에 해당 )  
						GameData.Instance.g_idxCM = GameData.Instance.g_infoForChangeMeter.GetLength (0) - 1;
					}
				
					// 인덱스값 변경시 게임 속도 변경  
					if ( idxPreVal != GameData.Instance.g_idxCM ) 
					{
						GameData.Instance.g_nowGameSpeed = 
						GameData.Instance.g_pixelPerFrame * 
							(GameData.Instance.g_infoForChangeMeter [GameData.Instance.g_idxCM] [1] * 1f) / 100;			 	
						idxPreVal = GameData.Instance.g_idxCM;
					}
				}
			}
			//--------------------------------------------------------------------------------------------------------//
		}
		else 
		{
			if( isPlayerDeadState == false )
			{
				isPlayerDeadState = true;
				GameData.Instance.g_nowGameSpeed = 0 ;
				Invoke( "DeadPlayer", 5f );
			}
		}

	}	 

	void DeadPlayer()
	{
		// 최고 점수인지 판단 후 처리 
		if (GameData.Instance.g_scDistFromStage + GameData.Instance.g_scHitMonFromStage > GameData.Instance.g_scBestHighTotal)
			GameData.Instance.g_scBestHighTotal = GameData.Instance.g_scDistFromStage + GameData.Instance.g_scHitMonFromStage;

		GameData.Instance.g_nowGameSpeed = GameData.Instance.g_pixelPerFrame ;
		GameData.Instance.g_playerState = PlayerState.play ;
		GameData.Instance.g_nowScene = GameSceneState.result; 
		Application.LoadLevel ("GameResult");
	}

}
