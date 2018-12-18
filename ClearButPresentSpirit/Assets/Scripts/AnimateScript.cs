using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateScript : MonoBehaviour 
{
	private Animator MyAnimation;
	public enum Animations { Up, Down, Left, Right, still, Hit, Miss };
	private DirectionalMovement dm;
	private Weapon Wep;

	// Use this for initialization
	void Start () 
	{
		MyAnimation = GetComponent<Animator>();
		dm = GetComponent<DirectionalMovement>();
		Wep = GetComponent<Weapon>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!dm.Pause)
			Animate();	
	}

	public void UnPause()
	{
		dm.Pause = false;
		Wep.Recover();
	}

	

	private void Animate()
	{
		
		Animations myAnimation = Animations.still;
		if (Mathf.Abs(dm.Direction.x) > 0.5f)
		{
			if (dm.Direction.x > 0)
				myAnimation = Animations.Right;
			else
				myAnimation = Animations.Left;
		}
		else
		{
			if (Mathf.Abs(dm.Direction.y) > 0.5f)
			{
				if (dm.Direction.y > 0)
					myAnimation = Animations.Up;
				else
					myAnimation = Animations.Down;
			}
		}

		SetAnimations(myAnimation, dm.Direction.magnitude);

	}
	public void SetAnimations(Animations AnimDirection, float Speed)
	{
		Vector3 SetScale = transform.localScale;
		if (AnimDirection != Animations.still)
			SetScale = new Vector3(1, 1, 1);
		switch (AnimDirection)
		{
			case (Animations.Up):
				MyAnimation.SetBool("Up", true);
				MyAnimation.SetBool("Down", false);
				MyAnimation.SetBool("Side", false);
				break;
			case (Animations.Down):
				MyAnimation.SetBool("Up", false);
				MyAnimation.SetBool("Down", true);
				MyAnimation.SetBool("Side", false);
				break;
			case (Animations.Left):
				SetScale = new Vector3(-1, 1, 1);
				MyAnimation.SetBool("Up", false);
				MyAnimation.SetBool("Down", false);
				MyAnimation.SetBool("Side", true);
				break;
			case (Animations.Right):
				MyAnimation.SetBool("Up", false);
				MyAnimation.SetBool("Down", false);
				MyAnimation.SetBool("Side", true);
				break;
			case (Animations.Hit):
				MyAnimation.SetBool("Up", false);
				MyAnimation.SetBool("Down", false);
				MyAnimation.SetBool("Side", false);
				MyAnimation.SetBool("Catch", true);
				MyAnimation.SetTrigger("Bag");
				dm.Pause = true;
				return;
				break;
			case (Animations.Miss):
				MyAnimation.SetBool("Up", false);
				MyAnimation.SetBool("Down", false);
				MyAnimation.SetBool("Side", false);
				MyAnimation.SetBool("Catch", false);
				MyAnimation.SetTrigger("Bag");
				dm.Pause = true;
				return;
				break;
			default:
				break;
		}
		if (Speed > 0.1f)
			MyAnimation.SetBool("Moving", true);
		else
			MyAnimation.SetBool("Moving", false);

		transform.localScale = SetScale;
	}
}

