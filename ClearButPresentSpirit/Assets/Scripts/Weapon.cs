using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
	public float range = 1;
	private GameObject AttackedObject;
	private AnimateScript AS;
	private bool Hit;
	// Use this for initialization
	void Start () {
		AS = GetComponent<AnimateScript>();
	}
	
	public bool attack()
	{
		if(GamePlay.instance.ClosestGhost.Value < range)
		{
			AttackedObject = GamePlay.instance.ClosestGhost.item;
			SetAnimation(Animations.Hit);
			return true;
		}
		//Animate
		SetAnimation(Animations.Miss);
		return false;
	}
	public void Recover()
	{
		GamePlay.instance.RemoveGhost(AttackedObject);
	}

	private enum Animations { Hit, Miss, Charge };
	private void SetAnimation(Animations hit)
	{
		if(hit == Animations.Hit)
		{
			Hit = true;
			AS.SetAnimations(AnimateScript.Animations.Hit, 0);
		}
		else
		{
			Hit = false;
			AS.SetAnimations(AnimateScript.Animations.Miss, 0);
		}
	}
	public void AttackEnd()
	{
		GamePlay.instance.RemoveGhost(AttackedObject);
	}

}
