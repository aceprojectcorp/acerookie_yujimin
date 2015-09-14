using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// << 배경 이미지 출력에 관련된 스크립트 >>
// - 씬에 따라서 추가되는 이미지(darkBg)가 있고, 변경되는 이미지(플레이씬에서 배경 이미지 랜덤 선택)가 있음.
// - 배경 이미지 이동 : 두개의 이미지가 -y방향으로 이동함. 일정 시점에 도달하면 위에 있는 이미지의 위로 아래 이미지가 이동    
//[주의] 초반에 screenHeight를 제대로 받지 못하면 이미지가 저 하늘 위로 날라감... 
public class DrawBG : MonoBehaviour {

	public float m_totalPixelMoved;

	Vector3 bgPos1 			= new Vector3( 0, 0, 0 );		// selectBg1 위치  
	Vector3 bgPos2 			= new Vector3( 0, 0, 0 );		// selectBg2 위치
	
	public List <UISprite> BgList = new List<UISprite> ();	// 배경 스프라이트를 저장할 리스트 
	
	public UISprite selectBg1;								// 스크롤할 배경 객체 
	public UISprite selectBg2;

	float bgSpeed = 0;										// bg move speed 

	// 두번째 배경객체에 세로 크기 만큼 y 위치값 추가, play씬일때만 배경 이미지 랜덤 변경. 
	void Start () 
	{
		bgPos2.y += selectBg1.localSize.y - 2;
		selectBg2.transform.localPosition = bgPos2;

		GameObject darkBg = transform.Find ("BG_Dark").gameObject;

		if ( GameData.Instance.g_nowScene == GameSceneState.play ) 
		{
			darkBg.SetActive(false);
			int randListIdx = Random.Range(0, BgList.Count);
			selectBg1.spriteName = BgList[randListIdx].spriteName; 
			selectBg2.spriteName = BgList[randListIdx].spriteName; 		
		} 
		else 
		{
			darkBg.SetActive(true);
			selectBg1.spriteName = "BG01";
			selectBg2.spriteName = "BG01";
		}

		selectBg1.gameObject.SetActive (true); 
		selectBg2.gameObject.SetActive (true); 

		bgPos1 = selectBg1.transform.localPosition;
	}

	void Update () 
	{ 
		// 배경 이동. - 먼저 이동시킬 위치 바꿔 주고 나서 해당 배경 이동하기.------------------- 
		bgSpeed = GameData.Instance.g_nowGameSpeed * GameData.Instance.g_framePerSec; 
		bgPos1.y -= bgSpeed * Time.deltaTime;
		bgPos2.y -= bgSpeed * Time.deltaTime;

		if( Mathf.Abs( bgPos1.y ) >= GameData.Instance.g_screenHeight )		
			bgPos1.y = bgPos2.y + selectBg2.localSize.y - 2; 		// -2씩 안해주면 눈에 보이는 크랙 발생. 
	
		if( Mathf.Abs( bgPos2.y ) >= GameData.Instance.g_screenHeight )
			bgPos2.y = bgPos1.y + selectBg1.localSize.y - 2; 

		selectBg1.transform.localPosition = bgPos1;
		selectBg2.transform.localPosition = bgPos2;
		//------------------------------------------------------------------------
	}
}
