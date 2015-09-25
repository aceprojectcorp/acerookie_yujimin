using UnityEngine;
using System.Collections;

public class HandlingFeedInterview : MonoBehaviour 
{
	UISprite sprBg ;
	UISprite sprPortrait ; 
	UILabel lbRecordPlay; 
	UILabel lbResultContent; 
	UILabel lbResultMood;
	UILabel lbBtnSelectUp;
	UILabel lbBtnSelectDown;
	UILabel lbBtnSelectCenter;

	GameObject goBtnSelect ;
	
//	List <MVPData> MvpDataList = new List<MVPData>();

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

			case "RecordPlay_Label" :
				lbRecordPlay = child.GetComponent<UILabel>();
				break;

			case "ResultContent_Label" :
				lbResultContent = child.GetComponent<UILabel>();
				break;
				
			case "ResultMood_Label" :
				lbResultMood = child.GetComponent<UILabel>();
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
		lbResultContent.gameObject.SetActive (false);
		lbResultMood.gameObject.SetActive (false);
	}

	void SetTextBtn()
	{
		// win
		if( GameData.Instance.iStraightWinCnt > 0 )
		{
			lbRecordPlay.text = "(2 : 1 승리)";
			lbBtnSelectUp.text 	 	= GameData.Instance.arrStrMsgBtnInterview [0,0];
			lbBtnSelectCenter.text 	= GameData.Instance.arrStrMsgBtnInterview [1,0];
			lbBtnSelectDown.text 	= GameData.Instance.arrStrMsgBtnInterview [2,0];
		}
		else
		{
			lbRecordPlay.text = "(8 : 10 패배)";
			lbBtnSelectUp.text 	 	= GameData.Instance.arrStrMsgBtnInterview [0,1];
			lbBtnSelectCenter.text	= GameData.Instance.arrStrMsgBtnInterview [1,1];
			lbBtnSelectDown.text 	= GameData.Instance.arrStrMsgBtnInterview [2,1];
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// 버튼 누를 경우 결과 처리 
	// mvp버튼에서 버튼 클릭시 바로 이 함수 호출되게 이어놓음 ( 코딩x ) 
	void SetResultPressBtn()
	{
		// mood result 
		int iRand0to1 = Random.Range (0, 2);
		if( iRand0to1 == 0 )
			lbResultMood.text = GameData.Instance.AddColorText(GameData.Instance.arrStrMsgResultInterview[0], "red") ; 
		else
			lbResultMood.text = GameData.Instance.AddColorText(GameData.Instance.arrStrMsgResultInterview[1], "blue") ;

		lbResultContent.gameObject.SetActive (true);
		lbResultMood.gameObject.SetActive (true); 
		Destroy ( goBtnSelect ); 
		UIFeedManager.Instance.DownSizeHeihgtBg ( sprBg, sprPortrait, minusBgHeight );
	}
	 
	public void OnClickBtnUp()
	{
		lbResultContent.text = lbBtnSelectUp.text;
		SetResultPressBtn ();
	}
	public void OnClickBtnCenter()
	{
		lbResultContent.text = lbBtnSelectCenter.text;
		SetResultPressBtn ();
	}
	public void OnClickBtnDown()
	{
		lbResultContent.text = lbBtnSelectDown.text;
		SetResultPressBtn ();
	}


}
