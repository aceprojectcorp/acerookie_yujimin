using UnityEngine;
using System;
using System.Collections.Generic;

// sealed 한정자를 통해 해당 클래스가 상속이 불가능하도록 조치 
public sealed class GameData {

	// 싱글톤 인스턴스를 저장 
	public static volatile GameData uniqueInstance;		// volatile 한정자 : 동시에 실행되는 여러 스레드에 의해 필드가 수정될 수 있음을 알림 
	public static object _lock = new System.Object();	// lock : 지정된 객체를 잠그고 프로그래밍된 문장을 실행 

	// 생성자 
	private GameData(){}
	
	public static GameData Instance
	{
		get
		{
			if( uniqueInstance == null )
			{
				//lock 으로 지정된 블록안의 코드를 하나의 쓰레드만 접근하도록 함 
				lock(_lock)
				{
					if( uniqueInstance == null )
						uniqueInstance = new GameData();
				}
			}
			return uniqueInstance ; 
		}
	}

	public GameManager gamePlayManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
