using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandlingFeedStraightLose : MonoBehaviour 
{
	UISprite sprBg ;
	UISprite sprPortrait ; 
	UILabel lbContentUp; 
	UILabel lbContentDown;
	UILabel lbContentResult; 
	UILabel lbBtnSelectUp;
	UILabel lbBtnSelectDown;
	UILabel lbBtnSelectCenter;

	GameObject goBtnSelect ;

	float minusBgHeight = 30f;

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

			case "ContentUp_Label" :
				lbContentUp = child.GetComponent<UILabel>();
				break;

			case "ContentDown_Label" :
				lbContentDown = child.GetComponent<UILabel>();
				break;		

			case "ContentResult_Label" :
				lbContentResult = child.GetComponent<UILabel>();
				break;

			case "SelectUp_Spr" :
				lbBtnSelectUp = child.FindChild("Select_Label").GetComponent<UILabel>();
				break; 
				
			case "SelectCenter_Spr" :
				lbBtnSelectDown = child.FindChild("Select_Label").GetComponent<UILabel>();
				break;

			case "SelectDown_Spr" :
				lbBtnSelectCenter = child.FindChild("Select_Label").GetComponent<UILabel>();
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
		SetTextBtn ();
		lbContentResult.gameObject.SetActive (false);
	}

	void SetTextBtn()
	{
		lbContentUp.text 		= GameData.Instance.iStraightLoseCnt + "연패에 빠졌습니다.\n선수단의 분위기가 침체된 상태입니다.";
		lbContentDown.text 		= "(래더 최근 "+ GameData.Instance.iStraightLoseCnt + "연패)";
		lbBtnSelectUp.text 	 	= GameData.Instance.arrStrContentStraightLose [0];
		lbBtnSelectCenter.text 	= GameData.Instance.arrStrContentStraightLose [1];
		lbBtnSelectDown.text 	= GameData.Instance.arrStrContentStraightLose [2];
	}
 
	// Update is called once per frame
	void Update () 
	{
	
	}

	// 버튼 누를 경우 결과 처리 
	public void SetResultPressBtn()
	{
		int iRand0to1 = Random.Range (0, 2);
		if( iRand0to1 == 0 )
			lbContentResult.text = GameData.Instance.AddColorText( GameData.Instance.arrStrMsgResultStraightLose[0], "red" ) ; 
		else
			lbContentResult.text = GameData.Instance.AddColorText( GameData.Instance.arrStrMsgResultStraightLose[1], "blue" ) ;

		lbContentResult.gameObject.SetActive (true);
		Destroy ( goBtnSelect ); 
		UIFeedManager.Instance.DownSizeHeihgtBg ( sprBg, sprPortrait, minusBgHeight );
	}

}
