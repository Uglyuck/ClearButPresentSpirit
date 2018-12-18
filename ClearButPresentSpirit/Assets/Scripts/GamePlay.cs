using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour 
{
	public float RemainingTime;
	public int Ghosts;

	public UnityEngine.UI.Text Timer;
	public AudioSource SoundSource;
	public float DifficultyDistance;

	public FloatVariable ClosestGhost;
	public static GamePlay instance;

	private GameObject MyClosestGhost;
	private float MyClosestDistance = 9999;

	private bool Silence = false;
	
	void Start () 
	{
		//ClosestGhost = new FloatVariable();
		instance = this;
		if (Ghosts > 0)
			PopulateGhosts();
		else
			SetCountOfGhosts();
	}
	void SetCountOfGhosts()
	{
		Ghosts = GameObject.FindObjectsOfType<Spirit>().Length;
	}
	void PopulateGhosts()
	{
		GameObject Spirit = GameObject.FindObjectOfType<Spirit>().gameObject;
		for(int x = 0; x < Ghosts; x++)
		{
			float TempX = Random.Range(-9f, 9f);
			float TempY = Random.Range(-5f, 5f);
			GameObject g = GameObject.Instantiate(Spirit);
			g.transform.position = new Vector3(TempX, TempY, 0);
		}
		Destroy(Spirit);
	}

	private void LateUpdate()
	{
		if (MyClosestDistance < 999 && !Silence)
		{
			if (ClosestGhost.item != null)
			{
				if (MyClosestGhost.transform.position != ClosestGhost.item.transform.position)
				{
					Spirit mySpirit1 = ClosestGhost.item.GetComponent<Spirit>();
					if (mySpirit1 != null)
					{
						mySpirit1.ClosestAction(9999);
					}
				}
			}
			ClosestGhost.item = MyClosestGhost;
			ClosestGhost.Value = MyClosestDistance;
			Spirit mySpirit = MyClosestGhost.GetComponent<Spirit>();
			if (mySpirit != null)
			{
				mySpirit.ClosestAction(MyClosestDistance);
			}
		}

		Silence = false;
		MyClosestDistance = 999;
	}

	// Update is called once per frame
	void Update () 
	{
		RemainingTime -= Time.deltaTime;
		if(RemainingTime <  0)
		{
			if (Ghosts > 0)
				Lose();
			else
				Win();
		}
		else
		{
			SetTime();
		}
	}

	public void SetTime()
	{
		int MyTime = Mathf.Abs(Mathf.CeilToInt(RemainingTime));
		Timer.text = Mathf.FloorToInt(MyTime / 60) + ":" + (MyTime % 60).ToString("00");
	}


	public void Win()
	{
		SceneLoader sl = GameObject.FindObjectOfType<SceneLoader>();
		sl.LoadScene("Win");
	}
	public void Lose()
	{
		SceneLoader sl = GameObject.FindObjectOfType<SceneLoader>();
		sl.LoadScene("Lose");
	}

	public void RemoveGhost(GameObject go)
	{
		Log("REmoving Ghost:" + go.name);
		go.active = false;
		Ghosts--;
		if (Ghosts <= 0)
			Win();
		Silence = true;
	}


	public static FloatVariable CLosest()
	{
		return instance.ClosestGhost;
	}

	public void CheckAndReplaceClosest(GameObject go, float Distance)
	{
		//Log("Checking for the closest with:" + Distance);
		if (Distance < MyClosestDistance)
		{
			MyClosestDistance = Distance;
			MyClosestGhost = go;
		}
	}
	private void Log(string s)
	{
		print(s);
	}
}
