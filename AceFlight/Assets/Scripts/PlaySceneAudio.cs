using UnityEngine;
using System.Collections;

// < 플레이어가 죽는다면 배경음악 정지 >>
public class PlaySceneAudio : MonoBehaviour {


	void Start () 
	{
	
	}

	void Update () 
	{
		if( GameData.Instance.g_playerState == PlayerState.dead )
			gameObject.GetComponent<AudioSource>().Stop(); 
	}
}
