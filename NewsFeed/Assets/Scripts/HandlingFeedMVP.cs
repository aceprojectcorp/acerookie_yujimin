﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HandlingFeedMVP : MonoBehaviour 
{
	UISprite sprBg ;
	UISprite sprPortrait ; 
	UILabel lbResultContent;
	UILabel lbResultMood;
	UILabel lbBtnSelectUp;
	UILabel lbBtnSelectDown;
	UIButton btnSelectUp;		
	UIButton btnSelectDown;

	GameObject goBtnSelect ;

	//edit name...
	List <MVPData> MvpDataList = new List<MVPData>();

	float minusBgHeight = 20f;

	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
		
		foreach ( Transform child in transforms )
		{
			switch ( child.name )
			{
			case "Portrait_Sprite" :
				sprPortrait = child.GetComponent<UISprite>();
				break;

			case "ResultContent_Label" :
				lbResultContent = child.GetComponent<UILabel>();
				break;
				
			case "ResultMood_Label" :
				lbResultMood = child.GetComponent<UILabel>();
				break;

			case "SelectUp_Label" :
				lbBtnSelectUp = child.GetComponent<UILabel>();
				break; 
				
			case "SelectDown_Label" :
				lbBtnSelectDown = child.GetComponent<UILabel>();
				break;

			case "SelectUp_Spr" :
				btnSelectUp = child.GetComponent<UIButton>();
				break; 
				
			case "SelectDown_Spr" :
				btnSelectDown = child.GetComponent<UIButton>();
				break;
				
			case "SelectBtn" :
				goBtnSelect = child.gameObject;
				break;		

			}
		} 
	}

	void Awake()
	{
		OnGetChildObject ();
		sprBg = gameObject.GetComponent<UISprite> ();
	}

	// Use this for initialization
	void Start () 
	{
		CreateMvpDataList ( MvpDataList );
		lbBtnSelectUp.text 	 = GameData.Instance.arrStrMsgBtnMvp [0];
		lbBtnSelectDown.text = GameData.Instance.arrStrMsgBtnMvp [1];
		lbResultContent.gameObject.SetActive (false);
		lbResultMood.gameObject.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void CreateMvpDataList ( List <MVPData> list )
	{
		list.Add ( new MVPData ( GameData.Instance.arrStrMsgBtnMvp [0] ));
		list.Add ( new MVPData ( GameData.Instance.arrStrMsgBtnMvp [1] ));
	}

	void DownSizeHeightBg()
	{
		sprBg.height = (int)( Mathf.Abs ( sprPortrait.transform.localPosition.y ) + sprPortrait.height + minusBgHeight );
		UIFeedManager.Instance.ResetOnlyFeedPos ();
	}

	// 버튼 누를 경우 결과 처리 
	// mvp버튼에서 버튼 클릭시 바로 이 함수 호출되게 이어놓음 ( 코딩x ) 
	void SetResultPressBtn()
	{
		// mood result 
		int iRand0to1 = Random.Range (0, 2);
		if( iRand0to1 == 0 )
		{
			lbResultMood.text = GameData.Instance.arrStrColor[0] + GameData.Instance.arrStrMsgResultBtnMvp[0] + "[-]" ;
		}
		else
		{
			lbResultMood.text = GameData.Instance.arrStrColor[1] + GameData.Instance.arrStrMsgResultBtnMvp[1] + "[-]" ;
		}

		lbResultContent.gameObject.SetActive (true);
		lbResultMood.gameObject.SetActive (true);
		Destroy ( goBtnSelect ); 
		DownSizeHeightBg ();
	}

	public void OnClickBtnUp()
	{
		lbResultContent.text = GameData.Instance.arrStrMsgBtnMvp [0];
		SetResultPressBtn ();
	}
	public void OnClickBtnDown()
	{
		lbResultContent.text = GameData.Instance.arrStrMsgBtnMvp [1];
		SetResultPressBtn ();
	}


}
