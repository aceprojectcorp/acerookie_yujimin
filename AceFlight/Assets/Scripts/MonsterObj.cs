using UnityEngine;
using System.Collections;

public class MonsterObj : MonoBehaviour {

	public int monLv 			= 0;
	public int monHp 			= 0;
	public int monHitScore 		= 0 ; 

	Vector3 monPos 	= new Vector3( 0, 0, 0 );				

	public float moveSpeed 		= 0;

	public float monStartPosX 	= 0 ; 
 	float monStartPosY = 544 ; 

	// Use this for initialization 
	void Start () 
	{
		// 배치 확률에 따라 몬스터 레벨 생성. 레벨 생성 후 레벨에 따른 몬스터 정보 셋팅 (MoninfoInit()) 
		makeLv ();

		monPos.x = monStartPosX;
		monPos.y = monStartPosY;
		gameObject.transform.localPosition = monPos;  
	}

	void makeLv()
	{
		int highNum 	 = 0;	// 더 높은 확률값 저장 (70%)
		int lowNum 		 = 0;
		int idxOfHighNum = 0;	// 더 높은 확률값의 테이블 인덱스 번호 
		int idxOfLowNum  = 0;

		// 테이블보고 랜덤 돌려서 레벨 설정, 이미지 설정 
		int randMonsLv = Random.Range( 1, 101 );	// 1~100

		// 등장확률 비교 
		if ( GameData.Instance.infoForChangeMeter [GameData.Instance.idxCM ][ 3] > GameData.Instance.infoForChangeMeter [GameData.Instance.idxCM ][ 5] ) 
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
		// 위에서 정해진 몬스터 레벨에 해당하는 변수 값으로 초기화 
		MoninfoInit();
	}


	// 레벨에 해당하는 몬스터의 이미지 관련 정보 변경 
	void changeSprOfMon( string objName, int i, int j )
	{
		transform.FindChild( objName ).GetComponent<UISprite>().spriteName
													= GameData.Instance.infoForMonSprName[i,j];		
		transform.FindChild( objName ).GetComponent<UISprite>().MakePixelPerfect();

		if ( objName == "EyeR" ) 
		{
			transform.FindChild( objName ).rotation = Quaternion.Euler( 0, 180, 0 ) ;
		}
	}


	void MoninfoInit()
	{
//		for (int i=0; i < GameData.Instance.infoForMon.GetLength(0); i++) 
//		{
//			if( monLv == GameData.Instance.infoForMon[i,0] )
//			{
		monHp = GameData.Instance.infoForMon[ monLv-1, 1 ] ;
		monHitScore = GameData.Instance.infoForMon[ monLv-1, 2 ] ;
				
		// image change
		changeSprOfMon( "Body", 	monLv-1, 0 );
		changeSprOfMon( "EyeL", 	monLv-1, 1 );
		changeSprOfMon( "EyeR", 	monLv-1, 1 );
		changeSprOfMon( "WingR", 	monLv-1, 3 );
		changeSprOfMon( "WingL", 	monLv-1, 3 );
				
//				break;
//			}
//		}
	}

	// Update is called once per frame
	void Update () 
	{
		// 몬스터 -y 방향으로 이동-------------------------------------------------------------//
		moveSpeed = GameData.Instance.nowGameSpeed * GameData.Instance.framePerSec; 
		monPos.y -= moveSpeed * Time.deltaTime;		
		gameObject.transform.localPosition = monPos;
		if ( monPos.y <= -1 * monStartPosY )		
			Destroy (gameObject); 	 
		//-------------------------------------------------------------------------------// 

		// 초기 monHp값에서 변동이 있을 경우 눈 이미지 변환 
		if( monHp < GameData.Instance.infoForMon[ monLv-1, 1 ] )
		{
			changeSprOfMon( "EyeL", 	monLv-1, 2 );
			changeSprOfMon( "EyeR", 	monLv-1, 2 );
		}

	}

}
