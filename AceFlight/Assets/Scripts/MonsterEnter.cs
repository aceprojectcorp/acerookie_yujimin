using UnityEngine;
using System.Collections;

// << 누적 이동 거리에 따라 몬스터 객체 생성 >> 
public class MonsterEnter : MonoBehaviour {

	public GameObject MonsterObj ;

	Vector3 monFirstPos 		= Vector3.zero ;
	float[] monStartPosX 		= { -255, -128, 0, 128, 255 };
	float 	monStartPosY 		= 544 ; 	
	bool 	monEnterSwc 		= false; 	// 몬스터 등장 스위치 
	int 	scDistFromStagePre  = 0 ;		// 거리값이 변경 됐을때만, 몬스터 생성여부 판단하기 위해 이전 거리값 저장  


	void Start ()  
	{
		monStartPosY = 544; 	// == gameObject.GetComponent<Collider> ().transform.localScale.y + GameData.Instance.g_screenHeight )/2 ;  
		CreateMonsters(); 		// 시작과 동시에 몬스터 1줄 생성. 
		scDistFromStagePre = GameData.Instance.g_scDistFromStage ;
	}

	void Update () 
	{
		if( scDistFromStagePre != GameData.Instance.g_scDistFromStage )
		{
			monEnterSwc = true;
			scDistFromStagePre = GameData.Instance.g_scDistFromStage ;
		}

		// 누적 이동 거리가 몬스터 생성의 거리 기준 만큼 누적된 경우 몬스터 생성   
		if( GameData.Instance.g_scDistFromStage % GameData.Instance.g_createMonMeter == 0 && monEnterSwc == true )
		{
			monEnterSwc = false;
			CreateMonsters();
		}
	}

	// 몬스터 객체 생성. 위치, 부모, 크기, 객체내 변수, 이미지 모두 초기화해줌. 
	void CreateMonsters()
	{
		int monLv = 0 ;
		for( int i=0 ; i < 5 ; i++ )
		{			
			monFirstPos.x = monStartPosX[i];
			monFirstPos.y = monStartPosY;
			GameObject monInst = Instantiate(MonsterObj) as GameObject;
			monInst.transform.parent = GameObject.Find("MonsterManager").transform;
			monInst.transform.localScale = Vector3.one;
			monInst.transform.localPosition = monFirstPos;
			monLv = MakeLv() ;
			monInst.GetComponent<MonsterObj>().monLv = monLv; 
			monInst.GetComponent<MonsterObj>().monHp = GameData.Instance.g_infoForMon[ monLv-1, 1 ] ;
			monInst.GetComponent<MonsterObj>().monHitScore = GameData.Instance.g_infoForMon[ monLv-1, 2 ] ;
			ChangeSprOfMon( monInst, "Body", 	monLv-1, 0 );
			ChangeSprOfMon( monInst, "EyeL", 	monLv-1, 1 );
			ChangeSprOfMon( monInst, "EyeR", 	monLv-1, 1 );
			ChangeSprOfMon( monInst, "WingR", 	monLv-1, 3 );
			ChangeSprOfMon( monInst, "WingL", 	monLv-1, 3 );	

			//GameObject monInst = (GameObject) Instantiate( MonsterObj, monFirstPos, mlFirstAngle );	
			// 위와 같이 사용시, 객체가 잠깐 중앙(0,0,0)에 위치 했다가 자신의 위치로 이동함. 
		}
	}

	// 배치 확률에 따라 몬스터 레벨 설정 [ GameData의 g_monPlaceRateList 참고 ]
	int MakeLv()
	{ 
		int rand1to100		= Random.Range( 1, 101 );	// 1~100		
		int selectPlaceRate = 0;
		
		for( int i=1 ; i < GameData.Instance.g_monPlaceRateList[ GameData.Instance.g_idxCM ].m_PlaceRatesList.Count ; i++ )
		{
			selectPlaceRate += GameData.Instance.g_monPlaceRateList[ GameData.Instance.g_idxCM ].m_PlaceRatesList[i] ;
			if( selectPlaceRate >= rand1to100 )
				return i ; 
		}
		return 1;
	}
	// MakeLv()문
	// GameData.Instance.g_monPlaceRateList[ GameData.Instance.g_idxCM ].m_PlaceRatesList.Count == 이동거리에 해당하는 리스트에 접근. 
	/** ex) g_monPlaceRateList에 {1001,	0,	0,	0,	0,	0,	70,	20,	10} 입력되어있고,  rand1to100= 98일 경우.
	 * 1) i=6일때, selectPlaceRate는 누적되어 70 됨 (+70)
	 *  -> rand1to100값이 1~70인 경우에.70% 해당.	=> x
	 * 2) i=7일때, selectPlaceRate는 누적되어 90이 됨 (+20)
	 *  -> rand1to100값이 71~90인 경우에 20%에 해당. => x 
	 *  -> (1~70일 경우는 해당하지 않음. i=6일때 증명됨)
	 * 3) i=8일때, selectPlaceRate는 누적되어 100이 됨 (+10)
	 *  -> rand1to100값이 91~100인 경우 10%에 해당 => o
	 */

	
	// 레벨에 해당하는 몬스터의 이미지 관련 정보 변경 
	void ChangeSprOfMon( GameObject monInst, string objName, int i, int j )
	{
		monInst.transform.FindChild( objName ).GetComponent<UISprite>().spriteName
			= GameData.Instance.g_monSprNames[i,j];		
		monInst.transform.FindChild( objName ).GetComponent<UISprite>().MakePixelPerfect();
		
		if ( objName == "EyeR" ) 
		{
			monInst.transform.FindChild( objName ).rotation = Quaternion.Euler( 0, 180, 0 ) ;
		}
	}

}
