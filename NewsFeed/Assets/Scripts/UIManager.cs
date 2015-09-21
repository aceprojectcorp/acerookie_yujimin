using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// << 화면에 보일 피드 리스트를 조건에 따라 재구성 >> 
public class UIManager : MonoBehaviour {

	public List <GameObject> allFeedList = new List<GameObject>();			// 모든 피드들 저장
	public List <GameObject> nowEnableFeedList = new List<GameObject>();	// 조건에 따라 화면에 보일 피드들 저장 

	public float spaceYBetweenFeed = 5f ;


	// Use this for initialization
	void Start () 
	{
		UpdateFeedList ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateFeedList()
	{
		// Clear nowEnableFeedList -> nowEnableFeedList.Add ( allFeedList[0] ); (TodayFeed).
		ResetNewsFeedList ();

		// add nowEnableFeedList 
		int idx = 1; 

		// add MVP Feed - 100%
		if( GameData.Instance.numOfStraightWin >= 1 )
		{
			nowEnableFeedList.Add ( allFeedList [1] ) ; // allFeedList[ SearchFeedFromAllFeedList("FeedOfMVP") ] ); //allFeedList [1] );
			SettingFeed ( nowEnableFeedList [idx], idx );
			idx++;
		}

		// Power Analysis
		nowEnableFeedList.Add ( allFeedList [allFeedList.Count - 1] );
		SettingFeed ( nowEnableFeedList [idx], idx );


	}

	//um... add todaymission?...um....
	void ResetNewsFeedList()
	{
		for(int i = 0 ; i < allFeedList.Count ; i++ )
		{
			allFeedList[i].SetActive( false );
		}

		nowEnableFeedList.Clear ();
		nowEnableFeedList.Add ( allFeedList[0] );	// add todayMission
		SettingFeed ( nowEnableFeedList [0], 0 );
	}

	// set posision,.....um...
	void SettingFeed( GameObject obj, int myIdx )
	{
		obj.SetActive (true);
		if( myIdx != 0 )
		{
			Vector3 objPos = nowEnableFeedList [myIdx - 1].transform.localPosition ;
			objPos.y -= nowEnableFeedList [myIdx - 1].GetComponent<UISprite> ().height + spaceYBetweenFeed;
			obj.transform.localPosition = objPos;
		}
		else
		{
			obj.transform.localPosition = new Vector3( -400f, 165, 0 );
		}
	}

	int SearchFeedFromAllFeedList( string feedName )
	{
		for( int i=0 ; i < allFeedList.Count ; i++ ) 
		{
			if( allFeedList[i].name == feedName )
				return i;
		}

		return 0;
	}
}
