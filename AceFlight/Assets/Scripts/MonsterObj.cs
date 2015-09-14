using UnityEngine;
using System.Collections;

// << 몬스터 객체에 대한 정보 셋팅 및 변경 >>
public class MonsterObj : MonoBehaviour {


	public int monLv 			= 0;
	public int monHp 			= 0;
	public int monHitScore 		= 0; 
	public float moveSpeed 		= 0;
	public float monStartPosX 	= 0; 
	public int secMonDeadEffect = 0;
 	float monDestoryPosY		= 0; 
	Vector3 monPos 				= Vector3.zero;	
	bool monDeadSw				= false; 

	void Start () 
	{
		// 배치 확률에 따라 몬스터 레벨 생성. 레벨 생성 후 레벨에 따른 몬스터 정보 셋팅 (MoninfoInit()) 
//		makeLv ();
		monPos = gameObject.transform.localPosition;
		monDestoryPosY		= -544 ; 
		transform.FindChild ("MonHpBar").gameObject.SetActive (false);
		secMonDeadEffect = 1; 
	}

	// 레벨에 해당하는 몬스터의 이미지 관련 정보 변경 
	void changeSprOfMon( string objName, int i, int j )
	{
		transform.FindChild( objName ).GetComponent<UISprite>().spriteName
														= GameData.Instance.monSprNames[i,j];		
		transform.FindChild( objName ).GetComponent<UISprite>().MakePixelPerfect();

		if ( objName == "EyeR" ) 
		{
			transform.FindChild( objName ).rotation = Quaternion.Euler( 0, 180, 0 ) ;
		}
	}




	// Update is called once per frame
	void Update ()  
	{
		if( monHp > 0 )
		{
			// 몬스터 -y 방향으로 이동-------------------------------------------------------------//
			moveSpeed = GameData.Instance.nowGameSpeed * GameData.Instance.framePerSec; 
			monPos.y -= moveSpeed * Time.deltaTime;		
			gameObject.transform.localPosition = monPos;
			if ( monPos.y <= monDestoryPosY )		
				Destroy (gameObject); 	 
			//-------------------------------------------------------------------------------// 

			// 초기 monHp값에서 변동이 있을 경우 눈 이미지 변환 + hp image!!!!!
			if( monHp != GameData.Instance.infoForMon[ monLv-1, 1 ] )
			{
				changeSprOfMon( "EyeL", 	monLv-1, 2 );
				changeSprOfMon( "EyeR", 	monLv-1, 2 );
				transform.FindChild("MonHpBar").GetComponent<UISlider>().value = (monHp * 1f) / GameData.Instance.infoForMon [ monLv-1, 1 ] ;
				transform.FindChild ("MonHpBar").gameObject.SetActive (true);
			}
		}
		else
		{
			gameObject.GetComponent<UIPanel>().alpha -= secMonDeadEffect * Time.deltaTime ; 		// secMonDeadEffect == 1

			if( monDeadSw == false )
			{
				monDeadSw = true;
				GameData.Instance.scHitMonFromStage += GameData.Instance.infoForMon[ monLv - 1 , 2 ];
				transform.FindChild ("MonHpBar").gameObject.SetActive (false);

				Destroy ( gameObject.GetComponent<Rigidbody> () );
				Destroy ( gameObject.GetComponent<BoxCollider>() );
				Destroy ( gameObject, 1.0f ) ; 
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if ( other.transform.tag == "Missile" )
		{
			changeSprOfMon( "EyeL", 	monLv-1, 2 );
			changeSprOfMon( "EyeR", 	monLv-1, 2 );
			// hp down,
			monHp -= other.GetComponent<MissileObj>().msiPower ;
		}


	}


}
