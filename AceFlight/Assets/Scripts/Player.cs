using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public UIDragObject playerDg;

	public void OnPress()
	{
		Debug.Log ("OnPress");
	}

	public void OnRelease()
	{
		Debug.Log ("OnRelease");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		playerDg.target.transform
	}
}
