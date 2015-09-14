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
		msiPower 		 = GameData.Instance.infoMsiPowerPerMeter[ GameData.Instance.idxMsiPower, 1];
		msiMovePos 	 = gameObject.transform.localPosition; 
		msiDestoryPosY = ( gameObject.GetComponent<UIWidget>().localSize.y + GameData.Instance.screenHeight ) /2 ; 
	}
	
	// Update is called once per frame 
	void Update () 
	{
		if ( GameData.Instance.playerState == PlayerState.play ) 
		{
			// move ( 1frame, 64px move ) 
			msiMovePos.y += ( GameData.Instance.msiMovePixelPerFrame * GameData.Instance.framePerSec ) * Time.deltaTime; 
			gameObject.transform.localPosition = msiMovePos; 
		}

		// gameobject destory
		if ( gameObject.transform.localPosition.y > msiDestoryPosY )
			Destroy ( gameObject ); 
	}

	void OnTriggerEnter( Collider other )
	{
		if ( other.transform.tag == "Monster" )
		{
			Destroy( gameObject );
		}
	}

}
