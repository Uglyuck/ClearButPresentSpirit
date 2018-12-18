using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(Animator))]
public class DirectionalMovement : MonoBehaviour 
{
	public Vector2 Direction;
	public float Speed;
	private Collider2D MyCollider;
	
	// Use this for initialization
	void Start () 
	{
		MyCollider = gameObject.GetComponent<Collider2D>();
		MyAnimation = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Direction.magnitude > 0)
		{
			Direction.Normalize();
			float Distance = Speed * Time.deltaTime;
			if(CanMove(Distance))
			{
				//Log("Moving");
				Vector2 Pos = gameObject.transform.position;
				Pos = Pos + (Direction * Distance);
				gameObject.transform.position = Pos;
			}
			else
			{
				Direction = Vector2.zero;
				//Log("Cant Move");
			}
		}
		Animate();
	}


	private bool CanMove(float Distance)
	{
		RaycastHit2D[] hits = new RaycastHit2D[10];
		ContactFilter2D filter = new ContactFilter2D() { };
		filter.NoFilter();
		int numHits = MyCollider.Cast(Direction, filter, hits, Distance, false);
		for (int i = 0; i < numHits; i++)
		{
			if (!hits[i].collider.isTrigger)
			{
				return false;
			}
		}
		return true;
	}





	private Animator MyAnimation;
	private enum Animations { Up, Down, Left, Right, still};
	
	private void Animate()
	{
		Animations myAnimation = Animations.still;
		if(Mathf.Abs(Direction.x) > 0.5f)
		{
			if (Direction.x > 0)
				myAnimation = Animations.Right; 
			else
				myAnimation = Animations.Left;
		}
		else  
		{
			if (Mathf.Abs(Direction.y) > 0.5f)
			{
				if (Direction.y > 0)
					myAnimation = Animations.Up;
				else
					myAnimation = Animations.Down;
			}
		}
		
		SetAnimations(myAnimation, Direction.magnitude);
		
	}
	private void SetAnimations(Animations AnimDirection, float Speed)
	{
		Vector3 SetScale = transform.localScale;
		if(AnimDirection != Animations.still)
			SetScale = new Vector3(1, 1, 1);
		switch(AnimDirection)
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
			default:
				break;
		}
		if(Speed > 0.1f)
			MyAnimation.SetBool("Moving", true);
		else
			MyAnimation.SetBool("Moving", false);

		transform.localScale = SetScale;
	}
	private void  Log(string s)
	{
		print(s);
	}
}
