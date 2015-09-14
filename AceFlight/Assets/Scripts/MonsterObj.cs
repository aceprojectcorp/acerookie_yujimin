using UnityEngine;
using System.Collections;

// << 몬스터 객체 이동, 몬스터 죽음 효과 및 관련 스코어 누적, hp변동에 따른 이미지 처리, 돈 객체 생성 >>
public class MonsterObj : MonoBehaviour {

	public GameObject monExplosion ;
	public GameObject moneyObj ; 

	public int monLv 			 = 0;
	public int monHp 			 = 0;
	public int monHitScore 		 = 0; 
	public float moveSpeed 		 = 0;
	public float monStartPosX 	 = 0; 
	public float secMonDeadEffect= 1f;
 	float monDestoryPosY		 = -544; 
	Vector3 monPos 				 = Vector3.zero;	
	bool monDeadSw				 = false; 

	void Start () 
	{
		monPos 			 = gameObject.transform.localPosition;
		transform.FindChild ("MonHpBar").gameObject.SetActive (false);
	}

	// 레벨에 해당하는 몬스터의 이미지 관련 정보 변경 
	void changeSprOfMon( string objName, int i, int j )
	{
		transform.FindChild( objName ).GetComponent<UISprite>().spriteName
														= GameData.Instance.g_monSprNames[i,j];		
		transform.FindChild( objName ).GetComponent<UISprite>().MakePixelPerfect();

		if ( objName == "EyeR" ) 
			transform.FindChild( objName ).rotation = Quaternion.Euler( 0, 180, 0 ) ;
	}
	
	// Update is called once per frame
	void Update ()  
	{
		if( monHp > 0 )
		{
			// 몬스터 -y 방향으로 이동-------------------------------------------------------------//
			moveSpeed = GameData.Instance.g_nowGameSpeed * GameData.Instance.g_framePerSec * 250/100 ; 
			monPos.y -= moveSpeed * Time.deltaTime;		
			gameObject.transform.localPosition = monPos; 
			if ( monPos.y <= monDestoryPosY )		
				Destroy (gameObject); 	 
			//-------------------------------------------------------------------------------// 

			// 초기 monHp값에서 변동이 있을 경우 눈 이미지 변환 + hp image!!!!!
			if( monHp != GameData.Instance.g_infoForMon[ monLv-1, 1 ]  && GameData.Instance.g_playerState == PlayerState.play )
			{
				changeSprOfMon( "EyeL", 	monLv-1, 2 );
				changeSprOfMon( "EyeR", 	monLv-1, 2 );
				transform.FindChild("MonHpBar").GetComponent<UISlider>().value = (monHp * 1f) / GameData.Instance.g_infoForMon [ monLv-1, 1 ] ;
				transform.FindChild ("MonHpBar").gameObject.SetActive (true);
			}
			// 플레이어 죽으면 몬스터들이 비웃는 눈으로 변경 
			else if ( GameData.Instance.g_playerState == PlayerState.dead )
			{
				changeSprOfMon( "EyeL", 	0, 2 );
				transform.FindChild( "EyeL" ).rotation = Quaternion.Euler( 0, 0, -140 ) ;
				changeSprOfMon( "EyeR", 	0, 2 );
				transform.FindChild( "EyeR" ).rotation = Quaternion.Euler( 0, 0, -115 ) ;
			}
		}
		else
		{
			// secMonDeadEffect == 1.5
			gameObject.GetComponent<UIPanel>().alpha -= 1 * ( Time.deltaTime * secMonDeadEffect ) ; 		

			if( monDeadSw == false )
			{
				monDeadSw = true;

				// 돈 객체 생성! --------------------------------------------------------------------
				GameObject moneyInst = Instantiate(moneyObj) as GameObject;
				moneyInst.transform.parent = GameObject.Find("MoneyObjs").transform;
				moneyInst.transform.localPosition = gameObject.transform.localPosition;
				int rand1to100 = Random.Range( 1, 101 ) ; 
				int selectPersent = 0 ; 
				// 확률에 따라 이미지, 획득금액 설정 
				for( int i=0 ; i < GameData.Instance.g_infoMoney.GetLength(0) ; i++ )
				{
					selectPersent += GameData.Instance.g_infoMoney[i,0] ;
					if( rand1to100 <= selectPersent )
					{
						moneyInst.GetComponent<UISprite>().spriteName = GameData.Instance.g_moneySprNames[i];
						moneyInst.GetComponent<UISprite>().MakePixelPerfect() ;
						moneyInst.GetComponent<MoneyObj>().myPrice = GameData.Instance.g_infoMoney[i,1]; 	
						if( i != 0 )
						{
							moneyInst.GetComponent<UISprite>().width = moneyInst.GetComponent<UISprite>().width / 2 ;
							moneyInst.GetComponent<UIWidget>().height = moneyInst.GetComponent<UISprite>().height / 2 ;
						}
						break;
					}
				}
				//--------------------------------------------------------------------------------------

				// 몬스터 폭팔 효과 프리팹 생성 
				GameObject explosionEffect = Instantiate(monExplosion) as GameObject;
				explosionEffect.transform.parent = GameObject.Find("MonsterManager").transform;
				explosionEffect.GetComponent<UISprite> ().MakePixelPerfect ();
				explosionEffect.transform.localPosition = gameObject.transform.localPosition;

				// hit스코어 누적, hp바 비활성화
				GameData.Instance.g_scHitMonFromStage += GameData.Instance.g_infoForMon[ monLv - 1 , 2 ];
				transform.FindChild ("MonHpBar").gameObject.SetActive (false);

				Destroy ( gameObject.GetComponent<Rigidbody> () );
				Destroy ( gameObject.GetComponent<CapsuleCollider>() );
				Destroy ( gameObject, secMonDeadEffect ) ; 
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
