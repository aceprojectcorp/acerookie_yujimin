﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandlingFeedMVP : MonoBehaviour 
{
	private UISprite	sprBg ;
	private UISprite 	sprPortrait ; 
	private UILabel 	lbResultContent;
	private UILabel 	lbResultMood;
	private UILabel 	lbBtnSelectUp;
	private UILabel 	lbBtnSelectDown;

	private float 		fDownScaleHeightBg = 20f;

	private GameObject 	goBtnSelect ;
	
	private List <MVPData> MvpDataList = new List<MVPData>();
	

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

	// 버튼 누를 경우 결과 처리 
	void SetResultPressBtn()
	{
		// mood result 
		int iRand0to1 = Random.Range (0, 2);
		if (iRand0to1 == 0) 
			lbResultMood.text = GameData.Instance.AddColorText (GameData.Instance.arrStrMsgResultMvp [0], "red"); 
		else 
			lbResultMood.text = GameData.Instance.AddColorText (GameData.Instance.arrStrMsgResultMvp [1], "blue");

		lbResultContent.gameObject.SetActive (true);
		lbResultMood.gameObject.SetActive (true);
		Destroy ( goBtnSelect ); 
		UIFeedManager.Instance.DownSizeHeihgtBg ( sprBg, sprPortrait, fDownScaleHeightBg );
	}
	 
	public void OnClickBtnUp()
	{
		lbResultContent.text = MvpDataList [0].GetStrContent ();//GameData.Instance.arrStrMsgBtnMvp [0];
		SetResultPressBtn ();
	}
	public void OnClickBtnDown()
	{
		lbResultContent.text = MvpDataList [1].GetStrContent (); //GameData.Instance.arrStrMsgBtnMvp [1];
		SetResultPressBtn ();
	}
}
