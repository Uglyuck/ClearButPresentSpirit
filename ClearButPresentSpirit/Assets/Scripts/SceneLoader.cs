using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager
{
	public static SceneManager instance;
	private MonoBehaviour mb;
	private AsyncOperation async;

	// Begin loading and load on finish.
	public static void LoadScene(string name, MonoBehaviour blah)
	{
		instance = Init();
		instance.log("Initialized Scene Manager");
		instance.LoadMyScene(name, blah);
	}

	// Initialize it, if it's not found create it;
	public static SceneManager Init()
	{
		return SceneManager.instance == null ? new SceneManager() : SceneManager.instance;
	}

	// Starts the async process 
	public void LoadMyScene(string name, MonoBehaviour mb2)
	{
		/*
		if(mb == null)
		{
			log("Creating MonoBehavior");
			mb = mb2;
		}
		*/
		log("Load Scene " + name);
		mb2.StartCoroutine(AsynchronousLoad(name));

	}

	private IEnumerator AsynchronousLoad(string scene)
	{
		yield return null;
		async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
		async.allowSceneActivation = true;
		//yield return null;
	}

	private void log(string Message)
	{
		Debug.Log(Message);
	}
}

public class SceneLoader : MonoBehaviour 
{
	public void LoadScene(string MyScene)
	{
		SceneManager.LoadScene(MyScene, this);
	}
}
