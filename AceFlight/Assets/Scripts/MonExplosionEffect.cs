using UnityEngine;
using System.Collections;

//<< 몬스터 죽을경우 터짐효과 후 자신의 객체 삭제 >> 
public class MonExplosionEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DestoryThis()
	{
		Destroy( gameObject );
	}
}
