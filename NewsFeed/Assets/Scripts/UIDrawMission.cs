using UnityEngine;
using System.Collections;

public class UIDrawMission : MonoBehaviour 
{
	private MissionData m_data;
	private UILabel lbContent;
	private UILabel	lbSuccMissionCnt ;
	private UISprite sprClear;

	public void SetData( MissionData data )
	{
		m_data = data;

		lbContent.text = m_data.missionContent;
		lbSuccMissionCnt.text = m_data.nowSuccVal + "/" + m_data.fullSuccVal;
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

			//case "Mission1":
			//	ListMission.Add(child.GetComponent<UIDrawMission>());
			//	break;
			}
		}
	}

	void Awake()
	{
		OnGetChildObject ();
	}

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
