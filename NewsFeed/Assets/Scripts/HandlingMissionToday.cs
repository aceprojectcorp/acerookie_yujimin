using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO : 오늘의 미션 순서 수정해주기. 무조건 1번은 아니고... 그냥 인덱스값 순서에 따라 바뀔 수 있게 수정하기  
// public minus!!!!!!!!!!!!!!!!!!!!!!! 
// << 오늘의 미션 피드 출력 및 상태에 따른 배경이미지 변경 >>
public class HandlingMissionToday : MonoBehaviour 
{	
	public GameObject UIManager ; 
	
	private UILabel TitleContentUp;
	private UILabel TitleContentDown;
	private UISprite BackgroundSpr;  
	private UISprite PortSpr ;  

	private GameObject TitleContentColor;
	private GameObject ReceiveRewardBtn ; 
	private GameObject Mission1;
	private GameObject Mission2;
	private GameObject Mission3;

	private List<UIDrawMission> ListMission = new List<UIDrawMission>();

	string msgMissionClear = "wowowowowow";
	string msgMsReward = "[FF6868FF]Receive Reward ! [-]";

	bool isClearTM = false; 

	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
		
		foreach (Transform child in transforms)
		{
			switch (child.name)
			{
			case "Title_Color_Label":
				TitleContentColor = child.gameObject;
				break;
				
			case "Title_ContentUp_Label" :
				TitleContentUp = child.GetComponent<UILabel>();
				break;
				
			case "Title_ContentDown_Label" :
				TitleContentDown = child.GetComponent<UILabel>();
				break;

			case "ReceiveReword_Btn" :
				ReceiveRewardBtn = child.gameObject;
				break;

			case "Portrait_Sprite" :
				PortSpr = child.GetComponent<UISprite>();
				break;

			case "Clear_Sprite" :
				BackgroundSpr = child.GetComponent<UISprite>();
				break;

			}
		}
	}



	void Awake()
	{
		OnGetChildObject ();
		BackgroundSpr = gameObject.GetComponent<UISprite> ();
	}

	// Use this for initialization
	void Start () 
	{
		OnGetChildMissionObject ();
		//SetMission ();

		ReceiveRewardBtn.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	private void OnGetChildMissionObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
		
		foreach (Transform child in transforms)
		{
			switch ( child.name )
			{
			case "Mission1":
				Mission1 = child.gameObject;
				ListMission.Add( child.GetComponent<UIDrawMission>() );
				ListMission [ ListMission.Count-1 ].SetData ( GameData.Instance.listMission [ ListMission.Count-1 ] );
				break;
				
			case "Mission2":
				Mission2 = child.gameObject;
				ListMission.Add( child.GetComponent<UIDrawMission>() );
				ListMission [ ListMission.Count-1 ].SetData ( GameData.Instance.listMission [ ListMission.Count-1 ] );
				break;
				
			case "Mission3" :
				Mission3 = child.gameObject;
				ListMission.Add( child.GetComponent<UIDrawMission>() );
				ListMission [ ListMission.Count-1 ].SetData ( GameData.Instance.listMission [ ListMission.Count-1 ] );
				break;
			}
			
		}
	}

	// 보상 받기전 화면 처리 - 미션들 비활성화, 보상받기 버튼 활성화, 메인 라벨 텍스트 변경, 배경 이미지 크기 변경 및 피드리스트 위치 재구성 
	void BeforeRewardAtClearTM()
	{		 
		Destroy (Mission1);
		Destroy (Mission2);
		Destroy (Mission3);

		ReceiveRewardBtn.SetActive (true);

		TitleContentColor.SetActive (false);

		BackgroundSpr.GetComponent<UISprite>().height = (int)( Mathf.Abs (PortSpr.transform.localPosition.y) + PortSpr.GetComponent<UISprite> ().localSize.y + 20f );

		Vector3 tmpPos = TitleContentUp.transform.localPosition; 
		tmpPos.x = 161f;
		TitleContentUp.transform.localPosition = tmpPos;

		UIManager.GetComponent<UIManager> ().ResetOnlyFeedPos ();

		TitleContentUp.GetComponent<UILabel> ().text = msgMissionClear;
	}

	// 보상버튼 누를 경우 처리 
	public void OnClickReceiveRewardBtn()
	{
		TitleContentDown.GetComponent<UILabel> ().text = msgMsReward;
		GameData.Instance.isOffTodayMissionFeed = true;
		ReceiveRewardBtn.SetActive (false);
	}
}
