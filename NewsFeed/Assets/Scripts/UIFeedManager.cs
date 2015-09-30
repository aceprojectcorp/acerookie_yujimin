using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// << 화면에 보일 피드 리스트를 조건에 따라 재구성, 피드 세로 크기 변경 >> 
public class UIFeedManager : MonoBehaviour 
{	
	public GameObject goCamera03 ;				// 팝업창 활성화 하기 위해 연결 
	public GameObject goScrollView ;			// 피드를 추가할때 부모 객체로 사용하기 위해 

	// 오늘의 미션 피드 관련 변수 
	public bool isOffFeedMission 	 = false;	// 오늘의 미션 활성화 여부
	public bool isSuccessAllMission	 = false;	// 오늘의 미션이 모두 클리어 되었는지 여부

	// 피드 활성화 확률
	private int iEnableRateMVP 		 = 50 ; 
	private int iEnableRateInterview = 50 ; 
	private int iEnableRateTiredness = 50 ; 
	private int iEnableRateInjury 	 = 30 ; 

	float 	fSpaceBetweenFeed 		 = 5f ;		// 피드간 세로 간격  
	int 	iWidthBgFeed 			 = 800 ;	// 피드의 가로 크기(고정) 	
	
	public List <GameObject> goFeedEnableList = new List<GameObject>();	// 화면에 보일 피드 리스트
	
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
	// (if문의 순서를 조정하면, 피드 리스트 안에서 순위를 바꿀 수 있음)
	public void UpdateFeedList()
	{
		// goFeedEnableList내 객체를 destory()하고, goFeedEnableList를 Clear함.
		ResetNewsFeedList ();

		int iIdxFeedList = 0 ;

		// 오늘의미션 피드 
		if( UIFeedManager.Instance.isOffFeedMission == false )
		{ 
			CreateInsAndAddList( "FeedMissionToday", 270, iIdxFeedList ); 	// 피드 인스턴스 추가 및 초기 셋팅 후, goFeedEnableList에 추가
			iIdxFeedList++;
		}

		if( GameData.Instance.iTotalPlayCnt > 0 )
		{
			// MVP 피드 
			int iRand1to100MVP = Random.Range (1, 101);
			if( GameData.Instance.iStraightWinCnt > 0 && iEnableRateMVP >= iRand1to100MVP )
			{
				CreateInsAndAddList( "FeedMVP", 180, iIdxFeedList ); 
				iIdxFeedList++;
			}

			// 인터뷰 피드
			int iRand1to100Interview = Random.Range (1, 101);
			if( iEnableRateInterview >= iRand1to100Interview )
			{
				CreateInsAndAddList( "FeedInterview", 210, iIdxFeedList ); 
				iIdxFeedList++;
			}

			// 피곤함 피드
			int iRand1to100Tiredness = Random.Range (1, 101);
			if( iEnableRateTiredness >= iRand1to100Tiredness )
			{
				CreateInsAndAddList( "FeedTiredness", 170, iIdxFeedList ); 
				iIdxFeedList++;
			}

			// 부상 피드
			int iRand1to100Injury = Random.Range (1, 101);
			if( iEnableRateInjury >= iRand1to100Injury )
			{
				CreateInsAndAddList( "FeedInjury", 170, iIdxFeedList ); 
				iIdxFeedList++;
			}

			// 연패 피드
			if( GameData.Instance.iStraightLoseCnt >= 3 )
			{
				CreateInsAndAddList( "FeedStraightLose", 230, iIdxFeedList ); 
				iIdxFeedList++;
			}
		}

		// 전력분석 피드 
		CreateInsAndAddList( "FeedPowerAnalysis", 300, iIdxFeedList ); 


	}

