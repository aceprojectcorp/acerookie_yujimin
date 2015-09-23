using UnityEngine;
using System.Collections;

// << 미션 내용을 받아 그려줌 >>
public class UIDrawMission : MonoBehaviour 
{
	private MissionData mData; 
	private UILabel lbContent;
	private UILabel	lbSuccMissionCnt ;
	private UISprite sprClear;
	bool isSuccessMission = false;

	// 미션 데이터를 받아서 내부 객체에 값을 분배하고, 미션 성공여부 판단함. 
	public void SetData( MissionData data )
	{
		mData = data;

		lbContent.text 		  = mData.missionContent;
		lbSuccMissionCnt.text = mData.nowSuccVal + "/" + mData.fullSuccVal;

		if( mData.fullSuccVal <= mData.nowSuccVal )
		{
			isSuccessMission = true;
		}
	}

	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
		
		foreach (Transform child in transforms)
		{
			switch (child.name)
			{
			case "Content_Label":
				lbContent = child.GetComponent<UILabel>();
				break;

			case "SuccMissionCnt_Label" :
				lbSuccMissionCnt = child.GetComponent<UILabel>();
				break;

			case "Clear_Sprite" :
				sprClear = child.GetComponent<UISprite>();
				break;

			}
		}
	}

	void Awake()
	{
		OnGetChildObject ();
		sprClear.gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () 
	{
		// 미션 성공시 미션성공 이미지 보여줌. 미션상황 라벨은 비활성화. 
		if( isSuccessMission == true )
		{
			sprClear.gameObject.SetActive(true);
			lbSuccMissionCnt.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
