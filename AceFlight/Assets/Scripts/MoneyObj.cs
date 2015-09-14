using UnityEngine;
using System.Collections;

// << 돈 이동, 충돌 처리 >> 
public class MoneyObj : MonoBehaviour {
	
	public int myPrice 			= 0 ;	 	// 자신의 값어치(플레이어와 충돌시 골드 상승액) 
	float destoryYpos  			= -513f;	// 돈객체가 아래로 떨어질때 파괴하기 위한 y위치 기준 값 
	float mnMoveYPixelPerFrame 	= 5f;		// 1프레임당 세로 이동 픽셀 값
	float mnMoveXPixelPerFrame 	= 0.5f;		// 1프레임당 가로 이동 픽셀 값
	float mnMoveUpTwTime 		= 0.2f;		// 위로 올라가는 트윈 애니메이션 지속 시간 
	float mnMoveLimitXpos 		= 290f;

	bool isGoDownState = false;				// 돈 아래로 떨어지는 상태알리는 스위치 

	Vector3 mnInitPos = Vector3.zero;		// 돈 위치 초기값 
	Vector3 mnMovePos = Vector3.zero;		// 돈 이동값 

	int rand1to2 ; 							// 돈 하락 상태에서 랜덤하게 좌우 방향을 주기위한 랜덤값 생성


	// Use this for initialization
	void Start () 
	{
		rand1to2 = Random.Range( 1, 3 );	
		mnMovePos = mnInitPos = gameObject.transform.localPosition;
		goUpState();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( GameData.Instance.g_playerState == PlayerState.play )
		{
			// 돈 하락 상태 
			if ( isGoDownState == true )
			{
				mnMovePos = gameObject.transform.localPosition;

				// x방향 이동. 가로 방향값을 만들어줌. 
				if( rand1to2 == 1 )
					mnMovePos.x -= ( mnMoveXPixelPerFrame * GameData.Instance.g_framePerSec ) * Time.deltaTime;
				else // rand1to2 == 2
					mnMovePos.x += ( mnMoveXPixelPerFrame * GameData.Instance.g_framePerSec ) * Time.deltaTime;

				if( mnMovePos.x > mnMoveLimitXpos && mnMovePos.x < -1*mnMoveLimitXpos )
					mnMovePos.x = gameObject.transform.localPosition.x ;

				// y방향  이동 ( 동전 수직이동). 화면의 1/2부터 2배의 속도로 떨어짐 
				if( mnMovePos.y <= GameData.Instance.g_screenHeight - GameData.Instance.g_screenHeight/2)
					mnMovePos.y -= ( (mnMoveYPixelPerFrame*2) * GameData.Instance.g_framePerSec ) * Time.deltaTime;
				else
					mnMovePos.y -= ( mnMoveYPixelPerFrame * GameData.Instance.g_framePerSec ) * Time.deltaTime;

				gameObject.transform.localPosition = mnMovePos;
			}

			// 화면 하단까지 내려가면 객체파괴 
			if( gameObject.transform.localPosition.y <= destoryYpos )
			{
				Destroy( gameObject );
			}
		}

	}

	// 플레이어와 충돌시 골드점수 올리고 돈 객체 파괴 . 
	void OnTriggerEnter(Collider other)
	{
		if ( other.transform.tag == "Player" )
		{
			GameData.Instance.g_scGoldFromStage += myPrice ;
			Destroy( gameObject );
		}		
	}

	// 위로 솟는 tweenAnimation 
	void goUpState()
	{
		TweenPosition tpGoUp = gameObject.AddComponent<TweenPosition>();
		tpGoUp.from = gameObject.transform.localPosition;
		tpGoUp.to = new Vector3( mnInitPos.x,
			           		 mnInitPos.y + gameObject.GetComponent<UISprite>().height,
			           		 mnInitPos.z
			          		);
		tpGoUp.duration = mnMoveUpTwTime;
		tpGoUp.callWhenFinished = "goDownState";
		tpGoUp.Play(true);

		Invoke("goDownState", mnMoveUpTwTime);
	}

	void goDownState()
	{
		isGoDownState = true;
	}
	
}
