using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closeness : MonoBehaviour 
{
	public GameObject Target;
	public float Distance;
	public bool ReplaceClosest;

	public void Start()
	{
		
	}

	// Update is called once per frame
	void Update () 
	{
		Distance = (gameObject.transform.position - Target.transform.position).magnitude;
		if(ReplaceClosest)
		{
			GamePlay.instance.CheckAndReplaceClosest(gameObject, Distance);
		}
		Log(Distance + "");
	}
	private void Log(string s)
	{
		//print(s);
	}
}
