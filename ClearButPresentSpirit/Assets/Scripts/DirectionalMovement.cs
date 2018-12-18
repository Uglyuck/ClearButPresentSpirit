using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(Animator))]
public class DirectionalMovement : MonoBehaviour 
{
	public Vector2 Direction;
	public float Speed;
	private Collider2D MyCollider;
	public bool Pause = false;
	private AnimateScript AS;
	// Use this for initialization
	void Start () 
	{
		MyCollider = gameObject.GetComponent<Collider2D>();
		AS = GetComponent<AnimateScript>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!Pause)
		{
			if (Direction.magnitude > 0)
			{
				Direction.Normalize();
				float Distance = Speed * Time.deltaTime;
				if (CanMove(Distance))
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
			
		}
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





	private void  Log(string s)
	{
		print(s);
	}
}
