using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectionalMovement), typeof(Weapon))]

public class Controls : MonoBehaviour 
{
	private DirectionalMovement dm;
	public GameObject KeyAudioSource;
	public Weapon myWeapon;
	// Use this for initialization
	void Start () 
	{
		dm = GetComponent<DirectionalMovement>();
		gameObject.GetComponent<Weapon>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 Direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		dm.Direction = Direction;
		if(Input.GetKeyDown(KeyCode.Space))
		{
			myWeapon.attack();
		}
	}
}
