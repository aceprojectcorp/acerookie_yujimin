using UnityEngine;
using System.Collections;

public class HandlingMissionToday : MonoBehaviour 
{	
	public GameObject msTitleContentColor;
	public GameObject msTitleContentUp;
	public GameObject msTitleContentDown;

	public GameObject ms1ContentLabel ;
	public GameObject ms2ContentLabel ;
	public GameObject ms3ContentLabel ;

	public GameObject ms1NowStateLabel ;
	public GameObject ms2NowStateLabel ;
	public GameObject ms3NowStateLabel ;

	public GameObject ms1SucssesSpr ;
	public GameObject ms2SucssesSpr ; 
	public GameObject ms3SucssesSpr ;

	public GameObject msObj1;
	public GameObject msObj2;
	public GameObject msObj3; 

	public GameObject msReceiveRewardBtn ; 
	public GameObject msPortSpr ;  

	public GameObject msBackgroundSpr;

	public GameObject UIManager ; 

	string msgMissionClear = "wowowowowow";
	string msgMsReward = "[FF6868FF]Receive Reward ! [-]";

	bool isClearTM = false;

	// Use this for initialization
	void Start () 
	{
		Debug.Log ( msPortSpr.transform.localPosition );
		ms1ContentLabel.GetComponent<UILabel>().text = GameData.Instance.missionList [0].missionContent;
		ms2ContentLabel.GetComponent<UILabel>().text = GameData.Instance.missionList [1].missionContent;
		ms3ContentLabel.GetComponent<UILabel>().text = GameData.Instance.missionList [2].missionContent;

		ms1NowStateLabel.GetComponent<UILabel> ().text = GameData.Instance.missionList [0].nowSuccVal + "/" + GameData.Instance.missionList [0].fullSuccVal;
		ms2NowStateLabel.GetComponent<UILabel> ().text = GameData.Instance.missionList [1].nowSuccVal + "/" + GameData.Instance.missionList [1].fullSuccVal;
		ms3NowStateLabel.GetComponent<UILabel> ().text = GameData.Instance.missionList [2].nowSuccVal + "/" + GameData.Instance.missionList [2].fullSuccVal;

		msReceiveRewardBtn.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( GameData.Instance.numOfStraightWin  >= 3 && 
		    GameData.Instance.numOfTotalWin 	>= GameData.Instance.missionList[1].fullSuccVal && 
		    GameData.Instance.numOfTotalPlay 	>= GameData.Instance.missionList[2].fullSuccVal )
		{
			if( isClearTM == false )
			{
				isClearTM = true ;
				BeforeRewardAtClearTM();
			}
		}
		else
		{
			// mission 1 suc?
			if( GameData.Instance.numOfStraightWin >= 3 )
			{
				ms1NowStateLabel.SetActive(false);
				ms1SucssesSpr.SetActive(true); 
			}
			else
			{
				ms1NowStateLabel.GetComponent<UILabel> ().text = GameData.Instance.missionList [0].nowSuccVal + "/" + GameData.Instance.missionList [0].fullSuccVal;
			}

			// mission 2 suc?
			if( GameData.Instance.numOfTotalWin >= GameData.Instance.missionList[1].fullSuccVal )
			{
				ms2NowStateLabel.SetActive(false);
				ms2SucssesSpr.SetActive(true);
			}
			else
			{
				GameData.Instance.missionList [1].nowSuccVal = GameData.Instance.numOfTotalWin ; 
				ms2NowStateLabel.GetComponent<UILabel> ().text = GameData.Instance.missionList [1].nowSuccVal + "/" + GameData.Instance.missionList [1].fullSuccVal;
			}

			// mission 3 suc?
			if( GameData.Instance.numOfTotalPlay >= GameData.Instance.missionList[2].fullSuccVal )
			{
				ms3NowStateLabel.SetActive(false);
				ms3SucssesSpr.SetActive(true);
			}
			else
			{
				GameData.Instance.missionList [2].nowSuccVal = GameData.Instance.numOfTotalPlay;
				ms3NowStateLabel.GetComponent<UILabel> ().text = GameData.Instance.missionList [2].nowSuccVal + "/" + GameData.Instance.missionList [2].fullSuccVal;
			}



		}
	}

	void BeforeRewardAtClearTM()
	{
		msObj1.SetActive (false);
		msObj2.SetActive (false);
		msObj3.SetActive (false); 

		msReceiveRewardBtn.SetActive (true);

		msTitleContentColor.SetActive (false);

		Vector3 tmpPos = Vector3.zero;
		tmpPos = msBackgroundSpr.GetComponent<UISprite>().localSize;

		tmpPos.y = ( Mathf.Abs (msPortSpr.transform.localPosition.y) + msPortSpr.GetComponent<UISprite> ().localSize.y + 20f );
		Debug.Log (tmpPos);
		msBackgroundSpr.GetComponent<UISprite>().height = (int)tmpPos.y; 

		tmpPos = msTitleContentUp.transform.localPosition; 
		tmpPos.x = 161f;
		msTitleContentUp.transform.localPosition = tmpPos;

		UIManager.GetComponent<UIManager> ().ResetOnlyFeedPos ();

		msTitleContentUp.GetComponent<UILabel> ().text = msgMissionClear;
	}

	public void OnClickReceiveRewardBtn()
	{
		msTitleContentDown.GetComponent<UILabel> ().text = msgMsReward;
		GameData.Instance.isSucssesTodayMs = true;
		msReceiveRewardBtn.SetActive (false);
	}
}
