using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
	public float range = 1;
	private GameObject AttackedObject;
	
	// Use this for initialization
	void Start () {
		
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

	private enum Animations { Hit, Miss, Charge };
	private void SetAnimation(Animations hit)
	{
		if(hit == Animations.Hit)
		{
			AttackEnd();
		}
	}
	public void AttackEnd()
	{
		GamePlay.instance.RemoveGhost(AttackedObject);
	}

}
