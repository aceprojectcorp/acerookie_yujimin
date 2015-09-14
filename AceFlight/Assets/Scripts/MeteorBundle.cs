using UnityEngine;
using System.Collections;

//<< 알림마크/레이저 위치 이동 및 파괴, 운석객체 생성. bundle은 메테오에서 파괴함 >>
public class MeteorBundle : MonoBehaviour {

	public GameObject MeteorObj ;
	
	float secNotifyTime 			= 3f ; 		// 등장알림, 레이저 그려지는 시간 
	float moveRagerPxPerFrame 		= 5f ; 		// 레이저 이동 속도 
	float accureNotifyTime			= 0	;		// 누적 알림 시간
	float timeOfNotifyMark			= 0.3f;		// 등장알림 깜빡임 시간 간격 
	bool isNotifyTime				= true;		// 등장알림시간인지 알려줌 

	Vector3 playerPos		= Vector3.zero ; 	
	Vector3 ragerPos		= Vector3.zero ;	// 레이저 이동시킬 위치 값 
	Vector3 notifyMarkPos 	= Vector3.zero ;
	
	Vector3 meteorInitPos = new Vector3(0, 2, 0);	// 운석 맨 위에 위치할때의 위치값 저장 


	// Use this for initialization
	void Start () 
	{
		isNotifyTime = true;
		//레이저 시작 가로 위치는 플레이어 위치에서 시작 
		ragerPos.x = GameObject.Find("Player").transform.localPosition.x ;  
		transform.FindChild("MeteorRager").transform.localPosition = playerPos; 

		//secNotifyTime(3초)후, 레이저/알림마크 파괴, 운석생성 
		Invoke( "destoryLagerAndMark", secNotifyTime ); 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( GameData.Instance.g_playerState == PlayerState.play && isNotifyTime == true )
		{
			// 레이저 이동 (초기 플레이어 위치에서 현재 플레이어 위치로 계속 쫓아감)----------------
			playerPos.x = GameObject.Find("Player").transform.localPosition.x ;
			
			// 레이저 위치가 플레이어 위치보다 오른쪽에 위치  
			if( playerPos.x < transform.FindChild("MeteorRager").transform.localPosition.x )
				ragerPos.x -= (moveRagerPxPerFrame * GameData.Instance.g_framePerSec) * Time.deltaTime;
			else if( playerPos.x > transform.FindChild("MeteorRager").transform.localPosition.x ) 
				ragerPos.x += (moveRagerPxPerFrame * GameData.Instance.g_framePerSec) * Time.deltaTime;
			else
				ragerPos.x = ragerPos.x;
			
			transform.FindChild("MeteorRager").transform.localPosition = ragerPos ; 
			//------------------------------------
			
			// 알림마크 - 레이저 위치로 알림마크 위치이동, 깜빡임 처리 ---------------------------  
			accureNotifyTime += Time.deltaTime ; 
			
			// 레이저 위치로 알림마크 위치 이동 
			notifyMarkPos = transform.FindChild("NotifyMarkMeteor").localPosition ;
			notifyMarkPos.x = transform.FindChild("MeteorRager").transform.localPosition.x;
			transform.FindChild("NotifyMarkMeteor").localPosition = notifyMarkPos;

			// timeOfNotifyMark(s)마다 켜졌다 꺼졌다함. 
			if( (int)( accureNotifyTime / timeOfNotifyMark ) % 2 == 1 )
				transform.FindChild("NotifyMarkMeteor").gameObject.SetActive(false);
			else
				transform.FindChild("NotifyMarkMeteor").gameObject.SetActive(true);
			
			//---------------------------------------------------------------------------
		}
		// 플레이어 죽으면 레이저, 알림마크 숨기기 
		else if( GameData.Instance.g_playerState == PlayerState.dead && isNotifyTime == true )
		{
			transform.FindChild("MeteorRager").gameObject.SetActive(false);
			transform.FindChild("NotifyMarkMeteor").gameObject.SetActive(false);
		}
	}

	//secNotifyTime(3초)후, 레이저/알림마크 파괴, 운석생성 
	void destoryLagerAndMark()
	{ 
		if( GameData.Instance.g_playerState == PlayerState.play )
		{
			isNotifyTime = false;
			Destroy( transform.FindChild("MeteorRager").gameObject );
			Destroy( transform.FindChild("NotifyMarkMeteor").gameObject );

			GameObject mtoInst = Instantiate(MeteorObj) as GameObject;
			mtoInst.transform.parent = gameObject.transform;
			meteorInitPos.x = transform.FindChild("MeteorRager").transform.localPosition.x ;   
			mtoInst.transform.localPosition = meteorInitPos ;
			mtoInst.transform.localScale = Vector3.one;
		}
	}
}
