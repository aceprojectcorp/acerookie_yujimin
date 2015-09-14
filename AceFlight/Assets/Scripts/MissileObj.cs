using UnityEngine;
using System.Collections;

// 미사일 이동, 파괴( 몬스터와 부딪히거나, 이동 중 일정 위치에 도달할 경우)
public class MissileObj : MonoBehaviour {

	public int msiPower 	= 0 ;				// 이 미사일 객체의 파워 
	Vector3 msiMovePos 	= Vector3.zero;		// 미사일 이동 위치 
	float msiDestoryPosY	= 0; 				// 미사일을 파괴할 위치의 y좌표 

	// Use this for initialization 
	void Start () 
	{
		msiPower 		 = GameData.Instance.g_infoMsiPowerPerMeter[ GameData.Instance.g_idxMsiPower, 1];
		msiMovePos 	 = gameObject.transform.localPosition; 
		msiDestoryPosY = ( gameObject.GetComponent<UIWidget>().localSize.y + GameData.Instance.g_screenHeight ) /2 ; 
	}
	
	// Update is called once per frame 
	void Update () 
	{ 

		// 플레이어 죽음시, 이미 발사된 총알은 앞으로 날라가게 
//		if ( GameData.Instance.g_playerState == PlayerState.play ) 
		{
			// Move msi ( 1frame, 64px move ) 
			msiMovePos.y += ( GameData.Instance.g_msiMovePixelPerFrame * GameData.Instance.g_framePerSec ) * Time.deltaTime; 
			gameObject.transform.localPosition = msiMovePos; 
		}

		// Destory msi
		if ( gameObject.transform.localPosition.y > msiDestoryPosY )
			Destroy ( gameObject ); 
	}

	// 충돌체의 태그를 이용한 충돌 처리 
	void OnTriggerEnter( Collider other )
	{
		if ( other.transform.tag == "Monster" )
		{
			Destroy( gameObject );
		}
	}

}
