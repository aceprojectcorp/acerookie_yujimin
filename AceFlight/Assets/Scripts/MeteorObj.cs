using UnityEngine;
using System.Collections;

//<운석 아래로 이동 및 파괴>>
public class MeteorObj : MonoBehaviour {

	float destoryYPosToMeteor	= -1088f;	// screenHeight + 혜성 세로 길이 만큼. 
	// destoryYPosToMeteor = -1*(GameData.Instance.g_screenHeight + Mathf.Abs(transform.FindChild("Meteor").GetComponent<UISprite>().localSize.y)) ;	// == -1088

	float movePxOfMeteor	 	= 1.5f;

	Vector3 meteorPos = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		meteorPos = gameObject.transform.localPosition ; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( GameData.Instance.g_playerState == PlayerState.play )
		{
			// 운석 아래로 이동. 
			if( meteorPos.y > destoryYPosToMeteor )
			{
				meteorPos.y -= ( (movePxOfMeteor* GameData.Instance.g_nowGameSpeed) * GameData.Instance.g_framePerSec) * Time.deltaTime;
				gameObject.transform.localPosition = meteorPos ; 
			}
			else
			{
				Destroy( transform.parent.gameObject );
			}
		}
	}
}