	// 피드 인스턴스 추가 및 초기 셋팅 후, goFeedEnableList에 추가
	void CreateInsAndAddList( string prepabName, int height, int iIdx )
	{
		GameObject goIns = Instantiate( Resources.Load( prepabName )) as GameObject;
		goIns.transform.parent = goScrollView.transform ; 
		goIns.GetComponent<UISprite> ().MakePixelPerfect ();
		goIns.GetComponent<UISprite>().width  = iWidthBgFeed ;	// 800
		goIns.GetComponent<UISprite>().height = height ;
		SettingPosFeed ( goIns, iIdx );
		
		goFeedEnableList.Add ( goIns );
		
	}

	// 모든 피드들 파괴 및 goFeedEnableList 비움.
	void ResetNewsFeedList()
	{
		// 스크롤 가장 위로 이동 
		goScrollView.transform.localPosition = Vector3.zero;
		goScrollView.GetComponent<UIPanel> ().clipOffset = Vector3.zero;

		for( int i=0 ; i < goFeedEnableList.Count ; i++ )
		{
			Destroy( goFeedEnableList[i] );
		}

		goFeedEnableList.Clear ();
	}

	// 피드들 활성화 및 위치만 초기화 
	public void ResetOnlyFeedPos()
	{
		for(int i=0 ; i < goFeedEnableList.Count ; i++ )
		{
			SettingPosFeed( goFeedEnableList[i], i );
		}
	}

	// 피드들 활성화 및 위치 값 조정 
	void SettingPosFeed( GameObject goFeed, int iMyIdx )
	{
		goFeed.SetActive (true);
		// 맨 상단에 위치하는 피드가 아닐 경우, 부모 배경 이미지의 fSpaceBetweenFeed만큼의 공백을 가지고 아래에 생성된다.
		if( iMyIdx != 0 )
		{
			Vector3 objPos 	=  goFeedEnableList [iMyIdx -1].transform.localPosition ;
			objPos.y 		-= goFeedEnableList [iMyIdx -1].GetComponent<UISprite> ().height + fSpaceBetweenFeed;
			goFeed.transform.localPosition = objPos;
		}
		else
		{
			goFeed.transform.localPosition = new Vector3( -400f, 165, 0 );
		}
	}

	// 시뮬레이션 팝업 띄움(활성화)
	public void OnClickSimulBtn()  
	{
		goCamera03.SetActive (true); 
	}

	// 피드의 배경이미지 높이 변경. 초상화 밑 위치를 기준으로 minusHeightBg만큼 배경 세로 길이가 변경됨.
	// 배경과 초상화 이미지의 pivot이 좌측 상단일 경우에만 사용가능. 
	// minusHeightBg를 많이 줄 경우 오히려 배경 이미지 세로 길이가 더 늘어날 수 있음. 
	public void DownSizeHeihgtBg( UISprite sprBg, UISprite sprPort, float fDownScaleHeightBg )
	{
		sprBg.height = (int)( Mathf.Abs ( sprPort.transform.localPosition.y ) + sprPort.height + fDownScaleHeightBg );
		ResetOnlyFeedPos ();
	}

	//개별 미션 관련 변수 카운트 및 모든 미션 성공여부 확인
	public void CheckMissionProgress()
	{
		// success all todaymission cnt! 
		int successMissionCnt = 0; 

		foreach( MissionData mData in GameData.Instance.MissionDataList )
		{
			switch( mData.mType )
			{
			case MissionType.MISSION_TYPE_STRAIGHT_WIN : 
				if( GameData.Instance.iStraightWinCnt >= 3 )
					mData.iNowSuccCnt++;
				break;			
			case MissionType.MISSION_TYPE_TOTAL_WIN : 
				if( GameData.Instance.iStraightWinCnt > 0 )
					mData.iNowSuccCnt++;
				break;
			case MissionType.MISSION_TYPE_TOTAL_PLAY : 
				if( GameData.Instance.iTotalPlayCnt > 0 )
					mData.iNowSuccCnt++;
				break;
			}
			if( mData.iNowSuccCnt >= mData.iFullSuccCnt )
				successMissionCnt++; 
		}
		
		if ( successMissionCnt >= GameData.Instance.MissionDataList.Count )
			isSuccessAllMission = true;
	}
	
}
