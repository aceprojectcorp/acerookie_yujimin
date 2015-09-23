using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// << 화면에 보일 피드 리스트를 조건에 따라 재구성 >> 
public class UIFeedManager : MonoBehaviour 
{	
	public GameObject Camera03 ;		// 팝업창 활성화 하기 위해 연결 
	public GameObject GoScrollView ;	// 피드를 추가할때 부모 객체로 사용하기 위해 
	
	float fSpaceYBetweenFeed = 5f ;		// 피드간 세로 간격  
	int iWidthBgFeed = 800 ;			// 피드의 가로 크기(고정) 	
	
	public List <GameObject> goListEnableFeed = new List<GameObject>();	// 조건에 따라 화면에 보일 피드들 저장 	// 테스트 끝나면 public 없얘주기 
	
	private static UIFeedManager m_instance;
	public static UIFeedManager Instance
	{
		get
		{
			return m_instance;
		}
	}

	void Awake()
	{		
		if (m_instance == null)
			m_instance = this;
	}

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
		// 스크롤 가장 위로 이동 
		GoScrollView.transform.localPosition = Vector3.zero;
		GoScrollView.GetComponent<UIPanel> ().clipOffset = Vector3.zero;

		// goListEnableFeed내 객체를 destory()하고, goListEnableFeed를 Clear함.
		ResetNewsFeedList ();

		// add goListEnableFeed (if문의 순서를 조정하면, 피드 리스트 안에서 순위를 바꿀 수 있음)
		int idxFeedList = 0 ;

		// 오늘의미션 피드 
		if( GameData.Instance.isOffTodayMissionFeed == false )
		{ 
			CreateInsAndAddList( "FeedOfMissionToday", 270, idxFeedList ); 

			idxFeedList++;
		}

		// MVP 피드 - 추가 현재 승리하면 - 100% 나옴 
		if( GameData.Instance.iStraightWinCnt >= 1 )
		{
			CreateInsAndAddList( "FeedOfMVP", 180, idxFeedList ); 

			idxFeedList++;
		}

		// 전력분석 피드 
		CreateInsAndAddList( "FeedOfPowerAnalysis", 300, idxFeedList ); 


	}

	// 피드 인스턴스 추가 및 초기 셋팅 후, goListEnableFeed에 추가
	void CreateInsAndAddList( string prepabName, int height, int idx )
	{
		GameObject ins = Instantiate(Resources.Load(prepabName)) as GameObject;
		ins.transform.parent = GoScrollView.transform ; 
		ins.GetComponent<UISprite> ().MakePixelPerfect ();
		ins.GetComponent<UISprite>().width  = iWidthBgFeed ;	// 800
		ins.GetComponent<UISprite>().height = height ;
		SettingFeed ( ins, idx );
		
		goListEnableFeed.Add ( ins );
		
	}

	// 모든 피드들 파괴 및 goListEnableFeed 비움.
	void ResetNewsFeedList()
	{
		for( int i=0 ; i < goListEnableFeed.Count ; i++ )
		{
			Destroy( goListEnableFeed[i] );
		}

		goListEnableFeed.Clear ();
	}


	// 피드들 활성화 및 위치만 초기화 
	public void ResetOnlyFeedPos()
	{
		for(int i=0 ; i < goListEnableFeed.Count ; i++ )
		{
			SettingFeed( goListEnableFeed[i], i );
		}
	}

	// 피드들 활성화 및 위치 값 조정 
	void SettingFeed( GameObject obj, int myIdx )
	{
		obj.SetActive (true);
		if( myIdx != 0 )
		{
			Vector3 objPos = goListEnableFeed [myIdx - 1].transform.localPosition ;
			objPos.y -= goListEnableFeed [myIdx - 1].GetComponent<UISprite> ().height + fSpaceYBetweenFeed;
			obj.transform.localPosition = objPos;
		}
		else
		{
			obj.transform.localPosition = new Vector3( -400f, 165, 0 );
		}
	}

	// 시뮬레이션 팝업 띄움(활성화)
	public void OnClickSimulBtn()  
	{
		Camera03.SetActive (true); 
	}
	
	/*
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
	*/
}
