using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// << 오늘의 미션 피드 출력 및 상태에 따른 배경이미지 변경 >>
public class HandlingFeedMissionToday : MonoBehaviour 
{		
	private UILabel	lbTitleContentUp;
	private UILabel lbTitleContentDown;
	private UISprite sprBackground;  
	private UISprite sprPortrait ;  
	private UILabel lbTitleContentColor;

	private GameObject GoBtnReceiveReward ; 
	private GameObject GoMission1;
	private GameObject GoMission2;
	private GameObject GoMission3; 

	private List<UIDrawMission> ListMission = new List<UIDrawMission>();

	string msgMissionClear = "축하합니다!! 오늘의 미션을 모두 완료 하였습니다." ;
	string msgMsReward = "[FF6868FF]▶ 보상으로 스페셜 선수를(을) 획득하였습니다. [-]" ;

	// 비활성화 상태면 못찾음. 처음에 해당 객체를 프리팹내에선 전부 활성화 시키고, start()에서 비활성화 시키기 
	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
		
		foreach (Transform child in transforms)
		{
			switch (child.name)
			{
			case "Title_Color_Label":
				lbTitleContentColor = child.GetComponent<UILabel>();
				break;
				
			case "Title_ContentUp_Label" :
				lbTitleContentUp = child.GetComponent<UILabel>();
				break;
				
			case "Title_ContentDown_Label" :
				lbTitleContentDown = child.GetComponent<UILabel>();
				break;

			case "ReceiveReword_Btn" :
				GoBtnReceiveReward = child.gameObject;
				break;

			case "Portrait_Sprite" :
				sprPortrait = child.GetComponent<UISprite>();
				break;

			case "Clear_Sprite" :
				sprBackground = child.GetComponent<UISprite>();
				break;

			}
		}
	}

	void Awake()
	{
		OnGetChildObject ();
		sprBackground = gameObject.GetComponent<UISprite> ();
	}

	// Use this for initialization
	void Start () 
	{

		OnGetChildMissionObject ();
		//SetMission ();

		// success all mission?
		if ( GameData.Instance.isSuccessAllTodayMission == false )		
			GoBtnReceiveReward.SetActive (false);
		else
			BeforeRewardAtClearTM ();
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
				GoMission1 = child.gameObject;
				ListMission.Add( child.GetComponent<UIDrawMission>() );
				ListMission [ ListMission.Count-1 ].SetData ( GameData.Instance.listMission [ ListMission.Count-1 ] );
				break;
				
			case "Mission2":
				GoMission2 = child.gameObject;
				ListMission.Add( child.GetComponent<UIDrawMission>() );
				ListMission [ ListMission.Count-1 ].SetData ( GameData.Instance.listMission [ ListMission.Count-1 ] );
				break;
				
			case "Mission3" :
				GoMission3 = child.gameObject;
				ListMission.Add( child.GetComponent<UIDrawMission>() );
				ListMission [ ListMission.Count-1 ].SetData ( GameData.Instance.listMission [ ListMission.Count-1 ] );
				break;
			}
			
		}
	}

	// 보상 받기전 화면 처리 - 미션들 비활성화, 보상받기 버튼 활성화, 메인 라벨 텍스트 변경, 배경 이미지 크기 변경 및 피드리스트 위치 재구성 
	void BeforeRewardAtClearTM()
	{		 
		Destroy (GoMission1);
		Destroy (GoMission2);
		Destroy (GoMission3);

		GoBtnReceiveReward.SetActive (true);

		lbTitleContentColor.gameObject.SetActive (false);

		sprBackground.GetComponent<UISprite>().height = (int)( Mathf.Abs (sprPortrait.transform.localPosition.y) + sprPortrait.GetComponent<UISprite> ().localSize.y + 20f );

		// 라벨 x위치 이동해서 재사용 
		Vector3 tmpPos = lbTitleContentUp.transform.localPosition; 
		tmpPos.x = 161f;
		lbTitleContentUp.transform.localPosition = tmpPos;
		lbTitleContentUp.text = msgMissionClear;
		lbTitleContentDown.gameObject.SetActive (false);

		UIFeedManager.Instance.ResetOnlyFeedPos ();


	}

	// 보상버튼 누를 경우 처리 
	public void OnClickGoBtnReceiveReward()
	{
		lbTitleContentDown.gameObject.SetActive (true);
		lbTitleContentDown.text = msgMsReward;
		GameData.Instance.isOffTodayMissionFeed = true;
		GoBtnReceiveReward.SetActive (false);
	}
}
