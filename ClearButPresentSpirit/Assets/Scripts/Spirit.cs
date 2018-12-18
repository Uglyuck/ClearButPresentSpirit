using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour 
{
	public AudioClip Scream;
	public float DifficultyDistance = 9;
	// Use this for initialization

	public void ClosestAction(float Distance)
	{
		Appear(Distance, DifficultyDistance);
	}
	public Color c = new Color(1, 1, 1, 1);
	private void Appear(float MyDist, float Difficulty)
	{
		c.a = 1 - (MyDist / Difficulty);
		gameObject.GetComponent<SpriteRenderer>().color = c;
	}
	public void Attacked()
	{
		
	}
	/*
	private void MakeSound()
	{
		// Control the volume
		if (ClosestGhost.item != null)
		{
			Vector3 v = new Vector3(0, 0, ClosestGhost.Value);
			v.x = ClosestGhost.item.transform.x - gameObject.transform.position;
			Vector3 cv = Camera.main.transform.position;
			v = v + cv;
			gameObject.transform.position = v;

			AudioClip AC = ClosestGhost.item.GetComponent<AudioClip>();
			if (SoundSource.clip != AC)
			{
				SoundSource.clip = AC;
			}
			if (ClosestGhost.Value < DifficultyDistance)
				SoundSource.volume = 1 - (ClosestGhost.Value / DifficultyDistance);
			else
			{
				SoundSource.volume = 0;
			}

		}
	}
	*/
}
