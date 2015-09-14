using UnityEngine;
using System.Collections;

// << 누적 이동 거리에 따라 몬스터 객체 생성 >> 
public class MonsterEnter : MonoBehaviour {

	public GameObject MonsterObj ;

	Vector3 monFirstPos 		= Vector3.zero ;
	float[] monStartPosX 		= { -255, -128, 0, 128, 255 };
	float 	monStartPosY 		= 0; 	
	bool 	monEnterSwc 		= false; 	// 몬스터 등장 스위치 
	int 	scDistFromStagePre  = 0 ;		// 거리값이 변경 됐을때만, 몬스터 생성여부 판단하기 위해 이전 거리값 저장  
	int 	monLv				= 0 ;
	int 	monHp				= 0 ;
	int 	monHitScore 		= 0 ;

	void Start ()  
	{
		monStartPosY = 544; 	// == gameObject.GetComponent<Collider> ().transform.localScale.y + GameData.Instance.screenHeight )/2 ;  
		CreateMonsters(); 		// 시작과 동시에 몬스터 1줄 생성. 
		scDistFromStagePre = GameData.Instance.scDistFromStage ;
	}

	void Update () 
	{
		if( scDistFromStagePre != GameData.Instance.scDistFromStage )
		{
			monEnterSwc = true;
			scDistFromStagePre = GameData.Instance.scDistFromStage ;
		}

		// 누적 이동 거리가 몬스터 생성의 거리 기준 만큼 누적된 경우 몬스터 생성   
		if( GameData.Instance.scDistFromStage % GameData.Instance.createMonMeter == 0 && monEnterSwc == true )
		{
			monEnterSwc = false;
			CreateMonsters();
		}
	}

	void CreateMonsters()
	{
		for( int i=0 ; i < 5 ; i++ )
		{			
			monFirstPos.x = monStartPosX[i];
			monFirstPos.y = monStartPosY;
			GameObject monInst = Instantiate(MonsterObj) as GameObject;
			monInst.transform.parent = GameObject.Find("Monsters").transform;
			monInst.transform.localScale = Vector3.one;
			monInst.transform.localPosition = monFirstPos;
			monInst.GetComponent<MonsterObj>().monLv = makeLv() ; 
			monInst.GetComponent<MonsterObj>().monHp = GameData.Instance.infoForMon[ monLv-1, 1 ] ;
			monInst.GetComponent<MonsterObj>().monHitScore = GameData.Instance.infoForMon[ monLv-1, 2 ] ;
			changeSprOfMon( monInst, "Body", 	monLv-1, 0 );
			changeSprOfMon( monInst, "EyeL", 	monLv-1, 1 );
			changeSprOfMon( monInst, "EyeR", 	monLv-1, 1 );
			changeSprOfMon( monInst, "WingR", 	monLv-1, 3 );
			changeSprOfMon( monInst, "WingL", 	monLv-1, 3 );	

			//GameObject monInst = (GameObject) Instantiate( MonsterObj, monFirstPos, mlFirstAngle );	
			// 위와 같이 사용시, 객체가 잠깐 중앙(0,0,0)에 위치 했다가 자신의 위치로 이동함. 
		}
	}

	int makeLv()
	{
		int highNum 	 = 0;	// 더 높은 확률값 저장 (70%)
		int lowNum 		 = 0;
		int idxOfHighNum = 0;	// 더 높은 확률값의 테이블 인덱스 번호 
		int idxOfLowNum  = 0;
		
		// 테이블보고 랜덤 돌려서 레벨 설정, 이미지 설정 
		int randMonsLv = Random.Range( 1, 101 );	// 1~100
		
		// 등장확률 비교 
		if ( GameData.Instance.infoForChangeMeter [ GameData.Instance.idxCM ][ 3 ] > GameData.Instance.infoForChangeMeter [ GameData.Instance.idxCM ][ 5 ] ) 
		{
			highNum = GameData.Instance.infoForChangeMeter[ GameData.Instance.idxCM ][ 3 ] ;
			lowNum = GameData.Instance.infoForChangeMeter[ GameData.Instance.idxCM ][ 5 ] ;
			idxOfHighNum = 3 ;
			idxOfLowNum  = 5 ;
		} 
		else 
		{
			highNum = GameData.Instance.infoForChangeMeter[ GameData.Instance.idxCM ][ 5 ] ;
			lowNum = GameData.Instance.infoForChangeMeter[ GameData.Instance.idxCM ][ 3 ] ;
			idxOfHighNum = 5 ;
			idxOfLowNum  = 3 ;
		}
		
		// 70%  -> randMonsLv의 숫자가 1~70 이면, 
		if ( randMonsLv <= highNum ) 
			monLv = GameData.Instance.infoForChangeMeter[ GameData.Instance.idxCM ][ idxOfHighNum-1 ] ; 
		
		// 30%
		else
		{
			// 이동거리 0~1000m 사이
			if ( GameData.Instance.idxCM != GameData.Instance.infoForChangeMeter.GetLength(0)-1 )	
				monLv = GameData.Instance.infoForChangeMeter[ GameData.Instance.idxCM ][ idxOfLowNum-1 ] ;
			
			// 이동거리 1000m 이상 (제일 빠른 속도)
			else
			{
				//20% 71~90
				if ( randMonsLv > highNum && randMonsLv <= highNum+lowNum ) 
					monLv = GameData.Instance.infoForChangeMeter[ GameData.Instance.idxCM ][ idxOfLowNum-1 ] ;
				//10% 91~100 
				else
					monLv = GameData.Instance.infoForChangeMeter[ GameData.Instance.idxCM ][ idxOfLowNum+1 ] ;
			}
		}
		
		//		int lvOfbestHighRate 	 = 0;
		//		int lvRateOfbestHighRate = 0;
		//		GameData.Instance.g_monPlaceRateList[ GameData.Instance.idxCM ]
		//		Debug.Log ( GameData.Instance.g_monPlaceRateList[ GameData.Instance.idxCM ].inputLvList.Count );
		//		Debug.Log (GameData.Instance.g_monPlaceRateList.[ GameData.Instance.idxCM ] );
		//		Debug.Log(		GameData.Instance.g_monPlaceRateList[ GameData.Instance.idxCM ].m_distUnder ); 
		//		for( int i=0 ; i < GameData.Instance.g_monPlaceRateList[ GameData.Instance.idxCM ].inputLvList.Count ; i++)

		return monLv;
	}

	// 레벨에 해당하는 몬스터의 이미지 관련 정보 변경 
	void changeSprOfMon( GameObject monInst, string objName, int i, int j )
	{
		monInst.transform.FindChild( objName ).GetComponent<UISprite>().spriteName
			= GameData.Instance.monSprNames[i,j];		
		monInst.transform.FindChild( objName ).GetComponent<UISprite>().MakePixelPerfect();
		
		if ( objName == "EyeR" ) 
		{
			monInst.transform.FindChild( objName ).rotation = Quaternion.Euler( 0, 180, 0 ) ;
		}
	}


}
