using UnityEngine;
using System.Collections;

// << 미사일 생성, 이동 거리당 미사일 파워 계산을 위한 인덱스 설정 >>
public class MissileEnter : MonoBehaviour {

	public GameObject MissileObj ;

	int frmCnt = 0 ; 					// 프레임 갯수를 카운트해서 미사일 객체의 생성 타이밍을 위해 사용.
	int scDistFromStagePre  = 0 ;		// 거리값이 변경 됐을때 생성 여부 판단하기 위해 이전 거리값 저장  

	// 미사일 스프라이트 크기에 따라 콜라이더 크기 변경값 저장  
	float[,] msiColliderSizes = new float[,]
	{	{ 10f, 	40f, 	0 },
		{ 15f, 	100f, 	0 },
		{ 35f, 	95f, 	0 }, 
		{ 120f, 110f, 	0 }
	};

	// Use this for initialization 
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// update idxMsiPower. 
		// idxMsiPower는 미사일 공격력 테이블의 세로 인덱스값을 저장하는 int형 글로벌 변수. 쉽고 빠른 참조를 위해 저장하여 사용 
		if( scDistFromStagePre != GameData.Instance.g_scDistFromStage )
		{
			scDistFromStagePre = GameData.Instance.g_scDistFromStage ;

			for ( int i=0 ; i < GameData.Instance.g_infoMsiPowerPerMeter.GetLength(0) ; i++ )
			{
				// 현재 이동한 거리 < 미사일 테이블[i]의 공격력 기준 거리  == 미사일 파워를 바꿀 타이밍 !  
				if( GameData.Instance.g_scDistFromStage < GameData.Instance.g_infoMsiPowerPerMeter[ i,0 ] )
				{
					GameData.Instance.g_idxMsiPower = i ;
					break; 
				}
				// 최대 파월 
				GameData.Instance.g_idxMsiPower = GameData.Instance.g_infoMsiPowerPerMeter.GetLength(0)-1 ;
			}
		}
	

		// Make missile 
		if (GameData.Instance.g_playerState == PlayerState.play) 
		{
			frmCnt++; 				 
			// 매 프레임마다 발사하면.... 플레이어 용 입에 불달고 다님 ... 2프레임당 하나씩으로 수정 해봄. 
			if (frmCnt % 2 == 0) {
				GameObject msiInst = Instantiate (MissileObj) as GameObject;
				msiInst.transform.parent = GameObject.Find ("Missiles").transform;
				msiInst.GetComponent<UISprite> ().spriteName = GameData.Instance.g_msiSprNames [GameData.Instance.g_idxMsiPower];						 
				msiInst.GetComponent<UISprite> ().MakePixelPerfect ();
				msiInst.transform.localPosition = new Vector3 (-0.5f, 60f, 0f);
				msiInst.GetComponent<BoxCollider> ().size = new Vector3 (msiColliderSizes [GameData.Instance.g_idxMsiPower, 0],
			                                                     	msiColliderSizes [GameData.Instance.g_idxMsiPower, 1],
			                                                     	msiColliderSizes [GameData.Instance.g_idxMsiPower, 2]);
			}
		}
	
	}
}
