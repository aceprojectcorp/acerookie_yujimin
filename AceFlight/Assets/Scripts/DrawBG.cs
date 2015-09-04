using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawBG : MonoBehaviour {

	Vector3 bgPos = new Vector3( 0, 0, 0 );				// 배경 최초 위치  
	Vector3 posPlusSizeY = new Vector3( 0, 0, 0 );

	public List <UISprite> BgList = new List<UISprite> ();
	
	public UISprite selectBg1;							// 배경. 이동테스트 
	public UISprite selectBg2;


	// Use this for initialization
	void Start () 
	{

		selectBg1.gameObject.SetActive (true);
		selectBg2.gameObject.SetActive (true);

		posPlusSizeY.y += selectBg2.localSize.y;
		selectBg2.transform.localPosition = posPlusSizeY;

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


	}
	
	// Update is called once per frame
	void Update () 
	{
		bgMove( selectBg1 );
		bgMove( selectBg2 );		
	}

	void bgMove( UISprite bg )
	{
		//		Debug.Log ( "bg.pixelSize " + bg.pixelSize );
		//		Debug.Log ( "bg.transform.localScale : " + bg.transform.localScale );
		//		Debug.Log ( "bg.transform.lossyScale : " + bg.transform.lossyScale );
		//		Debug.Log ( "bg.localSize" + bg.localSize );
		//		Debug.Log ( "bg.transform.position" + bg.transform.position );
		//		Debug.Log ( "bg.transform.localPosition : " + bg.transform.localPosition );
				
		
		if ( -(bg.localSize.y) >= bg.transform.localPosition.y ) 
		{ 	
			bgPos.y = bg.localSize.y - ( GameData.bgSpeed * 1000f);
			bg.transform.localPosition = bgPos ;
		} 
		else 
		{
			bgPos = bg.transform.position;
			bgPos.y -= GameData.bgSpeed;
			bg.transform.position = bgPos;
		}
		
		
		
	}

}
