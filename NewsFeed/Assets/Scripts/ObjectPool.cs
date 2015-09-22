using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 자주 쓰이는 객체들을 미리 등록하고 사용하는 방법!!
// 알아두기 별표별표별표별표별표 
public class ObjectPool : MonoBehaviour 
{
	private static ObjectPool m_instance;
	public static ObjectPool Instance()
	{
		return m_instance;
	}

	public List<GameObject> ListFeedObject = new List<GameObject>();

	void Awake()
	{
		m_instance = this;
	}

	// Use this for initialization
	void Start ()  
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}

// 외부에서 불러서 사용하는 경우 
//GameObject FeedTodayMissionTmp = Instantiate(ObjectPool.Instance().ListFeedObject[0]) as GameObject;