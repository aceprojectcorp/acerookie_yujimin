using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// [ GameManager 하는일 ] 
//  - 이동거리 계산 
//  - gamedata의 scDistFromStagePre에 빠른 참조를 위해 세로 인덱스 값인 idxCM를 이동거리에 따라 변경, 
//  - 이동 거리에 따라 게임속도 변경 (gamedata의 scDistFromStagePre 참조) 
//  - gamePlayScene에 필요한 값들 초기화 
public class GameManager : MonoBehaviour {
	
	float totalPixelMoved   = 0 ;		
	int scDistFromStagePre  = 0 ; 
	int idxPreVal 		 	= 0 ; 

	void Awake()
	{
		Application.targetFrameRate = GameData.Instance.framePerSec; //60;	// 초당 프레임수 고정 
	}

	void Start () 
	{
		InitPlayScene ();
	}
	
	// Update is called once per frame
	void Update () 
	{ 
		// 이동 거리 계산 
		scDistFromStagePre = GameData.Instance.scDistFromStage ;
		totalPixelMoved += (GameData.Instance.nowGameSpeed * GameData.Instance.framePerSec) * Time.deltaTime ;  
		GameData.Instance.scDistFromStage = (int)( totalPixelMoved / 10.0f );

		// 1m씩 증가할 때마다 거리 점수, 이동 거리에 해당하는 idxCM값 변경 여부 결정, 게임속도 변경 ---------------------------------------------------
		if( GameData.Instance.scDistFromStage != scDistFromStagePre )
		{
			scDistFromStagePre = GameData.Instance.scDistFromStage ;

			// 이동 거리 구간에 따라 infoForChangeMeter를 참조할 세로 인덱스번호인 idxCM 구하고, idxCM값 변경 시 게임 속도 변경 
			if( GameData.Instance.idxCM != GameData.Instance.infoForChangeMeter.GetLength(0) -1 )  
			{
				for( int i=0 ; i < GameData.Instance.infoForChangeMeter.GetLength(0); i++ )
				{				
					if( GameData.Instance.scDistFromStage < GameData.Instance.infoForChangeMeter[i][0])
					{
						GameData.Instance.idxCM = i ;
						break;
					}
					// 거리가 1000미터 이상일 경우, ( 테이블에서 마지막 줄에 해당 )  
					GameData.Instance.idxCM = GameData.Instance.infoForChangeMeter.GetLength(0) -1 ;
				}
				
				// 인덱스값 변경시 게임 속도 변경  
				if( idxPreVal != GameData.Instance.idxCM )
				{
					GameData.Instance.nowGameSpeed = 
						GameData.Instance.pixelPerFrame * 
						( GameData.Instance.infoForChangeMeter[ GameData.Instance.idxCM ][1]*1f ) / 100 ;			 	
					idxPreVal = GameData.Instance.idxCM;
				}
			}
		}
		//			Debug.Log("GetLength(0) : " + GameData.Instance.infoForChangeMeter.GetLength(0) );	// 11
		//--------------------------------------------------------------------------------------------------------


	}

	// 게임결과창 에서 다시 게임 할때, 처음 게임할때 초기화해야할 변수들 생각해서 만들기 
	void InitPlayScene()
	{
		GameData.Instance.scGoldFromStage = 0;
		GameData.Instance.scHitMonFromStage = 0;
		GameData.Instance.scDistFromStage = 0 ;
		GameData.Instance.idxCM = 0;  
		idxPreVal = 0;

	}
	 

	//			Debug.Log("RANK : " + GameData.Instance.infoForChangeMeter.Rank);					// 2 (차원) 
	//			Debug.Log("GetLength(0) : " + GameData.Instance.infoForChangeMeter.GetLength(0) );	// 11
	//			Debug.Log("GetLength(1) : " + GameData.Instance.infoForChangeMeter.GetLength(1) );	//	6
	//		if(Input.GetMouseButton(0) == true)	// 왼쪽 마우스 눌렀을때!!!
	//		{
	//			Input.mousePosition	// vector3로 받아옴.  
	// 광재님이 주신 팁!!!!!! 유니티 NGUI 개발 팁!!!!
	// 스프라이트 하나하나 조각조각 이쁘게.
	// transform에서 날개 안바꿔도됨 UISprite의 Flip을 Horizontally를 바꿔줌 
	// uiEventTrigger 에서 해당 객체에 관련 이벤트가 있을때 처리해줌
	// ngui에서 이벤트는 트윈써라!!!!!. 애니메이션도 트윈써라!!!!!
	// 		핑퐁 쓰면 같은 반복 2개 돌릴 수 있음 
	// 스프라이트등등 편하게 추가하려면 위젯툴 쓰셩
	// press일때, 위치받고, 마우스 위치받아서 쏼라쏼라 하면 될듯 
	//		}

}
