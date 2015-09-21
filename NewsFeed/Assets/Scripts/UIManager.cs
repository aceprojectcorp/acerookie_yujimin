using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO : 모든 피드 리스트를 처음부터 가지지 말고 조건이 만족할 경우 피드 생성하기 
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

	// 피드목록 재구성 
	public void UpdateFeedList()
	{
		// Clear nowEnableFeedList -> nowEnableFeedList.Add ( allFeedList[0] ); (TodayFeed).
		ResetNewsFeedList ();

		// add nowEnableFeedList 
		// 오늘의 미션이 완료 유무에 따라 인덱스 시작 번호가 달라짐 
		int idxFeedList = 0 ;

		if( GameData.Instance.isSucssesTodayMs == false )
		{
			nowEnableFeedList.Add ( allFeedList[idxFeedList] );	// add todayMission
			SettingFeed ( nowEnableFeedList [idxFeedList], idxFeedList );
			idxFeedList++;
		}

		// MVP Feed 추가 현재 승리하면 - 100% 나옴 
		if( GameData.Instance.numOfStraightWin >= 1 )
		{
			nowEnableFeedList.Add ( allFeedList [1] ) ; // allFeedList[ SearchFeedFromAllFeedList("FeedOfMVP") ] ); //allFeedList [1] );
			SettingFeed ( nowEnableFeedList [idxFeedList], idxFeedList );
			idxFeedList++;
		}

		// Power Analysis
		nowEnableFeedList.Add ( allFeedList [allFeedList.Count - 1] );
		SettingFeed ( nowEnableFeedList [idxFeedList], idxFeedList );


	}
	
	// 모든 피드들 비활성화 후, 오늘의미션피드가 완료되지 않았다면 추가.
	void ResetNewsFeedList()
	{
		for(int i = 0 ; i < allFeedList.Count ; i++ )
		{
			allFeedList[i].SetActive( false );
		}

		nowEnableFeedList.Clear ();

		if( GameData.Instance.isSucssesTodayMs == false )
		{
			nowEnableFeedList.Add ( allFeedList[0] );	// add todayMission
			SettingFeed ( nowEnableFeedList [0], 0 );
		}
	}

	// 피드들 위치만 초기화 
	public void ResetOnlyFeedPos()
	{
		for(int i=0 ; i < nowEnableFeedList.Count ; i++ )
		{
			SettingFeed( nowEnableFeedList[i], i );
		}
	}

	// 피드들 활성화 및 위치 값 조정 
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

	// 해당이름의 피드를 찾아서 인덱스 값 반환 
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
