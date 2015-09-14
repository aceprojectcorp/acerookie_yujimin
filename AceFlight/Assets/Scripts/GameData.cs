using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 게임 씬 위치 저장  
public enum GameSceneState { init, title, play, result }

// 플레이어 상태. 플레이어가 죽을 경우 죽음 이펙트처리를 위해 dead상태를 이용함. 
public enum PlayerState { play, dead } 

// << 게임에서 저장되어 사용되는 데이터들 >>
public class GameData: MonoBehaviour
{
	private static GameData m_instance;
	public static GameData Instance
	{
		get
		{
			return m_instance;
		}
	}

	// 점수 관련 
	public int	g_scGoldFromStage 	= 0;	// 현재(or 이전)씬 획득골드
	public int	g_scHitMonFromStage	= 0;	//		"		 타격에 대한 점수 
	public int	g_scDistFromStage 	= 0;	// 		"		 누적 이동거리 
	public int	g_scBestHighTotal 	= 0; 	// 최대 점수

	// 속도 관련 
	public int 	 g_pixelPerFrame 	= 2;	 
	public int 	 g_pixelPerMeter 	= 10;	// 거리 측정 기준.	 
	public int 	 g_framePerSec 		= 60;
	public float g_nowGameSpeed 	= 2f;	// 현재 게임 속도.  
	public float g_createMonMeter 	= 60 ;  

	// 미사일 
	public float g_msiMovePixelPerFrame = 64;	// 미사일 1프레임당 이동 픽셀 

	// 운석 
	public int	g_meterOfEnterMeteor = 100 ; 	// 운석 등장 미터  
	public int	g_enterMeteorPersent = 50 ; 	// 운석 등장 확률 

	public float g_screenWidth = 0f;			// 화면크기 저장. 현재 title씬에서 초기화 
	public float g_screenHeight = 0f;
	
	public GameSceneState g_nowScene = GameSceneState.init ; 
	public PlayerState g_playerState = PlayerState.play;

	public int g_idxCM = 0;  					// 현재 누적 거리에 해당하는 g_infoForChangeMeter 테이블의 세로 인덱스값을 저장하는 변수 
	public int g_idxMsiPower = 0 ; 				// 현재 누적 거리에 해당하는 g_infoMsiPowerPerMeter 테이블의 세로 인덱스값을 저장하는 변수 

	// 골드 정보 테이블 
	public int[,] g_infoMoney = new int[,]
//		확률		상승점수  
	{ 	{ 80,	10 },	// 동전	
		{ 14,	20 },	// 분홍보석
		{ 5,	30 },	// 초록보석
		{ 1,	40 }	// 파랑보석
	};

	// 골드 스프라이트 이름 정보 테이블 
	public string[] g_moneySprNames = new string[]
//	동전				분홍보석			초록보석			파랑보석
	{ "item_coin", 	"item_gem", 	"item_gem_02", 	"item_gem_03" };
	
	// 누적 거리에 따른 스크롤 속도 테이블 
	public int[][] g_infoForChangeMeter = new int[][]
//					거리(m)		스크롤속도		레벨		<-등장확률	레벨		<- 등장확률 
	{ 	new int[]{ 100, 		100},//, 		1, 		70,			2, 		30 					},
		new int[]{ 200, 		120},//, 		1, 		30,			2, 		70 					},
		new int[]{ 300, 		140},//, 		2, 		70,			3, 		30 					},
		new int[]{ 400, 		160},//, 		2, 		30,			3, 		70 					},
		new int[]{ 500, 		180},//, 		3, 		70,			4, 		30 					},
		new int[]{ 600, 		200},//, 		3, 		30,			4, 		70 					},
		new int[]{ 700, 		220},//, 		4, 		70,			5, 		30 					},
		new int[]{ 800, 		240},//, 		4, 		30,			5, 		70 					},
		new int[]{ 900, 		260},//, 		5, 		70,			6, 		30 					},
		new int[]{ 1000,		280},//, 		5, 		30,			6, 		70 					},
		new int[]{ 1500,		300},//, 		6, 		70,			7, 		20,		8,		10 	}
		new int[]{ 2000,		350},//, 		6, 		70,			7, 		20,		8,		10 	}
		new int[]{ 2500,		400},//, 		6, 		70,			7, 		20,		8,		10 	}
		new int[]{ 3000,		450},//, 		6, 		70,			7, 		20,		8,		10 	}
		new int[]{ 3001,		500}//, 		6, 		70,			7, 		20,		8,		10 	}
	  };

	// 클래스 리스트. infoForChangeMeter 정보에서 몬스터 객체 생성에 필요한 자료만 저장.
	public List<PlaceRateOfMonsterLv> g_monPlaceRateList = new List<PlaceRateOfMonsterLv>() ; 

	// 몬스터 관련 정보 테이블 
	public int[,] g_infoForMon = new int[,]
//			lv,		hp,		hitPlusScore 
	{ 	{	1,		150,	50 	},
		{	2,		300,	100	},
		{	3,		500,	150	},
		{	4,		700,	200	},
		{	5,		900,	250	},
		{	6,		1300,	300	},
		{	7,		2000,	350	},
		{	8,		4000,	500	}
	};

