using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO : bgMove() 배경1이 밑에있고, 배경2가 위일때 약간 틈이 생김. 조정하기 
public class DrawBG : MonoBehaviour {
	
	Vector3 bgPos1 			= new Vector3( 0, 0, 0 );		// selectBg1 위치  
	Vector3 bgPos2 			= new Vector3( 0, 0, 0 );		// selectBg2 위치
	
	public List <UISprite> BgList = new List<UISprite> ();	// 배경 스프라이트를 저장할 리스트 
	
	public UISprite selectBg1;								// 스크롤할 배경 객체 
	public UISprite selectBg2;
	
	// 두번째 배경객체에 세로 크기 만큼 y 위치값 추가, play씬일때만 배경 이미지 랜덤 변경. 
	void Start () 
	{
		bgPos2.y += selectBg1.localSize.y;
		selectBg2.transform.localPosition = bgPos2;

		GameObject darkBg = transform.Find ("BG_Dark").gameObject;

		if ( GameData.nowScene == GameSceneState.play ) 
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
		// 배경 이미지 스크롤. GameData.bgSpeed 값 증가하면 배경 이미지 스크롤 속도 향상. -----
		if (Mathf.Abs (bgPos1.y - GameData.bgSpeed) < 960 /*GameData.screenHeight */) 
			bgPos1.y -= GameData.bgSpeed;
		else 
			bgPos1.y = selectBg2.transform.localPosition.y + selectBg2.localSize.y - GameData.bgSpeed;//-1; 

		if (Mathf.Abs (bgPos2.y - GameData.bgSpeed) < 960 /*GameData.screenHeight */) 
			bgPos2.y -= GameData.bgSpeed;
		else 
			bgPos2.y = selectBg1.transform.localPosition.y + selectBg1.localSize.y - GameData.bgSpeed;// -1;
			
		selectBg1.transform.localPosition = bgPos1;
		selectBg2.transform.localPosition = bgPos2;
		//------------------------------------------------------------------------
	}
}
