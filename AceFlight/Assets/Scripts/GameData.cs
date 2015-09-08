using UnityEngine;
using System.Collections;

public enum GameSceneState { init, title, play, result, loading }

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
	public int scGoldFromStage = 0;		// 스테이지 획득골드
	public int scHitMonFromStage = 0;	// 스테이지 몬스터 타격에 대한 점수 
	public int scDistFromStage = 0 ;	// 스테이지 이동거리에 대한 점수 . meter 
	public int maxScore = 0 ; 			// 최대 점수 (지금까지 했던 점수 중 최대 점수 저장... 나중에 시간되면..^^..) 

	// 속도 관련 
	public float bgSpeed = 120f;		// 초당 120픽셀 이동 / 초당 60프레임. -> 1프레임당 2픽셀씩 이동. 2px * 60 frame 
	public int pixelPerFrame = 2;		// gameSpeed~
	public int pixelPerMeter = 10;		// 거리 측정 기준. fixelPerMeter(픽셀) 만큼 이동하면 1m 임. 
	public int pixelPerSec = 120 ; 		 
	public int framePerSec = 60;

	public float screenWidth = 0f;
	public float screenHeight = 0f;
	
	public GameSceneState nowScene ;

	// 스크롤 속도 / 몬스터 배치 테이블 
	//		거리(m)		스크롤속도		레벨		<-등장확률		레벨		<- 등장확률 
	public int[,] ScSpeedAndMonPlace = new int[,]
	  { { 100, 		100, 		1, 		70,			2, 		30 },
		{ 200, 		120, 		1, 		30,			2, 		30 },
		{ 300, 		140, 		2, 		70,			3, 		30 },
		{ 400, 		160, 		2, 		30,			3, 		30 },
		{ 500, 		180, 		3, 		70,			4, 		30 },
		{ 600, 		200, 		3, 		30,			4, 		30 },
		{ 700, 		220, 		4, 		70,			5, 		30 },
		{ 800, 		240, 		4, 		30,			5, 		30 },
		{ 900, 		260, 		5, 		70,			6, 		30 },
		{ 1000,		280, 		5, 		30,			6, 		30 },
		{ 1000,		300, 		6, 		70,			7, 		20,		8,		10 }
	  };
	
	// 몬스터 테이블 
//			lv,		hp,		hitPlusScore
	public int[,] monsterInfo = new int[,]
	{ 	{	1,		100,	50 	},
		{	2,		300,	100	},
		{	3,		500,	150	},
		{	4,		700,	200	},
		{	5,		900,	250	},
		{	6,		1100,	300	},
		{	7,		1300,	350	},
		{	8,		1500,	400	}
	};

	public string[,] monsterSprName = new string[,]
//		
	{	{"dragon_01_body", "dragon_01_eye_1", "dragon_01_eye_1", "dragon_01_wing"},
		{"dragon_02_body", "dragon_01_eye_1", "dragon_01_eye_1", "dragon_02_wing"},
		{"dragon_03_body", "dragon_01_eye_1", "dragon_01_eye_1", "dragon_03_wing"},
		{"dragon_04_body", "dragon_01_eye_1", "dragon_01_eye_1", "dragon_04_wing"},
		{"dragon_05_body", "dragon_01_eye_1", "dragon_01_eye_1", "dragon_05_wing"},
		{"dragon_06_body", "dragon_01_eye_1", "dragon_01_eye_1", "dragon_06_wing"},
		{"dragon_07_body", "dragon_01_eye_1", "dragon_01_eye_1", "dragon_07_wing"},
		{"dragon_08_body", "dragon_01_eye_1", "dragon_01_eye_1", "dragon_08_wing"},
	};

	void Awake()
	{
		// 인스턴스 중복생성 방지 ( 아래 if문 안쓰면 타이틀씬 들어올 때 마다 Gamedata 객체 생성됨. ) 
		if (Instance != null) 
		{
			Destroy (this.gameObject);
			return;
		}
		if( m_instance == null )
			m_instance = this;

		// 스크린 사이즈 저장	--->> 완성하고 나서 빼기. 나중에 BtnScript에서 구하니까 중복으로 구할 필요 없음 
		if (GameData.Instance.nowScene == GameSceneState.title && GameData.Instance.screenHeight == 0f) 
		{
			GameObject UIRootObj = GameObject.Find ("UI Root");
			GameData.Instance.screenHeight = UIRootObj.GetComponent<UIRoot>().manualHeight ; 
			GameData.Instance.screenWidth = UIRootObj.GetComponent<UIRoot>().manualWidth ;
		}


		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
//		Application.LoadLevel ("Title");	//-->> 완성하고 주석 제거. 지금은 테스트를 위해 gamedata프리팹 씬마다 복사해놓음 
	}

	void Update()
	{
	}
}
