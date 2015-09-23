using UnityEngine;
using System.Collections;

public class HandlingFeedMVP : MonoBehaviour 
{
	UILabel lbTitleMood;
	UILabel lbSelectUpBtn;
	UILabel lbSelectDownBtn;
	UILabel lbResultContent;
	UILabel lbResultMood;

	GameObject GoBtnSelect ;

	bool isSelectBtn = false ;

	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
		
		foreach (Transform child in transforms)
		{
			switch (child.name)
			{
			case "TitleMood_Label":
				lbTitleMood = child.GetComponent<UILabel>();
				break;
				
			case "SelectAnswarUp_Label" :
				lbSelectUpBtn = child.GetComponent<UILabel>();
				break;
				
			case "SelectAnswarDown_Label" :
				lbSelectDownBtn = child.GetComponent<UILabel>();
				break;
				
			case "ResultContent_Label" :
				lbResultContent = child.GetComponent<UILabel>();
				break;
				
			case "ResultMood_Label" :
				lbResultMood = child.GetComponent<UILabel>();
				break;

			case "SelectAnsear_Btn" :
				GoBtnSelect = child.gameObject;
				break;				
			}
		}
	}
	void Awake()
	{
		OnGetChildObject ();
		lbResultContent.gameObject.SetActive (false);
		lbResultMood.gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void OnClickBtn()
	{
		isSelectBtn = true;
		lbResultContent.gameObject.SetActive (true);
		lbResultMood.gameObject.SetActive (true);

		GoBtnSelect.SetActive (false);
	}

}