	// 몬스터 스프라이트 이름 저장
	public string[,] g_monSprNames = new string[,]
	{	{"dragon_01_body", "dragon_01_eye_1", "dragon_01_eye_2", "dragon_01_wing"},
		{"dragon_02_body", "dragon_01_eye_1", "dragon_01_eye_2", "dragon_02_wing"},
		{"dragon_03_body", "dragon_03_eye_1", "dragon_01_eye_2", "dragon_03_wing"},
		{"dragon_04_body", "dragon_04_eye_1", "dragon_04_eye_2", "dragon_04_wing"},
		{"dragon_05_body", "dragon_05_eye_1", "dragon_05_eye_2", "dragon_05_wing"},
		{"dragon_06_body", "dragon_01_eye_1", "dragon_06_eye_2", "dragon_06_wing"},
		{"dragon_07_body", "dragon_07_eye_1", "dragon_07_eye_2", "dragon_07_wing"},
		{"dragon_08_body", "dragon_08_eye_1", "dragon_08_eye_2", "dragon_08_wing"},
	};

	// 플레이어 스프라이트 이름 저장 
	public string[,] g_playerSprNames = new string[,]
	{	{ "sunny_01_body", "sunny_01_wing" },
		{ "sunny_02_body", "sunny_02_wing" },
		{ "sunny_03_body", "sunny_03_wing" }, 
		{ "sunny_04_body", "sunny_04_wing" }
	};

	// 미사일 스프라이트 이름 저장 
	public string[] g_msiSprNames = new string[]
	{ "missile_01_01", "missile_01_02", "missile_01_03", "missile_01_04" };

	// 미사일 미터당 파워 정보 저장 
	public int[,] g_infoMsiPowerPerMeter = new int[,]
	// 	meter, 	missilePower 
	{ 	{ 500,		50	},
		{ 1000, 	100	},
		{ 1500, 	150 },
		{ 1501, 	300  }
	};

	void Awake()
	{		
		if( m_instance == null )
			m_instance = this;

		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
		g_nowScene = GameSceneState.title;	
		Application.LoadLevel ("Title");	
																//	dis		1	2	3	4	5	6	7	8Rate
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 100,	70,	30,	0,	0,	0,	0,	0,	0 } )); 
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 200,	30,	70,	0,	0,	0,	0,	0,	0 } ));
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 300,	0,	70,	30,	0,	0,	0,	0,	0 } ));
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 400,	0,	30,	70,	0,	0,	0,	0,	0 } ));
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 500,	0,	0,	70,	30,	0,	0,	0,	0 } ));
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 600,	0,	0,	30,	70,	0,	0,	0,	0 } ));
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 700,	0,	0,	0,	70,	30,	0,	0,	0 } ));
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 800,	0,	0,	0,	30,	70,	0,	0,	0 } ));
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 900,	0,	0,	0,	0,	70,	30,	0,	0 } ));
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 1000,	0,	0,	0,	0,	30,	70,	0,	0 } ));
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 1500,	0,	0,	0,	0,	0,	70,	30,	0 } ));
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 2000,	0,	0,	0,	0,	0,	50,	50,	00 } ));
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 2500,	0,	0,	0,	0,	0,	40,	30,	30 } ));
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 3000,	0,	0,	0,	0,	0,	30,	40,	30 } ));
		g_monPlaceRateList.Add( new PlaceRateOfMonsterLv ( new int[]{ 3001,	0,	0,	0,	0,	0,	0,	50,	50 } ));
	}

	void Update()
	{
	}
	
}

// 스크롤 속도/ 몬스터 배치 확률을 저장하는 클래스  
public class PlaceRateOfMonsterLv
{
	public int m_distUnder  = 0 ; 	// 누적 거리 기준값. 해당값의 이하값임 (ex : m_distUnder == 100이면, 100m 이하일때 라는 뜻이 됨 ) 
	public int m_inputValErr = 0 ; 	// 입력된 레벨 확률값을 모두 더해서, 100이 되는지 확인. 
	
	public List<int> m_PlaceRatesList = new List<int>();	// 입력된 배치 확률 값들 모두 리스트로 저장, 인덱스 0은 거리, 1은  1번째 배치비율 ~ 이후 반복 
	
	public PlaceRateOfMonsterLv( int[] inputArr )
	{
		if( inputArr.Length != 9 )
		{
			Debug.LogError("누적거리당 몬스터 배치확률의 입력값 확인 바람 !!!");
			return;
		}

		m_distUnder  = inputArr[0] ;

		for( int i = 0 ; i < inputArr.Length ; i++ )
		{
			m_PlaceRatesList.Add( inputArr[i] );
			if( i != 0 )
				m_inputValErr += inputArr[i];
		}

		if( m_inputValErr != 100 )
		{
			Debug.LogError("누적거리당 몬스터 배치확률의 입력된 레벨당 배치확률값이 유효하지 않음");
			return;
		}
	}
}
