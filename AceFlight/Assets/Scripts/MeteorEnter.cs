using UnityEngine;
using System.Collections;

//<< 운석번들 (MeteorBundle) 확률에 따라 랜덤 등장 >> 
public class MeteorEnter : MonoBehaviour {

	public GameObject MeteorBundle ; 

	int distancePre	= 0 ;		// 이전거리값( 거리 변화 없이 프레임 돌때 update문에서 중복 사용 자제를 위해!!!) 

	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( GameData.Instance.g_playerState == PlayerState.play )
		{
			// 메테오 등장 거리 범위 인지 확인 
			if( GameData.Instance.g_scDistFromStage % GameData.Instance.g_meterOfEnterMeteor == 0 &&
			    distancePre != GameData.Instance.g_scDistFromStage 		   
			   )
			{
				int ran1to100 = Random.Range( 1, 101 );	// 1 ~ 100

				// 확률에 따라 메테오 번들 등장 
				if( ran1to100 <= GameData.Instance.g_enterMeteorPersent ) 
				{
					GameObject mtoBdInst = Instantiate(MeteorBundle) as GameObject;
					mtoBdInst.transform.parent = GameObject.Find("MeteorManager").transform;
					mtoBdInst.transform.localPosition = Vector3.zero ;
					mtoBdInst.transform.localScale = Vector3.one;
				}
			}

			// 거리 변화 없을때 if문 안에 내용 중복사용 방지 
			distancePre = GameData.Instance.g_scDistFromStage ; 

		}
	}
}
