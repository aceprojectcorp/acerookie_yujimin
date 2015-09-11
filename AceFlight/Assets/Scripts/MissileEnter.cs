using UnityEngine;
using System.Collections;

// 미사일 생성, 미터당 파워 계산을 위한 인덱스 설정
public class MissileEnter : MonoBehaviour {

	public GameObject MissileObj ;

	int frmCnt = 0 ; 

	int scDistFromStagePre  = 0 ;		// 거리값이 변경 됐을때만, 생성여부 판단하기 위해 이전 거리값 저장  

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
		// update idxMsiPower
		if( scDistFromStagePre != GameData.Instance.scDistFromStage )
		{
			scDistFromStagePre = GameData.Instance.scDistFromStage ;

			for ( int i=0 ; i < GameData.Instance.infoMsiPowerPerMeter.GetLength(0) ; i++ )
			{
				// 이동거리 < 미사일 테이블[i]의 공격력 기준 거리 
				if( GameData.Instance.scDistFromStage < GameData.Instance.infoMsiPowerPerMeter[ i,0 ] )
				{
					GameData.Instance.idxMsiPower = i ;
					break; 
				}
				// 최대 파월 
				GameData.Instance.idxMsiPower = GameData.Instance.infoMsiPowerPerMeter.GetLength(0)-1 ;
			}
		}
	


		if (GameData.Instance.playerState == PlayerState.play) 
		{
			frmCnt++; 		
			// make missile  
			// 매 프레임마다 발사하면.... 플레이어 용 입에 불달고 다님 ... 2프레임이 하나씩으로 수정 해봄.. 
			if (frmCnt % 2 == 0) {
				GameObject msiInst = Instantiate (MissileObj) as GameObject;
				msiInst.transform.parent = GameObject.Find ("Missiles").transform;
				msiInst.GetComponent<UISprite> ().spriteName = GameData.Instance.msiSprNames [GameData.Instance.idxMsiPower];						 
				msiInst.GetComponent<UISprite> ().MakePixelPerfect ();
				msiInst.transform.localPosition = new Vector3 (-0.5f, 60f, 0f);
				msiInst.GetComponent<BoxCollider> ().size = new Vector3 (msiColliderSizes [GameData.Instance.idxMsiPower, 0],
			                                                     	msiColliderSizes [GameData.Instance.idxMsiPower, 1],
			                                                     	msiColliderSizes [GameData.Instance.idxMsiPower, 2]);
			}
		}
	
	}
}
